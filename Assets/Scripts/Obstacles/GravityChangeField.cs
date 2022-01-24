using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangeField : MonoBehaviour
{
    [SerializeField]
    private float gravityScale;
    private float originGravityScale;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Arrow")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            originGravityScale = rb.gravityScale;
            rb.gravityScale = gravityScale;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = originGravityScale;
        }
    }
}
