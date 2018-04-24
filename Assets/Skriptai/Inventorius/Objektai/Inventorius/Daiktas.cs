using UnityEngine;
using System;

[CreateAssetMenu,Serializable]
public class Daiktas : ScriptableObject
{
    public Sprite daiktoPaveiksliukas;
    public string pavadinimas;
    public string aprasymas;
    public int laukelisPriklausimo;
    public Rigidbody patavarumas;
}
