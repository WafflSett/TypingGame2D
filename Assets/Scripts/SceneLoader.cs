using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public TMP_Text highscore;
    public Animator transition;
    public float transitionTime = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (highscore!=null)
        {   
            highscore.text = "Highscore: " + PlayerPrefs.GetInt("highscore").ToString();
        }
    }
    public void BackToTitle()
    {
        StartCoroutine(LoadLevel(0));
    }
    public void StartGame()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void GoToShop()
    {
        StartCoroutine(LoadLevel(2));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
