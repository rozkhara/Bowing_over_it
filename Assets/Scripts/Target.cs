using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private GameObject resultCanvas;
    [SerializeField]
    private float clearTime;        // clear time dependent on level (max time when player can get all star)
    public float ClearTime
    {
        get
        {
            return clearTime;
        }
    }
    [SerializeField]
    private int maxStar;
    [SerializeField]
    private int numberOfScore;      // number of discrete score (e.g. numOfScore = 10 ==> score = 1, 2 ... 10)
    [SerializeField]
    private int arrowCount;         // ���ῡ ������ϴ� ȭ���� ����
    private float scoreInterval;    // additional radius value per score
    private float targetSize;       // size of target based on y value
    private Vector2 targetLoc;      // location of target
    private Vector2 baseLocation;   // base location of target which is used to calculate score
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
        baseLocation = transform.position +  Quaternion.AngleAxis(transform.rotation.eulerAngles.z,new Vector3(0,0,1))*new Vector3(-1,0,0)* transform.localScale.x/2;
        targetLoc = transform.position;
        targetSize = transform.localScale.y;
        scoreInterval = targetSize / 2f / numberOfScore;
        scores = new int[arrowCount];
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
            ClearStage();
        }
    }

    private void ClearStage() // calculate number of star player get, show stage clear panel
    {
        float sum = 0;
        foreach(int i in scores)
        {
            sum += i;
        }
        meanScore = sum / scores.Length;
        int star = CalcNumOfStar();
        GameObject canv = Instantiate(resultCanvas);
        ResultPanel panel = canv.transform.GetComponentInChildren<ResultPanel>();
        panel.maxStar = maxStar;
        panel.playerStar = star;
        panel.clearTime = timer;
        panel.score = meanScore;
        panel.maxScore = numberOfScore;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("TRIGGER!");
    }

    private int CalcNumOfStar() // calculate number of star player get
    {
        int ret = maxStar;
        //Ŭ���� �ð� �ʰ��� ���� �� ������ ��ȹ�� ���� �� ����
        if (timer > ClearTime)
        {
            ret *= 0;
        }
        ret = (int)Mathf.Round(ret * meanScore / (float)numberOfScore);
        return ret;
    }

    private int CalcScore(Vector2 arrowLoc) // calculate score based on arrow location
    {
        float dist = Vector2.Distance(arrowLoc, baseLocation);
        
        Debug.Log(arrowLoc.ToString("F4"));
        Debug.Log(baseLocation.ToString("F4"));
        Debug.Log(Vector2.Distance(arrowLoc, baseLocation));
        
        return Mathf.Min(Mathf.Max(-(int)(dist / scoreInterval), -numberOfScore + 1), 0) + numberOfScore;
    }
}
