using System.Collections;
using UnityEngine;

public abstract class PaveluotaReakcija : Reakcija
{
    public float velinimas;


    protected WaitForSeconds laukti;


    public new void Init()
    {
        laukti = new WaitForSeconds(velinimas);

        SpecificInit();
    }


    public new void React(MonoBehaviour vienoElgesio)
    {
        vienoElgesio.StartCoroutine(ReactCoroutine());
    }


    protected IEnumerator ReactCoroutine()
    {
        yield return laukti;

        ImmediateReaction();
    }
}
