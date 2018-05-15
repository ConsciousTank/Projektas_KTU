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
    public GameObject[] RoomsE;
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
        RoomsE = new GameObject[50];
        generatedOnce = false;
        DontDestroyOnLoad(gameObject);
        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                roomCoord[x, y] = new RoomData(-1, 0);
            }
        }
        AddPlayerRoom();
    }

    public void AddPlayerRoom()
    {
        Destroy(currentStartRoomE);
        Destroy(currentStartRoom);
        Destroy(currentFinishRoom);
        Destroy(currentFinishRoomE);
        startingPointX = gameManager.GetStartingPointX();
        startingPointY = gameManager.GetStartingPointY();
        endingPointX = gameManager.GetEndingPointX();
        endingPointY = gameManager.GetEndingPointY();
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

    public static void AddRoom(int x, int y, int rotation, int id)
    {
        roomCoord[x, y].rotation = rotation;
        roomCoord[x, y].id = id;
    }

    public static void RemoveRoom(int x, int y)
    {
        if (!(x == 9 && y == 1))
        {
            roomCoord[x, y].rotation = 0;
            roomCoord[x, y].id = -1;
        }    
    }

    public static bool isEmpty(int x, int y)
    {
        if (roomCoord[x, y].id == -1 && !(x == startingPointX && y == startingPointY) && !(x == endingPointX && y == endingPointY))
            return true;
        else
        {
            Debug.Log("not empty");
            return false;   
        }
    }

    public void ClearRooms()
    {
        RoomsE = GameObject.FindGameObjectsWithTag("PlacedRoom");

        for (int i = 0; i < RoomsE.Length; i++)
        {
            Destroy(RoomsE[i]);
        }

        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if (roomCoord[x, y].id != -1 && !(x == startingPointX && y == startingPointY) && !(x == endingPointX && y == endingPointY))
                {
                    RemoveRoom(x, y);
                }
            }
        }
    }



    public void DestroyRealRooms()
    {
        while (Rooms.Count != 0)
        {
            Destroy(Rooms[0]);
            Rooms.RemoveAt(0);
        }
        while (coins.Count != 0)
        {
            Destroy(coins[0]);
            coins.RemoveAt(0);
        }

    }

    public void ResetRooms(bool erase)
    {
        
        if (generatedOnce == true)
        {
            DestroyRealRooms();
        }
        if (erase == true)
        ClearRooms();
        gameManager.SetupPlayer();

    }
    
    //public void CheckForDarkness(Room room, int x, int y)
    //{
    //    switch (room.id)
    //    {
    //        case 0:
    //            switch (room.rotation)
    //            {
    //                case 0:
    //                    if(roomCoord[x+1,y].id == -1)
    //                    {
    //                        GameObject d = Instantiate(darkDoor, new Vector3(10 * x, 3, 10 * y), transform.rotation);
    //                        d.transform.Rotate(new Vector3(d.transform.rotation.x, -90 * roomCoord[x, y].rotation, d.transform.rotation.z));
    //                    }
    //                    break;
    //                case 1:
    //                    break;
    //                case 2:
    //                    break;
    //                case 3:
    //                    break;
    //            }
    //            break;
    //        case 1:
    //            switch (room.rotation)
    //            {
    //                case 0:
    //                    break;
    //                case 1:
    //                    break;
    //                case 2:
    //                    break;
    //                case 3:
    //                    break;
    //            }
    //            break;
    //        case 2:
    //            switch (room.rotation)
    //            {
    //                case 0:
    //                    break;
    //                case 1:
    //                    break;
    //                case 2:
    //                    break;
    //                case 3:
    //                    break;
    //            }
    //            break;
    //        case 3:
    //            switch (room.rotation)
    //            {
    //                case 0:
    //                    break;
    //                case 1:
    //                    break;
    //                case 2:
    //                    break;
    //                case 3:
    //                    break;
    //            }
    //            break;
    //    }
    //}


    public void GenerateRooms()
    {
        //CheckForDarkness();
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
                }
            }
        }
        generatedOnce = true;
    }


}
