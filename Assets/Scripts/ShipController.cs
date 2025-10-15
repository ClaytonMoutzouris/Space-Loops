using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public enum ShipHostility {  Friendly, Hostile, Neutral };
public class ShipController : MonoBehaviour
{
    public SpriteRenderer shipSprite;
    public ShipData shipData;
    public ShipController enemy;
    public Vector3 anchorPosition;
    public Rigidbody2D rb;
    public ShipMovement shipMovement;
    public HealthBar healthbar;
    public HealthBar shieldsbar;
    public AttackData defaultAttack;
    public AttackData defaultAttack2;
    //UI stuff
    public CrewManager crewManager;

    //regen
    public float lastRegenTime = 0;
    public float regenTick = 1;

    float lastDamagedTime = 0;


    public int shipLevel = 0;
    public int exp = 0;

    public ShipStats stats;

    public List<Projectile> activeProjectiles = new List<Projectile>();

    public ShipEquipmentManager equipmentManager;
    public ShipInventory inventory;

    //PlayerStats
    public int totalKills = 0;
    public float totalDamageDealt = 0;
    public float totalDamageTaken = 0;
    public float totalDamageBlocked = 0;

    public int totalHits = 0;
    public int totalShots = 0;
    public int totalEvades = 0;
    public ParticleSystem explosionPrefab;
    public FloatingText floatingTextPrefab;

    public List<ShipAbility> shipAbilities = new List<ShipAbility>();
    public List<Effect> shipEffects = new List<Effect>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        SetData(shipData);
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttacks();
        ShipUpdate();
    }

    public void ShipUpdate()
    {
        float currentTime = Time.time;
        if(currentTime > lastRegenTime + regenTick)
        {
            lastRegenTime = currentTime;
            if(shipData.currentHeath + stats.GetStat(ShipStatType.HealthRegen).GetValue() < stats.GetStat(ShipStatType.MaxHealth).GetValue())
            {
                shipData.currentHeath += stats.GetStat(ShipStatType.HealthRegen).GetValue();
            }
            else
            {
                shipData.currentHeath = stats.GetStat(ShipStatType.MaxHealth).GetValue();

            }
            healthbar.SetHealth(shipData.currentHeath, stats.GetStat(ShipStatType.MaxHealth).GetValue());


        }

        if(currentTime > lastDamagedTime + stats.GetStat(ShipStatType.ShieldRegenTime).GetValue())
        {
                float shieldRegen = stats.GetStat(ShipStatType.ShieldRegen).GetValue() * Time.deltaTime;
                if (shipData.currentShields + shieldRegen < stats.GetStat(ShipStatType.MaxShields).GetValue())
                {
                    shipData.currentShields += shieldRegen;
                }
                else
                {
                    shipData.currentShields = stats.GetStat(ShipStatType.MaxShields).GetValue();

                }
                shieldsbar.SetHealth(shipData.currentShields, stats.GetStat(ShipStatType.MaxShields).GetValue());


        }

        if (shipData.hosility == ShipHostility.Friendly)
        {
            //StatsPanelUI.instance.SetText(this);
            //SideBarPanelUI.instance
            StatSummaryPanel.instance.SetStats(this);
            TopBarInfoPanelUI.instance.SetInfo(this);
        }
    }

    public void SetData(ShipData data)
    {
        shipData = Instantiate(data);
        stats = shipData.GetStats();
        if(shipData.attack)
        {
            shipData.attack = Instantiate(shipData.attack);
            defaultAttack = shipData.attack;

        }
        if (shipData.attack2)
        {
            shipData.attack2 = Instantiate(shipData.attack2);
            defaultAttack2 = shipData.attack2;
        }




        foreach(ShipAbility ability in shipData.baseAbilities)
        {
            ability.GainAbility(this);
            //shipAbilities.Add(Instantiate(ability));
        }
        shipSprite.sprite = shipData.possibleSprites[Random.Range(0, shipData.possibleSprites.Count)];
        shipSprite.color = shipData.possibleColors[Random.Range(0, shipData.possibleColors.Count)];
        shipMovement.amplitude = shipData.amplitude;

        crewManager.SetInitialCrew(shipData);

        shipData.currentHeath = stats.GetStat(ShipStatType.MaxHealth).GetValue();
        shipData.currentShields = stats.GetStat(ShipStatType.MaxShields).GetValue();

        anchorPosition = transform.position;
        transform.localScale = shipData.shipSize * stats.GetStat(ShipStatType.Size).GetValue();
        healthbar.SetHealth(shipData.currentHeath, stats.GetStat(ShipStatType.MaxHealth).GetValue());
        shieldsbar.SetHealth(shipData.currentShields, stats.GetStat(ShipStatType.MaxShields).GetValue());

        switch (shipData.hosility)
        {
            case ShipHostility.Friendly:
                if (equipmentManager == null)
                {
                    equipmentManager = new ShipEquipmentManager(this);
                }
                //equipmentManager.InitSlots(); 
                if (inventory == null)
                {
                    inventory = new ShipInventory(this);
                }
                break;
            case ShipHostility.Hostile:
                for (int i = 0; i < GameManager.instance.currentWave.sectorData.sectorLevel; i++)
                {
                    LevelUp();
                }
                shipMovement.amplitude = Random.Range(2.25f, 4.5f);
                break;
            case ShipHostility.Neutral:

                break;
        }

        if (shipData.defaultWeapon)
        {
            shipData.defaultWeapon = Instantiate(shipData.defaultWeapon);
            equipmentManager.Equip(shipData.defaultWeapon, 0);
        }

    }

    public void AddCrew(CrewMember newCrew)
    {

        crewManager.PickUpCrew(newCrew);

    }


    public void CheckAttack(AttackData attack)
    {
        if (!attack)
            return;

        attack.activeProjectiles.RemoveAll(s => s == null);

        if (attack.projectile.projectileType == ProjectileTypeEnum.Laser)
        {
            if (attack.activeProjectiles.Count == 0)
            {
                Vector3 centerPos = (enemy.transform.position + transform.position) / 2f;

                Vector3 direction = (enemy.transform.position - transform.position).normalized;



                Projectile projectile = Instantiate(attack.projectile.projectileBodyPrefab, centerPos, Quaternion.Euler(direction));
                Vector2 scale = Vector2.one;
                scale.x = Vector2.Distance(transform.position, enemy.transform.position)*projectile.scale.x;
                projectile.SetProjectileData(Instantiate(attack.projectile));
                projectile.attackData = attack;
                attack.activeProjectiles.Add(projectile);
                projectile.spriteRenderer.size = scale;
                projectile.target = enemy;
                projectile.owner = this;
                activeProjectiles.Add(projectile);
                projectile.laserResetTime = 1 / (attack.attackSpeed + attack.attackSpeed * (stats.GetStat(ShipStatType.AttackSpeedBonus).GetValue()*0.01f));
                totalShots++;

                foreach (ShipAbility ability in shipAbilities)
                {
                    ability.OnShoot(enemy, attack);
                }

            }
            else
            {
                attack.activeProjectiles[0].laserResetTime = 1 / (attack.attackSpeed + attack.attackSpeed * (stats.GetStat(ShipStatType.AttackSpeedBonus).GetValue()) * 0.01f);
            }


        }
        else
        {
            if (Time.time > attack.lastAttackTime + 1 / (attack.attackSpeed + attack.attackSpeed * (stats.GetStat(ShipStatType.AttackSpeedBonus).GetValue() * 0.01f)))
            {

                float predictionTime = Vector3.Distance(enemy.transform.position, transform.position) / attack.projectile.speed;
                float variance = Mathf.Clamp(predictionTime - predictionTime * (stats.GetStat(ShipStatType.Accuracy).GetValue()*0.01f), 0, predictionTime);
                predictionTime = predictionTime + Random.Range(-variance, variance)*2;


                Vector3 predictedPosition = enemy.transform.position + ((Vector3)enemy.rb.linearVelocity * predictionTime);
                float delta = Mathf.Abs(predictedPosition.y) - 6;

                if(predictedPosition.y > 6)
                {
                    predictedPosition.y = 5.5f;
                }

                if (predictedPosition.y < -6)
                {
                    predictedPosition.y = -5.5f;
                }
                
                Vector2 moveDirection = (predictedPosition - transform.position).normalized;
                Quaternion predictedRotation = Quaternion.identity;
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Vector3 spawnPosition = transform.position;

                if (moveDirection != Vector2.zero)
                {
                    float predictedAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                    predictedRotation = Quaternion.AngleAxis(predictedAngle, Vector3.forward);
                }

                int numProjs = attack.numberOfProjectiles + (int)stats.GetStat(ShipStatType.AdditionalProjectilesBonus).GetValue();
                if (numProjs > 1)
                {
                    //Multi projectile spread
                    float anglePer = attack.spreadAngle / numProjs;
                    float midAngle = (attack.spreadAngle / 2);
                    //float yoffset = stats.GetStat(ShipStatType.Size).GetValue()/(numProjs-1);
                    Debug.Log("Multi Shot Fired");
                    for (int i = 0; i < numProjs; i++)
                    {
                        //- (Vector3)(Vector2.up * stats.GetStat(ShipStatType.Size).GetValue()/2 + Vector2.up*yoffset*i).Rotate(-midAngle + (anglePer * i))
                        Projectile projectile = Instantiate(attack.projectile.projectileBodyPrefab, spawnPosition, predictedRotation);
                        projectile.SetProjectileData(Instantiate(attack.projectile));
                        projectile.attackData = attack;
                        attack.activeProjectiles.Add(projectile);
                        projectile.target = enemy;
                        projectile.owner = this;
                        projectile.data.speed += projectile.data.speed * stats.GetStat(ShipStatType.ProjectileSpeedBonus).GetValue() * 0.01f;

                        //float angleInRads = ((anglePer * i) - midAngle);
                        Vector2 tempDir = moveDirection;
                        if (attack.projectile.projectileType != ProjectileTypeEnum.Laser)
                        {
                            tempDir = tempDir.Rotate(-midAngle + (anglePer * i));
                            projectile.transform.position += (Vector3)(tempDir.normalized * .1f);
                            projectile.rb.AddForce(tempDir.normalized, ForceMode2D.Impulse);

                        }

                        activeProjectiles.Add(projectile);
                        totalShots++;
                    }
                }
                else
                {
                    Projectile projectile = Instantiate(attack.projectile.projectileBodyPrefab, spawnPosition, predictedRotation);
                    projectile.SetProjectileData(Instantiate(attack.projectile));
                    projectile.attackData = attack;
                    attack.activeProjectiles.Add(projectile);
                    projectile.target = enemy;
                    projectile.owner = this;
                    projectile.data.speed += projectile.data.speed * stats.GetStat(ShipStatType.ProjectileSpeedBonus).GetValue() * 0.01f;

                    if (attack.projectile.projectileType != ProjectileTypeEnum.Laser)
                    {
                        projectile.rb.AddForce(moveDirection * projectile.data.speed, ForceMode2D.Impulse);

                    }


                    activeProjectiles.Add(projectile);
                    totalShots++;
                }





                    attack.lastAttackTime = Time.time;

                foreach (ShipAbility ability in shipAbilities)
                {
                    ability.OnShoot(enemy, attack);
                }
            }

        }
    }

    public void CheckAttacks()
    {

        if(!enemy)
        {
            switch (shipData.hosility)
            {
                case ShipHostility.Friendly:
                    if (GameManager.instance.enemyShips.Count > 0)
                    {
                        enemy = GameManager.instance.enemyShips[0];
                    }
                    break;
                case ShipHostility.Hostile:
                    if (GameManager.instance.playerShip)
                    {
                        enemy = GameManager.instance.playerShip;
                    }
                    break;
            }
        }

        if (enemy)
        {
            if(equipmentManager != null && equipmentManager.GetSlot(EquipmentSlot.Weapon, 0) != null && equipmentManager.GetSlot(EquipmentSlot.Weapon, 0).equipment && equipmentManager.GetSlot(EquipmentSlot.Weapon, 0).equipment is WeaponData weaponData)
            {
                CheckAttack(weaponData.attackData);
            } else
            {
                CheckAttack(defaultAttack);


            }

            if (equipmentManager != null && equipmentManager.GetSlot(EquipmentSlot.Weapon, 1) != null && equipmentManager.GetSlot(EquipmentSlot.Weapon, 1).equipment && equipmentManager.GetSlot(EquipmentSlot.Weapon, 1).equipment is WeaponData weaponData2)
            {

                CheckAttack(weaponData2.attackData);
            }
            else
            {

                CheckAttack(defaultAttack2);
            }

            //CheckAttack(shipData.attack);
            //CheckAttack(shipData.attack2);
        }
        


    }

    public void ShipDamaged(Projectile projectile)
    {
        bool hit = false;

        if(projectile.owner)
        {
            if(Random.Range(0, 100) < Mathf.Clamp(stats.GetStat(ShipStatType.Evasion).GetValue(), 0, 80))
            {
                //dodged
                LogPanelUI.instance.AddEntry(gameObject.name + " evaded a hit.", LogEntryType.Combat);
                ShowFloatingText("Evade", Color.cyan);
                totalEvades++;
                return;
            }

            projectile.owner.totalHits++;
        }

        float damage = projectile.attackData.GetDamage();
        if (projectile.owner)
        {
            damage += damage * projectile.owner.stats.GetStat(ShipStatType.DamageBonus).GetValue() * 0.01f;
        }

        bool crit = (Random.Range(0, 100) < projectile.owner.stats.GetStat(ShipStatType.CritChanceBonus).GetValue());

        if(crit)
        {
            damage += damage * projectile.owner.stats.GetStat(ShipStatType.CritDamageBonus).GetValue() * 0.01f;

        }
        else
        {

        }

        float damageReduction = Mathf.Clamp((damage * stats.GetStat(ShipStatType.DamageReduction).GetValue() * 0.01f), 0, damage * .8f);
        totalDamageBlocked += Mathf.FloorToInt(damageReduction);
        damage = damage - damageReduction;
        totalDamageTaken += damage;

        if (projectile.owner)
        {
            projectile.owner.totalDamageDealt += damage;

            foreach (ShipAbility ability in projectile.owner.shipAbilities)
            {
                ability.OnHitAPlayer(this, projectile);
            }
        }

        float damageToShields = 0;

        if (shipData.currentShields > 0)
        {
            if(damage > shipData.currentShields)
            {
                damage -= shipData.currentShields;
                shipData.currentShields = 0;
            }
            else
            {
                shipData.currentShields -= damage;
                damage = 0;
            }

        }

        shipData.currentHeath -= damage;

        foreach (ShipAbility ability in shipAbilities)
        {
            ability.OnGetHit(projectile);
        }


        healthbar.SetHealth(shipData.currentHeath, stats.GetStat(ShipStatType.MaxHealth).GetValue());
        shieldsbar.SetHealth(shipData.currentShields, stats.GetStat(ShipStatType.MaxShields).GetValue());

        lastDamagedTime = Time.time;

        if(crit)
        {
            LogPanelUI.instance.AddEntry(gameObject.name + " was CRIT for " + damage + "!", LogEntryType.Combat);
            ShowFloatingText(((int)damage).ToString(), Color.red, 1, 1, 2);

        }
        else
        {
            LogPanelUI.instance.AddEntry(gameObject.name + " was hit for " + damage, LogEntryType.Combat);
            ShowFloatingText(((int)damage).ToString(), Color.yellow);

        }

        if (shipData.currentHeath <= 0)
        {
            if(shipData.hosility == ShipHostility.Hostile)
            {
                if(projectile.owner)
                {
                    projectile.owner.GainEXP(GetEXPValue());
                    foreach (ItemData item in shipData.lootTable.GetLoot((int)projectile.owner.stats.GetStat(ShipStatType.Luck).GetValue()))
                    {
                        if(!item)
                        {
                            continue;
                        }
                        ItemData newItem = Instantiate(item);
                        newItem.Randomize();
                        projectile.owner.inventory.AddItem(newItem);
                    }

                    foreach (CardData card in shipData.lootTable.GetCards((int)projectile.owner.stats.GetStat(ShipStatType.Luck).GetValue()))
                    {
                        if (!card)
                        {
                            continue;
                        }
                        CardData newCard = Instantiate(card);
                        newCard.GenerateCard();
                        MapPanelUI.instance.PickUpCard(newCard);
                    }

                    projectile.owner.totalKills++;

                    foreach (ShipAbility ability in projectile.owner.shipAbilities)
                    {
                        ability.OnDestroyed(projectile);
                    }

                    foreach (ShipAbility ability in projectile.owner.shipAbilities)
                    {
                        ability.OnDestroyEnemy(this, projectile);
                    }

                }
            }
            else
            {
                GameOverScreenUI.instance.DisplayStats(this);
            }
            LogPanelUI.instance.AddEntry(gameObject.name + " was destroyed.", LogEntryType.Combat);

            ParticleSystem splosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            CleanUpProjectiles();
            Destroy(gameObject);
        }
    }

    public void ShipDestroyed()
    {

    }

    public void CleanUpProjectiles()
    {
        foreach (Projectile proj in activeProjectiles)
        {
            Destroy(proj.gameObject);
        }
        activeProjectiles.Clear();
    }


    public void ShowFloatingText(string text, Color color, float dTime = 1, float sSpeed = 1, float sizeMult = 1.0f)
    {
        FloatingText newText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        newText.SetOffset(Vector3.up * (transform.localScale.y));
        newText.floatingText.fontSize = newText.floatingText.fontSize * sizeMult;
        newText.duration = dTime;
        newText.scrollSpeed = sSpeed;
        newText.floatingText.text = "" + text;
        newText.floatingText.color = color;
    }

    public void LevelUp()
    {
        float oldMax = stats.GetStat(ShipStatType.MaxHealth).GetValue();
        float oldMaxShield = stats.GetStat(ShipStatType.MaxShields).GetValue();
        stats.AddBonus(new StatBonus(ShipStatType.Speed, 1));
        stats.AddBonus(new StatBonus(ShipStatType.Power, 1));
        stats.AddBonus(new StatBonus(ShipStatType.Armor, 1));
        stats.AddBonus(new StatBonus(ShipStatType.Hull, 1));
        stats.AddBonus(new StatBonus(ShipStatType.Charisma, 1));
        stats.AddBonus(new StatBonus(ShipStatType.Shields, 1));
        float difference = stats.GetStat(ShipStatType.MaxHealth).GetValue() - oldMax;
        float shieldDifference = stats.GetStat(ShipStatType.MaxShields).GetValue() - oldMaxShield;

        if(difference != 0)
        {
            shipData.currentHeath += difference;
        }
        
        if(shieldDifference != 0)
        {
            shipData.currentShields += shieldDifference;
        }
        //shipData.shipSize += Vector2.one*0.1f;
        //transform.localScale = shipData.shipSize;
        transform.localScale = shipData.shipSize * stats.GetStat(ShipStatType.Size).GetValue();
        healthbar.SetHealth(shipData.currentHeath, stats.GetStat(ShipStatType.MaxHealth).GetValue());
        shieldsbar.SetHealth(shipData.currentShields, stats.GetStat(ShipStatType.MaxShields).GetValue());
        shipLevel++;

        if(shipData.hosility==ShipHostility.Friendly)
        {
            LogPanelUI.instance.AddEntry("Level Up!", LogEntryType.Other);
        }
    }



    public int GetEXPToLevel()
    {
        return 5 + (shipLevel + shipLevel * shipLevel) * 2;
    }

    public int GetEXPToLevel(int level)
    {
        return 5 + (level + level * level) * 2;
    }

    public int GetEXPValue()
    {
        return (int)stats.GetStat(ShipStatType.MaxHealth).GetValue() / 10 + shipLevel;
    }

    public void GainEXP(int xp)
    {
        exp += xp + (int)(xp * stats.GetStat(ShipStatType.XpBonus).GetValue()*0.01f);
        LogPanelUI.instance.AddEntry("Gained " + xp + " xp", LogEntryType.Other);
        while(exp >= GetEXPToLevel())
        {
            LevelUp();
        }
    }

}
