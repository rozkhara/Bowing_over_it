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
        if (Arrow.isReloaded) return;

        if (--(GameManager.Instance.arrowCount) >= 0)
        {
            var arrowPrefab = AssetLoader.LoadPrefab<GameObject>("Arrows/Arrow");
            var newArrow = Instantiate(arrowPrefab, originPos, Quaternion.identity).GetComponent<SpriteRenderer>();
            if (GameManager.Instance.isFlipped) newArrow.flipX = false;

            Arrow.isReloaded = true;
        }
        else
        {
            Debug.Log("화살이 다 떨어졌습니다!");
        }
    }

    public void ReloadStoneArrow()
    {
        if (Arrow.isReloaded) return;

        if (--(GameManager.Instance.stoneArrowCount) >= 0)
        {
            var stoneArrowPrefab = AssetLoader.LoadPrefab<GameObject>("Arrows/StoneArrow");
            var newArrow = Instantiate(stoneArrowPrefab, originPos, Quaternion.identity).GetComponent<SpriteRenderer>();
            if (GameManager.Instance.isFlipped) newArrow.flipX = false;

            Arrow.isReloaded = true;
        }
        else
        {
            Debug.Log("돌 화살이 다 떨어졌습니다!");
        }
    }

    public void ReloadExplosiveArrow()
    {
        if (Arrow.isReloaded) return;

        if (--(GameManager.Instance.explosiveArrowCount) >= 0)
        {
            var explosiveArrowPrefab = AssetLoader.LoadPrefab<GameObject>("Arrows/ExplosiveArrow");
            var newArrow = Instantiate(explosiveArrowPrefab, originPos, Quaternion.identity).GetComponent<SpriteRenderer>();
            if (GameManager.Instance.isFlipped) newArrow.flipX = false;

            Arrow.isReloaded = true;
        }
        else
        {
            Debug.Log("폭발 화살이 다 떨어졌습니다!");
        }
    }

    public void ReloadWarpArrow()
    {
        if (Arrow.isReloaded) return;

        if (--(GameManager.Instance.warpArrowCount) >= 0)
        {
            var warpArrowPrefab = AssetLoader.LoadPrefab<GameObject>("Arrows/WarpArrow");
            var newArrow = Instantiate(warpArrowPrefab, originPos, Quaternion.identity).GetComponent<SpriteRenderer>();
            if (GameManager.Instance.isFlipped) newArrow.flipX = false;

            Arrow.isReloaded = true;
        }
        else
        {
            Debug.Log("워프 화살이 다 떨어졌습니다!");
        }
    }

    public void ReloadLureArrow()
    {
        if (Arrow.isReloaded) return;

        if (--(GameManager.Instance.lureArrowCount) >= 0)
        {
            var lureArrowPrefab = AssetLoader.LoadPrefab<GameObject>("Arrows/LureArrow");
            var newArrow = Instantiate(lureArrowPrefab, originPos, Quaternion.identity).GetComponent<SpriteRenderer>();
            if (GameManager.Instance.isFlipped) newArrow.flipX = false;

            Arrow.isReloaded = true;
        }
        else
        {
            Debug.Log("미끼 화살이 다 떨어졌습니다!");
        }
    }

    public void ReloadTripleArrow()
    {
        if (Arrow.isReloaded) return;

        if (--(GameManager.Instance.tripleArrowCount) >= 0)
        {
            var tripleArrowPrefab = AssetLoader.LoadPrefab<GameObject>("Arrows/TripleArrow");
            var newArrow = Instantiate(tripleArrowPrefab, originPos, Quaternion.identity).GetComponent<SpriteRenderer>();
            if (GameManager.Instance.isFlipped) newArrow.flipX = false;

            Arrow.isReloaded = true;
        }
        else
        {
            Debug.Log("3단 화살이 다 떨어졌습니다!");
        }
    }
}
