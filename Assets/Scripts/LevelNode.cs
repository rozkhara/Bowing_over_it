using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNode : MonoBehaviour
{
    public GameObject emptyStar;
    public GameObject Star;
    public SpriteRenderer renderer;
    public LevelNode upNode;
    public LevelNode downNode;
    public LevelNode leftNode;
    public LevelNode rightNode;

    [SerializeField]
    private Transform stars;

    bool isTriggerStay;
    string sceneName;
    int starCount;

    private void Start()
    {
        sceneName = this.gameObject.name;
        renderer = GetComponent<SpriteRenderer>();
        isTriggerStay = false;
        LoadLevelNode();
        starCount = PlayerPrefs.GetInt(sceneName + "stars");
        ShowStars();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTriggerStay = true;
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

    private void LoadLevelNode()
    {
        if (PlayerPrefs.HasKey(sceneName + "stars")&&PlayerPrefs.GetInt(sceneName+"stars")>0)
        {
            renderer.color = new Color(0, 255, 0, 1);
        }
        else
        {
            PlayerPrefs.SetInt(sceneName + "stars", 0);
            renderer.color = new Color(255, 0, 0, 1);
        }
        
    }
    private void ShowStars() // calculate number of star player get, show stage clear panel
    {
        float starInterval = Star.transform.localScale.x * 1.2f;
        float starStartPoint = -starInterval;
        int i;
        GameObject starInstance;
        for (i = 0; i < starCount; i++)
        {

            starInstance = Instantiate(Star, stars);
            starInstance.transform.Translate(new Vector2(starStartPoint + starInterval * i, 0));
        }

        Debug.Log(sceneName);
        for (; i < 3; i++)
        {
            starInstance = Instantiate(emptyStar, stars);
            starInstance.transform.Translate(new Vector2(starStartPoint + starInterval * i, 0));
        }
    }
}
