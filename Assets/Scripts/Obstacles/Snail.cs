using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snail : MonoBehaviour
{
    public int hp;
    GameObject prfHpBar;
    GameObject prfHpBarBackGround;
    public GameObject canvas;
    RectTransform HpBar;
    RectTransform HpBarBackGround;

    private float height = 0.61f;


    public float movePower = 1f;

    Animator animator;
    Vector3 movement;
    int movementFlag = 0;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        HpBar.GetComponent<Image>().fillAmount -= (float)damage / (float)hp;
    }

    // Start is called before the first frame update
    void Start()
    {
        prfHpBar = GameObject.Find("prfHpBar");
        prfHpBarBackGround = GameObject.Find("prfHpBarBackGround");
        HpBarBackGround = Instantiate(prfHpBarBackGround, canvas.transform).GetComponent<RectTransform>();
        HpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        
        animator = gameObject.GetComponentInChildren<Animator>();

        StartCoroutine("ChangeMovement");     
    }

    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        Vector3 _hpBarBackGroundPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        HpBar.position = _hpBarPos;
        HpBarBackGround.position = _hpBarBackGroundPos;

        if (hp <= 0){
            Destroy(GameObject.Find("prfHpBar(Clone)"));
            Destroy(GameObject.Find("prfHpBarBackGround(Clone)"));
            Destroy(gameObject);
        }
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);

        if(movementFlag == 0){
            animator.SetBool("isMoving", false);
        }
        else{
            animator.SetBool("isMoving", true);
        }

        yield return new WaitForSeconds(3f);

        StartCoroutine("ChangeMovement");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if(movementFlag == 1){
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3 (1, 1, 1);
        }
        else if(movementFlag == 2){
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3 (-1, 1, 1);
        }

        transform.position += moveVelocity*movePower*Time.deltaTime;
    }
}