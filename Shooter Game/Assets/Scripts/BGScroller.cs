﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeZ;
    private Vector3 startPosition;
    public GameController gameController;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {   if (gameController.points >= 100)
        {
            Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
            scrollSpeed = -20;
        }

            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);

            transform.position = startPosition + Vector3.forward * newPosition;
        }
}
