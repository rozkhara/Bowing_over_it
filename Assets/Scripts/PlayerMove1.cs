//연속적 이동 조작
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove1 : MonoBehaviour
{

    public float speed = 8f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 translateVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //Debug.Log(translateVector);
        gameObject.transform.Translate(translateVector.normalized * speed * Time.deltaTime);
        Debug.Log(gameObject.transform);
    }
}
