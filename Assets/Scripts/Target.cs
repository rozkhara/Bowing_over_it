using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private int numberOfScore;      // number of discrete score (e.g. numOfScore = 10 ==> score = 1, 2 ... 10)
    public int MaxScore
    {
        get
        {
            return numberOfScore;
        }
    }
    [Tooltip("���ῡ ����� �ϴ� ȭ���� ����")]
    [SerializeField]
    private int arrowCountOrigin;
    private int arrowCount;         // ���ῡ ������ϴ� ���� ȭ���� ����
    private float scoreInterval;    // additional radius value per score
    private float targetSize;       // size of target based on y value
    private Vector2 normalVec;      // Ÿ���� ������ ��������
    private int[] scores;           // scores player get, which dependent on location of arrow
    private float meanScore;        // score�� ���
    private float timer;            // time since the start of the stage
    public float Timer
    {
        get
        {
            return timer;
        }
    }
    private void Start() 
    {
        normalVec = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, new Vector2(0, 0)) * new Vector2(0, 1);
        Vector2 targetLoc = transform.position;
        targetSize = transform.localScale.y;
        scoreInterval = targetSize / 2f / numberOfScore;
        scores = new int[arrowCountOrigin];
        arrowCount = arrowCountOrigin;
        if(arrowCountOrigin == 0)
        {
            Debug.LogError("ERROR: Arrow count of Target is zero");
            gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //arrow�� position�� ȭ���� ���̶� ����
        if (collision.transform.tag == "Arrow")
        {
            StartCoroutine(ArrowTrigger(collision.transform.GetComponent<Rigidbody2D>()));
        }
    }
    
    private IEnumerator ArrowTrigger(Rigidbody2D rb)
    {
        Vector2 arrowLoc = rb.gameObject.transform.GetChild(0).position;
        if(arrowCount>0)
            scores[arrowCount-1] = CalcScore(arrowLoc);
        yield return new WaitForFixedUpdate(); // ȭ���� ���ῡ ������ �ð�
        rb.velocity = new Vector2(0, 0);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (--arrowCount == 0)
        {
            float sum = 0;
            foreach (int i in scores)
            {
                sum += i;
            }
            TargetManager.Instance.Hit(timer, sum / arrowCountOrigin);
        }
    }

 


    private int CalcScore(Vector2 arrowLoc) // calculate score based on arrow location
    {
        Vector2 dirVec = arrowLoc - (Vector2)transform.position;
        float dist = Mathf.Abs(Vector2.Dot(dirVec, normalVec));
        Debug.Log(arrowLoc.ToString("F4"));
        Debug.Log(dist.ToString("F4"));
        
        return Mathf.Min(Mathf.Max(-(int)(dist / scoreInterval), -numberOfScore + 1), 0) + numberOfScore;
    }
}
