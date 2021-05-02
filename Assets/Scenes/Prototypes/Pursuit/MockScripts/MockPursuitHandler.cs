using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pursuits;

public class MockPursuitHandler : MonoBehaviour
{
    List<MockSphere> mockSpheres;
    List<MockAngryCube> mockAngryCubes;

    [SerializeField] GameObject mockSpherePrefab;
    [SerializeField] GameObject mockAngryCubesPrefab;

    Pursuit pursuit = new Pursuit();
    MockAngryCube incomingAngryCube = null;

    void Start()
    {
        MockSphere sphere =  Instantiate(mockSpherePrefab).GetComponent<MockSphere>();
        sphere.runner = pursuit.Add<Runner>();

        incomingAngryCube = Instantiate(mockAngryCubesPrefab).GetComponent<MockAngryCube>();
        incomingAngryCube.transform.position = new Vector3(-5, 0, 0);
    }

    void Update()
    {
        if (incomingAngryCube != null)
        {
            if (incomingAngryCube.transform.position.x > 0)
            {
                incomingAngryCube.pursuer = pursuit.Add<Pursuer>();
                incomingAngryCube = null;
            }
        }
    }
}
