using Frogs.Instances;
using Levels;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Util;

namespace EndScreen
{
    public class EndScreenTest : MonoBehaviour
    {
        int frame = 0;
        int givenFrames = 100;
        float framesNrml = 0;

        Level currentLevel = null;
        Frog frog = null;

        /*
        * Load scene
        * move the frog through the level
        * check split total time
        */

        [UnityTest]
        public IEnumerator TotalSplitTimeTest()
        {
            int attempts = 100;
            int attempt = 0;
            int bugOccurences = 0;

            while (attempt < attempts)
            {
                attempt++;
                frame = 0;

                SceneManager.LoadScene("Concept Level");


                SceneManager.sceneLoaded += (scene, mode) =>
                {
                    currentLevel = GM.currentLevel;
                    frog = currentLevel.frogManager.Frogs[0];
                    frog.rb.isKinematic = true;
                };

                yield return new WaitForSeconds(0.1f);
                //scene should now be loaded


                while (frame < givenFrames)
                {

                    //get frames normalised
                    frame++;
                    framesNrml = Normalise.Normalise01(frame, givenFrames);

                    //move frog
                    float xPos = Mathf.Lerp(0, currentLevel.region.end, framesNrml);
                    Vector2 newPos = new Vector2(xPos, 1);
                    frog.transform.position = newPos;

                    //finished the level
                    if (frame >= givenFrames) Debug.Log("arrived");
                    yield return null;
                }

                if (LevelEndScreen.bugOccured)
                {
                    Debug.Log("CAUGHT THE BUG");
                    bugOccurences++;
                    Debug.Break();
                }

                yield return null;
            }

            Debug.Log(bugOccurences);
            Assert.That(bugOccurences == 0);
        }
    }
}
