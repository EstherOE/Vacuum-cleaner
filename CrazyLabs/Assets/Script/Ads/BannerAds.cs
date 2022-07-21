using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class BannerAds : MonoBehaviour
{
    public AdsManager adsManager;

    public Button showBannerBtn;

    public Button hideBannerBtn;

    // public Button toggleBannerBtn;

    public Text debugLogText;

    private string textLog = "DEBUG LOG: \n";

    private void Awake()
    {
        //if you didn't assign in the inspector
        if (adsManager == null)
        {
            adsManager = FindObjectOfType<AdsManager>();
        }
    }

    private void Start()
    {
        // Initialize Admanager
        adsManager.Initialize();

        // toggleBannerBtn.onClick.AddListener(unityAdsManager.ToggleBanner);

        showBannerBtn.onClick.AddListener(adsManager.ShowBanner);

        hideBannerBtn.onClick.AddListener(adsManager.HideBanner);
    }

    private void OnEnable()
    {
        AdsManager.OnDebugLog += HandleDebugLog;
    }

    private void OnDisable()
    {
        AdsManager.OnDebugLog -= HandleDebugLog;
    }


    void HandleDebugLog(string msg)
    {
        textLog += "\n" + msg + "\n";
        debugLogText.text = textLog;
    }
}

