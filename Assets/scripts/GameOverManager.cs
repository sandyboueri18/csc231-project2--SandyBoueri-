
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject gameOverPanel;

    [Header("Player Lives")]
    public int maxLives = 3;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioClip losingSound;

    private int remainingLives;
    private bool gameOver = false;

    private void Start()
    {
        remainingLives = maxLives;
        gameOver = false;
        gameOverPanel.SetActive(false);
    }

    public bool PlayerDied()
    {
        if (gameOver)
            return true;

        remainingLives--;

        Debug.Log("Lives remaining: " + remainingLives);

        if (remainingLives <= 0)
        {
            ShowGameOver();
            return true;
        }

        return false;
    }

    private void ShowGameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);

        backgroundMusic.Stop();
        audioSource.PlayOneShot(losingSound);

        Time.timeScale = 0f;
    }

    public void TryAgain()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }
}
