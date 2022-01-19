//레벨 간 불연속적 이동

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    public float dist = 20; //Distance between levels.
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(-dist, 0, 0);
        }else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(dist, 0, 0);
        }
    }

}
