using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Vector2 originPos;

    public void ReloadArrow()
    {
        if (--(GameManager.Instance.arrowCount) >= 0)
        {
            var arrowPrefab = AssetLoader.LoadPrefab<GameObject>("Arrows/Arrow");
            Instantiate(arrowPrefab, originPos, Quaternion.identity);
        }
        else
        {
            Debug.Log("화살이 다 떨어졌습니다!");
        }
    }

    public void ReloadStoneArrow()
    {
        if (--(GameManager.Instance.stoneArrowCount) >= 0)
        {
            var stoneArrowPrefab = AssetLoader.LoadPrefab<GameObject>("Arrows/StoneArrow");
            Instantiate(stoneArrowPrefab, originPos, Quaternion.identity);
        }
        else
        {
            Debug.Log("돌 화살이 다 떨어졌습니다!");
        }
    }
}
