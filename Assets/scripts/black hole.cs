using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class blackhole : MonoBehaviour
{
    Rigidbody target;
    enemy targetScript;

    private void Awake()
    {
        Destroy(gameObject, 4);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("enemy"))
        {
            target = other.GetComponent<Rigidbody>();

            target.transform.position = Vector3.MoveTowards(target.transform.position, transform.position, 1f * Time.fixedDeltaTime);
        }
    }
}
