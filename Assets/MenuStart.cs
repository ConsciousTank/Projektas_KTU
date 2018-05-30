using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour {


    public void NextScene()
    {
        Debug.Log("Loading Editor Level");
        SceneManager.LoadScene("LEditor", LoadSceneMode.Single);
    }
}
