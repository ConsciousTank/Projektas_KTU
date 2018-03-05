using UnityEngine;

public abstract class Reakcija : ScriptableObject
{
    public void Init()
    {
        SpecificInit();
    }


    protected virtual void SpecificInit()
    { }


    public void React(MonoBehaviour vienoElgesio)
    {
        ImmediateReaction();
    }


    protected abstract void ImmediateReaction();
}

