using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Inventorius : MonoBehaviour
{
    public GameObject laukelis;
    public DaiktuSarasasVeikejo daiktai;
    public List<GameObject> eilutes = new List<GameObject>();

    public void Update()
    {
        if (daiktai.DaiktuKiekis() < eilutes.Count)
        {
            for (int i = eilutes.Count - 1; i >= daiktai.DaiktuKiekis(); i--)
            {
                DestroyImmediate(eilutes[i]); //Sunaikina editoriaus metu
                //Destroy(eilutes[i]); //tures buti zaidimo metu
                eilutes.RemoveAt(i);
            }
        }
    }

    public void Prideti(ScriptableObject daiktas)
    {
        PapildytiInventoriu(daiktas as Daiktas, daiktai.PridetiDaikta(daiktas as Daiktas));
    }

    private void PapildytiInventoriu(Daiktas daiktas, int skaicius)
    {
        if (skaicius == 0)
        {
            GameObject objektas = Instantiate(laukelis, gameObject.transform);
            objektas.AddComponent<RectMask2D>();
            Image[] paveiksliukas = objektas.GetComponentsInChildren<Image>();
            paveiksliukas[1].sprite = daiktas.daiktoPaveiksliukas;
            Text[] pavIrKiek = objektas.GetComponentsInChildren<Text>();
            pavIrKiek[0].text = daiktas.pavadinimas;
            pavIrKiek[1].enabled = false;
            eilutes.Add(objektas);
        }
        else
        {
            GameObject eilute = RastiDaiktoEilute(daiktas.pavadinimas);
            Text[] pavIrKiek = eilute.GetComponentsInChildren<Text>();
            pavIrKiek[1].text = string.Format("({0})", GautiSkaiciu(pavIrKiek[1].text) + 1);
            pavIrKiek[1].enabled = true;
        }
    }

    private int GautiSkaiciu(string skaicius)
    {
        skaicius = skaicius.Substring(skaicius.IndexOf('(') + 1, skaicius.IndexOf(')') - skaicius.IndexOf('(') - 1);
        return int.Parse(skaicius);
    }

    private GameObject RastiDaiktoEilute(string pavadinimas)
    {
        foreach (GameObject eilute in eilutes)
        {
            if (eilute.GetComponentInChildren<Text>().text.CompareTo(pavadinimas) == 0)
            {
                return eilute;
            }
        }
        return null;
    }
}