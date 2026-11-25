using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class LockPuzzle : MonoBehaviour
{
    public GameObject displayPanel;
    public TextMeshProUGUI displayText;
    private int[] correctCode = {1, 2, 3};
    public Image[] images;
  
    private int[] guess = new int[3];
    private bool puzzleSolved = false;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();

      

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(1.0f);
        

        ExitPuzzle();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collided box");
            
                if (displayPanel != null)
                {
                    displayPanel.SetActive(true);
                    Spells.inPauseMenu = true;
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
        images[0].color = new Color32(255, 255, 255, 90);
        images[1].color = new Color32(255, 255, 255, 90);
        images[2].color = new Color32(255, 255, 255, 90);
        displayText.text = "";
    }
    public void ExitPuzzle()
    {
        if(puzzleSolved)
        {
            GetComponent<Collider>().enabled = false;
        }

        displayPanel.SetActive(false);
        Spells.inPauseMenu = false;
    }

    public void OkGuess()
    {

        
            if(correctCode.SequenceEqual(guess))
            {
                images[0].color = new Color32(0, 255, 0, 90);
                images[1].color = new Color32(0, 255, 0, 90);
            images[2].color = new Color32(0, 255, 0, 90);
            puzzleSolved = true;
                anim.SetTrigger("CaveUnlocked");
                 StartCoroutine(StartCountdown());
               
               
            }
            for (int i = 0; i < guess.Length; i++)

            if (guess[i] == correctCode[i])
            {
                images[i].color = new Color32(0, 255, 0, 90);
            }
            else if (correctCode.Contains(guess[i])) { 
                images[i].color = new Color32(255, 215, 0, 90);
            }
            else {
                images[i].color = new Color32(255, 0, 0, 90);
            }
        }
    }




