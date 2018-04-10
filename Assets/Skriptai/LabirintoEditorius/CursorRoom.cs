using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorRoom : MonoBehaviour {

    private static bool holding = false;
    private static GameObject currentRoom;
    private ButtonManager butMan;
    private int id;
    private int rotation;
    private GameObject placeRoom;
    private Room room;
    private SpriteRenderer sprd;
    private float camRayLength = 100f;
    private int spaceMask;
    private int floorMask;
    
    void Awake()
    {
        sprd = GetComponent<SpriteRenderer>();
    }

    void Start ()
    {
        holding = true;
        Physics2D.queriesHitTriggers = true;
        floorMask = LayerMask.GetMask("EditorBackground");
        spaceMask = LayerMask.GetMask("PlaceSpace");
    }
	
	void Update ()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            transform.position = floorHit.point;
            SnapToGrid();
        }
        if (Input.GetMouseButtonDown(0) && Generator.isEmpty(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)) && Physics.Raycast(camRay, camRayLength, spaceMask))
        {
            GameObject gm = Instantiate(placeRoom,transform.position,transform.rotation);
          //Neleido dirbti su kitais scriptais,kol yra error  gm.GetComponent<PlacedRoom>().Setup(room);
            Generator.AddRoom(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), rotation, id);
        }
        if (Input.GetKeyDown("r"))
        {
            Rotate();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
            holding = false;
        }  
    }

    public static void SetCurrentRoom(GameObject room)
    {
        currentRoom = room;
    }

    public static void ChangeCurrentRoom(GameObject room)
    {
        if(currentRoom != null)
        Destroy(currentRoom);
        currentRoom = room;
    }

    public static bool GetHolding()
    {
        return holding;
    }

    public void Setup(Room currentRoom, ButtonManager buttonManager)
    {
        butMan = buttonManager;
        placeRoom = butMan.creaRoom;
        room = currentRoom;
        sprd.sprite = room.icon;
        id = room.id;
        rotation = room.rotation;
    }

    public void SnapToGrid()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    }

    void Rotate()
    {
        switch (rotation)
        {
            case 0:
                transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, 90));
                rotation++;
                break;
            case 1:
                transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, 90));
                rotation++;
                break;
            case 2:
                transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, 90));
                rotation++;
                break;
            case 3:
                transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, 90));
                rotation = 0;
                break;
        }

    }
}
