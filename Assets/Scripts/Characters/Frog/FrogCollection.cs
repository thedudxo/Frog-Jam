using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frogs;
using Levels;
using Characters;
using static GM.PlayerMode;

public static class SingletonThatNeedsToBeRemoved
{
    public static Frog frog;
}
namespace Frogs.Collections {
    public static class FrogStartSettings
    {
        public static Level level;
        public static FrogCollection frogCollection;
    }

    public class FrogCollection : MonoBehaviour
    {
        [HideInInspector] public List<Frog> Frogs { get; private set; } = new List<Frog>();
        [SerializeField] public Level level;
        [SerializeField] public PursuitController pursuitHandler;

        [Header("Player Prefabs")]
        [SerializeField] GameObject player1Prefab, player2Prefab, singlePlayerPrefab;


        public Dictionary<int, Frog> IDFrogs = new Dictionary<int, Frog>();
        public FrogCollectionEvents events = new FrogCollectionEvents();

        private void Awake()
        {
            FrogStartSettings.level = level;
            FrogStartSettings.frogCollection = this;

            AddFrogsToLevel();

            void AddFrogsToLevel()
            {
                switch (GM.playerMode)
                {
                    case (single):
                        CreateFrog(singlePlayerPrefab);
                        break;

                    case (SplitScreen):
                        CreateFrog(player1Prefab);
                        CreateFrog(player2Prefab);
                        break;
                }

                void CreateFrog(GameObject frogPrefab)
                {
                    GameObject.Instantiate(frogPrefab, gameObject.transform);
                }
            }
        }

        public void AddFrog(Frog frog)
        {
            Frogs.Add(frog);
            IDFrogs.Add(frog.gameObject.GetInstanceID(), frog);
            frog.events.SubscribeOnDeath(events);
        }

        public Frog GetFrogComponent(GameObject obj)
        {
            if (obj.gameObject.CompareTag(GM.playerTag))
            {
                int objID = obj.GetInstanceID();

                if (IDFrogs.TryGetValue(objID, out Frog frog))
                {
                    return frog;
                }
            }
            return null;
        }

        public bool FrogIsFirst(Frog givenFrog)
        {
            if (!Frogs.Contains(givenFrog))
            {
                Debug.LogError("Given frog not managed by this", this);
            }

            bool isFirst = true;

            foreach (Frog frog in Frogs)
            {
                if (frog != givenFrog)
                {
                    bool givenFrogIsBehind = frog.transform.position.x > givenFrog.transform.position.x;
                    if (givenFrogIsBehind)
                    {
                        isFirst = false;
                    }
                }
            }

            return isFirst;
        }

        public Frog GetLastFrog()
        {
            Frog last = null;
            foreach (Frog frog in Frogs)
            {
                if (last == null) last = frog;
                else
                {
                    if (frog.transform.position.x < last.transform.position.x)
                        last = frog;
                }
            }
            return last;
        }

        public bool AllFrogsOnPlatform()
        {
            foreach (Frog frog in Frogs)
            {
                bool frogNotOnPlatform = frog.transform.position.x > level.StartPlatformLength;
                if (frogNotOnPlatform) return false;
            }
            return true;
        }
    }
}
