using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tool;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private Text tutorialText;

    [SerializeField]
    private Arrow arrow;
    [SerializeField]
    private Target target;
    [SerializeField]
    private GameObject playerSprite;

    [SerializeField]
    private float textChangingTime;

    private float timer;

    private int counter = 0;

    [SerializeField]
    private string[] texts;

    private void Start()
    {
        tutorialText.text = "";
        timer = textChangingTime;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (counter < 3 && timer > textChangingTime)
        {
            tutorialText.text = texts[counter++];
            timer = 0f;
        }

        if (counter >= 3 && timer > textChangingTime)
        {
            arrow.gameObject.SetActive(true);
            target.gameObject.SetActive(true);
            playerSprite.SetActive(true);
            Destroy(tutorialText.gameObject);

            GameManager.Instance.gameObject.AddComponent<TargetManager>();

            this.enabled = false;
        }
    }
}
