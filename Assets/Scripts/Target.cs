using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    
    [SerializeField] private GameObject Door;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        if (collision.CompareTag("Arrow"))
        {
            Destroy(Door);
            Destroy(gameObject);
        }
    }
}
