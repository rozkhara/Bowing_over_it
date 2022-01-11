using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool isPressed = false;
    private bool isFlying = false;
    private bool isCancelled = false;

    // ����
    [SerializeField]
    private GameObject pointPrefab;
    [SerializeField]
    private GameObject[] points;
    [SerializeField]
    private int numOfPoints;

    private Rigidbody2D rb;
    [SerializeField]
    private Rigidbody2D hook;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ��� �� �߷� ���� X
        rb.isKinematic = true;

        GetComponent<TrailRenderer>().enabled = false;

        points = new GameObject[numOfPoints];
        for (int i = 0; i < numOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
            points[i].SetActive(false);
        }
    }

    void Update()
    {
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

    void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }

    void OnMouseUp()
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
    }

    IEnumerator Release()
    {
        Vector2 moveVec = (hook.position - rb.position) * force;
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
        // ��� �� �ִ� �ִ� �Ÿ� ����
        if (Vector2.Distance(mousePos, hook.position) > maxDragDist)
        {
            rb.MovePosition(hook.position + (mousePos - hook.position).normalized * maxDragDist);
        }
        else
        {
            rb.MovePosition(mousePos);
        }

        // ���� ���
        if (Vector2.Distance(mousePos, hook.position) < minDragDist)
        {
            rb.position = hook.position;
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

        rb.MoveRotation(Quaternion.LookRotation(hook.position - mousePos));

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
        Vector2 curPointPos = (Vector2)transform.position + (hook.position - rb.position) * force * t + 0.5f * Physics2D.gravity * (t * t);
        return curPointPos;
    }
}