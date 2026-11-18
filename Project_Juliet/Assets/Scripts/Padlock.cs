using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Padlock : MonoBehaviour
{
    private int correctCode = 0828;
    int enteredCode;
    public TextMeshProUGUI firstTxt;
    public TextMeshProUGUI secondTxt;
    public TextMeshProUGUI thirdTxt;
    public TextMeshProUGUI fourthTxt;
    public Image img;
    public Sprite newImg;
    public Animator anim;
    private void Start()
    {

    }
    public void Update()
    {
        CheckCode();
    }
    public void CheckCode()
    {
        string codeString = firstTxt.text + secondTxt.text + thirdTxt.text + fourthTxt.text;
        enteredCode = int.Parse(codeString);

        if (enteredCode == correctCode)
        {
            Debug.Log("Correct code entered!");
            img.sprite = newImg;
            Invoke(nameof(Leave), 0.5f);
            Invoke(nameof(WallUnlocked), 0.8f);
        }
        else
        {
        }
    }
    public void WallUnlocked()
    {
        anim.SetTrigger("WallDown");
    }
    public void Leave()
    {
        gameObject.SetActive(false);
    }
}

