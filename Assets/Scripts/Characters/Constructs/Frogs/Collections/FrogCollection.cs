using Characters;
using Frogs.Instances;
using Frogs.Instances.Setups;
using Levels;
using System.Collections.Generic;
using UnityEngine;
using static GM.PlayerMode;

namespace Frogs.Collections
{
    public class FrogCollection : MonoBehaviour
    {
        [HideInInspector] public List<Frog> Frogs { get; private set; } = new List<Frog>();
        [SerializeField] public Level level;
        [SerializeField] public PursuitController pursuitHandler;
        FrogFactory factory;

        [Header("Player Prefabs")]
        [SerializeField] GameObject singlePlayerPrefab;


        public Dictionary<int, Frog> IDFrogs = new Dictionary<int, Frog>();

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

        public void Add(Frog frog)
        {
            frog.collection = this;
            Frogs.Add(frog);
            IDFrogs.Add(frog.gameObject.GetInstanceID(), frog);
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
    }
}
