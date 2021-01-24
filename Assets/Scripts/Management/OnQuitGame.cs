using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class OnQuitGame : MonoBehaviour
{

    [SerializeField] SplitManager splitManager;

    private void OnApplicationQuit()
    {
        if (!GM.sendAnyalitics) { return; }

        //move to end of level, when theres more levels

        //doesnt work with multiplayer
        //foreach(Split split in splitManager.splits)
        //{
        //    Analytics.CustomEvent("Best Time when quit at " + split.GetSplitName(), new Dictionary<string, object>
        //        { {"Time", split.GetBestTime() }
        //        });
        //}

        Analytics.CustomEvent("Endgame Stats", new Dictionary<string, object>{
                {"Total Deaths"    , Statistics.totalDeaths },
                {"Wave Deaths"     , Statistics.waveDeaths },
                {"Hole Deaths"     , Statistics.deathsAtThatHole },
                {"Aligator Deaths" , Statistics.aligatorDeaths},
                {"Water Deaths"    , Statistics.waterDeaths },
                {"Suicide Deaths"  , Statistics.suicideDeaths },

                {"Session Time"  , Statistics.timeInSession },
                //{"Personal Best" , GM.progressBar.GetPersonalBest() },
                });

    }
}
