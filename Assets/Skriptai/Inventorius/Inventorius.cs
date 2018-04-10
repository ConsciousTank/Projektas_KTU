using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Inventorius : MonoBehaviour
{
    public GameObject laukelis;
    public List<Daiktas> daiktai = new List<Daiktas>();
    public List<GameObject> eilutes = new List<GameObject>();

    public void Update()
    {
        if (daiktai.Count < eilutes.Count)
        {
            for (int i = eilutes.Count - 1; i >= daiktai.Count; i--)
            {
                DestroyImmediate(eilutes[i]); //Sunaikina editoriaus metu
                //Destroy(eilutes[i]); //tures buti zaidimo metu
                eilutes.RemoveAt(i);
            }
        }
        for (int i = eilutes.Count; i < daiktai.Capacity; i++)
        {
            GameObject objektas = Instantiate(laukelis, gameObject.transform);
            eilutes.Add(objektas);
        }
    }
}