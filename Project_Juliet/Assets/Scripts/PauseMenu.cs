using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject backUI;
    [SerializeField] private GameObject pauseGreyout;
    [SerializeField] private GameObject controlsUI;

    public void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (pauseMenuUI.activeInHierarchy)
            {
                Resume();
            }
            else if (!pauseMenuUI.activeInHierarchy)
            {
                Pause();
            }

        }
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Spells.inPauseMenu = true;
        Time.timeScale = 0f;
        pauseGreyout.SetActive(true);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseGreyout.SetActive(false);
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
        playerUI.SetActive(false);

    }
    public void Controls()
    {
        controlsUI.SetActive(true);
        backUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(false);
    }
    public void backButton()
    {
        backUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        if(settingsUI.activeInHierarchy)
        {
            settingsUI.SetActive(false);
        }
        if (controlsUI.activeInHierarchy)
        {
            controlsUI.SetActive(false);
        }
        if (!playerUI.activeInHierarchy)
        {
            playerUI.SetActive(true);
        }
    }
}
