using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    float time = 0f;
    /// <summary>
    /// 0.5초 이상 구름에 머무르면 화살이 얼어붙음
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "UnstoppedArrow") return;

        if (collision.tag.Contains("Arrow"))
        {
            time += Time.fixedDeltaTime;
            Debug.Log(time);
            if (time >= 0.5f)
            {
                time = 0f;
                Freeze(collision.GetComponent<Arrow>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "UnstoppedArrow") return;

        if (collision.tag.Contains("Arrow"))
        {
            time = 0f;
        }
    }
    private void Freeze(Arrow arrow)
    {
        arrow.isFrozen = true;
        arrow.GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
