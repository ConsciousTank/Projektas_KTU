using UnityEngine;

[CreateAssetMenu]
public class Daiktas : ScriptableObject
{
    public static int visoDaiktu;
    private Sprite daiktoPaveiksliukas;
    private string pavadinimas;
    private string aprasymas;
    private int laukelisPriklausimo;

    public Daiktas()
    {

    }

    public Daiktas(Sprite paveiksliukas, string pavadinimas, string aprasymas, int laukelis)
    {
        paveiksliukas = daiktoPaveiksliukas;
        this.pavadinimas = pavadinimas;
        this.aprasymas = aprasymas;
        laukelis = laukelisPriklausimo;
    }

    public string Pavadinimas
    {
        get
        {
            return pavadinimas;
        }
    }

    public string Aprasymas
    {
        get
        {
            return aprasymas;
        }
    }

    public int LaukelisPaskirties
    {
        get
        {
            return laukelisPriklausimo;
        }
    }

    public Sprite Paveiksliukas
    {
        get
        {
            return daiktoPaveiksliukas;
        }
    }
}
