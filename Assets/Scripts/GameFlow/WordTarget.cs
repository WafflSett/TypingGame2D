using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordTarget : MonoBehaviour
{
    public float moveSpeed = 2;
    public string startingText = "test";
    public TMP_Text textMesh;
    private LogicScript logic;
    public bool isHighlighted = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMesh = gameObject.GetComponent<TMP_Text>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        textMesh.text = startingText;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isInGame) Destroy(gameObject);
        if (Input.anyKeyDown)
        {
            string inString = Input.inputString;
        }
        moveSelf();
    }

    private void OnDestroy()
    {
        if(isHighlighted) logic.highlightedWords.Remove(gameObject);
        logic.wordsInPlay.Remove(gameObject);
    }

    private void moveSelf() { 
        transform.position += (transform.position.y<=-2?moveSpeed/2:moveSpeed) * Time.deltaTime * Vector3.down;
        if (transform.position.y<-5)
        {
            Destroy(gameObject);
            logic.addScore(textMesh.text.Length*-2);
        }
    }
}
