using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaiktuSarasasVeikejo : MonoBehaviour
{
    private List<Daiktas> daiktuSarasas;

    public void Start()
    {
        daiktuSarasas = new List<Daiktas>();
    }


    public int DaiktuKiekis()
    {
        return daiktuSarasas.Count;
    }

    public Daiktas this[int indeksas]
    {
        get
        {
            return daiktuSarasas[indeksas];
        }
        set
        {
            daiktuSarasas[indeksas] = value;
        }
    }

    public int PridetiDaikta(Daiktas daiktas)
    {
        if (daiktuSarasas.Contains(daiktas))
        {
            return 1;
        }
        else
        {
            daiktuSarasas.Add(daiktas);
            return 0;
        }

    }

    public void IsmestiDaikta(bool ismesti, Daiktas daiktas)
    {
        if (ismesti)
        {
            daiktuSarasas.Remove(daiktas);
        }
    }

    public List<string> RastiDaiktaPagalPavadinimaIrAprasyma(string pavadinimas, bool aprasymasIeskoti)
    {
        List<string> sarasoIeskomu = new List<string>();
        if (aprasymasIeskoti)
        {
            foreach (Daiktas daiktas in daiktuSarasas)
            {
                if (daiktas.pavadinimas.Contains(pavadinimas) || daiktas.aprasymas.Contains(pavadinimas))
                {
                    sarasoIeskomu.Add(daiktas.pavadinimas);
                }
            }
            return sarasoIeskomu;
        }
        else
        {
            foreach (Daiktas daiktas in daiktuSarasas)
            {
                if (pavadinimas.Length == 1)
                {
                    pavadinimas = pavadinimas.ToUpper();
                }
                if (daiktas.pavadinimas.Contains(pavadinimas))
                {
                    sarasoIeskomu.Add(daiktas.pavadinimas);
                }
            }
            return sarasoIeskomu;
        }
    }
}
