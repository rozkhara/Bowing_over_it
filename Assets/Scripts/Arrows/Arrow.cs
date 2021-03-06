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
    private float maxDragDist;
    [SerializeField]
    private float minDragDist;
    [SerializeField]
    private float releaseTime;

    public bool isClone { get; set; }
    public bool isFrozen { get; set; }
    public bool isPressed { get; set; }
    public bool isFlying { get; set; }
    public bool isLanded { get; set; }
    private bool isCancelled;
    // private bool isReloaded = false;

    public static bool isReloaded;

    protected Vector2 originPos;

    private GameObject hook;
    private Rigidbody2D hookrb;
    private LineRenderer hooklr;

    // 궤적
    private GameObject[] points;
    [SerializeField]
    private int numOfPoints;

    public Rigidbody2D rb { get; set; }
    protected Collider2D col;
    public TrailRenderer tr { get; set; }
    private SpriteRenderer sr;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        tr = GetComponent<TrailRenderer>();
        sr = GetComponent<SpriteRenderer>();

        isClone = false;
        isFrozen = false;
        isPressed = false;
        isFlying = false;
        isLanded = false;
        isCancelled = false;

        // 쏘기 전 중력 지배 X
        rb.isKinematic = true;
        tr.enabled = false;
        originPos = rb.position;

        if (isClone)
        {
            return;
        }

        points = new GameObject[numOfPoints];

        for (int i = 0; i < numOfPoints; i++)
        {
            var pointPrefab = AssetLoader.LoadPrefab<GameObject>("Point");
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
            points[i].SetActive(false);
        }
    }

    protected void Start()
    {
        // 장애물에 arrow 대입
        for (int i = 0; i < ObstacleManager.Instance.suns.Length; i++)
        {
            ObstacleManager.Instance.suns[i].arrow = this;
        }
        for (int i = 0; i < ObstacleManager.Instance.monsters.Length; i++)
        {
            ObstacleManager.Instance.monsters[i].arrow = this;
        }
        for (int i = 0; i < ObstacleManager.Instance.moles.Length; i++)
        {
            ObstacleManager.Instance.moles[i].arrow = this;
        }
    }

    protected void Update()
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

        if (Input.GetMouseButtonUp(0) && isPressed && !isFlying && !isLanded)
        {
            if (!isCancelled)
            {
                isPressed = false;
                rb.isKinematic = false;
                if (Random.Range(0, 2) == 1)
                {
                    SoundManager.Instance.PlaySFXSound("Arrow_release1", 0.5f);
                }
                else
                {
                    SoundManager.Instance.PlaySFXSound("Arrow_release2", 0.5f);
                }
                SoundManager.Instance.PlaySFXSound("Arrow_midAir", 0.5f);
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
        if (isFlying && !isFrozen)
        {
            if (GameManager.Instance.isFlipped) sr.flipX = true;

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

        isReloaded = false;

        tr.enabled = false;

        SoundManager.Instance.PlaySFXSound("Arrow_hit", 0.5f);


        // Reload();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Field")
        {
            return;
        }

        isFlying = false;
        isLanded = true;
        isReloaded = false;

        col.enabled = false;
        tr.enabled = false;

        if (collision.gameObject.tag == "Rock" || collision.gameObject.tag == "Obstacle")
        {
            transform.SetParent(collision.transform);
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // Reload();
    }

    /*
    public void Reload()
    {
        if (!isClone && !isReloaded)
        {
            StartCoroutine(ReloadCoroutine());
            isReloaded = true;
            this.enabled = false;
        }
    }
    */

    /// <summary>
    /// 재장전
    /// </summary>
    /// 

    /*
    public IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(3.0f);

        if (--(GameManager.Instance.arrowCount) > 0)
        {
            var arrowPrefab = AssetLoader.LoadPrefab<GameObject>("Arrows/Arrow");
            Instantiate(arrowPrefab, originPos, Quaternion.identity);
        }
        else
        {
            Debug.Log("화살을 다 썼습니다!");
        }

        // this.enabled = false;
    }
    */

    protected virtual IEnumerator Release()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 moveVec = (originPos - rb.position) * (force - GameManager.Instance.drag);
        rb.velocity = moveVec;

        isFlying = true;
        tr.enabled = true;

        if (isClone)
        {
            yield break;
        }

        // 궤적 삭제
        for (int i = 0; i < points.Length; i++)
        {
            Destroy(points[i]);
        }

        yield return new WaitForSeconds(releaseTime);

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
        Vector2 curPointPos = (Vector2)transform.position + (originPos - rb.position) * (force - GameManager.Instance.drag) * t + 0.5f * Physics2D.gravity * (t * t);
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