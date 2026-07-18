using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public TMP_Text highscore;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (highscore!=null)
        {   
            highscore.text = "Highscore: " + PlayerPrefs.GetInt("highscore").ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void GoToShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
}
