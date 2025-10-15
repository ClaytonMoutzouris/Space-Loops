using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreenUI : MonoBehaviour
{
    public static GameOverScreenUI instance;
    public GameObject leftStats;
    public GameObject rightStats;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public UIStatObject prefab;
    public List<UIStatObject> statObjects = new List<UIStatObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        gameObject.SetActive(false);

    }

    public void OpenGameOver()
    {
        string leftString = "";
        string rightString = "";
        gameObject.SetActive(true);
    }

    public void CloseGameOver()
    {
        gameObject.SetActive(false);

    }

    public void DisplayStats(ShipController ship)
    {
        ClearStats();

        UIStatObject statObject = Instantiate(prefab, leftStats.transform);
        statObject.SetStat("Kills", ship.totalKills);
        statObjects.Add(statObject);

        statObject = Instantiate(prefab, leftStats.transform);
        statObject.SetStat("Shots", ship.totalShots);
        statObjects.Add(statObject);

        statObject = Instantiate(prefab, leftStats.transform);
        statObject.SetStat("Hits", ship.totalHits);
        statObjects.Add(statObject);

        statObject = Instantiate(prefab, leftStats.transform);
        statObject.SetStat("Accuracy", ((ship.totalHits/ship.totalShots)*100));
        statObjects.Add(statObject);


        statObject = Instantiate(prefab, leftStats.transform);
        statObject.SetStat("Damage Dealt", ship.totalDamageDealt, true);
        statObjects.Add(statObject);

        statObject = Instantiate(prefab, leftStats.transform);
        statObject.SetStat("Damage Taken", ship.totalDamageTaken, true);
        statObjects.Add(statObject);

        statObject = Instantiate(prefab, leftStats.transform);
        statObject.SetStat("Damage Blocked", ship.totalDamageBlocked, true);
        statObjects.Add(statObject);

        statObject = Instantiate(prefab, leftStats.transform);
        statObject.SetStat("Evades", ship.totalEvades);
        statObjects.Add(statObject);

        statObject = Instantiate(prefab, rightStats.transform);
        statObject.SetStat("Round", GameManager.instance.roundNumber);
        statObjects.Add(statObject);

        statObject = Instantiate(prefab, rightStats.transform);
        statObject.SetStat("Wave", GameManager.instance.waveNumber);
        statObjects.Add(statObject);

        statObject = Instantiate(prefab, rightStats.transform);
        statObject.SetStat("Time", Time.time, true);
        statObjects.Add(statObject);

        statObject = Instantiate(prefab, rightStats.transform);
        statObject.SetStat("Currency", ship.shipData.currency);
        statObjects.Add(statObject);
        //statsText.text = text;

        int score = CalculateScore(ship);
        int highscore = 0;
        
        highscore = PlayerPrefs.GetInt("highscore", highscore);

        if (score > highscore)
        {
            highscore = score;
        }

        PlayerPrefs.SetInt("highscore", highscore);
        highScoreText.text = highscore.ToString();
        scoreText.text = score.ToString();
    }


    public int CalculateScore(ShipController ship)
    {
        int score = 0;

        //money
        score += ship.shipData.currency;

        //rounds/waves
        score += GameManager.instance.roundNumber * GameManager.instance.roundNumber * 10;
        score += GameManager.instance.waveNumber * GameManager.instance.waveNumber * 2;

        score += ship.totalKills * 2;



        score += 100 * (ship.totalHits / ship.totalShots);

        return score;
    }

    public void ClearStats()
    {
        foreach (UIStatObject obj in statObjects)
        {
            Destroy(obj.gameObject);
        }

        statObjects.Clear();
    }

}
