using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCount : MonoBehaviour
{
    [SerializeField]
    private Text arrowText;

    private void Update()
    {
        arrowText.text = GameManager.Instance.arrowCount.ToString();
    }
}
