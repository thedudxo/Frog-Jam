using UnityEngine;
using Frogs.Instances.Setups;
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

            Frog frog = Object.Instantiate(
                original: frogPrefab,
                position: collection.transform.position,
                rotation: Quaternion.identity,
                parent: collection.transform
                ).GetComponent<Frog>();

            //frog.transform.position = frog.spawnpoint;

            FrogInstantiateSettings.factory = null;

            return frog;
        }

        public void SetupFrog(Frog frog)
        {
            collection.Add(frog);
            AddToLevel(frog);

            frog.setup.Setup(FrogInstantiateSettings.veiwMode);
        }

        void AddToLevel(Frog frog)
        {
            frog.currentLevel = level;
            frog.spawnpoint = new Vector2(level.region.start, frog.transform.position.y);
            frog.splitManager = level.splitManager;
        }
    }
}
