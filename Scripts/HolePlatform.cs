using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolePlatform : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.SetParent(gameObject.transform,true);
    }

    public void OnCollisionExit(Collision collision)
    {
        collision.gameObject.transform.parent = null;
    }
}
