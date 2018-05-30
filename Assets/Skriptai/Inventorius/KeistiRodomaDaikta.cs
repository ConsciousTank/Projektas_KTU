using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeistiRodomaDaikta : MonoBehaviour
{
    public GameObject naujas;
	// Use this for initialization
	void Start ()
    {
        Instantiate(naujas, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z + 3), transform.rotation, transform);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
