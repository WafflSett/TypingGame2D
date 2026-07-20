using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public Text scoreText;
    public Text comboText;
    public Text multText;
    public Text hiText;
    public Text timerText;
    public ParticleSystem comboParticles;
    public int playerScore = 0;
    public TimeSpan timeLeft = TimeSpan.FromSeconds(10);
    private int comboCounter = 0;
    private int highScore;
    public int ComboMult { get {
            if (comboCounter >= 50)
                return 5;
            if (comboCounter >= 35)
                return 4;
            if (comboCounter >= 20)
                return 3;
            if (comboCounter>=10)
                return 2;
            return 1;
        }}
    public Color ComboColor { get {
            switch (ComboMult)
            {
                case 1:
                    return new Color(42 / 255f, 168 / 255f, 242 / 255f);
                case 2:
                    return new Color(139 / 255f, 212 / 255f, 72 / 255f);
                case 3:
                    return new Color(251 / 255f, 169 / 255f, 73 / 255f);
                case 4:
                    return new Color(255 / 255f, 99 / 255f, 85 / 255f);
                case 5:
                    return new Color(156 / 255f, 79 / 255f, 150 / 255f);
                default:
                    return Color.yellow;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore");
        hiText.text = highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft.TotalMilliseconds>0)
        {
            timeLeft = timeLeft - TimeSpan.FromSeconds(Time.deltaTime);
            timerText.text = timeLeft.ToString(@"mm\'ss\'ff");
        }
        else
        {
            SceneManager.LoadScene("ShopScene");
        }
    }

    public void addScore(int value = 1) {
        if (value > 0)
            comboCounter += value;
        else
            comboCounter = 0;
        playerScore += (value*ComboMult);
        if (playerScore > highScore)
            highScore = playerScore;
        updateUI();
    }

    public void updateUI() {
        scoreText.text = playerScore.ToString();
        comboText.text = comboCounter.ToString();
        multText.text = ComboMult.ToString()+"x";
        //multText.color = ComboColor;
        comboLogic();
        hiText.text = highScore.ToString();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("highscore", highScore);
        PlayerPrefs.Save();
    }

    private void comboLogic() {
        if (ComboMult >= 3)
        {
            comboParticles.Play();
            ParticleSystem.ColorOverLifetimeModule col = comboParticles.colorOverLifetime;
            Gradient gradient = new Gradient();
            if (ComboMult == 3)
            {
                gradient.SetKeys(
                    new GradientColorKey[2] {
                        new GradientColorKey(ComboColor, 0f), new GradientColorKey(Color.white, 1f)
                    },
                    new GradientAlphaKey[] {
                        new GradientAlphaKey(1.0f, 0f), new GradientAlphaKey(0.0f, 1f)
                    }
                );
            }
            else if (ComboMult == 4)
            {
                gradient.SetKeys(
                    new GradientColorKey[2] {
                        new GradientColorKey(ComboColor, 0f), new GradientColorKey(Color.yellow, 1f)
                    },
                    new GradientAlphaKey[] {
                        new GradientAlphaKey(1.0f, 0f), new GradientAlphaKey(0.0f, 1f)
                    }
                );
            }
            else if (ComboMult == 5)
            {
                gradient.SetKeys(
                    new GradientColorKey[2] {
                        new GradientColorKey(ComboColor, 0f), new GradientColorKey(Color.cyan, 1f)
                    },
                    new GradientAlphaKey[] {
                        new GradientAlphaKey(1.0f, 0f), new GradientAlphaKey(0.0f, 1f)
                    }
                );
            }
            col.color = gradient;
        }
        else
        {
            comboParticles.Stop();
        }
    }
}
