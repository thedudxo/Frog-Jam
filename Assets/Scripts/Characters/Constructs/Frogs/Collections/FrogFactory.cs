using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frogs.Instances;
using Levels;

namespace Frogs.Collections 
{
    public static class FrogInstantiateSettings
    {
        //weird but unity doesn't have a nice way of sending parameters through Instantiate()
        public static FrogFactory factory;
        public static ViewMode veiwMode;
    }

    public class FrogFactory
    {
        FrogCollection collection;
        GameObject frogPrefab;

        Level level;

        public FrogFactory(FrogCollection collection, GameObject frogPrefab)
        {
            this.collection = collection;
            this.frogPrefab = frogPrefab;
            level = collection.level;
        }

        public Frog CreateFrog(ViewMode veiwMode)
        {

            FrogInstantiateSettings.factory = this;
            FrogInstantiateSettings.veiwMode = veiwMode;

            Frog frog = Object.Instantiate(frogPrefab, collection.transform).GetComponent<Frog>();

            FrogInstantiateSettings.factory = null;

            return frog;
        }

        public void SetupFrog(Frog f)
        {
            collection.Add(f);
            AddToLevel(f);

            f.setup.Setup(FrogInstantiateSettings.veiwMode);
        }

        void AddToLevel(Frog f)
        {
            f.currentLevel = level;
            f.spawnpoint = new Vector2(level.region.start, f.transform.position.y);
            f.splitManager = level.splitManager;
        }
    }
}
