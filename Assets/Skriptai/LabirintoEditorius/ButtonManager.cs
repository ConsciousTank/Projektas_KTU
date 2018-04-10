using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class Room
{
    public string itemName;
    public Sprite icon;
    public int id = -1;
    public int rotation = 0;
}

public class ButtonManager : MonoBehaviour {

    public GameObject buttonPrefab;
    public GameObject cursRoom;
    public GameObject creaRoom;
    public List<Room> rooms = new List<Room>();
    public GameObject Panel;


	void Start () {
        Physics2D.queriesHitTriggers = true;
        AddRooms();
	}
	
    public void AddRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            Room room = rooms[i];
            GameObject newRoom = Instantiate(buttonPrefab);
            newRoom.transform.SetParent(Panel.transform);
            SampleKambarys kamba = newRoom.GetComponent<SampleKambarys>();
            kamba.Setup(room, this);
        }
    }


}
