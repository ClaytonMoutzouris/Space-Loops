using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFieldHazard : MonoBehaviour
{
    public ProjectileData baseAsteroid;
    public int maxNumAsteroids = 10;
    public Vector2 direction = Vector2.down;
    float lastAsteroidTime = 0;
    public float asteroidSpawnRate = 1;
    public Vector2 spawnOrigin = new Vector2(0, 5);
    public Vector2 spawnRange = new Vector2(0, 4.5f);
    public List<Projectile> asteroids = new List<Projectile>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastAsteroidTime + asteroidSpawnRate && asteroids.Count < maxNumAsteroids)
        {
            SpawnAsteroid();
        }
    }

    public void SpawnAsteroid()
    {
        Projectile newAsteroid = Instantiate(baseAsteroid.projectileBodyPrefab, spawnOrigin + new Vector2(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y)), Quaternion.identity);
        newAsteroid.SetProjectileData(baseAsteroid);

        newAsteroid.rb.AddForce(direction.normalized * newAsteroid.data.speed, ForceMode2D.Impulse);

        asteroids.Add(newAsteroid);

    }
}
