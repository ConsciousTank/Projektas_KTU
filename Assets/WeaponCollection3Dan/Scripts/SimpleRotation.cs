using UnityEngine;
using System.Collections;

public class SimpleRotation : MonoBehaviour
{
    public float Speed = 2f;
    public Transform obj;
    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        obj.Rotate(new Vector3(0,Speed,0));
    }
}
