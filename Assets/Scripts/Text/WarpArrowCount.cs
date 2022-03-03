using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarpArrowCount : MonoBehaviour
{
    [SerializeField]
    private Text warpArrowText;

    private void Update()
    {
        warpArrowText.text = GameManager.Instance.warpArrowCount.ToString();
    }
}
