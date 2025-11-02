using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    [Header("Can Ayarları")]
    public int maxBaseHealth = 100;    // Base'in maksimum canı
    public int maxPlayerHealth = 100;   // Oyuncunun maksimum canı
    
    [Header("UI Elementleri")]
    public Slider baseHealthBar;        // Base can barı
    public Slider playerHealthBar;      // Oyuncu can barı
    public TextMeshProUGUI gameOverText; // Oyun sonu yazısı

    private int currentBaseHealth;      // Base'in mevcut canı
    private int currentPlayerHealth;    // Oyuncunun mevcut canı

    void Start()
    {
        // Başlangıç canlarını ayarla
        currentBaseHealth = maxBaseHealth;
        currentPlayerHealth = maxPlayerHealth;

        // UI'ı güncelle
        UpdateHealthUI();
    }

    // Base'e hasar ver
    public void DamageBase(int damage)
    {
        currentBaseHealth -= damage;
        UpdateHealthUI();
        CheckGameOver();
    }

    // Oyuncuya hasar ver
    public void DamagePlayer(int damage)
    {
        currentPlayerHealth -= damage;
        UpdateHealthUI();
        CheckGameOver();
    }

    // Can barlarını güncelle
    void UpdateHealthUI()
    {
        if (baseHealthBar)
        {
            baseHealthBar.value = (float)currentBaseHealth / maxBaseHealth;
        }
        if (playerHealthBar)
        {
            playerHealthBar.value = (float)currentPlayerHealth / maxPlayerHealth;
        }
    }

    // Oyun bitti mi kontrol et
    void CheckGameOver()
    {
        if (currentBaseHealth <= 0 || currentPlayerHealth <= 0)
        {
            // GameOver UI'ı göster
            var gameOverUI = FindObjectOfType<GameOverUI>();
            if (gameOverUI)
            {
                gameOverUI.Show();
            }

            if (gameOverText)
            {
                gameOverText.gameObject.SetActive(true);
                try
                {
                    gameOverText.text = "Game Over\nScore: " + ScoreManager.GetScore();
                }
                catch
                {
                    gameOverText.text = "Game Over";
                }
            }
            Time.timeScale = 0; // Oyunu durdur
        }
    }

    // Oyuncunun canını yenile
    public void HealPlayer(int healAmount)
    {
        currentPlayerHealth = Mathf.Min(currentPlayerHealth + healAmount, maxPlayerHealth);
        UpdateHealthUI();
    }
}