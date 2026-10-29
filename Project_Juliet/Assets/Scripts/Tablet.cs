using UnityEngine;

using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.InputSystem.Interactions;

public class Tablet : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    private int[] correctCode = { 0, 8, 2, 8 };
    public GameObject displayPanel;

    private int[] guess = new int[4];
    private bool puzzleSolved = false;
    public Animator anim;
    public GameObject lockPanel;
    public GameObject photoPanel;
    public Button lockBut;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }


    public void AppendNumber(int number)
    {
        if (puzzleSolved)
            return;

        // Add the number to the display first
        displayText.text += number.ToString();

        // Store the number in the guess array
        guess[displayText.text.Length - 1] = number;

        // Only check when all 4 numbers have been entered
        if (displayText.text.Length >= 4)
        {
            if (correctCode.SequenceEqual(guess))
            {
                puzzleSolved = true;
                Debug.Log("Puzzle Solved!");
                Invoke(nameof(ClearDisplay), .4f);
            }
            else
            {
                Debug.Log("Incorrect code. Try again!");
                // Leave the code visible briefly, then clear it
                Invoke(nameof(ClearDisplay), .1f);
            }
        }
    }
    public void ClearDisplay()
    {
        if (puzzleSolved)
        {
            displayPanel.SetActive(true);
        }
        displayText.text = "";
        System.Array.Clear(guess, 0, guess.Length);
    }
    public void PhotoApp()
    {
        photoPanel.SetActive(true); 
    }
    public void LockApp()
    {
        lockPanel.SetActive(true);
    }
    public void HomeButton()
    {
        if (photoPanel.activeSelf.Equals(true))
        {
            photoPanel.SetActive(false);
        }
        if (lockPanel.activeSelf.Equals(true))
        {
            lockPanel.SetActive(false);
        }
    }
    public void WallUnlocked()
    {
        lockBut.interactable = false;
        anim.SetTrigger("WallDown");
    }
}





