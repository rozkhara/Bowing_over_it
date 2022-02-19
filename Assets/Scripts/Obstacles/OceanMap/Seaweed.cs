using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seaweed : MonoBehaviour
{
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// 속도가 점점 줄어들며 일정 이하가 되면 해초에 묶임
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {

            Rigidbody2D arrow = collision.GetComponent<Rigidbody2D>();
            rb.velocity = arrow.velocity / 2;
            arrow.velocity /= 1.1f;
            Debug.Log(arrow.velocity.magnitude);
            if (arrow.velocity.magnitude < 3f)
                Bind(arrow);
        }
    }
    /// <summary>
    /// 화살이 묶이는 루틴
    /// </summary>
    /// <returns></returns>
    private void Bind(Rigidbody2D arrow)
    {
        arrow.constraints = RigidbodyConstraints2D.FreezeAll;
        arrow.transform.SetParent(transform, true);
        arrow.GetComponent<Arrow>().Reload();
        arrow.isKinematic = true;
        arrow.GetComponent<BoxCollider2D>().enabled = false;
    }
}
