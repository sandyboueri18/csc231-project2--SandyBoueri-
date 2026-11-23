
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenuPanel;
    public GameObject mainMenuPanel;
    public GameObject levelCompletePanel;
    public GameObject gameOverPanel;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
           
            if (mainMenuPanel.activeSelf ||levelCompletePanel.activeSelf ||gameOverPanel.activeSelf)
                return;

            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

        Debug.Log("Quitting game...");

        Application.Quit();
    }
}
