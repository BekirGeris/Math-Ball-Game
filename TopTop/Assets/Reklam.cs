using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Reklam : MonoBehaviour, IUnityAdsListener
{
    private string gameID = "4451675";
    private string devmEtReklam = "devamEtOdullu";
    private string banner = "bannerAna-EndSayfa";
    private bool testMode = true;
    private bool bannerActive = false;

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, testMode);
    }

    private void Update()
    {
        if (!bannerActive)
        {
            bannerShow();
        }
    }

    public void odulluReklamShow()
    {
        if (Advertisement.IsReady(devmEtReklam))
        {
            Advertisement.Show(devmEtReklam);
        }
    }

    public void bannerShow()
    {
        if (Advertisement.IsReady(banner))
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show(banner);
            bannerActive = true;
        }
    }

    public void bannerHide()
    {
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsReady(string placementId)
    {

    }

    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == devmEtReklam)
        {
            if (showResult == ShowResult.Finished)
            {
                Debug.Log("AD " + placementId +" COMPLETE");
                //reklam izlendimi 1 ise izlendi, 0ise izlenmedi
                PlayerPrefs.SetInt("reklam izlendimi", 1);
            }
            else if (showResult == ShowResult.Skipped)
            {

            }
            else if (showResult == ShowResult.Failed)
            {

            }
        }
    }
}
