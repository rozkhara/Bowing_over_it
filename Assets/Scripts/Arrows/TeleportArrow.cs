using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportArrow : Arrow
{
    private new void Update()
    {
        base.Update();
        if (isFlying && Input.GetMouseButtonDown(0))
        {
            transform.position += (Vector3)Vector2.right * 5;
        }
    }
}
