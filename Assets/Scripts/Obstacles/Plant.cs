using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private float height; // 식물이 올라가는 최대 높이
    [SerializeField]
    private Vector2 speed;  // 식물 초기속도 ( 크기와 방향 모두 고려 )
    [SerializeField]
    private float coolTime;   // 식물이 완전히 내려오고 다시 올라가기까지 시간
    private float curTime = 0;
    private float movingTime;   // 식물이 동작하는데 걸리는 시간
    private Vector2 acceleration; // 식물에게 적용되는 가속도 ( 올라갔다 내려가는 효과를 주기 위함 )
    private Vector2 originPos;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        originPos = rb.position;
        acceleration = -speed * speed.magnitude / 2 / height;
        if (speed.magnitude == 0)
        {
            Debug.LogError("ERROR: speed of plant is zero");
            gameObject.SetActive(false);
        }
        movingTime = -2 * (acceleration.x == 0 ? speed.y / acceleration.y : speed.x / acceleration.x);
    }
    private void FixedUpdate()
    {
        curTime -= Time.fixedDeltaTime;
        if (curTime <= 0)
        {
            rb.velocity = speed;
            curTime = coolTime + movingTime;
        }
        if (curTime - coolTime > 0)
        {
            rb.AddForce(acceleration * rb.mass);
        }
        else
        {
            rb.velocity = Vector2.zero;
            transform.position = originPos;
        }
    }

}
