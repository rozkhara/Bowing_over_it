using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private float range;

    [HideInInspector]
    public Arrow arrow;

    private void Update()
    {
        if (arrow != null)
        {
            if (Vector2.Distance(arrow.rb.position, transform.position) <= range)
            {
                arrow.rb.velocity = ((Vector2)transform.position - arrow.rb.position).normalized * 5f;
                // arrow.rb.MovePosition(Vector2.MoveTowards(arrow.rb.position, transform.position, 60f * Time.deltaTime));
            }

            if (Vector2.Distance(arrow.rb.position, transform.position) <= 0.05f)
            {
                // StartCoroutine(arrow.ReloadCoroutine());
                Destroy(arrow.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ExplosiveArrow")
        {
            Destroy(gameObject);
        }
    }
}
