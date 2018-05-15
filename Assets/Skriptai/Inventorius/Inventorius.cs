using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Inventorius : MonoBehaviour
{
    public GameObject laukelis;
    public DaiktuSarasasVeikejo daiktai;
    public List<GameObject> daiktuEilutes;

    private Toggle aprasymoTikrinimas;
    private InputField ieskomasDaiktas;
    private List<GameObject> pasleptosDaiktuEilutes;
    private bool pakeistasDaiktuSarasas;

    public void Start()
    {
        daiktuEilutes = new List<GameObject>();
        pasleptosDaiktuEilutes = new List<GameObject>();
        GameObject inventorius = GameObject.Find("Inventorius");
        aprasymoTikrinimas = inventorius.GetComponentInChildren<Toggle>();
        ieskomasDaiktas = inventorius.GetComponentInChildren<InputField>();
        pakeistasDaiktuSarasas = false;
    }

    public void Update()
    {
        PakitoReiksmePaieskosLaukelyje();
    }

    public void RodytiTikSlotoDaiktus()
    {
        if (pakeistasDaiktuSarasas)
        {
            PakeistiMatomumaDaiktu(daiktai.RastiVisusSlotoDaiktus(), false);
        }
        else
        {

        }
    }

    public void Prideti(Daiktas daiktas)
    {
        PapildytiInventoriu(daiktas, daiktai.PridetiDaikta(daiktas));
    }

    public void PakitoReiksmePaieskosLaukelyje()
    {
        if (ieskomasDaiktas.textComponent.text.Length == 0)
        {
            pakeistasDaiktuSarasas = false;
            AtkeistiNematomuma();
        }
        else
        {
            pakeistasDaiktuSarasas = true;
            PakeistiMatomumaDaiktu(daiktai.RastiDaiktaPagalPavadinimaIrAprasyma(ieskomasDaiktas.textComponent.text, aprasymoTikrinimas), false);
        }
    }

    private void AtkeistiNematomuma()
    {
        foreach (GameObject daiktas in pasleptosDaiktuEilutes)
        {
            daiktas.SetActive(true);
        }
        pasleptosDaiktuEilutes.Clear();
    }

    private void PakeistiMatomumaDaiktuPasleptuEiluciu(List<Daiktas> nekeiciamiDaiktai, bool matomumas)
    {
        if (nekeiciamiDaiktai.Count == 0)
        {
            return;
        }
        else
        {
            foreach (GameObject daiktoEilute in pasleptosDaiktuEilutes)
            {
                if (!PatikrinimasArYraNekeiciamas(nekeiciamiDaiktai, daiktoEilute.name))
                {
                    daiktoEilute.SetActive(matomumas);
                }
            }
        }
    }

    private void PakeistiMatomumaDaiktu(List<Daiktas> nekeiciamiDaiktai, bool matomumas)
    {
        if (nekeiciamiDaiktai.Count == 0)
        {
            pasleptosDaiktuEilutes = daiktuEilutes;
            for(int i = 0; i < daiktuEilutes.Count;i++)
            {
                daiktuEilutes[i].SetActive(matomumas);
            }
        }
        else
        {
            AtkeistiNematomuma();
            foreach (GameObject daiktoEilute in daiktuEilutes)
            {
                if (!PatikrinimasArYraNekeiciamas(nekeiciamiDaiktai, daiktoEilute.name))
                {
                    daiktoEilute.SetActive(matomumas);
                    pasleptosDaiktuEilutes.Add(daiktoEilute);
                }
            }
        }
    }

    private bool PatikrinimasArYraNekeiciamas(List<Daiktas> nekeiciamiDaiktai, string daiktas)
    {
        foreach (Daiktas nekeiciamasDaiktas in nekeiciamiDaiktai)
        {
            if (daiktas.CompareTo(nekeiciamasDaiktas.pavadinimas) == 0)
            {
                return true;
            }
        }
        return false;
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
            objektas.name = daiktas.pavadinimas;
            pavIrKiek[1].enabled = false;
            int vieta = RastiVieta(objektas.name);
            objektas.transform.SetSiblingIndex(vieta);
            Debug.Log(vieta + " " + daiktas.pavadinimas);
            daiktuEilutes.Insert(RastiVieta(objektas.name), objektas);
        }
        else
        {
            GameObject eilute = RastiDaiktoEilute(daiktas.pavadinimas);
            Text[] pavIrKiek = eilute.GetComponentsInChildren<Text>();
            pavIrKiek[1].text = string.Format("({0})", GautiSkaiciu(pavIrKiek[1].text) + 1);
            pavIrKiek[1].enabled = true;
        }
    }

    private int RastiVieta(string pavadinimas)
    {
        for (int i = 0; i < daiktuEilutes.Count; i++)
        {
            int palyginimas = Palyginimai.PalygtintiZodzius(daiktuEilutes[i].name,pavadinimas);
            if (palyginimas < 0)
            {
                return i;
            }
        }
        return daiktuEilutes.Count;
    }
    private int GautiSkaiciu(string skaicius)
    {
        skaicius = skaicius.Substring(skaicius.IndexOf('(') + 1, skaicius.IndexOf(')') - skaicius.IndexOf('(') - 1);
        return int.Parse(skaicius);
    }

    private GameObject RastiDaiktoEilute(string pavadinimas)
    {
        foreach (GameObject eilute in daiktuEilutes)
        {
            if (eilute.name.CompareTo(pavadinimas) == 0)
            {
                return eilute;
            }
        }
        return null;
    }

}