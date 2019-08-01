using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text PointText;
    public Text restartText;
    public Text gameOverText;
    public Text creditsText;

    private bool gameOver;
    private bool restart;
    private int points;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        creditsText.text = "";
        points = 0;
        Update();
        StartCoroutine (SpawnWaves());

        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }


    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddPoints(int newPointsValue)
    {
        points += newPointsValue;
        UpdatePoints();
    }

    void UpdatePoints()
    {
        PointText.text = "Points: " + points;

            if (points >= 100)
            {
                gameOverText.text = "You win!";
                gameOver = true;
                restart = true;
                creditsText.text = "Game Created By Emily Moritz";
            }
     }

    public void GameOver ()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
}
