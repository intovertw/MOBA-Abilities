using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float hp = 100;
    bool giveDamageAgain = true;
    void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag.Equals("damageCircle"))
        {
            Debug.Log("Collision!");
            if (giveDamageAgain)
            {
                giveDamageAgain = false;
                StartCoroutine(Damage());
            }
        }
    }

    IEnumerator Damage()
    {
        hp -= 25;
        yield return new WaitForSeconds(2);
        giveDamageAgain = true;
    }
}
