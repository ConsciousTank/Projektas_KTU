using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInteractibility : MonoBehaviour {

    private float camRayLength = 100f;
    private int floorMask;
    private bool isFollowing;
    public int rotation;
    public SpriteRenderer sr;

	void Start () {
        rotation = 0;
        sr = GetComponent<SpriteRenderer>();
        isFollowing = true;
        floorMask = LayerMask.GetMask("EditorBackground");
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
            if (Input.GetMouseButtonDown(0))
            {
                isFollowing = false;
                SnapToGrid();
            }
            if (Input.GetKeyDown("r"))
            {
                Rotate();
            }
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
