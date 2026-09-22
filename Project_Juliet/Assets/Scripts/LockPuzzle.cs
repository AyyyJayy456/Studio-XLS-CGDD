using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LockPuzzle : MonoBehaviour
{
    public GameObject displayPanel;
    public TextMeshProUGUI displayText;
    private int[] correctCode = {1, 2, 3};
    public Image[] images;
  
    private int[] guess = new int[3];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collided box");
            
                if (displayPanel != null)
                {
                    displayPanel.SetActive(true);
                }
            
        }
    }
    public void AppendNumber(int number)
    {
        if(displayText.text.Length >= 3)
        {
            return;
        }
        displayText.text += number.ToString();
        guess[displayText.text.Length - 1] = number;
    }
    public void ClearDisplay()
    {
        guess = new int[3];
        images[0].color = Color.white;
        images[1].color = Color.white;
        images[2].color = Color.white;
        displayText.text = "";
    }
    public void ExitPuzzle()
    {
        displayPanel.SetActive(false);
    }

    public void OkGuess()
    {
        for(int i = 0; i <guess.Length; i++)
        {
            if (guess[i] == correctCode[i])
            {
                images[i].color = Color.green; 
            }
            else if (correctCode.Contains(guess[i])) { 
                images[i].color = Color.yellow;
            }
            else {
                images[i].color = Color.red;
            }
        }
    }


}




