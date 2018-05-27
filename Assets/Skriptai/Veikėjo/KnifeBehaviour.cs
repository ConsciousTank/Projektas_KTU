using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : MonoBehaviour
{
    public AudioClip hitSound;

    private GameObject owner;
    private AudioSource source;
    private bool stopRotating = false;

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();

    }

    void Update()
    {
        if (owner.GetComponent<Boomerang>().CheckIfState(Boomerang.knifeState.THROWING) && stopRotating == false )
            Rotate();
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }

    void Rotate()
    {
        gameObject.transform.Rotate(new Vector3(0, -480,0) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "Player" && collider.tag != "Knife" && collider.tag != "Coin" && owner.GetComponent<Boomerang>().CheckIfState(Boomerang.knifeState.THROWING))
        {
            StartCoroutine(HitTheWall(collider));
        }
    }

    private IEnumerator HitTheWall(Collider collider)
    {
        source.PlayOneShot(hitSound);
        stopRotating = true;
        gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (collider.transform.parent != null)
            transform.SetParent(collider.transform.parent, true);
        else
            transform.SetParent(collider.transform, true);
        yield return new WaitForSeconds(1);
        owner.GetComponent<Boomerang>().SetState(Boomerang.knifeState.INGROUND);
    }
}