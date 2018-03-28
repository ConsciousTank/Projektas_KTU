using UnityEngine;
using UnityEditor;
using System;

public class Medis<tipas> : ScriptableObject where tipas: IComparable
{
    private sealed class Nuoroda<tipas> where tipas: IComparable
    {
        public tipas duomenis;
        public Nuoroda<tipas> kaire;
        public Nuoroda<tipas> desine;
        
        public Nuoroda()
        {
        }

        public Nuoroda(tipas duomenis)
        {
            this.duomenis = duomenis;
            kaire = null;
            desine = null;
        }

        public Nuoroda(tipas duomenis, Nuoroda<tipas> kaire, Nuoroda<tipas> desine)
        {
            this.duomenis = duomenis;
            this.kaire = kaire;
            this.desine = desine;
        }
    }

    private Nuoroda<tipas> virsune;
    private Nuoroda<tipas> kaire;
    private Nuoroda<tipas> desine;

    public Medis()
    {
        virsune = null;
        kaire = null;
        desine = null;
    }

    public void Prideti(tipas duomenis)
    {

    }
}