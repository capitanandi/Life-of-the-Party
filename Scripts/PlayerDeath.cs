using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private UIManager uIManager;

    private void Start()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player Crushed");
        }
    }
}
