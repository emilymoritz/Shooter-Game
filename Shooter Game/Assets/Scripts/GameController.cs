using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject pickup;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text restartText;
    public Text gameOverText;
    public Text creditsText;
    public Text PointText;

    private bool gameOver;
    private bool restart;
    private int score;
    public int points;

    public AudioSource audioSource;
    public AudioClip music_background;
    public AudioClip victory_music;
    public AudioClip lose_sound;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSource = audioSources[0];
        music_background = audioSources[0].clip;
        victory_music = audioSources[1].clip;
        lose_sound = audioSources[2].clip;
   
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
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            
            yield return new WaitForSeconds(waveWait);

            while (true)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(pickup, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(waveWait);

                break;
            }

            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        {

        ScoreText.text = "Score: " + score;
        }
        PointText.text = "Points: " + points;

            if (points >= 100)
            {
                gameOverText.text = "You win!";
                gameOver = true;
                restart = true;
                creditsText.text = "Game Created By Emily Moritz";
                audioSource.PlayOneShot(victory_music);
            }
     }

    public void GameOver ()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
        audioSource.PlayOneShot(lose_sound);
    }
}
