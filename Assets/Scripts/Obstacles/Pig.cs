using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public GameObject targets;
    private Rigidbody2D rb;
    private List<Arrow> arrows = new List<Arrow>();
    private bool isFeed = false; // 먹이화살 쐈을 때만 먹도록
    [SerializeField]
    private bool isPigRun = true;
    // Start is called before the first frame update
    void Start()
    {
        RunToFeed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Floor"&& isFeed == true)
        {
            Destroy(collision);
            Destroy(gameObject);
        }
    }
    public void RunToFeed()
    {
        StartCoroutine(Run());
    }
    private IEnumerator Run()
    {
        if (isPigRun == true)
        {
            
            while (rb.position != (Vector2)targets.gameObject.transform.localPosition)
            {
                rb.MovePosition(Vector2.MoveTowards(rb.position, targets.transform.localPosition, 60f * Time.deltaTime));
                yield return null;
            }
        }

        isPigRun = false;

    }

}
