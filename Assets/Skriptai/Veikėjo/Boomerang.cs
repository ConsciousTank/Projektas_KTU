using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boomerang : MonoBehaviour
{
    public enum knifeState
    {
        INHAND,
        THROWING,
        INGROUND,
        BACKING
    }
    public GameObject knife;
    public AudioClip throwSound;

    private GameObject currentKnife;
    private GameObject follow;
    private knifeState state = knifeState.INHAND;
    private AudioSource source;

    public bool CheckIfState(knifeState state)
    {
        return this.state == state;
    }

    public void SetState(knifeState state)
    {
        this.state = state;
    }

    void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && state == knifeState.INHAND)
        {
            Throw();
            GameObject.Find("Cursor").GetComponent<Image>().color = Color.red;
        }
        else
        if (Input.GetMouseButtonDown(0) && state == knifeState.INGROUND)
        {
            Destroy(currentKnife);
            state = knifeState.INHAND;
        }
    }


    void Throw()
    {
        source.PlayOneShot(throwSound);
        state = knifeState.THROWING;
        currentKnife = Instantiate(knife, transform.position + Camera.main.transform.forward + new Vector3(0,1f), Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 90, 270)));
        currentKnife.GetComponent<KnifeBehaviour>().SetOwner(this.gameObject);
        currentKnife.GetComponent<Rigidbody>().velocity = 20 * Camera.main.transform.forward;
    }


}
