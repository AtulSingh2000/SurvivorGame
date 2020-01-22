using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject player;

   private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    /*private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision");
        /*if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }*/


}
