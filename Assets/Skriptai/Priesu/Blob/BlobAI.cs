using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobAI : MonoBehaviour {

    public GameObject Gib;
    Transform player;
    GameMan gameManager;
    float rotSpeed = 4f;
    float moveSpeed = 0.1f;

	// Use this for initialization
	void Awake () {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.Find("GameManager").GetComponent<GameMan>();
    }
	
    public void RefreshPlayer()
    {
        player = gameManager.GetCurrentPlayerTransform();
    }

	// Update is called once per frame
	void Update () {
        if (gameManager.GetState() == State.INGAME)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotSpeed * Time.deltaTime);
            transform.position += transform.forward * moveSpeed;
        }
	}

    void OnDestroy()
    {
            GameObject part = Instantiate(Gib, transform.position , Quaternion.identity );
    }
}
