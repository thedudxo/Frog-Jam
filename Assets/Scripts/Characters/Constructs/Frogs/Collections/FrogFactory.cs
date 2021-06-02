using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frogs.Instances;
using Levels;

namespace Frogs.Collections 
{
    public class FrogFactory : MonoBehaviour
    {
        [SerializeField] FrogCollection collection;
        [SerializeField] GameObject frogPrefab;

        public Frog CreateFrog()
        {
            FrogStartSettings.factory = this;
            Frog frog = Instantiate(frogPrefab, gameObject.transform).GetComponent<Frog>();
            FrogStartSettings.factory = null;

            collection.AddFrog(frog);

            return frog;
        }

        public void SetupFrog(Frog f)
        {
            AddToCollection();
            constructFrog();



            void AddToCollection()
            {
                f.collection = collection;
                collection.AddFrog(f);
            }

            void constructFrog()
            {
                Level level = FrogStartSettings.level;
                f.currentLevel = level;
                f.spawnpoint = new Vector2(level.region.start, transform.position.y);
                f.splitManager = level.splitManager;
            }
        }
    }
}
