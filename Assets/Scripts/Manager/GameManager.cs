using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int arrowCount;
    public int stoneArrowCount;
    public int explosiveArrowCount;
    public int warpArrowCount;
    public int lureArrowCount;
    public int tripleArrowCount;

    private GameObject menuCanvas;
    private GameObject canvus;

    private void Awake()
    {
        Instance = this;
        menuCanvas = Tool.AssetLoader.LoadPrefab<GameObject>("MenuCanvas");
        canvus = Instantiate(menuCanvas);
        canvus.SetActive(false);
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            canvus.SetActive(!canvus.activeSelf);
        }
        
    }

    /// <summary>
    /// 새로운 스테이지 시작할 때마다 호출해줘야되는 메소드
    /// maxStar: 스테이지에서 획득 가능한 최대 별
    /// clearTime: 스테이지 기본 클리어 제한 시간
    /// </summary>
    public void StartNewStage(int maxStar, float clearTime, int arrowCount)
    {
        this.arrowCount = arrowCount;
        Target[] targets = FindObjectsOfType<Target>();
        TargetManager.Instance.countOrigin = targets.Length;
        TargetManager.Instance.count = TargetManager.Instance.countOrigin;
        if (TargetManager.Instance.countOrigin == 0)
        {
            Debug.LogError("ERROR: There is no target in the stage");
            gameObject.SetActive(false);
        }

        TargetManager.Instance.meanOfMeanScore = 0;
        TargetManager.Instance.meanOfMaxScore = 0;
        foreach (Target i in targets)
        {
            TargetManager.Instance.meanOfMaxScore += i.MaxScore;
        }
        TargetManager.Instance.meanOfMaxScore /= TargetManager.Instance.countOrigin;

        TargetManager.Instance.maxStar = maxStar;
        TargetManager.Instance.clearTime = clearTime;
    }
}

