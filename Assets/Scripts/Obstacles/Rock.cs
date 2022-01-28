using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private Transform endPos;
    private Transform desPos;

    private int hitCount = 0;
    [SerializeField]
    private int maxHitCount;

    private List<Arrow> arrows = new List<Arrow>();

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.position = startPos.position;
        desPos = endPos;

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(Vector2.MoveTowards(rb.position, desPos.position, speed * Time.fixedDeltaTime));

        if (Vector2.Distance(rb.position, desPos.position) <= 0.05f)
        {
            if (desPos == startPos)
            {
                desPos = endPos;
            }
            else
            {
                desPos = startPos;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            ++hitCount;
            arrows.Add(collision.gameObject.GetComponent<Arrow>());

            if (hitCount >= maxHitCount)
            {
                foreach (Arrow arrow in arrows)
                {
                    arrow.DetachRock();
                }

                Destroy(gameObject);
            }
        }
    }
}
