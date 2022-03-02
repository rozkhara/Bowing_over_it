using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, -10);
    }
}
