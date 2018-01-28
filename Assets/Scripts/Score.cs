﻿using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    public int score = 0;                   // The player's score.


    private DogController playerControl;    // Reference to the player control script.
    //private int previousScore = 0;          // The score in the previous frame.


    void Awake()
    {
        // Setting up the reference.
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<DogController>();
    }


    void Update()
    {
        // Set the score text.
        //GetComponent<GUIText>().text = "Score: " + score;
    }
}
