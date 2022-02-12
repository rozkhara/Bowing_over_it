using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnPoints;

    [SerializeField]
    private float waitTime;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        sr.enabled = false;

        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            int idx = Random.Range(0, spawnPoints.Length);

            yield return StartCoroutine(MoveMole(spawnPoints[idx]));
        }
    }

    private IEnumerator MoveMole(GameObject spawnPoint)
    {
        sr.enabled = true;

        rb.position = spawnPoint.transform.localPosition;

        while (rb.position != (Vector2)spawnPoint.transform.localPosition + Vector2.up * 3f)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, (Vector2)spawnPoint.transform.localPosition + Vector2.up * 3f, 50f * Time.deltaTime));
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);

        while (rb.position != (Vector2)spawnPoint.transform.localPosition)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, (Vector2)spawnPoint.transform.localPosition, 50f * Time.deltaTime));
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Arrow"))
        {
            StartCoroutine(DestroyCoroutine(collision.gameObject));
        }
    }

    private IEnumerator DestroyCoroutine(GameObject arrow)
    {
        yield return new WaitForEndOfFrame();

        Destroy(arrow);
        Destroy(gameObject);
    }
}
