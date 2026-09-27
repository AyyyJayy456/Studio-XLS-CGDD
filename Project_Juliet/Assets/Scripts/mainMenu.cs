using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject initialMenu;
    public GameObject backButton;
    void Start()
    {
        initialMenu = GameObject.FindGameObjectWithTag("initial");
        backButton = GameObject.FindGameObjectWithTag("back");
        backButton.SetActive(false);
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
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
