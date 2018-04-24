using System.Collections.Generic;
using UnityEngine;

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

    public void IsmestiDaikta(Daiktas daiktas)
    {
        daiktuSarasas.Remove(daiktas);
    }

    public List<Daiktas> RastiDaikta(string pavadinimas, bool aprasymasIeskoti)
    {
        List<Daiktas> sarasoIeskomu = new List<Daiktas>();
        if (aprasymasIeskoti)
        {
            foreach (Daiktas daiktas in daiktuSarasas)
            {
                if (daiktas.pavadinimas.Contains(pavadinimas) || daiktas.aprasymas.Contains(pavadinimas) && aprasymasIeskoti)
                {
                    sarasoIeskomu.Add(daiktas);
                }
            }
            return sarasoIeskomu;
        }
        else
        {
            foreach (Daiktas daiktas in daiktuSarasas)
            {
                if (daiktas.pavadinimas.Contains(pavadinimas))
                {
                    sarasoIeskomu.Add(daiktas);
                }
            }
        }
        return sarasoIeskomu;
    }
}
