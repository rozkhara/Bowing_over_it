using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private float gravityFieldSize; // 행성 크기를 기준으로한 중력 영향 범위 ( 중력가속도가 4가 되는 지점을 경계로 함 )
    private float gravityAcceleration; // a = GM/r^2에서 GM/R^2
    private float originGravityScale;
    private Rigidbody2D rbPlanet;
    private const int G = 500; // 질량 설정를 편하게 하기 위해 G를 500으로 설정 ( 나중에 변경 가능 )
    private void Start()
    {
        rbPlanet = GetComponent<Rigidbody2D>();
        gravityFieldSize = Mathf.Sqrt(rbPlanet.mass * G / 4);
        Transform child = transform.GetChild(0);
        child.localScale *= gravityFieldSize / transform.localScale.x;

    }
    /// <summary>
    /// gravityField 내부에 위치할 때 중력 영향
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
            Vector2 r = ((Vector2)transform.position - rb.position);
            gravityAcceleration = G * rbPlanet.mass / r.magnitude / r.magnitude;
            rb.AddForce(gravityAcceleration * rb.mass * r.normalized);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "UnstoppedArrow") return;

        if (collision.tag.Contains("Arrow"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.gravityScale = originGravityScale;
        }
    }
}
