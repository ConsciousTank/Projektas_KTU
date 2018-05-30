using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {


    public GameMan gameManager;
    public float speed;
    public Rigidbody rb;
    public AudioClip coinSound;


    public float invulTime = 1f;
    private bool invulnerable = false;
    private AudioSource source;
    private bool firstPerson = true;
    private Vector3 movement;
    private Camera Cam;
    private int floorMask;
    private float camRayLength = 100f;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        source = gameObject.GetComponent<AudioSource>();
    }

    public void SetCam(Camera camera)
    {
        
        Cam = camera;
    }

    void Start () {
        floorMask = LayerMask.GetMask("Floor");
        Transform transform = gameObject.transform;
	}
	
    

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Finish")
        {
            gameManager.SwitchState(State.SCOREBOARD);
            gameManager.glfm.UpdateLevel();
            gameManager.AddToScore();
            gameManager.EnemyDefeatedRefresh();
        }
        if (other.gameObject.tag == "Coin")
        {
            gameManager.AddCurrentScore(1);
            source.PlayOneShot(coinSound);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Item")
        {
            Daiktas daiktas = other.gameObject.GetComponent<SaugojimasDaiktoInformacijos>().NuDuokInformacija();
            GameObject inventorius = GameObject.Find("Daiktai");
            inventorius.GetComponent<Inventorius>().Prideti(daiktas);
            source.PlayOneShot(coinSound);
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Damage")
        {
            gameManager.ApplyDamage(10);
        }
    }

	void FixedUpdate () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (firstPerson == false)
        {
            Mov3P(h, v);
            Turning();
        }
        else
        {
            transform.Translate(h * Time.deltaTime * speed, 0, v * Time.deltaTime * speed);
        }

    }

    void Mov3P(float h, float v)
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
