using UnityEngine;

public class PaimimasDaikto : PaveluotaReakcija
{
    public Daiktas daiktas;

    private Inventorius inventorius;

    protected override void SpecificInit()
    {
        inventorius = FindObjectOfType<Inventorius>();
    }

    protected override void ImmediateReaction()
    {
        inventorius.PridetiDaikta(daiktas);
    }
}
