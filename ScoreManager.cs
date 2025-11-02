using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    [Header("UI")]
    public TextMeshProUGUI scoreText;

    void Start()
    {
        score = 0;
        UpdateText();
    }

    public static void AddScore(int amount)
    {
        score += amount;
        var mgr = FindObjectOfType<ScoreManager>();
        if (mgr) mgr.UpdateText();
    }

    void UpdateText()
    {
        if (scoreText) scoreText.text = "Score: " + score;
    }

    public static int GetScore()
    {
        return score;
    }
}
