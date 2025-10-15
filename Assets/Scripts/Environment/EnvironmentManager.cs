using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public EnvironmentManager instance;

    public List<EnvironmentHazard> hazards;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpawnEnvironment()
    {

    }

    public void CleanUp()
    {
        foreach(EnvironmentHazard hazard in hazards)
        {
            foreach(Projectile proj in hazard.asteroids)
            {
                Destroy(proj);
            }
            hazard.asteroids.Clear();
        }
    }
}
