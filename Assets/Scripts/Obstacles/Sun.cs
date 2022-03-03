using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField]
    private float range;

    [HideInInspector]
    public Arrow arrow;

    private void Update()
    {
        if (arrow != null && Vector2.Distance(arrow.rb.position, transform.position) <= range)
        {
            // StartCoroutine(arrow.ReloadCoroutine());
            Arrow.isReloaded = false;
            Destroy(arrow.gameObject);
        }
    }
}
