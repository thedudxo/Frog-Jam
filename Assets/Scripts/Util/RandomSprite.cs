using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    [SerializeField] GameObject objectToSwap;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] bool randomOnStart = false;

    SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = objectToSwap.GetComponent<SpriteRenderer>();

        if (randomOnStart) { randomSprite(); }

        GM.currentLevel.randomSprites.Add(this); //added for frog jam
    }

    public void randomSprite()
    {
        
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
    }


    public void specificSprite(int index)
    {
        spriteRenderer.sprite = sprites[index];
    }
}
