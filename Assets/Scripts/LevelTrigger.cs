using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    bool isTriggerStay;
    string sceneName;

    private void Start()
    {
        isTriggerStay = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTriggerStay = true;
        sceneName = this.gameObject.name;
    }
    private void Update()
    {
        if (isTriggerStay)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggerStay = false;
        sceneName = null;
    }
}
