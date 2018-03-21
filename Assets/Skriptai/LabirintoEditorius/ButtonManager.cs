using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class Room
{
    public string itemName;
    public Sprite icon;
    public int id = 1;
    public GameObject room = null;
}

public class ButtonManager : MonoBehaviour {

    public GameObject prefab;
    public List<Room> rooms = new List<Room>();
    public List<GameObject> RoomsM = new List<GameObject>();
    public GameObject Panel;


	void Start () {
        AddRooms();
	}
	
    public void AddRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            Room room = rooms[i];
            GameObject newRoom = Instantiate(prefab);
            newRoom.transform.SetParent(Panel.transform);
            SampleKambarys kamba = newRoom.GetComponent<SampleKambarys>();
            kamba.Setup(room, this);
        }
    }


}
