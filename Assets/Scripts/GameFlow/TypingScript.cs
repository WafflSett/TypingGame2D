using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypingScript : MonoBehaviour
{
    public float moveSpeed = 2;
    public string startingText = "test";
    private TMP_Text textMesh;
    private LogicScript logic;
    private LaserSpawnerScript pss;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMesh = gameObject.GetComponent<TMP_Text>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        pss = GameObject.FindGameObjectWithTag("LaserSpawn").GetComponent<LaserSpawnerScript>();
        textMesh.text = startingText;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            //Debug.Log(textMesh.text);
            string inString = Input.inputString;
            if (!String.IsNullOrEmpty(Input.inputString) && textMesh.text.ToLower().StartsWith(inString.ToLower()))
            {
                logic.addScore();
                textMesh.text = textMesh.text.Remove(0, 1);
                textMesh.color = Color.yellow;
                pss.spawnLaser(this.gameObject);
                if (String.IsNullOrEmpty(textMesh.text))
                {
                    logic.addScore(10);
                    Destroy(gameObject);
                }
            }
        }
        moveSelf();
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
