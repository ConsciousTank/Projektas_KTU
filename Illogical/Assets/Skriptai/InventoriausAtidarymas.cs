using UnityEngine;

public class InventoriausAtidarymas : MonoBehaviour
{
    public GameObject inventorius;
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
    }
}
