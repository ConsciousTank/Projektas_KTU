using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaimtIsGamemano : MonoBehaviour
{

 //   private GameMan gameManager;

	//void Start ()
 //   {
 //       gameManager = GameObject.Find("GameManager").GetComponent<GameMan>();
		
	//}
	
    public void UpdateAtt(int[] duom)
    {
        transform.GetChild(0).gameObject.GetComponent<Text>().text = "Strength: " + duom[0];
        transform.GetChild(1).gameObject.GetComponent<Text>().text = "Agility: " + duom[1];
        transform.GetChild(2).gameObject.GetComponent<Text>().text = "Intelektas: " + duom[2];
        transform.GetChild(3).gameObject.GetComponent<Text>().text = "";
        transform.GetChild(4).gameObject.GetComponent<Text>().text = "Score: " + duom[5];
        transform.GetChild(5).gameObject.GetComponent<Text>().text = "Level: 1";
        transform.GetChild(6).GetComponent<Slider>().value = duom[6];
    }
}
