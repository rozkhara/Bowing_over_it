using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private float clearTime;        // clear time dependent on level (max time when player can get all star)
    private GameObject resultCanvas; // ���â�� ���Ǵ� canvas prefab
    private int maxStar; // �ش� stage�� �ִ� �� ����
    private float timer;
    private float meanOfMeanScore; // �� ���ῡ�� ���� ��� ������ ���
    private float meanOfMaxScore; // ���� �ִ� ������ ���
    private int count; // ������� ���� ���� ��
    private int countOrigin; // �����ؾ��ϴ� ���� ��
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
    /// ���ο� �������� ������ ������ ȣ������ߵǴ� �޼ҵ�
    /// maxStar: ������������ ȹ�� ������ �ִ� ��
    /// clearTime: �������� �⺻ Ŭ���� ���� �ð�
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
