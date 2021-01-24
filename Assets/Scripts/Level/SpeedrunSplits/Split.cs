using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrogScripts.UI;

public class Split : MonoBehaviour
{
    [SerializeField] public string Name;
    [SerializeField] ParticleSystem newPBParticles;
    [SerializeField] SplitManager splitManager;

    List<SplitUI> splitUIs;

    public void AddSplitUI(SplitUI splitUI)
    {
        splitUIs.Add(splitUI);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool collisionIsPlayer = collision.gameObject.tag == GM.playerTag;

        if (collisionIsPlayer)
        {
            foreach(SplitUI splitUI in splitUIs)
            {
                splitUI.ReachedSplit();
            }
        }
    }

    public void EmitParticles()
    {
        const int ParticleEmitAmmount = 20;
        newPBParticles.Emit(ParticleEmitAmmount);
    }



    public string GetSplitName()
    {
        return Name;
    }

    public float GetBestTime()
    {
        return bestTime;
    }
}
