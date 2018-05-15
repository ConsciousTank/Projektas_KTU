using UnityEngine;
using System;

[CreateAssetMenu,Serializable]
public class Daiktas : ScriptableObject
{
    public enum LaukelisPriklausimo
    {
        uzsidejimuiVeikejo, gaminimui
    };

    public LaukelisPriklausimo laukelioNr;
    public Sprite daiktoPaveiksliukas;
    public string pavadinimas;
    public string aprasymas;
    public Rigidbody patavarumas;

}
