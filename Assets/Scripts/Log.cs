using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour {
    [SerializeField] GameObject player;
    [SerializeField] GameObject logPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < player.transform.position.x - 20)
        {
            //Debug.Log("YES");
            
           
            //transform.position = new Vector2(player.transform.position.x + 20 + Random.Range(0,logSpawnRange+1), transform.position.y );
        }
	}

    public void PhillDied()
    {

    }
}
