using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {
    public static GM instance = null;

    List<GameObject> gaters = new List<GameObject>();


	// Use this for initialization
	void Awake () {
		if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PhillDied()
    {
     foreach(GameObject gater in gaters)
        {
            gater.GetComponent<AlliA>().ResetGater();
        }
    }

    public void AddGater(GameObject gater)
    {
        gaters.Add(gater);
    }
}
