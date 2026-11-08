using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject backUI;

    
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Spells.inPauseMenu = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Spells.inPauseMenu = false;
        Time.timeScale = 1f;
    }

    public void ResetGame()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Settings()
    {
        settingsUI.SetActive(true);
        backUI.SetActive(true);
        pauseMenuUI.SetActive(false);

    }
    public void backButton()
    {
        backUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        if(settingsUI.activeInHierarchy)
        {
            settingsUI.SetActive(false);
        }
    }
}
