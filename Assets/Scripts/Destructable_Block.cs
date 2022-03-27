using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable_Block : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (collision.gameObject.tag == "Player" && player.UnlockedZodiacsignIdx == 0 && Mathf.Abs(player.rb.velocity.x) > 0)
        {
            Destroy(this.gameObject);
        }
    }
   
}
