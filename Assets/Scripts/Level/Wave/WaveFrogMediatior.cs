using System.Collections;
using UnityEngine;
using waveScripts;
using FrogScripts;


public class WaveFrogMediatior : MonoBehaviour, INotifyAnyFrogLeftPlatform
{
    [SerializeField] WaveManager waveManager;
    [SerializeField] FrogManager frogManager;

    private void Start()
    {
        frogManager.events.SubscribeAnyFrogLeftPlatform(this);
    }

    public void AnyFrogLeftPlatform()
    {
        waveManager.ReleaseWave();
    }
    public Frog CheckIfHitFrog(Collider2D collision)
    {
        return frogManager.GetFrogFromGameobject(collision.gameObject);
    }

    public bool CheckIfFrogIsFirst(Frog frog)
    {
        if (frogManager.FrogIsFirst(frog)) return true;
        else return false;
            
    }
}