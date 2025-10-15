using UnityEngine;


public enum ProjectileTypeEnum { Beam, Missile, Energy, Laser, Homing }
[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public float lifeTime = 5;
    public float accel;
    public float speed;
    public float turnSpeed = 0.05f;
    public Sprite sprite;
    public Projectile projectileBodyPrefab;
    public ProjectileTypeEnum projectileType;
    public Color color;
    public Vector2 size;

    public int numPierces = 0;
    public int numChain = 0;
    public int numSplit = 0;

}
