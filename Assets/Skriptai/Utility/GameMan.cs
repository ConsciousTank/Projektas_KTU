using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.                                // The audio clip to play when the player dies.
    public float flashSpeed = 5;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    private int maxX;
    private int startingPointX;
    private int startingPointY;
    private int endingPointX;
    private int endingPointY;
    private bool invulnerable = false;
    private float invTime = 1f;
    private int level;

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
    private int currentScore = 0;
    private int maxScore = 0;
    private int playerMaxHealth = 100;
    private int playerHealth = 100;
    private int playerNumberOfSlots;
    private int playerStrength = 10;
    private int playerAgility = 10;
    private int playerIntelligence = 10;
    private int playerExperiencePoints = 20;
    private int playerMaxExperiencePoints = 100;
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


    public int[] GetAttributes()
    {
        int[] duom = new int[7];
        duom[0] = playerStrength;
        duom[1] = playerAgility;
        duom[2] = playerIntelligence;
        duom[3] = playerMaxExperiencePoints;
        duom[4] = playerExperiencePoints;
        duom[5] = currentScore + maxScore;
        duom[6] = playerHealth;
        return duom;
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

        if (Input.GetButtonDown("Inventorius") == true && !GameObject.Find("Daikto paieska").GetComponent<InputField>().isFocused)
        {
            if ((State)state == State.INGAME)
            {
                SwitchState(State.INVENTORY);
            }
            else
            if ((State)state == State.INVENTORY)
            {
                SwitchState(State.INGAME);
            }
        }
        if (invulnerable == false)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
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

    //Player Coordinates--------------------------------------------------------
    public Vector3 GetPlayerCoordinates()
    {
        return currentPlayer.transform.position;
    }

    public Ray GetPlayerRay()
    {
        return gameCamera.ScreenPointToRay(new Vector3(100,0,100));
    }

    public Transform GetCurrentPlayerTransform()
    {
        return currentPlayer.transform;
    }

    public  Vector3 GetPlayerFront()
    {
        return currentPlayer.transform.forward;
    }

    public Vector3 GetPlayerCoordinates(int x, int y, int z)
    {
        return new Vector3(currentPlayer.transform.position.x + x, currentPlayer.transform.position.y + y, currentPlayer.transform.position.z + z);
    }
    //---------------------------------------------------------------------------

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

    public void ApplyDamage(int damage)
    {
        if (!invulnerable)
        {
            playerHealth = playerHealth - damage;
            GameObject.Find("GameHealth").GetComponent<Slider>().value = playerHealth;
            damageImage.color = flashColour;
            Debug.Log("Player has taken 10 damage");
            if (playerHealth <= 0)
            {
                GoToEditor(false);
                GameObject.Find("GameOver").GetComponent<Text>().color = Color.red;
                StartCoroutine(GameOverFade());
                playerHealth = playerMaxHealth;
                GameObject.Find("GameHealth").GetComponent<Slider>().value = playerHealth;
                Debug.Log("Player has died, resetting health, going back to Editor");
            }
            StartCoroutine(JustHurt());
        }
    }

    IEnumerator GameOverFade()
    {
        yield return new WaitForSeconds(3f);
        GameObject.Find("GameOver").GetComponent<Text>().color = Color.clear;
    }

    IEnumerator JustHurt()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invTime);
        invulnerable = false;
    }

    //kviečiamas su Scoreboard continue mygtuku
    public void NextLevel()
    {
        level = level + 1;
        glfm.UpdateLevel();
        Debug.Log("Proceeding to the next level");
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
        generator.ResetRooms(erase);
        SwitchState((State.EDITOR));
    }

    public void GoBackToMainMenu()
    {
        Debug.Log("Loading Editor Level");
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
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
                Debug.Log("Switching to state: " + intoState);
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
                Debug.Log("Switching to state: " + intoState);
                state = (int)State.INGAME;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<BlobAI>().RefreshPlayer();
                }
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
                Debug.Log("Switching to state: " + intoState);
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
                Debug.Log("Switching to state: " + intoState);
                state = (int)State.MENU;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case State.INVENTORY:
                GameObject.Find("Atributai").GetComponent<PaimtIsGamemano>().UpdateAtt(GetAttributes());
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
                Debug.Log("Switching to state: " + intoState);
                state = (int)State.INVENTORY;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
}
