using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    private LevelNode curLevelNode;

    private Rigidbody2D rb;

    private bool isReady = true;

    private string tmp;

    private void Start()
    {
        if (PlayerPrefs.HasKey("LastPlayedLevel"))
        {
            tmp = PlayerPrefs.GetString("LastPlayedLevel");
            curLevelNode = GameObject.Find(tmp).GetComponent<LevelNode>();
        }
        rb = GetComponent<Rigidbody2D>();
        rb.position = curLevelNode.gameObject.transform.position;
    }

    private void Update()
    {
        if (isReady)
        {
            float dx = Input.GetAxisRaw("Horizontal");

            if (dx == 1)
            {
                if (curLevelNode.rightNode != null)
                {
                    isReady = false;
                    StartCoroutine(SelectorMove(curLevelNode.rightNode));
                    curLevelNode = curLevelNode.rightNode;
                }
            }
            else if (dx == -1)
            {
                if (curLevelNode.leftNode != null)
                {
                    isReady = false;
                    StartCoroutine(SelectorMove(curLevelNode.leftNode));
                    curLevelNode = curLevelNode.leftNode;
                }
            }

            float dy = Input.GetAxisRaw("Vertical");

            if (dy == 1)
            {
                if (curLevelNode.upNode != null)
                {
                    isReady = false;
                    StartCoroutine(SelectorMove(curLevelNode.upNode));
                    curLevelNode = curLevelNode.upNode;
                }
            }
            else if (dy == -1)
            {
                if (curLevelNode.downNode != null)
                {
                    isReady = false;
                    StartCoroutine(SelectorMove(curLevelNode.downNode));
                    curLevelNode = curLevelNode.downNode;
                }
            }
        }
    }

    private IEnumerator SelectorMove(LevelNode dest)
    {
        while (rb.position != (Vector2)dest.gameObject.transform.localPosition)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, dest.gameObject.transform.localPosition, 60f * Time.deltaTime));
            yield return null;
        }

        isReady = true;
    }
}
