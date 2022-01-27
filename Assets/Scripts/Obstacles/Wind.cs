using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField]
    private float windforce;       //바람 강도


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * windforce * Physics2D.gravity.magnitude * 0.5f);
        }
    }
}
