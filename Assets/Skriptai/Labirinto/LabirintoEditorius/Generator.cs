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



    public GameObject playerPrefab;
    public GameObject startRoom;
    public GameObject finishRoom;
    public GameObject startRoomE;
    public GameObject finishRoomE;
    public List<GameObject> prefabId;
    public List<GameObject> Rooms;
    public List<GameObject> coins;
    public ButtonManager buttonManager;
    public GameMan gameManager;
    public GameObject coin;
    public GameObject darkDoor;
    private GameObject currentPlayer;
    private GameObject currentFinishRoom;
    private GameObject currentStartRoom;
    private GameObject currentFinishRoomE;
    private GameObject currentStartRoomE;
    bool generatedOnce;
    static RoomData[,] roomCoord = new RoomData[25, 15];
    private static int startingPointX;
    private static int startingPointY;
    private static int endingPointX;
    private static int endingPointY;
    

    public void Start()
    {
        generatedOnce = false;
        DontDestroyOnLoad(gameObject);
        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                roomCoord[x, y] = new RoomData(-1, 0);
            }
        }
    }

    public void AddPlayerRoom()
    {
        Destroy(currentStartRoomE);
        Destroy(currentStartRoom);
        Destroy(currentFinishRoom);
        Destroy(currentFinishRoomE);
        SetStartingEndingPoints(gameManager.GetStartingPointX(), gameManager.GetStartingPointY(), gameManager.GetEndingPointX(), gameManager.GetEndingPointY());
        currentStartRoom = Instantiate(startRoom, new Vector3(gameManager.GetStartingPointX()*10, 0, gameManager.GetStartingPointY() * 10), transform.rotation);
        currentStartRoom.transform.Rotate(new Vector3(0,-90,0));
        currentFinishRoom = Instantiate(finishRoom, new Vector3(gameManager.GetEndingPointX() * 10, 0, gameManager.GetEndingPointY() * 10), transform.rotation);
        currentFinishRoom.transform.Rotate(new Vector3(0, 90, 0));
        currentFinishRoomE = Instantiate(finishRoomE, new Vector3(gameManager.GetStartingPointX(), gameManager.GetStartingPointY()), transform.rotation);
        currentStartRoomE = Instantiate(startRoomE, new Vector3(gameManager.GetEndingPointX(), gameManager.GetEndingPointY()), transform.rotation);
        currentStartRoomE.transform.Rotate(new Vector3(0, 0, 180));
        gameManager.SetupPlayer(gameManager.GetStartingPointX() *10, 1, gameManager.GetStartingPointY() * 10);
        gameManager.SetupCamera(gameManager.GetStartingPointX()*10, 18, gameManager.GetStartingPointY()* -10);
    }

    public void SetStartingEndingPoints(int sX, int sY, int eX, int eY)
    {
        startingPointX = sX;
        startingPointY = sY;
        endingPointX = eX;
        endingPointY = eY;
    }

    public static bool AddRoom(int x, int y, int rotation, int id)
    {
        if (!(x == startingPointX && y == startingPointY) && !(x == endingPointX && y == endingPointY))
        {
            roomCoord[x, y].rotation = rotation;
            roomCoord[x, y].id = id;
            Debug.Log("Added a Room at " + x + " " + y);
            return true;
        }
        else
        {
            Debug.Log("Didn't add the room at " + x + " " + y + " it is the player room!");
            return false;
        }
            
    }

    public static bool RemoveRoom(int x, int y)
    {
        if (!(x == startingPointX && y == startingPointY) && !(x == endingPointX && y == endingPointY))
        { 
            roomCoord[x, y].rotation = 0;
            roomCoord[x, y].id = -1;
            Debug.Log("Removed a Room at " + x + " " + y);
            return true;
        }
        else
        {
            Debug.Log("Didn't remove the room at " + x + " " + y + " it is the player room!");
            return false;
        }
            
    }

    public static bool isEmpty(int x, int y)
    {
        if (roomCoord[x, y].id == -1)
            return true;
        else
        {
            Debug.Log("not empty at " + x +" " + y + " id: " + roomCoord[x,y].id);
            return false;   
        }
    }

    public static int checkRoomId(int x, int y)
    {
        return roomCoord[x, y].id;
    }

    public static int checkRoomRotation(int x, int y)
    {
        return roomCoord[x, y].rotation;
    }

    public static void ClearRooms()
    {
        Debug.Log("Clearing all Editor rooms..");
        GameObject[] RoomsE = GameObject.FindGameObjectsWithTag("PlacedRoom");

        for (int i = 0; i < RoomsE.Length; i++)
        {
            Destroy(RoomsE[i]);
        }

        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if(isEmpty(x,y) == false)
                    RemoveRoom(x, y);
            }
        }
        Debug.Log("Clearing has ended succesfully..");
    }



    public void DestroyRealRooms()
    {
        Debug.Log("Destroying Rooms in the Level");
        while (Rooms.Count != 0)
        {
            Destroy(Rooms[0]);
            Rooms.RemoveAt(0);
        }
        Debug.Log("Destroying Coins in the Level");
        while (coins.Count != 0)
        {
            Destroy(coins[0]);
            coins.RemoveAt(0);
        }

    }

    public void ResetRooms(bool erase)
    {
        Debug.Log("Reseting the Rooms");
        if (generatedOnce == true)
        {
            DestroyRealRooms();
        }
        if (erase == true)
        {
            Debug.Log("Erase Checked true");
            ClearRooms();
        }
        gameManager.SetupPlayer();

    }
    
    public void GenerateRooms()
    {
        Debug.Log("Generating level from the Editor");
        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if(roomCoord[x,y].id != -1)
                {
                    GameObject r = Instantiate(prefabId[roomCoord[x, y].id], new Vector3(10 * x, 0, 10 * y), transform.rotation);
                    if (Random.value >= 0.5)
                    {
                        GameObject c = Instantiate(coin, new Vector3(10 * x, 1, 10 * y), transform.rotation);
                        coins.Add(c);
                    }
                    r.transform.Rotate(new Vector3(r.transform.rotation.x, -90 * roomCoord[x, y].rotation, r.transform.rotation.z));
                    Rooms.Add(r);
                    Debug.Log("Room generated at: " + x + " " + y);
                }
            }
        }
        generatedOnce = true;
    }


}
