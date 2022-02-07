using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField]
    private float range;

    [SerializeField]
    private GameObject arrow;

    private void Update()
    {
        if (Vector2.Distance(arrow.transform.position, transform.position) <= range)
        {
            arrow.SetActive(false);
        }
    }
}
