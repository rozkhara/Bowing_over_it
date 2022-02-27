using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostArrow2: Arrow
{
    [SerializeField]
    private float BoostVelocity;
    bool onBoost = false;
    private new void Update()
    {
        base.Update();
        if (!onBoost & isFlying && Input.GetMouseButtonDown(0))
        {
            onBoost = true;
            BoostRoutine(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    /// <summary>
    /// 클릭 받았을 때 화살이 가속하는 루틴
    /// </summary>
    /// <returns></returns>
    private void BoostRoutine(Vector2 clickPos)
    {
        rb.velocity = (clickPos - (Vector2)transform.position).normalized * BoostVelocity;
        rb.gravityScale = 0;
    }
}
