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
    private float scoreInterval;    // additional radius value per score
    private float targetSize;       // size of target based on y value
    private Vector2 targetLoc;      // location of target
    private Vector2 baseLocation;   // base location of target which is used to calculate score
    private int score;            // score player get, which dependent on location of arrow
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
    }
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //arrow의 position이 화살촉 끝이라 가정
        if (collision.transform.tag == "Arrow")
        {
            StartCoroutine(ArrowTrigger(collision.transform.GetComponent<Rigidbody2D>()));
        }
    }
    
    private IEnumerator ArrowTrigger(Rigidbody2D rb)
    {
        Vector2 arrowLoc = rb.gameObject.transform.GetChild(0).position;
        score = CalcScore(arrowLoc);
        yield return new WaitForFixedUpdate(); // 화살이 과녁에 박히는 시간
        rb.velocity = new Vector2(0, 0);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
        ClearStage();
    }

    private void ClearStage() // calculate number of star player get, show stage clear panel
    {
        int star = CalcNumOfStar();
        GameObject canv = Instantiate(resultCanvas);
        ResultPanel panel = canv.transform.GetComponentInChildren<ResultPanel>();
        panel.maxStar = maxStar;
        panel.playerStar = star;
        panel.clearTime = timer;
        panel.score = score;
        panel.maxScore = numberOfScore;
    }

    private int CalcNumOfStar() // calculate number of star player get
    {
        int ret = maxStar;
        //클리어 시간 초과에 따른 별 감점은 기획이 나온 후 구현
        if (timer > ClearTime)
        {
            ret *= 0;
        }
        ret = (int)Mathf.Round(ret * (float)score / (float)numberOfScore);
        return ret;
    }

    private int CalcScore(Vector2 arrowLoc) // calculate score based on arrow location
    {
        float dist = Vector2.Distance(arrowLoc, baseLocation);
        
        Debug.Log(arrowLoc);
        Debug.Log(baseLocation);
        Debug.Log(Vector2.Distance(arrowLoc, baseLocation));
        
        return Mathf.Min(Mathf.Max(-(int)(dist / scoreInterval), -numberOfScore + 1), 0) + numberOfScore;
    }
}
