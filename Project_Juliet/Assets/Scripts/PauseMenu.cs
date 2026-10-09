using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
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
}
