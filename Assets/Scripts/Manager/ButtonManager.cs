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

    public void ReloadExplosiveArrow()
    {
        if (--(GameManager.Instance.explosiveArrowCount) >= 0)
        {
            var stoneArrowPrefab = AssetLoader.LoadPrefab<GameObject>("Arrows/ExplosiveArrow");
            Instantiate(stoneArrowPrefab, originPos, Quaternion.identity);
        }
        else
        {
            Debug.Log("폭발 화살이 다 떨어졌습니다!");
        }
    }

    public void ReloadWarpArrow()
    {
        if (--(GameManager.Instance.warpArrowCount) >= 0)
        {
            var stoneArrowPrefab = AssetLoader.LoadPrefab<GameObject>("Arrows/WarpArrow");
            Instantiate(stoneArrowPrefab, originPos, Quaternion.identity);
        }
        else
        {
            Debug.Log("워프 화살이 다 떨어졌습니다!");
        }
    }

    public void ReloadLureArrow()
    {
        if (--(GameManager.Instance.lureArrowCount) >= 0)
        {
            var stoneArrowPrefab = AssetLoader.LoadPrefab<GameObject>("Arrows/LureArrow");
            Instantiate(stoneArrowPrefab, originPos, Quaternion.identity);
        }
        else
        {
            Debug.Log("미끼 화살이 다 떨어졌습니다!");
        }
    }

    public void ReloadTripleArrow()
    {
        if (--(GameManager.Instance.tripleArrowCount) >= 0)
        {
            var stoneArrowPrefab = AssetLoader.LoadPrefab<GameObject>("Arrows/TripleArrow");
            Instantiate(stoneArrowPrefab, originPos, Quaternion.identity);
        }
        else
        {
            Debug.Log("3단 화살이 다 떨어졌습니다!");
        }
    }
}
