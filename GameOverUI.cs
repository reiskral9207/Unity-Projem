using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("Refs")]
    public GameObject panel;        // GameOverPanel
    public Button restartButton;    // RestartBtn

    void Awake()
    {
        if (panel) panel.SetActive(false);           // Oyunun başında gizle
        if (restartButton) restartButton.onClick.AddListener(Restart);
    }

    public void Show()
    {
        if (panel) panel.SetActive(true);
        Time.timeScale = 0f;                         // Oyunu durdur
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
