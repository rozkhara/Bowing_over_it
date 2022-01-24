using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangeField : MonoBehaviour
{
    [SerializeField]
    private float gravityScale;         // 바꿔줄 중력의 계수
    private float originGravityScale;   // 중력을 원래대로 돌려주기 위한 변수

    /// <summary>
    /// 화살이 들어올 때 중력의 계수를 설정된 값으로 변경한다.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Arrow")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            originGravityScale = rb.gravityScale;
            rb.gravityScale = gravityScale;
        }
    }
    /// <summary>
    /// 화살이 나갈 때 중력의 계수를 원래대로 되돌린다.
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = originGravityScale;
        }
    }
}
