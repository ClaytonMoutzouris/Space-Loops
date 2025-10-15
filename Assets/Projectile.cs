using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;

public enum AimType { Straight, Homing, Targetted, Scatter }
public class Projectile : MonoBehaviour 
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Collider2D collider;
    public ProjectileData data;
    public AttackData attackData;

    public ShipController owner;
    public ShipController target;
    public Vector3 targetLastPosition;
    public float spawnTime = 0;

    //laser things
    float lastResetTime = 0;
    public float laserResetTime = 0.5f;
    public List<ShipController> collisions = new List<ShipController>();

    public Vector3 centerPos;
    public Vector3 direction;
    public Vector3 scale;
    public Vector2 size;

    int numPierced = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {

        if (data.projectileType == ProjectileTypeEnum.Laser)
        {

            if (!target)
            {
                switch (owner.shipData.hosility)
                {
                    case ShipHostility.Friendly:
                        if (GameManager.instance.enemyShips.Count > 0)
                        {
                            target = GameManager.instance.enemyShips[0];
                        }
                        break;
                    case ShipHostility.Hostile:
                        if (GameManager.instance.playerShip)
                        {
                            target = GameManager.instance.playerShip;
                        }
                        break;
                }
            }
            //Destroy(gameObject, data.lifeTime);
            if(!target)
            {
                DestroyProjectile();
            }
            else
            {
                LaserMovement();
            }

            if (Time.time > lastResetTime + laserResetTime)
            {
                /*
                foreach(ShipController ship in collisions)
                {
                    ship.ShipDamaged(this);
                }
                */

                collisions.Clear();
                lastResetTime = Time.time;

            }
        }
        else if (Time.time > spawnTime + data.lifeTime)
        {
            DestroyProjectile();
        }
    }

    public void FixedUpdate()
    {
        if (target)
        {
            ProjectileMovement();
        }

        //transform.forward = rb.linearVelocity.normalized;

    }

    public void ProjectileMovement()
    {
        //transform.forward = rb.linearVelocity.normalized;
        //transform.rotation = Quaternion.LookRotation(rb.linearVelocity, Vector2.up);
        switch (data.projectileType)
        {
            case ProjectileTypeEnum.Beam:
                BeamMovement();

                break;
            case ProjectileTypeEnum.Missile:
                BeamMovement();

                break;
            case ProjectileTypeEnum.Energy:
                BeamMovement();

                break;
            case ProjectileTypeEnum.Laser:
                //LaserMovement();

                break;
            case ProjectileTypeEnum.Homing:
                HomingMovement();
                break;
        }

        if (data.projectileType == ProjectileTypeEnum.Homing)
        {
        } else
        {
        }

        targetLastPosition = target.transform.position;

    }

    public void BeamMovement() {
        rb.AddForce(rb.linearVelocity.normalized * data.speed, ForceMode2D.Force);
    }

    public void LaserMovement()
    {


        //rb.linearVelocity = Vector3.zero;
        //rb.angularVelocity = 0;

        //rb.AddForce(rb.linearVelocity.normalized * data.speed, ForceMode2D.Force);
        //Vector3 predictedPosition = target.transform.position + ((Vector3)target.rb.linearVelocity * Time.deltaTime);
        //rb.AddForce((predictedPosition - transform.position).normalized * data.speed, ForceMode2D.Force);
        //float distance = Mathf.Abs((target.transform.position - transform.position).magnitude);
        //rb.linearVelocity = (predictedPosition - transform.position).normalized * data.speed;
        //Vector2 moveDirection = (target.transform.position - transform.position).normalized;
        //if (moveDirection != Vector2.zero)
        //{
        //    float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //}

        //transform.position = Vector3.Lerp(target.transform.position, transform.position, 0.5f);

        //transform.localScale = new Vector3(distance * 2, .2f);

        centerPos = (target.transform.position + owner.transform.position) / 2f;
        transform.position = centerPos;

        direction = (target.transform.position - owner.transform.position).normalized;
        transform.right = direction;

        //scale = Vector2.one;
        size.x = Vector2.Distance(owner.transform.position, target.transform.position);
        spriteRenderer.size = size;
        BoxCollider2D boxCollider = (BoxCollider2D)collider;

        if(boxCollider)
        {
            boxCollider.size = size;
        }
    }

    public void HomingMovement()
    {
        Vector3 predictedPosition = target.transform.position + ((Vector3)target.rb.linearVelocity * Time.deltaTime);
        //rb.AddForce((predictedPosition - transform.position).normalized * data.speed, ForceMode2D.Force);
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, (predictedPosition - transform.position).normalized * data.speed, 0.05f);
        Vector2 moveDirection = rb.linearVelocity;

        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

    public void SetProjectileData(ProjectileData newData)
    {
        data = newData;
        spriteRenderer.sprite = data.sprite;
        spriteRenderer.color = data.color;
        size = data.size;

        if (data.projectileType == ProjectileTypeEnum.Laser)
        {
            spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            spriteRenderer.size = Vector2.one*size;

        }
        else
        {
            transform.localScale = transform.localScale * size;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShipController controller = collision.gameObject.GetComponent<ShipController>();

        if (controller != null && owner != controller && (!owner || owner.shipData.hosility != controller.shipData.hosility))
        {
            controller.ShipDamaged(this);

            if (data.projectileType == ProjectileTypeEnum.Laser)
            {
                //Destroy(gameObject, data.lifeTime);
                collisions.Add(controller);
            }
            else
            {
                CheckSplit();
                CheckChain();

                if(data.numPierces + (int)owner.stats.GetStat(ShipStatType.ProjectilePierceNumberBonus).GetValue() > numPierced)
                {
                    numPierced++;
                } else
                {
                    DestroyProjectile();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ShipController controller = collision.gameObject.GetComponent<ShipController>();

        if (controller != null && owner != controller && (!owner || owner.shipData.hosility != controller.shipData.hosility))
        {
            collisions.Remove(controller);      
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        ShipController controller = collision.gameObject.GetComponent<ShipController>();

        if (controller != null && owner != controller && (!owner || owner.shipData.hosility != controller.shipData.hosility))
        {
            if (data.projectileType == ProjectileTypeEnum.Laser && !collisions.Contains(controller))
            {
                controller.ShipDamaged(this);
                collisions.Add(controller);
            }
        }
    }

    public void DestroyProjectile()
    {
        if (owner)
        {
            owner.activeProjectiles.Remove(this);
        }

        if (attackData)
        {
            attackData.activeProjectiles.Remove(this);

        }

        Destroy(gameObject);
    }

    public void CheckChain()
    {
        if (data.numChain <= 0)
        {
            return;
        }
    }

    public void CheckSplit()
    {
        if(data.numSplit <= 0)
        {
            return;
        }

        float anglePer = attackData.spreadAngle / data.numSplit;
        float midAngle = (attackData.spreadAngle / 2)*Mathf.Deg2Rad;

        for(int i = 0; i < data.numSplit; i++)
        {
            Projectile proj = Instantiate(data.projectileBodyPrefab, transform.position, transform.rotation);
            proj.SetProjectileData(Instantiate(data));
            //+new Vector2(Mathf.Cos(radsPer), Mathf.Sin(radsPer))
            proj.attackData = attackData;
            attackData.activeProjectiles.Add(proj);
            proj.target = target;
            proj.owner = owner;
            proj.transform.Rotate(new Vector3(0, 0, -midAngle));
            proj.transform.Rotate(new Vector3(0, 0, anglePer * i));
        } 


    }

}
