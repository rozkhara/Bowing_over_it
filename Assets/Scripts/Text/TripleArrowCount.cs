using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TripleArrowCount : MonoBehaviour
{
    [SerializeField]
    private Text tripleArrowText;

    private void Update()
    {
        tripleArrowText.text = GameManager.Instance.tripleArrowCount.ToString();
    }
}
