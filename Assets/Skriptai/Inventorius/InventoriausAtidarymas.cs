using UnityEngine;
using System.Collections.Generic;

public class InventoriausAtidarymas : MonoBehaviour
{
    public GameObject inventorius;
    public List<Daiktas> daiktai;
    public bool Ijungtas = false;

    private void Start()
    {
        inventorius.SetActive(Ijungtas);
    }

    private void Update()
    {
        BusenosKeitimas();
    }

    private void BusenosKeitimas()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Ijungtas = !Ijungtas;
            inventorius.SetActive(Ijungtas);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (Daiktas daiktas in daiktai)
            {
                inventorius.GetComponentInChildren<Inventorius>().Prideti(daiktas);
            }
        }

    }
}
