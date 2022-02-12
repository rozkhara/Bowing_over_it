using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    public float hp;
    public float speed;
    private float pos;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hp :" + hp);
        pos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Die();
        }
    }

    public float GetHp()
    {
        return hp;
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        TakeDamage(10);
        Debug.Log("hp :" + hp);
    }

     void Die()
     {
        Destroy(this.gameObject);
     }

     void Move()
     {
        float rightMax = 2.0f;
        float leftMax = -2.0f;
        pos += Time.deltaTime * speed;
        if(pos >= rightMax)
        {
            speed *= -1;
            pos = rightMax;
        }
        else if(pos <= leftMax)
        {
            speed *= -1;
            pos = leftMax;
        }
        transform.position = new Vector3(pos, 0, 0);
     }
}
