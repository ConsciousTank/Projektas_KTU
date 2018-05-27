using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public GameMan gameManager;
    private Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    public void UpdateScore()
    {
        textComponent.text = "Score: " + gameManager.GetCurrentScore();
    }
}
