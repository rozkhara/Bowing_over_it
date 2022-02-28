using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LureArrow : Arrow
{

    public bool isRun = false;
    private new void Start()
    {
        base.Start();
        for (int i = 0; i < ObstacleManager.Instance.pigs.Length; i++)
        {
            ObstacleManager.Instance.pigs[i].lurearrow = this;
        }
    }
    private new void Update()
    {
        base.Update();
        isRun = isLanded;
    }

}
