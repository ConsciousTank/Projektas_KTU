using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMan : MonoBehaviour {

    public ButtonManager buttonManager;
    public Generator generator;
    public Camera editorCamera;
    public Camera gameCamera;
    public Camera menuCamera;
    public Camera scoreCamera;
    Camera playerCamera;
    public Canvas editorOverlay;
    public Canvas gameOverlay;
    public Canvas menuOverlay;
    public Canvas scoreOverlay;
    public GameObject playerPrefab;
    public GetLevelFromManager glfm;
    public ScoreScript ss;
    private int maxX;
    private int startingPointX;
    private int startingPointY;
    private int endingPointX;
    private int endingPointY;
    private int level;
    private int score;

    GameObject currentPlayer;

    int state;
    int cameraX = 0;
    int cameraY = 0;
    int cameraZ = 0;
    int playerX = 0;
    int playerY = 0;
    int playerZ = 0;

    public enum State
    {
        DEFAULT, 
        INVENTORY,
        INGAME,
        EDITOR,
        OPTIONS,
        MENU,
        DIALOGUE,
        SCOREBOARD,
    };

   
    void Start () {
        maxX = 16;
        startingPointX = 9;
        startingPointY = 1;
        endingPointX = 9;
        endingPointY = 9;
        state = (int)State.EDITOR;
        editorOverlay.enabled = true;
        gameOverlay.enabled = false;
        menuOverlay.enabled = false;
        editorCamera.enabled = true;
        gameCamera.enabled = false;
        menuCamera.enabled = false;
        scoreCamera.enabled = false;
        scoreOverlay.enabled = false;
    }

    public int GetStartingPointX()
    {
        return startingPointX;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int value)
    {
        score = score + value;
        ss.UpdateScore();
    }

    public int GetStartingPointY()
    {
        return startingPointY;
    }

    public int GetEndingPointX()
    {
        return endingPointX;
    }

    public int GetEndingPointY()
    {
        return endingPointY;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape") && (State)state == State.INGAME)
        SwitchState(State.MENU);

    }
    public void SetupPlayer(int x, int y, int z)
    {
        Destroy(currentPlayer);
        currentPlayer = Instantiate(playerPrefab);
        currentPlayer.transform.position = new Vector3(x, y, z);
        playerX = x;
        playerY = y;
        playerZ = z;
        currentPlayer.GetComponent<Move>().SetCam(gameCamera);
        currentPlayer.GetComponent<Move>().gameManager = this;
        gameCamera.GetComponent<CamFollow>().target = currentPlayer.transform;
        currentPlayer.SetActive(false);
        //playerCamera = currentPlayer.GetComponentInChildren<Camera>();
        //playerCamera.GetComponent<CamFollowFP>().enabled = false;
        //playerCamera.enabled = false;
    }

    public void NextLevel()
    {
        level = level + 1;
        glfm.UpdateLevel();
        RenderSettings.ambientLight = new Color(1 - 0.2f * level, 1 - 0.2f * level, 1 - 0.2f * level);
        MixPosition();
        generator.AddPlayerRoom();
        GoToEditor(true);
    }



    public void SetupPlayer()
    {
        Destroy(currentPlayer);
        currentPlayer = Instantiate(playerPrefab);
        currentPlayer.transform.position = new Vector3(playerX, playerY, playerZ);
        currentPlayer.GetComponent<Move>().SetCam(gameCamera);
        currentPlayer.GetComponent<Move>().gameManager = this;
        gameCamera.GetComponent<CamFollow>().target = currentPlayer.transform;
        currentPlayer.SetActive(false);
        //playerCamera = currentPlayer.GetComponentInChildren<Camera>();
        //playerCamera.GetComponent<CamFollowFP>().enabled = false;
        //playerCamera.enabled = false;
    }


    public void SetupCamera(int x, int y, int z)
    {
        gameCamera.transform.position = new Vector3(x, y, z);
        gameCamera.GetComponent<CamFollow>().ResetOffset();
        cameraX = x;
        cameraY = y;
        cameraZ = z;
    }

    public void SetupCamera()
    {
        gameCamera.transform.position = new Vector3(cameraX, cameraY, cameraZ);
        gameCamera.GetComponent<CamFollow>().ResetOffset();
    }

    public void MixPosition()
    {
        startingPointX = Random.Range(3, maxX);
        endingPointX = Random.Range(3, maxX);
    }

    public void GetBackToState(int state)
    {
        SwitchState((State)state);
    }

    public void GoToGame()
    {
        generator.GenerateRooms();
        SetupCamera();
        SwitchState(State.INGAME);
    }

    public void GoToEditor(bool erase)
    {
        generator.ResetRooms(erase);
        SwitchState((State.EDITOR));
    }

    public void SwitchState(State intoState)
    {
        switch(intoState)
        {
            case State.SCOREBOARD:
                editorCamera.enabled = false;
                editorCamera.gameObject.GetComponent<AudioListener>().enabled = true;
                currentPlayer.SetActive(false);
                //playerCamera.enabled= true;
                //playerCamera.GetComponent<CamFollowFP>().enabled = true;
                gameOverlay.enabled = false;
                editorOverlay.enabled = false;
                menuCamera.enabled = false;
                menuOverlay.enabled = false;
                scoreCamera.enabled = true;
                scoreOverlay.enabled = true;
                Debug.Log(intoState);
                state = (int)State.SCOREBOARD;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case State.INGAME:
                editorCamera.enabled = false;
                editorCamera.gameObject.GetComponent<AudioListener>().enabled = false;
                currentPlayer.SetActive(true);
                //playerCamera.enabled= true;
                //playerCamera.GetComponent<CamFollowFP>().enabled = true;
                gameOverlay.enabled = true;
                editorOverlay.enabled = false;
                menuCamera.enabled = false;
                menuOverlay.enabled = false;
                scoreCamera.enabled = false;
                scoreOverlay.enabled = false;
                Debug.Log(intoState);
                state = (int)State.INGAME;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case State.EDITOR:
                editorCamera.enabled = true;
                editorCamera.gameObject.GetComponent<AudioListener>().enabled = true;
                currentPlayer.SetActive(false);
                //playerCamera.enabled = false;
                //playerCamera.GetComponent<CamFollowFP>().enabled = false;
                gameOverlay.enabled = false;
                editorOverlay.enabled = true;
                menuCamera.enabled = false;
                menuOverlay.enabled = false;
                scoreCamera.enabled = false;
                scoreOverlay.enabled = false;
                Debug.Log(intoState);
                state = (int)State.EDITOR;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case State.MENU:
                editorCamera.enabled = false;
                editorCamera.gameObject.GetComponent<AudioListener>().enabled = true;
                currentPlayer.SetActive(false);
                //playerCamera.enabled = false;
                //playerCamera.GetComponent<CamFollowFP>().enabled = false;
                gameOverlay.enabled = false;
                editorOverlay.enabled = false;
                menuCamera.enabled = true;
                menuOverlay.enabled = true;
                scoreCamera.enabled = false;
                scoreOverlay.enabled = false;
                Debug.Log(intoState);
                state = (int)State.MENU;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
}
