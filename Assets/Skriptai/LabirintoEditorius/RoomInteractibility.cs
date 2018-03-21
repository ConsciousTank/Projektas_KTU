using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInteractibility : MonoBehaviour {

    private float camRayLength = 100f;
    private int floorMask;
    private bool isFollowing;

	void Start () {
        isFollowing = true;
        floorMask = LayerMask.GetMask("EditorBackground");
        
    }

    void Update () {
        if (isFollowing)
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                transform.position = floorHit.point;
            }
            if (Input.GetMouseButtonDown(0))
            {
                isFollowing = false;
                SnapToGrid();
            }
                
            
        }

    }
    public void SnapToGrid()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    }    
}
