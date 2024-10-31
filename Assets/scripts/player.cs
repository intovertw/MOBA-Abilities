using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerScript : MonoBehaviour
{
    public float moveSpeed = 5f, range = 4f;
    public LayerMask enemyMask;
    public Rigidbody rb;
    public Transform player, target;
    public GameObject blackHolePrefab;

    bool ultMode = false, disableUlt;
    Vector3 movement;

    // basic movement
    void FixedUpdate()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        //skill activation
        if (Input.GetKey("e") && !ultMode)
        {
            if (target == null)
            {
                FindTarget();
                return;
            }

            if (!CheckTargetIsInRange())
            {
                target = null;
            }
            else
            {
                Vector3 targetDirection = target.position - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 1, 0.0f);
                transform.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));
            }
        }
        if (Input.GetKeyUp("e") && target != null && !ultMode)
        {
            Debug.Log("skill activate!");
            Skill();
        }

        if (Input.GetKey("r") && !ultMode && !disableUlt)
        {
            Debug.Log("ult activate!");
            ultMode = true;
        }

        if (ultMode)
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
            UltSkill();
        }
    }

    protected bool CheckTargetIsInRange()
    {
        Debug.Log(Vector2.Distance(target.position, transform.position) <= range);
        return Vector2.Distance(target.position, transform.position) <= range;
    }

    protected void FindTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
            Debug.Log("I FOUND SOMEONE!!!");
        }
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Skill()
    {
        Sequence jump = DOTween.Sequence();

        jump.Append(target.transform.DOJump(transform.position + (-transform.forward * range), 3f, 1, 0.5f));

        enemy enemyScript = target.GetComponent<enemy>();

        enemyScript.hp -= 15;
    }

    void UltSkill()
    {
        Vector3 screenPos, worldPos = new Vector3(0,0,0);

        Debug.Log("ult ongoing");
        screenPos = Input.mousePosition;
        
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        if(Physics.Raycast(ray, out RaycastHit hitData))
        {
            worldPos = hitData.point;
        }

        Vector3 targetDirection = worldPos - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 1, 0.0f);
        transform.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ult spawnedc");
            Instantiate(blackHolePrefab, new Vector3(worldPos.x, 1, worldPos.z), Quaternion.identity);
            StartCoroutine(DisableUltMode());
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("ult cancelled");
            rb.constraints = ~RigidbodyConstraints.FreezePosition;
            ultMode = false;
        }
    }

    IEnumerator DisableUltMode()
    {
        disableUlt = true;
        ultMode = false;
        yield return new WaitForSeconds(4);
        rb.constraints = ~RigidbodyConstraints.FreezePosition;
        disableUlt = false;
    }
}
