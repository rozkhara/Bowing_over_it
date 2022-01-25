using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private float gravityFieldSize; // �༺ ũ�⸦ ���������� �߷� ���� ���� ( �߷°��ӵ��� 4�� �Ǵ� ������ ���� �� )
    private float gravityAcceleration; // a = GM/r^2���� GM/R^2
    private float originGravityScale;
    private Rigidbody2D rbPlanet;
    private const int G = 500; // ���� ������ ���ϰ� �ϱ� ���� G�� 500���� ���� ( ���߿� ���� ���� )
    private void Start()
    {
        rbPlanet = GetComponent<Rigidbody2D>();
        gravityFieldSize = Mathf.Sqrt(rbPlanet.mass * G/4);
        Transform child = transform.GetChild(0);
        child.localScale *= gravityFieldSize / transform.localScale.x;
        
    }
    /// <summary>
    /// gravityField ���ο� ��ġ�� �� �߷� ����
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Arrow")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if(rb.gravityScale != 0)
            {
                originGravityScale = rb.gravityScale;
                rb.gravityScale = 0;
            }
            Vector2 r = ((Vector2)transform.position - rb.position);
            gravityAcceleration = G * rbPlanet.mass / r.magnitude / r.magnitude;
            rb.AddForce(gravityAcceleration * rb.mass * r.normalized);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Arrow")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.gravityScale = originGravityScale;
        }
    }
}
