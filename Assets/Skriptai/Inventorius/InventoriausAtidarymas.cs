using UnityEngine;

public class InventoriausAtidarymas : MonoBehaviour
{
    public GameObject inventorius;
    public ScriptableObject daiktas;
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
            inventorius.GetComponentInChildren<Inventorius>(false).Prideti(daiktas);

        }
    }
}
