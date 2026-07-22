using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private int balance;
    public int Balance
    {
        get { return balance; }
        set { balance = value; }
    }

    public WordDeck deck;
    public bool isInGame = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddBalance(int gain)
    {
        Balance += gain;
    }
}
