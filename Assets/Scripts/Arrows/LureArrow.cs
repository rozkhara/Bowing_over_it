using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LureArrow : Arrow
{
    private List<Pig> pigs = new List<Pig>();
    private new void Update()
    {
        base.Update();
        if (isLanded == true)
        {
            foreach (Pig pig in pigs)
            {
                pig.RunToFeed();
            }
            
        }
    }
}
