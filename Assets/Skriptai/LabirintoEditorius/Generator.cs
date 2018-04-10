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
    public Camera cam2;
    public Canvas editorOverlay;
    static RoomData[,] roomCoord= new RoomData[19,11];
    public List<GameObject> prefabId;
    bool generatedOnce;
    public List<GameObject> availableRooms;
    public GameObject[] placedRooms;

    public void Start()
    {
        editorOverlay.enabled = true;
        cam1.enabled = true;
        cam2.enabled = false;
        placedRooms = new GameObject[50];
        generatedOnce = false;
        DontDestroyOnLoad(gameObject);
        for (int x = 0; x < 19; x++)
        {
            for (int y = 0; y < 11; y++)
            {
                roomCoord[x, y] = new RoomData(-1, 0);
            }
        }
    }

    public static void AddRoom(int x, int y, int rotation, int id)
    {
        roomCoord[x, y].rotation = rotation;
        roomCoord[x, y].id = id;
    }

    public static void RemoveRoom(int x, int y)
    {
        roomCoord[x, y].rotation = 0;
        roomCoord[x, y].id = -1;
    }

    public static bool isEmpty(int x, int y)
    {
        if (roomCoord[x, y].id == -1)
            return true;
        else
        {
            Debug.Log("empty");
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
        for (int x = 0; x < 19; x++)
        {
            for (int y = 0; y < 11; y++)
            {
                if (roomCoord[x, y].id != -1)
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
    public void GenerateRoom()
    {
        if(generatedOnce == true)
        {
            DestroyRooms();
        }
        for (int x = 0; x < 19; x++)
        {
            for (int y = 0; y < 11; y++)
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
        ClearRooms();
        ChangeEditor();
    }

    public void ChangeEditor()
    {
        cam1.enabled = !cam1.enabled;
        editorOverlay.enabled = !editorOverlay.enabled;
        cam2.enabled = !cam2.enabled;
    }
}
