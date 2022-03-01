using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFeed = false; // 먹이화살 쐈을 때만 먹도록
    public LureArrow lurearrow;
    private bool isPigRun = true;
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Floor"&& isFeed == true)
        {
            Destroy(collision);
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (lurearrow.isRun == true)
        {
             RunToFeed();
        }
    }
    public void RunToFeed()
    {
        Debug.Log("달려");
        StartCoroutine(Run());
    }
    private IEnumerator Run()
    {
        if (isPigRun == true&&lurearrow)
        {
            
            while (rb.position != (Vector2)lurearrow.gameObject.transform.localPosition)
            {
                isFeed = true;
                rb.MovePosition(Vector2.MoveTowards(rb.position, lurearrow.transform.localPosition, 60f * Time.deltaTime));
                yield return null;
            }
        }

        isPigRun = false;

    }

}
