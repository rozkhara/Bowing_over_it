using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seaweed : MonoBehaviour
{
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {

            Rigidbody2D arrow = collision.GetComponent<Rigidbody2D>();
            rb.velocity = arrow.velocity / 2;
            StartCoroutine(Bind(arrow));
        }
    }
    /// <summary>
    /// 0.4초 후 해초에 묶이는 루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator Bind(Rigidbody2D arrow)
    {
        yield return new WaitForSeconds(0.4f);
        arrow.constraints = RigidbodyConstraints2D.FreezeAll;
        arrow.transform.SetParent(transform, true);
        arrow.GetComponent<Arrow>().Reload();
        arrow.isKinematic = true;
    }
}
