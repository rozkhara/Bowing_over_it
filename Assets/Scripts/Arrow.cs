using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using UnityEngine.SceneManagement;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float force;
    [SerializeField]
    private float drag;
    [SerializeField]
    private float maxDragDist;
    [SerializeField]
    private float minDragDist;
    [SerializeField]
    private float releaseTime;

    private bool isPressed = false;
    private bool isFlying = false;
    private bool isLanded = false;
    private bool isCancelled = false;
    private bool isReloaded = false;

    private Vector2 originPos;

    private GameObject hook;
    private Rigidbody2D hookrb;
    private LineRenderer hooklr;

    // 궤적
    private GameObject[] points;
    [SerializeField]
    private int numOfPoints;

    private Rigidbody2D rb;
    private Collider2D col;
    private TrailRenderer tr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        tr = GetComponent<TrailRenderer>();

        // 쏘기 전 중력 지배 X
        rb.isKinematic = true;
        
        tr.enabled = false;

        originPos = rb.position;

        points = new GameObject[numOfPoints];
        for (int i = 0; i < numOfPoints; i++)
        {
            var pointPrefab = AssetLoader.LoadPrefab<GameObject>("Point");
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
            points[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isFlying && !isLanded)
        {
            isPressed = true;
            rb.isKinematic = true;

            // 중심 생성
            var hookPrefab = AssetLoader.LoadPrefab<GameObject>("Arrow/Hook");
            hook = Instantiate(hookPrefab, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            hookrb = hook.GetComponent<Rigidbody2D>();
            hooklr = hook.GetComponent<LineRenderer>();
            hooklr.startWidth = 0.05f;
            hooklr.endWidth = 0.05f;
        }
        if (Input.GetMouseButtonUp(0) && !isFlying && !isLanded)
        {
            if (!isCancelled)
            {
                isPressed = false;
                rb.isKinematic = false;

                StartCoroutine(Release());
            }
            else
            {
                isPressed = false;
                isCancelled = false;
            }

            Destroy(hook);
        }

        if (isPressed && !isFlying)
        {
            Aim();
        }
    }

    private void FixedUpdate()
    {
        // 화살을 진행방향에 맞게 회전
        if (isFlying)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
            rb.MoveRotation(Quaternion.Euler(new Vector3(0, 0, (angle * 180) / Mathf.PI)));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isLanded)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            col.enabled = false;
        }

        isFlying = false;
        isLanded = true;

        tr.enabled = false;

        if (!isReloaded)
        {
            StartCoroutine(ReloadCoroutine());
            isReloaded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isFlying = false;
        isLanded = true;

        col.enabled = false;
        tr.enabled = false;

        if (collision.gameObject.tag == "Obstacle")
        {
            transform.SetParent(collision.transform);
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (!isReloaded)
        {
            StartCoroutine(ReloadCoroutine());
            isReloaded = true;
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(3.0f);

        if (--(GameManager.Instance.arrowCount) > 0)
        {
            var arrowPrefab = AssetLoader.LoadPrefab<GameObject>("Arrow/Arrow");
            Instantiate(arrowPrefab, originPos, Quaternion.identity);
        }
        else
        {
            Debug.Log("화살을 다 썼습니다!");
        }

        this.enabled = false;
    }

    private IEnumerator Release()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 moveVec = (originPos - rb.position) * (force - drag);
        rb.velocity = moveVec;

        isFlying = true;

        // 궤적 삭제
        for (int i = 0; i < points.Length; i++)
        {
            Destroy(points[i]);
        }

        yield return new WaitForSeconds(releaseTime);

        tr.enabled = true;
    }

    private void Aim()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        hooklr.SetPosition(0, hookrb.position);

        // 당길 수 있는 최대 거리 설정
        if (Vector2.Distance(mousePos, hookrb.position) >= maxDragDist)
        {
            rb.MovePosition(originPos + (mousePos - hookrb.position).normalized * maxDragDist);
            hooklr.SetPosition(1, hookrb.position + (mousePos - hookrb.position).normalized * maxDragDist);
        }
        else if (Vector2.Distance(mousePos, hookrb.position) > minDragDist)
        {
            rb.MovePosition(originPos + (mousePos - hookrb.position));
            hooklr.SetPosition(1, mousePos);
        }

        // 당기기 취소
        if (Vector2.Distance(mousePos, hookrb.position) <= minDragDist)
        {
            rb.position = originPos;
            rb.rotation = 0;
            isCancelled = true;

            for (int i = 0; i < points.Length; i++)
            {
                points[i].SetActive(false);
            }
        }
        else
        {
            isCancelled = false;
        }

        rb.MoveRotation(Quaternion.LookRotation(originPos - rb.position));

        // 취소 안할 시 궤적 생성
        if (!isCancelled)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].SetActive(true);
                points[i].transform.position = PointPosition(i * 0.1f);
            }
        }
    }

    private Vector2 PointPosition(float t)
    {
        Vector2 curPointPos = (Vector2)transform.position + (originPos - rb.position) * (force - drag) * t + 0.5f * Physics2D.gravity * (t * t);
        return curPointPos;
    }

    /// <summary>
    /// 움직이는 벽에서 분리될 때 호출
    /// </summary>
    public void DetachRock()
    {
        transform.SetParent(null);

        col.enabled = true;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.gravityScale = 1f;
    }
}