using System.Collections.Generic;
using UnityEngine;
using Levels;

namespace Frogs
{
    public class FrogCleanJumpManager : MonoBehaviour
    {
        [SerializeField] Frog frog;

        public Dictionary<CleanlyJumpableObstacle, CleanJumpEffect> 
            cleanJumpEffects = new Dictionary<CleanlyJumpableObstacle, CleanJumpEffect>();

        private void Start()
        {
            CreateInstances();

            void CreateInstances()
            {
                List<GameObject> templates;
                List<CleanJumpEffect> effects;

                MakeTemplateList();
                InstanceFromTemplates();
                CreateDictionary();


                void MakeTemplateList()
                {
                    templates = new List<GameObject>();

                    foreach (CleanlyJumpableObstacle obstacle in frog.currentLevel.cleanJumps)
                    {
                        templates.Add(obstacle.templateCleanJumpEffect);
                    }
                }

                void InstanceFromTemplates()
                {
                    effects = ObjectInstanceBuilder.CreateInstances
                        <CleanJumpEffect>(templates, frog.SetObjectUILayer);
                }

                void CreateDictionary()
                {
                    foreach(CleanJumpEffect effect in effects)
                    {
                        cleanJumpEffects.Add(effect.obstacle, effect);
                    }
                }
            }
        }

        public void DoCleanJumpEffect(CleanlyJumpableObstacle obstacle)
        {
            cleanJumpEffects[obstacle].DoEffects();
        }
    }
}
