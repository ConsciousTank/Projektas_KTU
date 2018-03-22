using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SampleKambarys : MonoBehaviour {
    public Button buttonComponent;
    public string nameLabel;
    public Sprite iconImage;
    public int id;
    public Image im;
    public GameObject roomM;


    private Room room;
    private ButtonManager BuMa;

    // Use this for initialization
    void Start () {
        buttonComponent.onClick.AddListener(HandleClick);
        im = GetComponent<Image>();
    }

    public void Setup(Room currentRoom, ButtonManager BuMa)
    {
        room = currentRoom;
        nameLabel = room.itemName;
        iconImage = room.icon;
        id = room.id;
        roomM = currentRoom.room;
        im.sprite = iconImage;
        this.BuMa = BuMa;
    }

    public void HandleClick()
    {
        GameObject roomR = Instantiate(roomM);
        RoomInteractibility id = roomR.GetComponent<RoomInteractibility>();
        id.id = this.id;
    }
}
