using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    public TMP_Text title_field;
    public SceneLoader sceneLoader;
    public Image selectedImage;


    private WordDeck[] decks;

    private WordDeck selectedDeck;
    public WordDeck SelectedDeck
    {
        get { return selectedDeck; }
        set { 
            selectedDeck = value;
            title_field.text = selectedDeck.title;
            selectedImage.sprite = selectedDeck.icon;
            }
    }

    private int selectedIndex;
    public int SelectedIndex
    {
        get { return selectedIndex; }
        set { selectedIndex = value; SelectedDeck = decks[selectedIndex]; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        decks = Resources.LoadAll<WordDeck>("Decks");
        Debug.Log($" {decks.Length} decks successfully loaded");
        SelectedDeck = decks[0];
    }

    public void ScrollFwd()
    {
        if (SelectedIndex >= decks.Length - 1) SelectedIndex = 0;
        else SelectedIndex++;
    }

    public void ScrollBck()
    {
        if (SelectedIndex == 0) SelectedIndex = decks.Length - 1;
        else SelectedIndex--;
    }

    public void StartGame()
    {
        GameManager.instance.deck = SelectedDeck;
        sceneLoader.StartGame();
    }
}
