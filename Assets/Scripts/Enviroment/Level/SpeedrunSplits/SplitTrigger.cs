using UnityEngine;
using Frogs.Instances;

namespace Levels
{
    public class SplitTrigger : Split
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject obj = collision.gameObject;
            bool isPlayer = obj.tag == GM.playerTag;
            int objInstanceID = obj.GetInstanceID();

            if (isPlayer)
            {
                foreach (SplitEffect effect in effects)
                {
                    bool collisionHasThisEffect = objInstanceID == effect.CharacterInstanceID;
                    if (collisionHasThisEffect)
                    {
                        effect.ReachedSplit();
                        break;
                    }
                }
            }
        }
    }
}
