using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangeField : MonoBehaviour
{
    [SerializeField]
    Vector2 gravityDir;                 // 중력의 방향
    [SerializeField]
    private float gravityScale;         // 바꿔줄 중력의 계수
    private float originGravityScale;   // 중력을 원래대로 돌려주기 위한 변수

    /// <summary>
    /// 화살이 들어올 때 중력을 설정된 값으로 변경한다.
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "UnstoppedArrow") return;

        if (collision.tag.Contains("Arrow"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb.gravityScale != 0)
            {
                originGravityScale = rb.gravityScale;
                rb.gravityScale = 0;
            }
            rb.AddForce(gravityDir * gravityScale * Physics2D.gravity.magnitude);
        }
    }
    /// <summary>
    /// 화살이 나갈 때 중력을 원래대로 되돌린다.
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "UnstoppedArrow") return;

        if (collision.tag.Contains("Arrow"))
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = originGravityScale;
        }
    }
}
