using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public string nextLevel;
    public string thisLevel;
    public string[] stringArray;

    public int sceneNum;
    public int sceneStringNum;
    
    public GameObject playerObj;
    public GameObject fadeImageObj;
    private Image fadeImage;


    // Start is called before the first frame update
    void Awake()
    {
        fadeImage = fadeImageObj.GetComponent<Image>();
        if (fadeImageObj.activeSelf == false)
        {
            fadeImageObj.SetActive(true);
        }
    }

    void Start()
    {
        // Fade in
        StartCoroutine(Fading(-1));
        NextLevelName();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneNum != 0)
        {
            StartCoroutine(ChangeLevel());
        }

        if (Input.GetKeyDown(KeyCode.Return) && sceneStringNum == 1)
        {
            sceneNum = 3;
        }
    }
    
    void NextLevelName()
    {
        // Split scene name by "_" (ex: Level_5 = [0](Level) [1](5))
        thisLevel = SceneManager.GetActiveScene().name;
        stringArray = thisLevel.Split("_"[0]);
    
        // Get int number from string and add to it by 1.
        sceneStringNum = System.Convert.ToInt32(stringArray[1]) + 1;

        if (sceneStringNum == 6)
            sceneStringNum = 1;
        // Create next level name, put it into nextLevel variable.
        nextLevel = stringArray[0] + "_" + sceneStringNum;
    }
    
    IEnumerator Fading(float negOrPosFade)
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 10; i++)
        {
            fadeImage.color += new Color(0,0,0,0.1f * negOrPosFade);
            yield return new WaitForSeconds(0.01f);
        }
        //Debug.Log(fadeImage.color.a);
    }

    IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Fading(1));
        if (fadeImage.color.a > 1)
        {
            if (sceneNum == 1)
            {
                SceneManager.LoadScene(thisLevel);
            }
            else
            {
                SceneManager.LoadScene(nextLevel);
            }
        }
    }


}
