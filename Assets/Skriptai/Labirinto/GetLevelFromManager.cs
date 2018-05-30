using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetLevelFromManager : MonoBehaviour {


    public GameMan gameManager;
    private Text textComponent;

	void Start () {
        textComponent = GetComponent<Text>();
    }

    public void UpdateLevel()
    {
        textComponent.text = "Level " + (gameManager.GetLevel() + 1) + " completed \n\nScore: " + gameManager.GetCurrentScore() + " \n\n Enemies defeated: " + gameManager.GetEnemiesDefeated();
    }

}
