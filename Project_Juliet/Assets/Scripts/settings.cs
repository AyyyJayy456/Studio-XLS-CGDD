using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown aspectDropdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnAspectValueChanged(int index)
    {
        
        string selectedText = aspectDropdown.options[index].text;

        Debug.Log($"Selected Index: {index} | Selected Text: {selectedText}");


        if (index == 0)
        {

        }
        else if (index == 1)
        {

        }
        else if (index == 2)
        {

        }
        else if (index == 3)
        {

        }

    }
    private void antiAliasing()
    {

    }
    private void shadowQuality(int index)
    {
        string selectedText = aspectDropdown.options[index].text;

        Debug.Log($"Selected Index: {index} | Selected Text: {selectedText}");


        if (index == 0)
        {

        }
        else if (index == 1)
        {

        }
        else if (index == 2)
        {

        }
        else if (index == 3)
        {

        }
    }
    private void textureQuality(int index)
    {
        string selectedText = aspectDropdown.options[index].text;

        Debug.Log($"Selected Index: {index} | Selected Text: {selectedText}");


        if (index == 0)
        {

        }
        else if (index == 1)
        {

        }
        else if (index == 2)
        {

        }
        else if (index == 3)
        {

        }
    }
    private void vSync()
    {

    }
}
