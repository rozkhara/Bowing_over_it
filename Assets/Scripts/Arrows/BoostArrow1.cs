using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostArrow1 : Arrow
{
    [SerializeField]
    private float BoostVelocity;
    bool onBoost = false;
    private new void Update()
    {
        base.Update();
        if (!onBoost&isFlying && Input.GetMouseButtonDown(0))
        {
            onBoost = true;
            StartCoroutine(BoostRoutine());
        }
    }
    /// <summary>
    /// 클릭 받았을 때 화살이 가속하는 루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator BoostRoutine()
    {
        float originXVel = rb.velocity.x;
        rb.velocity = new Vector2(BoostVelocity, 0);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(1.0f);
        rb.gravityScale = 1;
        rb.velocity = new Vector2(originXVel, 0);
    }
}
