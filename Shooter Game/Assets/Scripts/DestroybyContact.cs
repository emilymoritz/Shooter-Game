﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroybyContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot Find 'GameController' script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
<<<<<<< Updated upstream
        if (other.tag == "Boundary")
=======
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("Pick Up"))
>>>>>>> Stashed changes
        {
            return;
        }
    Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
    gameController.AddScore(scoreValue);
    Destroy(other.gameObject);
    Destroy(gameObject);

    }

}
