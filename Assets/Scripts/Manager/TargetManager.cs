using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private float clearTime;        // clear time dependent on level (max time when player can get all star)
    private GameObject resultCanvas; // 결과창에 사용되는 canvas prefab
    private int maxStar; // 해당 stage의 최대 별 갯수
    private float timer;
    private float meanOfMeanScore; // 각 과녁에서 계산된 평균 점수의 평균
    private float meanOfMaxScore; // 과녁 최대 점수의 평균
    private int count; // 현재까지 남은 과녁 수
    private int countOrigin; // 적중해야하는 과녁 수
    public static TargetManager Instance
    {
        get;
        private set;
    }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        resultCanvas = Tool.AssetLoader.LoadPrefab<GameObject>("ResultCanvas");
    }
    void Start()
    {
        StartNewStage(5, 180); // for test
    }

    /// <summary>
    /// 새로운 스테이지 시작할 때마다 호출해줘야되는 메소드
    /// maxStar: 스테이지에서 획득 가능한 최대 별
    /// clearTime: 스테이지 기본 클리어 제한 시간
    /// </summary>
    public void StartNewStage(int maxStar, float clearTime)
    {
        Target[] targets = FindObjectsOfType<Target>();
        count = targets.Length;
        countOrigin = count;
        if (countOrigin == 0)
        {
            Debug.LogError("ERROR: There is no target in the stage");
            gameObject.SetActive(false);
        }

        meanOfMeanScore = 0;
        meanOfMaxScore = 0;
        foreach(Target i in targets)
        {
            meanOfMaxScore += i.MaxScore;
        }
        meanOfMaxScore /= countOrigin;

        this.maxStar = maxStar;
        this.clearTime = clearTime;
    }
    /// <summary>
    /// 과녁별로 설정한 횟수만큼 화살이 적중할 때마다 호출됨
    /// </summary>
    public void Hit(float timer, float meanScore)
    {
        meanOfMeanScore += meanScore;
        if(--count == 0)
        {
            this.timer = timer;
            meanOfMeanScore /= countOrigin;
            Time.timeScale = 0f;
            ClearStage();
        }        
    }

    private void ClearStage() // calculate number of star player get, show stage clear panel
    {
        int star = CalcNumOfStar();
        GameObject canv = Instantiate(resultCanvas);
        ResultPanel panel = canv.transform.GetComponentInChildren<ResultPanel>();
        panel.maxStar = maxStar;
        panel.playerStar = star;
        panel.clearTime = timer;
        panel.score = meanOfMeanScore;
        panel.maxScore = meanOfMaxScore;
    }

    private int CalcNumOfStar() // calculate number of star player get
    {
        int ret = maxStar;
        //클리어 시간 초과에 따른 별 감점은 기획이 나온 후 구현
        if (timer > clearTime)
        {
            ret *= 0;
        }
        ret = (int)Mathf.Round(ret * meanOfMeanScore / meanOfMaxScore);
        return ret;
    }
}
