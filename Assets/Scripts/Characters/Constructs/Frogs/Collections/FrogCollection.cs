using Characters;
using Frogs.Instances;
using Levels;
using System.Collections.Generic;
using UnityEngine;
using static GM.PlayerMode;

public static class SingletonThatNeedsToBeRemoved
{
    public static Frog frog;
}
namespace Frogs.Collections
{
    public class FrogCollection : MonoBehaviour
    {
        [HideInInspector] public List<Frog> Frogs { get; private set; } = new List<Frog>();
        [SerializeField] public Level level;
        [SerializeField] public PursuitController pursuitHandler;
        FrogFactory factory;

        [Header("Player Prefabs")]
        [SerializeField] GameObject player1Prefab, player2Prefab, singlePlayerPrefab;


        public Dictionary<int, Frog> IDFrogs = new Dictionary<int, Frog>();
        public FrogCollectionEvents events = new FrogCollectionEvents();

        private void Awake()
        {
            factory = new FrogFactory(this, singlePlayerPrefab);

            if (GM.playerMode == SplitScreen)
            {
                factory.CreateFrog(ViewMode.SplitTop);
                factory.CreateFrog(ViewMode.SplitBottom);
            }
            else
            {
                factory.CreateFrog(ViewMode.Single);
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
