using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField]
    private float density; // 밀도, 1일 때 화살과 같다고 가정
    [SerializeField]
    private float drag; // 항력에서 속도와 밀도 외의 모든계수 곱 / 10
    Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Arrow")
            rb = collision.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1 - density;
    }
    /// <summary>
    /// 물 속에서 이동하는 화살에 항력 적용
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Arrow")
        {
            rb.AddForce(rb.velocity * rb.velocity.magnitude * -1 * drag * density/10f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        rb.gravityScale = 1f;
    }
}
