using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour, ILevelRestartResetable
{
    [SerializeField] GameObject objectToSwap;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] bool randomOnStart = false;

    SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = objectToSwap.GetComponent<SpriteRenderer>();

        if (randomOnStart) { RandomiseSprite(); }

        GM.AddLevelRestartResetable(this);
    }

    public void PhillRestartedLevel()
    {
        RandomiseSprite();
    }

    public void RandomiseSprite()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
    }

    public void SpecificSprite(int index)
    {
        spriteRenderer.sprite = sprites[index];
    }
}
