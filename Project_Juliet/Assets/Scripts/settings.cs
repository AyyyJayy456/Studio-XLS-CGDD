using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
    [Header("UI Dropdowns")]
    [SerializeField] private TMP_Dropdown aspectDropdown;
    [SerializeField] private TMP_Dropdown shadowDropdown;

    [Header("UI Toggles")]
    [SerializeField] private Toggle aaToggle;
    [SerializeField] private Toggle vsyncToggle; 
    private bool AA = true;
    private bool Vsync = true;

    
    private const string AspectKey = "Setting_AspectIndex";
    private const string AAKey = "Setting_AntiAliasing";
    private const string VsyncKey = "Setting_VSync";
    private const string ShadowKey = "Setting_ShadowQuality";

    void Start()
    {
        LoadAndApplySettings();
    }

    private void LoadAndApplySettings()
    {
        
        int savedAspect = PlayerPrefs.GetInt(AspectKey, 0);
        if (aspectDropdown != null)
        {
            aspectDropdown.value = savedAspect;
            aspectDropdown.RefreshShownValue();
        }
        ApplyResolution(savedAspect);

        
        int savedAA = PlayerPrefs.GetInt(AAKey, 1);
        AA = (savedAA == 1);
        QualitySettings.antiAliasing = AA ? 4 : 0;
        if (aaToggle != null)
        {
            aaToggle.isOn = AA;
        }

        
        int savedVsync = PlayerPrefs.GetInt(VsyncKey, 1);
        Vsync = (savedVsync == 1);
        QualitySettings.vSyncCount = Vsync ? 1 : 0;
        if (vsyncToggle != null)
        {
            vsyncToggle.isOn = Vsync;
        }

        
        int savedShadow = PlayerPrefs.GetInt(ShadowKey, 0);
        if (shadowDropdown != null)
        {
            shadowDropdown.value = savedShadow;
            shadowDropdown.RefreshShownValue();
        }
        ApplyShadowQuality(savedShadow);

        Debug.Log("Settings successfully loaded and UI synchronized.");
    }

    public void OnAspectValueChanged(int index)
    {
        if (aspectDropdown == null) return;
        string selectedText = aspectDropdown.options[index].text;
        Debug.Log($"Selected Aspect Index: {index} | Selected Text: {selectedText}");

        ApplyResolution(index);

        PlayerPrefs.SetInt(AspectKey, index);
        PlayerPrefs.Save();
    }

    private void ApplyResolution(int index)
    {
        if (index == 0) Screen.SetResolution(1920, 1080, true);
        else if (index == 1) Screen.SetResolution(1280, 720, true);
        else if (index == 2) Screen.SetResolution(2560, 1440, true);
        else if (index == 3) Screen.SetResolution(3840, 2160, true);
    }

    
    public void antiAliasing(bool isOn)
    {
        AA = isOn;
        QualitySettings.antiAliasing = AA ? 4 : 0;
        Debug.Log($"AA updated from UI. Enabled: {AA}");

        PlayerPrefs.SetInt(AAKey, AA ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void shadowQuality(int index)
    {
        if (shadowDropdown == null) return;
        string selectedText = shadowDropdown.options[index].text;
        Debug.Log($"Selected Shadow Index: {index} | Selected Text: {selectedText}");

        ApplyShadowQuality(index);

        PlayerPrefs.SetInt(ShadowKey, index);
        PlayerPrefs.Save();
    }

    private void ApplyShadowQuality(int index)
    {
        if (index == 0) QualitySettings.shadowResolution = UnityEngine.ShadowResolution.High;
        else if (index == 1) QualitySettings.shadowResolution = UnityEngine.ShadowResolution.Medium;
        else if (index == 2) QualitySettings.shadowResolution = UnityEngine.ShadowResolution.Low;
    }

    
    public void vSync(bool isOn)
    {
        Vsync = isOn;
        QualitySettings.vSyncCount = Vsync ? 1 : 0;
        Debug.Log($"VSync updated from UI. Enabled: {Vsync}");

        PlayerPrefs.SetInt(VsyncKey, Vsync ? 1 : 0);
        PlayerPrefs.Save();
    }
}
