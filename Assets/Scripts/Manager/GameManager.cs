using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int arrowCount;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    /// <summary>
    /// ���ο� �������� ������ ������ ȣ������ߵǴ� �޼ҵ�
    /// maxStar: ������������ ȹ�� ������ �ִ� ��
    /// clearTime: �������� �⺻ Ŭ���� ���� �ð�
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

