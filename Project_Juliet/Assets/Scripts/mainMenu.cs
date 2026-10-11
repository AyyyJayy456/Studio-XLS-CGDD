using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    [SerializeField] private GameObject initialMenu;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject settingMenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void settingsMenu()
    {
        initialMenu.SetActive(false);
        settingMenu.SetActive(true);
        backButton.SetActive(true);
    }
    public void creditsMenu()
    {
        initialMenu.SetActive(false);
        backButton.SetActive(true);
    }
    public void back()
    {
        initialMenu.SetActive(true);
        backButton.SetActive(false);
        if (settingMenu.activeInHierarchy)
        {
            settingMenu.SetActive(false);
        }
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
