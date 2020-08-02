using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    public float speed;

    public List<Renderer> renderers  = new List<Renderer>();
    Animator animatior;
    [SerializeField] GameObject spriteHolder;

    float repositionCooldown = 1;
    float lastRepositionTime = 0;

    private void Start()
    {
        animatior = spriteHolder.GetComponent<Animator>();

        //add all the renderers to a list so we can check if anything is on screen
        //gets the straws as well so that it is still counted as visible when phill gets lower
        

        Renderer[] r = spriteHolder.transform.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in r)
        {
            renderers.Add(renderer);
        }

        Debug.Log("CHILDREN ON THIS CLOUD: " + renderers.Count);
    }

    public bool IsOnScreen()
    {
        bool onScreen = false;
        foreach(Renderer i in renderers)
        {
            if (i.isVisible)
            {
                onScreen = true;
            }
        }
        return onScreen;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + speed, transform.position.y);
    }

    public void PopUp()
    {
        if(Time.time > repositionCooldown + lastRepositionTime)
        {
            animatior.SetTrigger("Hide");
            lastRepositionTime = Time.time;
        }


    }

}
