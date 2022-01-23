using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResultPanel : MonoBehaviour
{
    GameObject emptyStar;    // 빈 별 오브젝트
    GameObject Star;         // 별 오브젝트
    [SerializeField]
    Text clearTimeText;
    [SerializeField]
    Text ScoreText; 
    [SerializeField]
    private Transform stars;
    [HideInInspector]
    public int maxStar;      // Target.cs에서 최대 별 갯수 받아옴
    [HideInInspector]
    public int playerStar;   // Target.cs에서 플레이어가 획득한 별 갯수 받아옴
    [HideInInspector]
    public float clearTime;  // Target.cs에서 클리어 시간 받아옴
    [HideInInspector]
    public float score;        // Target.cs에서 플레이어 점수 받아옴
    [HideInInspector]
    public float maxScore;
    private void Start()
    {
        if (!FindObjectOfType<EventSystem>())
        {
            Instantiate(Tool.AssetLoader.LoadPrefab<GameObject>("defaultEventSystem"));
        }
        emptyStar = Tool.AssetLoader.LoadPrefab<GameObject>("forTest/emptyStar");
        Star = Tool.AssetLoader.LoadPrefab<GameObject>("forTest/Star");
        clearTimeText.text += $"{((int)(clearTime%3600/60)).ToString("D2")}:{((int)(clearTime%60)).ToString("D2")}.{((int)((clearTime-(int)clearTime)*100)).ToString("D2")}";
        ScoreText.text += $"{score.ToString("F1")} / {maxScore.ToString("F1")}";
        showStar();
    }
    private void showStar()
    {
        RectTransform rt = Star.GetComponent<RectTransform>();
        float starInterval = rt.rect.width+10;
        float starStartPoint = maxStar%2 == 0 ? -starInterval*(maxStar/2-0.5f) : -starInterval*(maxStar/2);
        int i;
        GameObject starInstance;
        for(i = 0; i < playerStar; i++)
        {
            starInstance = Instantiate(Star, stars);
            starInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(starStartPoint + starInterval * i, 0);
        }
        for (; i < maxStar; i++)
        {
            starInstance = Instantiate(emptyStar, stars);
            starInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(starStartPoint + starInterval * i, 0);
        }
        //yield return null;
        /*
        for (i = 0; i < maxStar; i++)
        {
            starInstances[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(starStartPoint + starInterval * i,0);
        }*/
    }
    public void ClosePanel()        // Panel을 닫고 다음 스테이지로 이동
    {
        gameObject.SetActive(false);
        //다음 스테이지로 이동은 차후 구현
    }


}
