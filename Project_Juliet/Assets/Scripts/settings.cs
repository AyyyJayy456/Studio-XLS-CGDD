using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown aspectDropdown;

    [SerializeField] private bool AA = true;
    [SerializeField] private bool Vsync = true;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnAspectValueChanged(int index)
    {
        
        string selectedText = aspectDropdown.options[index].text;

        Debug.Log($"Selected Index: {index} | Selected Text: {selectedText}");


        if (index == 0)
        {
            Screen.SetResolution(1920,1080, true);
        }
        else if (index == 1)
        {
            Screen.SetResolution(1280, 720, true);
        }
        else if (index == 2)
        {
            Screen.SetResolution(2560, 1440, true);
        }
        else if (index == 3)
        {
            Screen.SetResolution(3840, 2160, true);
        }

    }
    public void antiAliasing()
    {
       
        if (AA)
        {
            AA = false;
            QualitySettings.antiAliasing = 0;
            Debug.Log("AA enabled");
            
        }
        else if (!AA)
        {
            AA=true;
            QualitySettings.antiAliasing = 4;
            Debug.Log("AA disabled");
        }
    }
    public void shadowQuality(int index)
    {
        string selectedText = aspectDropdown.options[index].text;

        Debug.Log($"Selected Index: {index} | Selected Text: {selectedText}");


        if (index == 0)
        {
            QualitySettings.shadowResolution = UnityEngine.ShadowResolution.High;
        }
        else if (index == 1)
        {
            QualitySettings.shadowResolution = UnityEngine.ShadowResolution.Medium;
        }
        else if (index == 2)
        {
            QualitySettings.shadowResolution = UnityEngine.ShadowResolution.Low;
        }
        
    }
    
    public void vSync()
    {
        if (AA)
        {
            Vsync = false;
            QualitySettings.vSyncCount = 0;

        }
        else if (!AA)
        {
            Vsync = true;
            QualitySettings.vSyncCount = 1;
        }
    }
}
