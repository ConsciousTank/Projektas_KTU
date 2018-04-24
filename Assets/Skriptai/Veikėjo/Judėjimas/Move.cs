using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    
    public float speed;
    public Rigidbody rb;
    Vector3 movement;
    Camera Cam;
    int floorMask;                     
    float camRayLength = 100f;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void SetCam(Camera camera)
    {
        Cam = camera;
    }

    void Start () {

        floorMask = LayerMask.GetMask("Floor");
        Transform transform = gameObject.transform;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Mov(h, v);
        Turning();

    }
    void Mov(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
    void Turning()
    {
        
        Ray camRay = Cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }
    }
}
