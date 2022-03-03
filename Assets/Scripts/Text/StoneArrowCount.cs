using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneArrowCount : MonoBehaviour
{
    [SerializeField]
    private Text stoneArrowText;

    private void Update()
    {
        stoneArrowText.text = GameManager.Instance.stoneArrowCount.ToString();
    }
}
