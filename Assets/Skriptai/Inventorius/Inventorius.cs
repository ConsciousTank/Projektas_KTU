using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Inventorius : MonoBehaviour
{
    public GameObject laukelis;
    public DaiktuSarasasVeikejo daiktai;
    public List<GameObject> daiktuEilutes;

    public GameMan infoCentras;
    private GameObject inventorius;
    private Toggle aprasymoTikrinimas;
    private InputField ieskomasDaiktas;
    private List<GameObject> pasleptosDaiktuEilutes;
    private List<GameObject> nePasleptosDaiktuEilutes;
    private bool ArVisiMatomiDaiktai;
    private bool tikSloto;

    public void Start()
    {
        tikSloto = false;
        inventorius = GameObject.Find("Inventorius");
        daiktuEilutes = new List<GameObject>();
        pasleptosDaiktuEilutes = new List<GameObject>();
        nePasleptosDaiktuEilutes = new List<GameObject>();
        aprasymoTikrinimas = inventorius.GetComponentInChildren<Toggle>();
        ieskomasDaiktas = inventorius.GetComponentInChildren<InputField>();
        ArVisiMatomiDaiktai = true;
    }

    public void Update()
    {
        aprasymoTikrinimas = inventorius.GetComponentInChildren<Toggle>();
    }

    public void RodytiTikSlotoDaiktus()
    {
        if (tikSloto)
        {
            tikSloto = false;
            List<GameObject> trumpalaikis = new List<GameObject>();
            foreach (GameObject daiktas in pasleptosDaiktuEilutes)
            {
                if (daiktas.GetComponent<SaugojimasDaiktoInformacijos>().daiktoInformacija.laukelioNr == Daiktas.LaukelisPriklausimo.gaminimui)
                {
                    daiktas.SetActive(true);
                    trumpalaikis.Add(daiktas);
                    nePasleptosDaiktuEilutes.Add(daiktas);
                }
            }
            foreach (GameObject daiktas in trumpalaikis)
            {
                pasleptosDaiktuEilutes.Remove(daiktas);
            }
            if (pasleptosDaiktuEilutes.Count == 0)
            {
                ArVisiMatomiDaiktai = true;
                nePasleptosDaiktuEilutes.Clear();
            }
            PakitoReiksmePaieskosLaukelyje();
        }
        else
        {
            if (ArVisiMatomiDaiktai)
            {
                ArVisiMatomiDaiktai = false;
                foreach (GameObject daiktas in daiktuEilutes)
                {
                    if (daiktas.GetComponent<SaugojimasDaiktoInformacijos>().daiktoInformacija.laukelioNr == Daiktas.LaukelisPriklausimo.gaminimui)
                    {
                        pasleptosDaiktuEilutes.Add(daiktas);
                        daiktas.SetActive(false);
                    }
                }
                tikSloto = true;
            }
            else
            {
                List<GameObject> trumpalaikis = new List<GameObject>();
                foreach (GameObject daiktas in nePasleptosDaiktuEilutes)
                {
                    if (daiktas.GetComponent<SaugojimasDaiktoInformacijos>().daiktoInformacija.laukelioNr == Daiktas.LaukelisPriklausimo.gaminimui)
                    {
                        pasleptosDaiktuEilutes.Add(daiktas);
                        trumpalaikis.Add(daiktas);
                        daiktas.SetActive(false);
                    }
                }
                foreach (GameObject daiktas in trumpalaikis)
                {
                    nePasleptosDaiktuEilutes.Remove(daiktas);
                }
                tikSloto = true;
            }
        }
    }

    public void Prideti(Daiktas daiktas)
    {
        PapildytiInventoriu(daiktas, daiktai.PridetiDaikta(daiktas));
    }

    public void SunaikintiDaikta(Daiktas daiktas)
    {
        daiktai.IsmestiDaikta(IstrintiDaiktaIsInventoriaus(daiktas), daiktas);
    }

    public void PakitoReiksmePaieskosLaukelyje()
    {
        Debug.Log("Rasoma i lauka");
        if (ieskomasDaiktas.textComponent.text.Length == 0)
        {
            if (!ArVisiMatomiDaiktai)
            {
                ArVisiMatomiDaiktai = true;
                VisiMatomi();
            }
        }
        else
        {
            PasleptiKeiciamusDaiktus(daiktai.RastiDaiktaPagalPavadinimaIrAprasyma(ieskomasDaiktas.textComponent.text, aprasymoTikrinimas.isOn));
            if (tikSloto)
            {
                tikSloto = false;
                RodytiTikSlotoDaiktus();
            }
        }
    }

    private void VisiMatomi()
    {
        foreach (GameObject daiktas in pasleptosDaiktuEilutes)
        {
            daiktas.SetActive(true);
        }
        pasleptosDaiktuEilutes.Clear();
    }

    private void VisiNematomi()
    {
        if (ArVisiMatomiDaiktai)
        {
            foreach (GameObject daiktas in daiktuEilutes)
            {
                daiktas.SetActive(false);
                pasleptosDaiktuEilutes.Add(daiktas);
            }
            ArVisiMatomiDaiktai = false;
        }
        else
        {
            foreach (GameObject daiktas in nePasleptosDaiktuEilutes)
            {
                pasleptosDaiktuEilutes.Add(daiktas);
                daiktas.SetActive(false);
            }
            nePasleptosDaiktuEilutes.Clear();
            ArVisiMatomiDaiktai = false;
        }
    }


    private void PasleptiKeiciamusDaiktus(List<string> nekeiciamiDaiktai)
    {
        if (nekeiciamiDaiktai.Count == 0)
        {
            VisiNematomi();
        }
        else
        {
            ArVisiMatomiDaiktai = false;
            for (int i = 0; i < daiktuEilutes.Count; i++)
            {
                GameObject objektas = daiktuEilutes[i];
                if (nekeiciamiDaiktai.Contains(objektas.name))
                {
                    if (pasleptosDaiktuEilutes.Contains(objektas))
                    {
                        GameObject rastas = pasleptosDaiktuEilutes.Find(a => Palyginimai.PalygtintiZodzius(a.name, objektas.name) == 0);
                        rastas.SetActive(true);
                        pasleptosDaiktuEilutes.Remove(rastas);
                    }
                    nePasleptosDaiktuEilutes.Add(objektas);
                }
                else
                {
                    if (!pasleptosDaiktuEilutes.Contains(objektas))
                    {
                        pasleptosDaiktuEilutes.Add(objektas);
                        objektas.SetActive(false);
                    }
                }
            }
        }
    }

    private bool IstrintiDaiktaIsInventoriaus(Daiktas daiktas)
    {
        GameObject eilute = RastiDaiktoEilute(daiktas.pavadinimas);
        Text[] pavIrKiek = eilute.GetComponentsInChildren<Text>();
        int kiekis = GautiSkaiciu(pavIrKiek[1].text) - 1;
        if (kiekis == 0)
        {
            GameObject rasti = GameObject.Find("Daiktas");
            daiktuEilutes.Remove(eilute);
            Destroy(eilute);
            if (rasti != null && rasti.gameObject.GetComponent<SaugojimasDaiktoInformacijos>().daiktoInformacija.pavadinimas.CompareTo(daiktas.pavadinimas) == 0)
            {
                Destroy(rasti);
            }
            IsmestiDaiktoModeli(daiktas);
            return true;
        }
        else
        {
            pavIrKiek[1].text = string.Format("({0})", kiekis);
            if (kiekis == 1)
            {
                pavIrKiek[1].enabled = false;
            }
            IsmestiDaiktoModeli(daiktas);
            return false;
        }
    }

    private void IsmestiDaiktoModeli(Daiktas daiktas)
    {
        GameObject ismetamasDaiktas = Instantiate(daiktas.modelis, infoCentras.GetPlayerFront()*2 + infoCentras.GetPlayerCoordinates(), new Quaternion());
        ismetamasDaiktas.name = daiktas.pavadinimas;
    }

    private void PapildytiInventoriu(Daiktas daiktas, int skaicius)
    {
        if (skaicius == 0)
        {
            GameObject objektas = Instantiate(laukelis, gameObject.transform);
            objektas.AddComponent<SaugojimasDaiktoInformacijos>().daiktoInformacija = daiktas;
            objektas.AddComponent<DaiktoIrPelesReakcijos>();
            objektas.AddComponent<RectMask2D>();
            Image[] paveiksliukas = objektas.GetComponentsInChildren<Image>();
            paveiksliukas[1].sprite = daiktas.daiktoPaveiksliukas;
            Text[] pavIrKiek = objektas.GetComponentsInChildren<Text>();
            pavIrKiek[0].text = daiktas.pavadinimas;
            objektas.name = daiktas.pavadinimas;
            pavIrKiek[1].enabled = false;
            int vieta = RastiVieta(objektas.name);
            objektas.transform.SetSiblingIndex(vieta);
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