using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class WordSpawnerScript : MonoBehaviour
{
    public GameObject wordObject;
    public float spawnOffset = 6;
    public float spawnRate = 2;
    private string[] wordList;
    private List<string> wordBucket;
    private float timer = 0;
    private LogicScript logic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        wordList = GameManager.instance.deck.words;
        spawnWordObject();
    }

    // Update is called once per frame
    void Update()
    {
        // do nothing if game over
        if (!GameManager.instance.isInGame) return;

        if (timer > Random.Range(spawnRate, spawnRate+2))
        {
            timer = 0;
            spawnWordObject();
            if (spawnRate > 1.5f)
            {
                spawnRate -= 0.1f;
            }
            else if (spawnRate <= 1.5f && spawnRate > 1f)
            {
                spawnRate -= 0.05f;
            }
            else if (spawnRate <= 1f) { }
        }
        else {
            timer += Time.deltaTime;
        }
    }

    private void spawnWordObject() {
        float leftMax = transform.position.x - spawnOffset;
        float rightMax = transform.position.x + spawnOffset;
        GameObject newWord = Instantiate(wordObject, new Vector3(Random.Range(leftMax,rightMax), transform.position.y, 0), transform.rotation);
        WordTarget ts = newWord.GetComponent<WordTarget>();
        if (wordBucket==null || wordBucket.Count<=0)
        {
            wordBucket = wordList.ToList();
        }
        //Debug.Log(wordBucket.Count);
        int index = Random.Range(0, wordBucket.Count);
        ts.startingText = wordBucket[index];
        wordBucket.RemoveAt(index);
        // long ones around 2, short ones around 4
        if (ts.startingText.Length <= 4)
        {
            ts.moveSpeed = 3;
        }
        else if (ts.startingText.Length > 4 && ts.startingText.Length <= 6)
        {
            ts.moveSpeed = 2.5f;
        }
        else if (ts.startingText.Length>6&&ts.startingText.Length<=8)
        {
            ts.moveSpeed = 2f;
        }else 
        {
            ts.moveSpeed = 1.5f;
        }
        logic.wordsInPlay.Add(newWord);
    }
}
