using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotuLaukas : MonoBehaviour
{
    private Rect forma;
    private Rect slotoVektoriai;
    GameObject[] zaidejoSlotai;
    Vector3[] koordinates;
    public GameMan zaidejoInfoCentras;
    int kiekis;

	void Start()
    {
        forma = gameObject.GetComponent<RectTransform>().rect;
        zaidejoSlotai = new GameObject[GameMan.playerMaxNumberOfSlots];
        koordinates = new Vector3[GameMan.playerMaxNumberOfSlots];
        for (int i = 0; i < 9; i++)
        {
            SukurtiSlota();
        }
	}
	

	void Update ()
    {
		
	}

    private void SukurtiSlota()
    {
        GameObject naujasSlotas = new GameObject(string.Format("Sloto Langelis {0}", kiekis));
        naujasSlotas.transform.SetParent(gameObject.transform);
        naujasSlotas.AddComponent<Image>().color = Random.ColorHSV();
        RectTransform formaSloto = naujasSlotas.GetComponent<RectTransform>();
        formaSloto.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (forma.width * 0.8f) * 0.16f);
        formaSloto.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (forma.width * 0.8f) * 0.16f);
        slotoVektoriai = formaSloto.rect;
        //Debug.Log(slotoVektoriai.width);
        //(forma.width * 0.8f) * 0.1f;
        //(forma.height * 0.8f) * 0.2f;
        zaidejoSlotai[kiekis] = naujasSlotas;
        naujasSlotas.transform.localPosition = RastiNaujasKoordinates();
       // zaidejoInfoCentras.setNumberOfSlots();
        

    }

    private Vector3 RastiNaujasKoordinates()
    {
        Vector3 koordinatesXY;
        if (kiekis != 0)
        {
            do
            {
                koordinatesXY = new Vector3(Random.Range(-forma.width / 2 + (forma.width * 0.8f) * 0.14f, forma.width / 2 - (forma.width * 0.8f) * 0.14f), Random.Range(-forma.height / 2 + (forma.height * 0.8f) * 0.14f, forma.height / 2 - (forma.height * 0.8f) * 0.14f));
            }
            while (!PatikrintiArTinkamos(koordinatesXY));
            koordinates[kiekis++] = koordinatesXY;
            return koordinatesXY;
        }
        else
        {
            koordinatesXY = new Vector3(Random.Range(-forma.width / 2 + (forma.width * 0.8f) * 0.14f, forma.width / 2 - (forma.width * 0.8f) * 0.14f), Random.Range(-forma.height / 2 + (forma.height * 0.8f) * 0.14f, forma.height / 2 - (forma.height * 0.8f) * 0.14f));
            koordinates[kiekis++] = koordinatesXY;
            return koordinatesXY;
        }
    }

    private bool PatikrintiArTinkamos(Vector3 tikrinami)
    {
        for(int i = 0;i < kiekis;i++)
        {
            Vector3 koordinate = koordinates[i];
            float puseSloto = slotoVektoriai.width / 2;
            float posekisX = puseSloto + forma.width * 0.02f;
            float posekisY = puseSloto + forma.height * 0.05f;
            if (koordinate.x + posekisX > tikrinami.x - puseSloto && koordinate.x - posekisX < tikrinami.x + puseSloto
                && koordinate.y + posekisY > tikrinami.y - puseSloto && koordinate.y - posekisY < tikrinami.y + puseSloto)
            {
                return false;
            }
        }
        return true;
    }
}
