using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float velocity;   // 속도
    private Vector2[] route; // 각 경로의 이동 벡터
    private float[] time;   // 각 경로에서 걸리는 시간
    private int routeCount; // 경로의 갯수
    private int routeNow;   // 현재 이동 중인 경로
    private Rigidbody2D rb;
    private Vector2[] points; // 각 경로 중간 지점
    Coroutine routine;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        routeNow = 0;
        routeCount = transform.childCount;
        if (routeCount == 0) return;
        points = new Vector2[routeCount];
        route = new Vector2[routeCount];
        time = new float[routeCount];
        for(int i = 0; i < routeCount; i++)
        {
            points[i] = transform.GetChild(i).position;
        }
        for(int i = 0; i < routeCount; i++)
        {
            route[i] = (points[(i + 1) % routeCount] - points[i]);
            time[i] = route[i].magnitude / velocity;
            route[i] = route[i].normalized;
        }
        routine = StartCoroutine(moveRoutine());
    }
    
    private IEnumerator moveRoutine()
    {
        while (true)
        {
            transform.right = route[routeNow];
            rb.velocity = route[routeNow] * velocity;
            yield return new WaitForSeconds(time[routeNow]);
            routeNow = (routeNow + 1) % routeCount;
            transform.position = points[routeNow];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Arrow")
        {
            StopCoroutine(routine);
            rb.gravityScale = 1f;
        }
        if(collision.tag == "Floor")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
