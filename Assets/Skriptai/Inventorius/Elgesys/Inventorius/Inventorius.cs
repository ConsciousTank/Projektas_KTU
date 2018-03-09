using UnityEngine;
using UnityEngine.UI;

public class Inventorius : MonoBehaviour
{
    public Image[] daiktuPaveiksleliai = new Image[daiktuKiekis];
    public Daiktas[] daiktai = new Daiktas[daiktuKiekis];
    public const int daiktuKiekis = 4;

    public void PridetiDaikta(Daiktas daiktasPridejimui)
    {
        for (int i = 0; i < daiktai.Length; i++)
        {
            if (daiktai[i] == null)
            {
                daiktai[i] = daiktasPridejimui;
                daiktuPaveiksleliai[i].sprite = daiktasPridejimui.daiktoPaveiksliukas;
                daiktuPaveiksleliai[i].enabled = true;
                return;
            }
        }
    }

    public void IsimtiDaikta(Daiktas daiktasIsemimui)
    {
        for (int i = 0; i < daiktai.Length; i++)
        {
            if (daiktai[i] == daiktasIsemimui)
            {
                daiktai[i] = null;
                daiktuPaveiksleliai[i].sprite = null;
                daiktuPaveiksleliai[i].enabled = false;
                return;
            }
        }
    }
}
