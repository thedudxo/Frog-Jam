using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class OnQuitGame : MonoBehaviour
{

    private void OnApplicationQuit()
    {
        if(GM.splitManager = null)
        {
            Debug.Log("no split manager, not sending stats");
            return;
        }
        if (!GM.sendAnyalitics) { return; }

        //move to end of level, when theres more levels
        foreach(Split split in GM.splitManager.splits)
        {
            Analytics.CustomEvent("Best Time when quit at " + split.getSplitName(), new Dictionary<string, object>
                { {"Time", split.getBestTime() }
                });
        }

        Analytics.CustomEvent("Endgame Stats", new Dictionary<string, object>{
                {"Total Deaths"    , Statistics.totalDeaths },
                {"Wave Deaths"     , Statistics.waveDeaths },
                {"Hole Deaths"     , Statistics.deathsAtThatHole },
                {"Aligator Deaths" , Statistics.aligatorDeaths},
                {"Water Deaths"    , Statistics.waterDeaths },
                {"Suicide Deaths"  , Statistics.suicideDeaths },

                {"Session Time"  , Statistics.timeInSession },
                {"Personal Best" , GM.progressBar.GetPersonalBest() },
                });

    }
}
