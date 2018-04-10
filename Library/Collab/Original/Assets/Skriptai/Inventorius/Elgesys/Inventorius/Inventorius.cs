using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventorius : MonoBehaviour
{
    public List<Daiktas> daiktai = new List<Daiktas>();

    public GameObject antanas;

    public void PridetiDaikta(Daiktas daiktasPridejimui)
    {
        daiktai.Add(daiktasPridejimui);
        
        
    }

    public void IsimtiDaikta(Daiktas daiktasIsemimui)
    {
       /* for (int i = 0; i < daiktai.Length; i++)
        {
            if (daiktai[i] == daiktasIsemimui)
            {
                daiktai[i] = null;
               daiktuPaveiksleliai[i].sprite = null;
               daiktuPaveiksleliai[i].enabled = false;
                return;
            }
        }*/
    }
}
