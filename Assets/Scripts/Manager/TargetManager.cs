using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetManager : MonoBehaviour
{
    public float clearTime { get; set; }        // clear time dependent on level (max time when player can get all star)
    private GameObject resultCanvas; // 결과창에 사용되는 canvas prefab
    public int maxStar { get; set; } // 해당 stage의 최대 별 갯수
    private float timer;
    public float meanOfMeanScore { get; set; } // 각 과녁에서 계산된 평균 점수의 평균
    public float meanOfMaxScore { get; set; }// 과녁 최대 점수의 평균
    public int count { get; set; } // 현재까지 남은 과녁 수
    public int countOrigin { get; set; } // 적중해야하는 과녁 수
    private int bonusStar;
    Scene scene;
    public static TargetManager Instance
    {
        get;
        private set;
    }
    private void Awake()
    {
        if (Instance == null)
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
        scene = SceneManager.GetActiveScene();
        GameManager.Instance.StartNewStage(5,180,5); // for test
    }

    /// <summary>
    /// 과녁별로 설정한 횟수만큼 화살이 적중할 때마다 호출됨
    /// </summary>
    public void Hit(float timer, float meanScore)
    {
        meanOfMeanScore += meanScore;
        if (--count == 0)
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
        PlayerPrefs.SetInt(scene.name + "stars", star);
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
        maxStar += bonusStar;
        return ret + bonusStar;
    }

    public void GetBonusStar()
    {
        Debug.Log("GET BONUS STAR!!");
        bonusStar++;
    }

    public void StarStolen()
    {
        Debug.Log("별을 도둑질 당했다!");
    }
}
