using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessBehaviour : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Walls")
            Destroy(gameObject);
    }
}
