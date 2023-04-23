using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TMP_Text))]
public class Score : MonoBehaviour{
    private TMPro.TMP_Text scoreText;
    private int score = 0;

    void UpdateScoreText(){
        // Set the text to "Score: " with the current score
        scoreText.SetText("Score: " + score);
    }

    void Start(){
        // Retrieve the TMPro.TMP_Text component of the game object, which will display the score
        scoreText = GetComponent<TMPro.TMP_Text>();

        // Update the current score
        UpdateScoreText();
    }

    public void AddPoints(int points){
        // Add points to the current score, and update the score text
        score += points;
        UpdateScoreText();
    }
}
