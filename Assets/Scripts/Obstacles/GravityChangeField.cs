using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangeField : MonoBehaviour
{
    [SerializeField]
    Vector2 gravityDir;                 // �߷��� ����
    [SerializeField]
    private float gravityScale;         // �ٲ��� �߷��� ���
    private float originGravityScale;   // �߷��� ������� �����ֱ� ���� ����

    /// <summary>
    /// ȭ���� ���� �� �߷��� ������ ������ �����Ѵ�.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Arrow")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            originGravityScale = rb.gravityScale;
            rb.gravityScale = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Arrow")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.AddForce(gravityDir * gravityScale * Physics2D.gravity.magnitude);
        }
    }
    /// <summary>
    /// ȭ���� ���� �� �߷��� ������� �ǵ�����.
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = originGravityScale;
        }
    }
}
