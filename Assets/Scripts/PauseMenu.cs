using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // Ensure the pause menu is hidden at the start
        }
        if (deathMenu != null)
        {
            deathMenu.SetActive(false); // Ensure the pause menu is hidden at the start
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void DeathMenu()
    {
        isPaused = !isPaused;
        deathMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f; // Pauses or resumes the game
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Fase-1");
        Time.timeScale = 1f; // resumes the game
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f; // resumes the game
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f; // Pauses or resumes the game
    }
}
