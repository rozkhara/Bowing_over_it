using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bird : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float flightRange;
    public Transform center;
    private Rigidbody2D rb;
    private List<Arrow> arrows = new List<Arrow>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomMove());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;

    }


    IEnumerator RandomMove()
    {
        while (true)
        {
            
            if (Vector2.Distance(transform.position, center.position) >= flightRange)
            {
                transform.rotation= Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.z+180));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-180,180)));
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            arrows.Add(collision.gameObject.GetComponent<Arrow>());

            Destroy(gameObject);
        }
    }

}
