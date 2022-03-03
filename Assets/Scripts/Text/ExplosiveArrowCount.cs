using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplosiveArrowCount : MonoBehaviour
{
    [SerializeField]
    private Text explosiveArrowText;

    private void Update()
    {
        explosiveArrowText.text = GameManager.Instance.explosiveArrowCount.ToString();
    }
}
