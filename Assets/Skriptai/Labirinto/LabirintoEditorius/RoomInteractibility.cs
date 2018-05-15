using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInteractibility : MonoBehaviour {

    public static bool holding = false;
    private float camRayLength = 100f;
    private int floorMask;
    private int roomMask;
    private int spaceMask;
    public bool isFollowing;
    public int rotation;
    public int id;
    public SpriteRenderer sr;
    public GameObject ownRoom;

	void Start () {
        holding = true;
        rotation = 0;
        sr = GetComponent<SpriteRenderer>();
        isFollowing = true;
        floorMask = LayerMask.GetMask("EditorBackground");
        roomMask = LayerMask.GetMask("Room");
        spaceMask = LayerMask.GetMask("PlaceSpace");
    }

    void Update () {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (isFollowing)
        {
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                transform.position = floorHit.point;
            }
            if (Input.GetMouseButtonDown(0) && Generator.isEmpty(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)) && Physics.Raycast(camRay, camRayLength, spaceMask))
            {
                gameObject.tag = "PlacedRoom";
                GameObject gm = Instantiate(ownRoom);
                Generator.AddRoom(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), rotation, id);
                gm.GetComponent<RoomInteractibility>().isFollowing = false;
                gm.GetComponent<RoomInteractibility>().SnapToGrid();
            }
            if (Input.GetKeyDown("r"))
            {
                Rotate();
            }
        }   
    }

    void OnMouseOver()
    {
        if (!isFollowing && Input.GetMouseButtonDown(1))
        {
            Generator.RemoveRoom(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            Destroy(gameObject);
        }
            
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
