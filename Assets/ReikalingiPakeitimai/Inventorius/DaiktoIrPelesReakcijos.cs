using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DaiktoIrPelesReakcijos : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData ivykiai)
    {
        Debug.Log("bas");
        if (!ivykiai.IsPointerMoving() && ivykiai.clickTime >= 5)
        {
            Debug.Log("labas");
        }
    }

    public void OnPointerExit(PointerEventData ivykiai)
    {
    }

    public void OnPointerClick(PointerEventData ivykiai)
    {
        Daiktas saugomas = gameObject.GetComponent<SaugojimasDaiktoInformacijos>().daiktoInformacija;
        if (ivykiai.button == PointerEventData.InputButton.Left)
        {
            GameObject daiktas;
            GameObject rasti = GameObject.Find("Daiktas");
            GameObject kamera = GameObject.Find("DaiktoVaizdoKamera");
            if (rasti == null)
            {
                daiktas = Instantiate(saugomas.modelis, new Vector3(), new Quaternion(), kamera.transform);
                daiktas.name = "Daiktas";
                daiktas.transform.localPosition = new Vector3(0, 0, 3);
            }
            else
            {
                Destroy(rasti);
                daiktas = Instantiate(saugomas.modelis, new Vector3(), new Quaternion(), kamera.transform);
                daiktas.name = "Daiktas";
                daiktas.transform.localPosition = new Vector3(0, 0, 3);
            }
        }
        else if (ivykiai.button == PointerEventData.InputButton.Right)
        {
            GameObject pasirinkimuLaukas = Instantiate(new GameObject("DaiktoPasirinkimai", typeof(Dropdown)), Input.mousePosition, new Quaternion());
            Dropdown laukelisGalimuKomandu = pasirinkimuLaukas.GetComponent<Dropdown>();
            //laukelisGalimuKomandu.options = new System.Collections.Generic.List<Dropdown.OptionData>();
            laukelisGalimuKomandu.options.Add( new Dropdown.OptionData(saugomas.laukelioNr.ToString()));

        }
    }
}
