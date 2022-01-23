using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public float clearTime { get; set; }        // clear time dependent on level (max time when player can get all star)
    private GameObject resultCanvas; // ���â�� ���Ǵ� canvas prefab
    public int maxStar { get; set; } // �ش� stage�� �ִ� �� ����
    private float timer;
    public float meanOfMeanScore { get; set;  } // �� ���ῡ�� ���� ��� ������ ���
    public float meanOfMaxScore{ get; set;}// ���� �ִ� ������ ���
    public int count { get; set; } // ������� ���� ���� ��
    public int countOrigin{ get; set; } // �����ؾ��ϴ� ���� ��
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
        GameManager.Instance.StartNewStage(5, 180, 5); // for test
    }

    /// <summary>
    /// ���Ằ�� ������ Ƚ����ŭ ȭ���� ������ ������ ȣ���
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
        //Ŭ���� �ð� �ʰ��� ���� �� ������ ��ȹ�� ���� �� ����
        if (timer > clearTime)
        {
            ret *= 0;
        }
        ret = (int)Mathf.Round(ret * meanOfMeanScore / meanOfMaxScore);
        return ret;
    }
}
