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

    public bool AnyFrogAhead(Wave wave)
    {
        foreach(Frog frog in frogManager.Frogs)
        {
            if (frog.transform.position.x > wave.transform.position.x)
                return true;
        }
        return false;
    }

    public bool FrogWillSetbackBehindWave(Frog frog)
    {
        foreach(Wave wave in waveManager.waves)
        {
            float waveX = wave.transform.position.x;
            float frogX = frog.transform.position.x;

            bool waveBehindFrog = waveX < frogX;
            bool SetbackBehind = frogX - GM.respawnSetBack < waveX;

            if (waveBehindFrog && SetbackBehind) return true;
        }
        return false;
    }
}