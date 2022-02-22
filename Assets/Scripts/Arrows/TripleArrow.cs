using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleArrow : Arrow
{
    private GameObject arrowPrefab;
    private new void Awake()
    {
        arrowPrefab = Tool.AssetLoader.LoadPrefab<GameObject>("Arrows/Arrow");
        base.Awake();
    }
    protected override IEnumerator Release()
    {
        yield return base.Release();
        yield return DivisionRoutine();
    }
    /// <summary>
    /// 화살이 3갈래로 나뉘는 루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator DivisionRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        if (!isFlying)
        {
            yield break;
        }
        GetComponent<SpriteRenderer>().color = Color.blue;
        for(int i = 0; i < 2; i++)
        {
            Arrow arrow = Instantiate(arrowPrefab).GetComponent<Arrow>();
            arrow.tr.enabled = true;
            arrow.isFlying = true;
            arrow.isPressed = false;
            arrow.rb.isKinematic = false;
            arrow.GetComponent<SpriteRenderer>().color = Color.blue;
            arrow.rb.position = this.rb.position + new Vector2(0, (i-0.5f)*3);
            arrow.rb.velocity = this.rb.velocity + new Vector2(0,(i-0.5f)*5);
            arrow.isClone = true;
        }
    }
}
