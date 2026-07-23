using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public GameObject ResultScreen;
    public GameObject GameScreen;
    public Text scoreText;
    public Text comboText;
    public Text multText;
    public Text hiText;
    public Text timerText;
    public TMP_Text ResAccuracyText;
    public TMP_Text ResEarnedText;
    public TMP_Text ResComboText;
    public ParticleSystem comboParticles;
    
    public TimeSpan timeLeft = TimeSpan.FromSeconds(10);
    public HashSet<GameObject> wordsInPlay = new HashSet<GameObject>();
    public HashSet<GameObject> highlightedWords = new HashSet<GameObject>();
    private int maxHighlight = 2;
    private int playerScore = 0;
    private int comboCounter = 0;
    private int maxCombo = 0;
    private int highScore;
    private int missed;
    private int hit;
    private LaserSpawnerScript lss;
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
        lss = GameObject.FindGameObjectWithTag("LaserSpawn").GetComponent<LaserSpawnerScript>();
        highScore = PlayerPrefs.GetInt("highscore");
        hiText.text = highScore.ToString();
        GameManager.instance.isInGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft.TotalMilliseconds > 0)
        {
            timeLeft = timeLeft - TimeSpan.FromSeconds(Time.deltaTime);
            timerText.text = timeLeft.ToString(@"mm\'ss\'ff");

            CheckForHits();
        }
        else
        {
            if (GameManager.instance.isInGame)
            {
                SetResults();
            }
        } 
    }

    private void CheckForHits()
    {
        if (Input.anyKeyDown)
        {
            string inString = Input.inputString;
            if (String.IsNullOrEmpty(inString)) return;
            foreach (var word in wordsInPlay)
            {
                var wt = word.GetComponent<WordTarget>();
                if (wt.textMesh.text.ToLower().StartsWith(inString.ToLower()))
                {
                    if (HighlightWord(word))
                    {
                        wt.isHighlighted = true;
                    }
                }
            }

            int wordsHit = 0;
            foreach (var word in highlightedWords)
            {
                var wt = word.GetComponent<WordTarget>();
                if (wt.textMesh.text.ToLower().StartsWith(inString.ToLower()))
                {
                    int score = addScore();
                    wt.textMesh.text = wt.textMesh.text.Remove(0, 1);
                    wt.textMesh.color = Color.yellow;
                    lss.spawnLaser(word);
                    wordsHit++;
                    if (String.IsNullOrEmpty(wt.textMesh.text))
                    {
                        int value = addScore(wt.startingText.Length);
                        DamagePopup.Create(word.transform.position, $"+{value}", true);
                        Destroy(word);
                    }
                    else
                    {
                        DamagePopup.Create(word.transform.position, $"+{score}");
                    }
                }
            }

            if (wordsHit == 0)
            {
                missed++;
                addScore(0); // reset combo meter
            }
            else
                hit++;
        }
    }

    public bool HighlightWord(GameObject word)
    {
        if (maxHighlight>0 && highlightedWords.Count >= maxHighlight) return false;
        highlightedWords.Add(word);
        return true;
    }

    public void SetResults()
    {
        GameManager.instance.AddBalance(playerScore);
        GameManager.instance.isInGame = false;
        GameScreen.SetActive(false);
        ResultScreen.SetActive(true);
        if (hit + missed > 0)
            ResAccuracyText.text = $"Accuracy: {Math.Round((decimal)hit / (hit + missed) * 100)}%";
        else
            ResAccuracyText.text = $"Accuracy: 0%";
        ResEarnedText.text = $"Earned: ${playerScore}";
        ResComboText.text = $"Highest Combo: {maxCombo}";
    }

    public int addScore(int value = 1) {
        if (value > 0)
        {
            comboCounter += value;
            if (comboCounter>maxCombo) maxCombo = comboCounter;
        }
        else
        {
            comboCounter = 0;
        }
        playerScore += (value*ComboMult);
        if (playerScore > highScore)
            highScore = playerScore;
        updateUI();
        return (value * ComboMult);
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
