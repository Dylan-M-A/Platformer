using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int score;
    private string scoreText;

    private void Awake()
    {
        Instance = this;
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        // Assuming you have a UI Text element to display the score
        scoreText = "Score: " + score;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            ScoreManager.Instance.AddPoints(10);
            Destroy(other.gameObject);
        }
    }
}
