using UnityEngine;
using System;

[CreateAssetMenu,Serializable]
public class Daiktas : ScriptableObject
{
    public enum LaukelisPriklausimo
    {
        uzsidejimuiVeikejo, gaminimui
    };

    public GameObject modelis;
    public LaukelisPriklausimo laukelioNr;
    public Sprite daiktoPaveiksliukas;
    public string pavadinimas;
    public string aprasymas;

}
