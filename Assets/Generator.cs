using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    
    public static RoomData[,] roomCoord= new RoomData[19,11];
    public List<GameObject> prefabId;

    public void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
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
    public void GenerateRoom()
    {
        for (int x = 0; x < 19; x++)
        {
            for (int y = 0; y < 11; y++)
            {
                if(roomCoord[x,y].id != -1)
                {
                    GameObject r = Instantiate(prefabId[roomCoord[x, y].id], new Vector3(10 * x, 0, 10 * y), transform.rotation);
                    r.transform.Rotate(new Vector3(r.transform.rotation.x, -90 * roomCoord[x, y].rotation, r.transform.rotation.z));
                }
            }
        }
    }

}
