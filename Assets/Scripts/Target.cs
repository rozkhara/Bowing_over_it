using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float clearTime;        // clear time dependent on level (max time when player can get all star)
    public float ClearTime
    {
        get
        {
            return ClearTime;
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
    private float score;            // score player get, which dependent on location of arrow
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
        baseLocation = transform.position + transform.forward * transform.localScale.x/2;
        targetLoc = transform.position;
        targetSize = transform.localScale.y;
        scoreInterval = targetSize / 2f / numberOfScore;
    }
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //arrow�� position�� ȭ���� ���̶� ����
        if(collision.transform.tag == "arrow")
        {
            Vector2 arrowLoc = collision.transform.position;
            score = CalcScore(arrowLoc);
            ClearStage();
        }
    }

    private void ClearStage() // calculate number of star player get, show stage clear panel
    {
        int star = CalcNumOfStar();
    }

    private int CalcNumOfStar() // calculate number of star player get
    {
        int ret = maxStar;
        //Ŭ���� �ð� �ʰ��� ���� �� ������ ��ȹ�� ���� �� ����
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
        return (int)(dist / scoreInterval) + 1; 
    }
}
