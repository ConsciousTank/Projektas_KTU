using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedRoom : MonoBehaviour {

    private Room room;
    private SpriteRenderer sprd;

    void Awake()
    {
        sprd = GetComponent<SpriteRenderer>();
    }

    public void Setup(Room currentRoom)
    {
        room = currentRoom;
        sprd.sprite = currentRoom.icon;
    }

    void OnMouseOver()
    {
        if (CursorRoom.GetHolding() == false && Input.GetMouseButtonDown(1))
        {
            Generator.RemoveRoom(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            Destroy(gameObject);
        }

    }
}
