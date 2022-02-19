using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    public float hp;
    public float speed;

    private float pos;

    private List<Arrow> arrows = new List<Arrow>();

    private void Start()
    {
        pos = transform.position.x;
    }

    private void Update()
    {
        Move();
    }

    private void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    public float GetHp()
    {
        return hp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "StoneArrow")
        {
            TakeDamage(GetHp());
        }
        else
        {
            arrows.Add(collision.gameObject.GetComponent<Arrow>());

            TakeDamage(10);
        }
    }

    private void Die()
    {
        foreach (Arrow arrow in arrows)
        {
            arrow.DetachRock();
        }

        Destroy(gameObject);
    }

    private void Move()
    {
        float rightMax = 2.0f;
        float leftMax = -2.0f;

        pos += Time.deltaTime * speed;

        if (pos >= rightMax)
        {
            speed *= -1;
            pos = rightMax;
        }
        else if (pos <= leftMax)
        {
            speed *= -1;
            pos = leftMax;
        }

        transform.position = new Vector3(pos, transform.position.y, 0);
    }
}
