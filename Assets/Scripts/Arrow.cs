using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private bool isCancelled = false;

    private Vector2 originPos;

    [SerializeField]
    private GameObject nextArrow;

    [SerializeField]
    private GameObject hookPrefab;
    private GameObject hook;
    private Rigidbody2D hookrb;
    private LineRenderer hooklr;

    // ����
    [SerializeField]
    private GameObject pointPrefab;
    [SerializeField]
    private GameObject[] points;
    [SerializeField]
    private int numOfPoints;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ��� �� �߷� ���� X
        rb.isKinematic = true;

        GetComponent<TrailRenderer>().enabled = false;

        originPos = rb.position;

        points = new GameObject[numOfPoints];
        for (int i = 0; i < numOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
            points[i].SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isFlying)
        {
            isPressed = true;
            rb.isKinematic = true;

            // �߽� ����
            hook = Instantiate(hookPrefab, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            hookrb = hook.GetComponent<Rigidbody2D>();
            hooklr = hook.GetComponent<LineRenderer>();
            hooklr.startWidth = 0.05f;
            hooklr.endWidth = 0.05f;
        }
        if (Input.GetMouseButtonUp(0) && !isFlying)
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

        // ȭ���� ������⿡ �°� ȸ��
        if (isFlying)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
            rb.MoveRotation(Quaternion.Euler(new Vector3(0, 0, (angle * 180) / Mathf.PI)));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            this.enabled = false;

            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);

        if (nextArrow != null)
        {
            nextArrow.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator Release()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 moveVec = (originPos - rb.position) * (force - drag);
        rb.velocity = moveVec;

        isFlying = true;

        // ���� ����
        for (int i = 0; i < points.Length; i++)
        {
            Destroy(points[i]);
        }

        yield return new WaitForSeconds(releaseTime);

        GetComponent<TrailRenderer>().enabled = true;
    }

    void Aim()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        hooklr.SetPosition(0, hookrb.position);

        // ��� �� �ִ� �ִ� �Ÿ� ����
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

        // ���� ���
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

        // ��� ���� �� ���� ����
        if (!isCancelled)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].SetActive(true);
                points[i].transform.position = PointPosition(i * 0.1f);
            }
        }
    }

    Vector2 PointPosition(float t)
    {
        Vector2 curPointPos = (Vector2)transform.position + (originPos - rb.position) * (force - drag) * t + 0.5f * Physics2D.gravity * (t * t);
        return curPointPos;
    }
}