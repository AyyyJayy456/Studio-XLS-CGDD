using TMPro;
using UnityEngine;

public class PadButtons : MonoBehaviour
{
   int dialedNumber = 0;
    public TextMeshProUGUI dialTxt;

    public void LeftButton()
    {
        if(dialedNumber> 0)
        {
            dialedNumber--;
            dialTxt.text = dialedNumber.ToString();
        }
        else
        {
            dialedNumber = 9;
            dialTxt.text = dialedNumber.ToString();
        }
    }
    public void RightButton()
    {
        if(dialedNumber < 9)
        {
            dialedNumber++;
            dialTxt.text = dialedNumber.ToString();
        }
        else
        {
            dialedNumber = 0;
            dialTxt.text = dialedNumber.ToString();
        }
    }

}
