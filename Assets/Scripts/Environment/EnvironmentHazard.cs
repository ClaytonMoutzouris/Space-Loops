using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum HazardMovementType { AsteroidField, Nova, StaticStorm };
public class EnvironmentHazard : MonoBehaviour
{
    public AttackData asteroidAttack;
    public int maxNumAsteroids = 10;
    public Vector2 direction = Vector2.down;
    float lastAsteroidTime = 0;
    float lastDirectionChangeTime = 0;
    public float asteroidSpawnRate = 1;
    public float directionChangeTime = 10;
    public Vector2 spawnOrigin = new Vector2(0, 5);
    public Vector2 spawnRange = new Vector2(0, 4.5f);
    public List<Projectile> asteroids = new List<Projectile>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 0));
    }

    // Update is called once per frame
    void Update()
    {

        asteroids.RemoveAll(item => item == null);
        if (Time.time > lastAsteroidTime + asteroidSpawnRate && asteroids.Count < maxNumAsteroids)
        {
            SpawnAsteroid();
            lastAsteroidTime = Time.time;
        }

        if(Time.time > lastDirectionChangeTime + directionChangeTime)
        {
            direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 0));
            lastDirectionChangeTime = Time.time;
        }
    }

    public void SpawnAsteroid()
    {
        Projectile newAsteroid = Instantiate(asteroidAttack.projectile.projectileBodyPrefab, spawnOrigin + new Vector2(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y)), Quaternion.identity);
        newAsteroid.SetProjectileData(asteroidAttack.projectile);
        newAsteroid.attackData = Instantiate(asteroidAttack);
        newAsteroid.attackData.minDamage += GameManager.instance.roundNumber * 3 + GameManager.instance.waveNumber;
        newAsteroid.attackData.maxDamage += GameManager.instance.roundNumber * 3 + GameManager.instance.waveNumber;

        newAsteroid.rb.AddForce(direction.normalized * newAsteroid.data.speed, ForceMode2D.Impulse);

        asteroids.Add(newAsteroid);

    }
}
