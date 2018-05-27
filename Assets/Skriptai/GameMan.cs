using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public class GameMan : MonoBehaviour
{

    public ButtonManager buttonManager;
    public Generator generator;
    public Camera editorCamera;
    public Camera gameCamera;
    public Camera menuCamera;
    public Camera scoreCamera;
    public Camera inventoryCamera;
    Camera playerCamera;
    public Canvas editorOverlay;
    public Canvas gameOverlay;
    public Canvas menuOverlay;
    public Canvas scoreOverlay;
    public Canvas inventoryOverlay;
    public GameObject playerPrefab;
    public GetLevelFromManager glfm;
    public ScoreScript ss;
    private int maxX;
    private int startingPointX;
    private int startingPointY;
    private int endingPointX;
    private int endingPointY;
    [SerializeField] private int level;
    [SerializeField] private int currentScore;
    [SerializeField] private int maxScore;
    public void SetupForTests(Camera editorCamera, Camera gameCamera, Camera menuCamera, Camera scoreCamera, Camera inventoryCamera, Canvas editorOverlay, Canvas gameOverlay, Canvas menuOverlay, Canvas scoreOverlay, Canvas inventoryOverlay, Generator generator)
    {
        this.editorCamera = editorCamera;
        this.gameCamera = gameCamera;
        this.menuCamera = menuCamera;
        this.scoreCamera = scoreCamera;
        this.inventoryCamera = inventoryCamera;
        this.editorOverlay = editorOverlay;
        this.gameOverlay = gameOverlay;
        this.menuOverlay = menuOverlay;
        this.scoreOverlay = scoreOverlay;
        this.inventoryOverlay = inventoryOverlay;
        this.generator = generator;
}
    GameObject currentPlayer;
    //Player constants
    public const int playerMaxNumberOfSlots = 9;
    public const int playerMaxStrength = 100;
    public const int playerMaxAgility = 100;
    public const int playerMaxIntelligence = 100;
    //Player stats--------------------------
    private int playerHealth;
    private int playerMaxHealth;
    private int playerNumberOfSlots;
    private int playerStrength;
    private int playerAgility;
    private int playerIntelligence;
    private int playerExperiencePoints;
    public Inventorius playerListOfItems;
    public Daiktas daiktas;
    //-------------------------------------------------
    int state;
    int cameraX = 0;
    int cameraY = 0;
    int cameraZ = 0;
    int playerX = 0;
    int playerY = 0;
    int playerZ = 0;



    void Start ()
    {
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
        inventoryCamera.enabled = false;
        inventoryOverlay.enabled = false;
        generator.AddPlayerRoom();
    }


    public int GetStartingPointX()
    {
        return startingPointX;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public State GetState()
    {
        return (State)state;
    }

    public void AddCurrentScore(int value)
    {
        currentScore = currentScore + value;
        ss.UpdateScore();
    }

    public void AddToScore()
    {
        maxScore = maxScore + currentScore;
        currentScore = 0;
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

        if (Input.GetButtonDown("Inventorius") == true)
        {
            playerListOfItems.Prideti(daiktas);
            if ((State)state == State.INGAME)
            {
                SwitchState(State.INVENTORY);
            }
            else
            if ((State)state == State.INVENTORY)
                SwitchState(State.INGAME);
        }

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

    //Player stats get and set methods---------------------------------
    public int getNumberOfSlots()
    {
        return playerNumberOfSlots;
    }
    public void setNumberOfSlots(int number)
    {
        playerNumberOfSlots = number;
    }
    //-----------------------------------------------------------

        //kviečiamas su Scoreboard continue mygtuku
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
        currentScore = 0;
        ss.UpdateScore();
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
                inventoryCamera.enabled = false;
                inventoryOverlay.enabled = false;
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
                inventoryCamera.enabled = false;
                inventoryOverlay.enabled = false;
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
                inventoryCamera.enabled = false;
                inventoryOverlay.enabled = false;
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
                inventoryCamera.enabled = false;
                inventoryOverlay.enabled = false;
                Debug.Log(intoState);
                state = (int)State.MENU;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case State.INVENTORY:
                editorCamera.enabled = false;
                editorCamera.gameObject.GetComponent<AudioListener>().enabled = true;
                currentPlayer.SetActive(false);
                //playerCamera.enabled = false;
                //playerCamera.GetComponent<CamFollowFP>().enabled = false;
                gameOverlay.enabled = false;
                editorOverlay.enabled = false;
                menuCamera.enabled = false;
                menuOverlay.enabled = false;
                scoreCamera.enabled = false;
                scoreOverlay.enabled = false;
                inventoryCamera.enabled = true;
                inventoryOverlay.enabled = true;
                Debug.Log(intoState);
                state = (int)State.INVENTORY;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
}
