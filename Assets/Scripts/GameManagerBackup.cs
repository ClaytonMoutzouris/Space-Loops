using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManagerBackup : MonoBehaviour
{
    public static GameManagerBackup instance;

    public ShipController playerShip;
    public ShipData playerShipData;

    public List<ShipController> enemyShips;

    public List<ShipData> possibleShips;
    public List<ShipData> bossShips;

    public int waveNumber = 0;
    public int roundNumber = 0;

    public int score = 0;
    public bool waveOver = false;
    public bool newRound = false;

    public bool autoRun = false;
    public bool isPaused = false;
    public bool gameOver = false;

    public Vector3 enemyAnchor = new Vector3(8.5f, 0, 0);
    public Vector3 playerAnchor = new Vector3(-2.5f, 0, 0);

    public List<EventData> possibleEvents;
    public List<EventData> startingEvents;
    public float enemySpacing;

    public SectorData currentSector = null;
    public WaveData currentWave = null;

    public bool firstEventDone = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;


    }

    // Update is called once per frame
    void Update()
    {

        CleanupShips();
        CheckWaveOver();
        CheckSectorOver();
        CheckGameOver();
    }

    public void CleanupShips()
    {
        List<ShipController> cleanUp = new List<ShipController>();

        foreach (ShipController ship in enemyShips)
        {
            if (!ship)
            {
                cleanUp.Add(ship);
            }
        }
        foreach (ShipController clean in cleanUp)
        {
            enemyShips.Remove(clean);

        }
    }

    public void CheckWaveOver()
    {
        if (!waveOver && enemyShips.Count <= 0)
        {
            StartCoroutine(NewWave());
        }
    }
    public void CheckSectorOver()
    {
        if (!gameOver && !newRound && waveOver && currentSector && waveNumber > WaveMapPanel.instance.waveNodes.Count)
        {
            StartCoroutine(NewRound());
        }


        if (autoRun)
        {
            MapSectorPanel.instance.AutoPick();
        }
    }
    public void CheckGameOver()
    {
        if (!gameOver && !playerShip)
        {
            GameOver();
        }
    }

    public IEnumerator NewWave()
    {
        currentWave = null;
        waveOver = true;
        if (possibleEvents.Count > 0)
        {
            //EventLogUI.instance.AddEntry(possibleEvents[Random.Range(0, possibleEvents.Count)]);
            EventData eventData = null;

            if (!firstEventDone)
            {
                eventData = startingEvents[Random.Range(0, startingEvents.Count)];
                firstEventDone = true;
            }
            else
            {
                eventData = possibleEvents[Random.Range(0, possibleEvents.Count)];
            }

            LogPanelUI.instance.AddEntry("New Event: " + eventData.titleText, LogEntryType.Event);

            EventPopupWindowUI.instance.OpenWindow(eventData);
        }


        Paused();
        while (!EventPopupWindowUI.instance.redeemed)
        {
            yield return null;
        }

        Unpaused();


        Paused();
        while (currentWave == null)
        {
            currentWave = WaveMapPanel.instance.GetCurrentWave();
            yield return null;
        }
        //New Round
        Unpaused();

        waveNumber++;
        WaveMapPanel.instance.UpdateWaveCards();

        /*
        if (waveNumber%wavesPerRound==0)
        {
            yield return new WaitForSeconds(3);

            StartCoroutine(SpawnBoss());
        }
        else
        {
            yield return new WaitForSeconds(1);

            StartCoroutine(SpawnEnemies(Random.Range(1, Mathf.Clamp(waveNumber, 0, 5))));

        }
        */

        StartCoroutine(SpawnWaveEnemies());



        waveOver = false;

    }

    public IEnumerator NewRound()
    {
        waveNumber = 0;
        currentSector = null;
        newRound = true;

        WaveMapPanel.instance.ClearWaves();
        while (currentSector == null)
        {

            currentSector = MapSectorPanel.instance.GetCurrentSector();

            yield return null;
        }

        foreach (WaveData wave in currentSector.waveDatas)
        {
            WaveMapPanel.instance.AddWave(wave);
        }
        //WaveMapPanel.instance.AddWave(currentSector.bossWave);
        roundNumber++;
        MapSectorPanel.instance.UpdateSectorCards();
        newRound = false;

    }

    public IEnumerator SpawnEnemies(int numEnemies = 1)
    {
        float totalLeftSpace = 0;
        float totalRightSpace = 0;
        for (int i = 0; i < numEnemies; i++)
        {
            //float trueRadius = (i / 6) * followRadius + followRadius;
            ShipData newData = Instantiate(possibleShips[Random.Range(0, possibleShips.Count)]);

            ShipController newShip = Instantiate(newData.shipPrefab, enemyAnchor, Quaternion.identity);
            //Vector3 offset = new Vector3(enemySpacing * Mathf.Sin(i), enemySpacing * newShip.stats.GetStat(StatType.Size).GetValue() * Mathf.Cos(i));
            Vector3 offset = new Vector3();

            if (i % 2 == 0)
            {
                if (i != 0)
                    totalLeftSpace += newShip.transform.localScale.x / 2;

                offset = new Vector3(-i * enemySpacing / 2 - totalLeftSpace, 0, 0);
                totalLeftSpace += newShip.transform.localScale.x / 2;

            }
            else
            {
                totalRightSpace += newShip.transform.localScale.x / 2;
                offset = new Vector3(i * ((enemySpacing + 1) / 2) + totalRightSpace, 0, 0);
                totalRightSpace += newShip.transform.localScale.x / 2;
            }
            newShip.transform.position += offset;
            newShip.SetData(newData);
            enemyShips.Add(newShip);
            yield return new WaitForSeconds(1);

        }

    }

    public IEnumerator SpawnWaveEnemies()
    {
        float totalLeftSpace = 0;
        float totalRightSpace = 0;
        int count = 0;
        foreach(ShipData shipData in currentWave.waveShips)
        {
            //float trueRadius = (i / 6) * followRadius + followRadius;
            ShipData newData = Instantiate(shipData);

            ShipController newShip = Instantiate(newData.shipPrefab, enemyAnchor, Quaternion.identity);
            newShip.shipSprite.color = currentWave.sectorData.shipColors[Random.Range(0, currentWave.sectorData.shipColors.Count)];
            //Vector3 offset = new Vector3(enemySpacing * Mathf.Sin(i), enemySpacing * newShip.stats.GetStat(StatType.Size).GetValue() * Mathf.Cos(i));
            Vector3 offset = new Vector3();

            if (count % 2 == 0)
            {
                if (count != 0)
                    totalLeftSpace += newShip.transform.localScale.x / 2;

                offset = new Vector3(-count * enemySpacing / 2 - totalLeftSpace, 0, 0);
                totalLeftSpace += newShip.transform.localScale.x / 2;

            }
            else
            {
                totalRightSpace += newShip.transform.localScale.x / 2;
                offset = new Vector3(count * ((enemySpacing + 1) / 2) + totalRightSpace, 0, 0);
                totalRightSpace += newShip.transform.localScale.x / 2;
            }
            newShip.transform.position += offset;
            newShip.SetData(newData);
            enemyShips.Add(newShip);
            yield return new WaitForSeconds(1);

            count++;
        }

    }

    public IEnumerator SpawnBoss()
    {

        //float trueRadius = (i / 6) * followRadius + followRadius;
        ShipData newData = Instantiate(bossShips[Random.Range(0, bossShips.Count)]);

        ShipController newShip = Instantiate(newData.shipPrefab, enemyAnchor, Quaternion.identity);
        //Vector3 offset = new Vector3(enemySpacing * Mathf.Sin(i), enemySpacing * newShip.stats.GetStat(StatType.Size).GetValue() * Mathf.Cos(i));
        newShip.SetData(newData);
        enemyShips.Add(newShip);
        yield return new WaitForSeconds(1);



    }

    public void GameOver()
    {
        gameOver = true;

        GameOverScreenUI.instance.OpenGameOver();
    }

    public void Restart()
    {
        //Cleanup
        foreach (ShipController ship in enemyShips)
        {
            foreach (Projectile proj in ship.activeProjectiles)
            {
                Destroy(proj.gameObject);
            }
            ship.activeProjectiles.Clear();
            Destroy(ship.gameObject);
        }
        CrewPanelUI.instance.ClearCrew();
        ShipEquipmentPanel.instance.ClearSlots();
        enemyShips.Clear();

        //EventLogUI.instance.ClearLog();
        waveNumber = 0;
        roundNumber = 0;

        firstEventDone = false;
        WaveCardInventoryUI.instance.ClearWaves();
        WaveMapPanel.instance.ClearWaves();
        SectorCardInventoryUI.instance.ClearSectors();
        MapSectorPanel.instance.ClearSectors();

        playerShip = Instantiate(playerShipData.shipPrefab, playerAnchor, Quaternion.identity);
        playerShip.SetData(playerShipData);

        GameOverScreenUI.instance.CloseGameOver();

        gameOver = false;

    }

    public void ToggleAutoRun()
    {
        autoRun = !autoRun;
    }

    public void TogglePaused()
    {
        if (isPaused)
        {
            Unpaused();
        }
        else
        {
            Paused();
        }
    }

    public void Paused()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }
    public void Unpaused()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }
}
