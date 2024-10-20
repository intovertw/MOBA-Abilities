using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeScroll : MonoBehaviour
{
    public float scrollSpeed, topBarrier, bottomBarrier, leftBarrier, rightBarrier;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.mousePosition.y >= Screen.height * topBarrier)
        {
            transform.Translate(Vector3.forward * Time.fixedDeltaTime * scrollSpeed, Space.World);
        }

        if (Input.mousePosition.y <= Screen.height * bottomBarrier)
        {
            transform.Translate(Vector3.back * Time.fixedDeltaTime * scrollSpeed, Space.World);
        }

        if (Input.mousePosition.x <= Screen.width * leftBarrier)
        {
            transform.Translate(Vector3.left * Time.fixedDeltaTime * scrollSpeed, Space.World);
        }

        if (Input.mousePosition.x >= Screen.width * rightBarrier)
        {
            transform.Translate(Vector3.right * Time.fixedDeltaTime * scrollSpeed, Space.World);
        }
    }
}
