using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//REIKIA PABAIGTI IR KLAUSIASM, AR REIKALINGAS ?
public class Medis<tipas> : ScriptableObject, IEnumerator where tipas: IComparable
{
    private sealed class Nuoroda<tipas1> where tipas1: IComparable
    {
        public tipas1 duomenis;
        public int kiekis = 1;
        public Nuoroda<tipas1> kaire;
        public Nuoroda<tipas1> desine;
        
        public Nuoroda()
        {
        }

        public Nuoroda(tipas1 duomenis)
        {
            this.duomenis = duomenis;
            kaire = null;
            desine = null;
        }

        public Nuoroda(tipas1 duomenis, Nuoroda<tipas1> kaire, Nuoroda<tipas1> desine)
        {
            this.duomenis = duomenis;
            this.kaire = kaire;
            this.desine = desine;
        }
    }

    private Nuoroda<tipas> saknis;
    private Nuoroda<tipas> kaire;
    private Nuoroda<tipas> desine;
    private Comparer<tipas> kriterijus;

    public Medis()
    {
        saknis = null;
        kaire = null;
        desine = null;
        kriterijus = Comparer<tipas>.Default;
    }

    public Medis(Comparer<tipas> kriterijus)
    {
        saknis = null;
        kaire = null;
        desine = null;
        this.kriterijus = kriterijus;
    }

    public Medis(tipas[] duomenys)
    {
        saknis = null;
        kaire = null;
        desine = null;
        foreach (tipas duomenis in duomenys)
        {
            Prideti(duomenis);
        }
    }

    public Medis(Comparer<tipas> kriterijus, tipas[] duomenys)
    {
        saknis = null;
        kaire = null;
        desine = null;
        this.kriterijus = kriterijus;
        foreach (tipas duomenis in duomenys)
        {
            Prideti(duomenis);
        }
    }

    public void Prideti(tipas duomenis)
    {
        if (duomenis == null)
        {
            throw new Exception("Paduotas duomenis buvo null metode Prideti(tipas duomenis)");
        }

        saknis = RekursiskasPridejimas(saknis, duomenis);
    }

    private Nuoroda<tipas> RekursiskasPridejimas(Nuoroda<tipas> virsune, tipas duomenis)
    {
        if (virsune == null)
        {
            return virsune = new Nuoroda<tipas>(duomenis);
        }
        int palyginimas = kriterijus.Compare(virsune.duomenis, duomenis);

        if (palyginimas == 0)
        {
            virsune.kiekis++;
            return virsune;
        }
        else if (palyginimas > 0)
        {
            return RekursiskasPridejimas(virsune.kaire, duomenis);
        }
        else
        {
            return RekursiskasPridejimas(virsune.desine, duomenis);
        }
    }

    public bool Rasti()
    {
        return true;
    }

    public bool MoveNext()
    {
        return true;
    }

    public void Reset()
    {
    }

    public object Current
    {
        get
        {
            return saknis.duomenis;
        }
    }



}