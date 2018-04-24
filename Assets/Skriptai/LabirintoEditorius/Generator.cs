using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomData
{
    public int id = -1;
    public int rotation = 0;
    public RoomData(int id, int rotation)
    {
        this.id = id;
        this.rotation = rotation;
    }
}

public class Generator : MonoBehaviour {

    public Camera cam1;
    private Camera cam2;
    static int playerX;
    static int playerY;
    static int exitX;
    static int exitY;
    public GameObject playerRoom;
    public GameObject playerCamera;
    public GameObject playerPrefab;
    public Canvas editorOverlay;
    public Canvas gameOverlay;
    public List<GameObject> prefabId;
    public List<GameObject> availableRooms;
    public GameObject[] placedRooms;
    public ButtonManager buttonManager;
    public GameObject finish;
    //private GameObject playerPr;
    //private GameObject playerIc;
    private GameObject currentPlayer;
    bool generatedOnce;
    static RoomData[,] roomCoord = new RoomData[25, 15];

    public void Start()
    {
        playerX = 9;
        playerY = 1;
        exitX = 9;
        exitY = 10;
        editorOverlay.enabled = true;
        gameOverlay.enabled = false;
        cam1.enabled = true;
        placedRooms = new GameObject[50];
        generatedOnce = false;
        DontDestroyOnLoad(gameObject);
        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                roomCoord[x, y] = new RoomData(-1, 0);
            }
        }
        AddPlayerRoom(playerX, playerY);
    }

    public void AddPlayerRoom(int x, int y)
    {
        var playerPr = Instantiate(playerRoom, new Vector3(10 * playerX, 0, 10 * playerY), transform.rotation);
        playerPr.transform.Rotate(new Vector3(0,-90,0));
        var exitR = Instantiate(finish, new Vector3(10 * exitX, 0, 10 * exitY), transform.rotation);
        exitR.transform.Rotate(new Vector3(0, 90, 0));
        currentPlayer = Instantiate(playerPrefab, new Vector3(10 * playerX, 0, 10 * playerY), transform.rotation);
        var cam = Instantiate(playerCamera);
        cam.transform.position = new Vector3(10 * playerX, 18, 10 * (playerY-2));
        cam2 = cam.GetComponentInChildren<Camera>();
        cam2.GetComponent<CamFollow>().target = currentPlayer.transform;
        cam2.enabled = false;
        currentPlayer.GetComponent<Move>().SetCam(cam2);
        var playerIc = Instantiate(buttonManager.creaRoom, new Vector3(playerX, playerY), transform.rotation);
        playerIc.GetComponent<PlacedRoom>().Setup(buttonManager.rooms[0]);
    }

    public static void AddRoom(int x, int y, int rotation, int id)
    {
        roomCoord[x, y].rotation = rotation;
        roomCoord[x, y].id = id;
    }

    public static void RemoveRoom(int x, int y)
    {
        if (!(x == playerX && y == playerY))
        {
            roomCoord[x, y].rotation = 0;
            roomCoord[x, y].id = -1;
        }    
    }

    public static bool isEmpty(int x, int y)
    {
        if (roomCoord[x, y].id == -1 && !(x == playerX && y == playerY))
            return true;
        else
        {
            Debug.Log("not empty");
            return false;   
        }
    }

    public void ClearRooms()
    {
        placedRooms = GameObject.FindGameObjectsWithTag("PlacedRoom");

        for (int i = 0; i < placedRooms.Length; i++)
        {
            Destroy(placedRooms[i]);
        }

        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if (roomCoord[x, y].id != -1 && !(x == playerX && y == playerY))
                {
                    RemoveRoom(x, y);
                }
            }
        }
    }

    public void DestroyRooms()
    {
        while (availableRooms.Count != 0)
        {
            Destroy(availableRooms[0]);
            availableRooms.RemoveAt(0);
        }

    }

    public void GoToEditor(bool erase)
    {
        
        if (generatedOnce == true)
        {
            DestroyRooms();
        }
        if (erase == true)
        ClearRooms();
        currentPlayer.transform.position = new Vector3(playerX * 10, 0, playerY * 10);
        ChangeCanvas();
    }

    public void GoToGame()
    {
        GenerateRooms();
        ChangeCanvas();
    }

    public void GenerateRooms()
    {
        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if(roomCoord[x,y].id != -1)
                {
                    GameObject r = Instantiate(prefabId[roomCoord[x, y].id], new Vector3(10 * x, 0, 10 * y), transform.rotation);
                    r.transform.Rotate(new Vector3(r.transform.rotation.x, -90 * roomCoord[x, y].rotation, r.transform.rotation.z));
                    availableRooms.Add(r);
                }
            }
        }
        generatedOnce = true;
    }

    public void ChangeCanvas()
    {
        cam1.enabled = !cam1.enabled;
        editorOverlay.enabled = !editorOverlay.enabled;
        gameOverlay.enabled = !gameOverlay.enabled;
        cam2.enabled = !cam2.enabled;
    }
}
