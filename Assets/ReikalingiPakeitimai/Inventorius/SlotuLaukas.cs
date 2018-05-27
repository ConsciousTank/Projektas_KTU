using System.Collections.Generic;
using UnityEngine;

public class SlotuLaukas : MonoBehaviour
{
    public GameObject slotoLangelis;
    GameObject[] zaidejoSlotai;
    Vector3[] koordinates;
    public GameMan zaidejoInfoCentras;
    int kiekis;

	void Start()
    {
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
        GameObject naujasSlotas = Instantiate(slotoLangelis, transform);
        naujasSlotas.name = string.Format("Sloto Langelis {0}", kiekis);
        zaidejoSlotai[kiekis] = naujasSlotas;
        naujasSlotas.transform.localPosition = RastiNaujasKoordinates();
       // zaidejoInfoCentras.setNumberOfSlots();
        

    }

    private Vector3 RastiNaujasKoordinates()
    {
        Vector3 koordinatesXY;
        RectTransform forma = gameObject.GetComponent<RectTransform>();
        if (kiekis != 0)
        {
            do
            {
                koordinatesXY = new Vector3(Random.Range(-forma.rect.width / 2 + 50f, forma.rect.width / 2 - 50f), Random.Range(-forma.rect.height / 2 + 50f, forma.rect.height / 2 - 50f));
            }
            while (!PatikrintiArTinkamos(koordinatesXY));
            koordinates[kiekis++] = koordinatesXY;
            return koordinatesXY;
        }
        else
        {
            koordinatesXY = new Vector3(Random.Range(-forma.rect.width / 2 + 50f, forma.rect.width / 2 - 50f), Random.Range(-forma.rect.height / 2 + 50f, forma.rect.height / 2 - 50f));
            koordinates[kiekis++] = koordinatesXY;
            return koordinatesXY;
        }
    }

    private bool PatikrintiArTinkamos(Vector3 tikrinami)
    {
        for(int i = 0;i < kiekis;i++)
        {
            Vector3 koordinate = koordinates[i];
            if (koordinate.x + 60 > tikrinami.x - 40 && koordinate.x - 60 < tikrinami.x + 40 && koordinate.y + 60 > tikrinami.y - 40 && koordinate.y - 60 < tikrinami.y + 40)
            {
                return false;
            }
        }
        return true;
    }
}
