using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SampleKambarys : MonoBehaviour {

    private ButtonManager butMan;
    private Room room;
    private GameObject cursRoom;
    private Button buttonComponent;

    void Awake()
    {
        buttonComponent = GetComponent<Button>();
    }

    void Start () {
        buttonComponent.onClick.AddListener(HandleClick);
    }

    public void Setup(Room currentRoom, ButtonManager buttonManager)
    {
        butMan = buttonManager;
        room = currentRoom;
        cursRoom = butMan.cursRoom;
    }

    public void HandleClick()
    {
        GameObject roomR = Instantiate(cursRoom);
        CursorRoom cr = roomR.GetComponent<CursorRoom>();
        cr.Setup(room, butMan);
        CursorRoom.ChangeCurrentRoom(roomR);
    }
}
