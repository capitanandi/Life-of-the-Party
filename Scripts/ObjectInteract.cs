using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 10));

            Debug.DrawRay(ray.origin, ray.direction * 10);

            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log("Hit: " + hitInfo.transform.name);
            }
        }
    }
}
