using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LureArrowCount : MonoBehaviour
{
    [SerializeField]
    private Text lureArrowText;

    private void Update()
    {
        lureArrowText.text = GameManager.Instance.lureArrowCount.ToString();
    }
}
