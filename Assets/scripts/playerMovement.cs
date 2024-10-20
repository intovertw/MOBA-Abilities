using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    float moveSpeed = 5f;
    public Rigidbody rb;
    bool hookMode = false;
    public GameObject hookPrefab;
    public Camera mainCamera;
    Vector3 movement;

    // basic movement
    void FixedUpdate()
    {
        if (!hookMode)
        {
            movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void Update()
    {
        //hookMode bool checker
        if (hookMode)
        {
            Debug.Log("hook on");
        }
        else
        {
            Debug.Log("hook off");
        }

        if (Input.GetKeyDown("e"))
        {
            hookMode = true;
        }

        if (hookMode && Input.GetMouseButtonDown(1))
        {
            hookMode = false;
        }

        if (hookMode && Input.GetMouseButtonDown(0))
        {
            SpawnHook();
        }
    }

    void SpawnHook()
    {
        Debug.Log("hook spawned");

        Instantiate(hookPrefab, transform.position, Quaternion.identity);

        hookMode = false;
    }
}
