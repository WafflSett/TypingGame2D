using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Word Deck")]
public class WordDeck : ScriptableObject
{
    public string[] words;
    public Sprite icon;
    public string title;
}