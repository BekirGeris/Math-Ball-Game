using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TopTop.GameData;
using UnityEngine.Advertisements;

namespace TopTop.Reklam
{
    public class Reklam : MonoBehaviour, IUnityAdsListener
    {
        private string gameID = "4451675";
        private string devmEtReklam = "devamEtOdullu";
        private string banner = "bannerAna-EndSayfa";
        private bool testMode = false;
        private bool bannerActive = false;

        [SerializeField] private Data gameData;
        [SerializeField] private GameObject adsPanel;

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
            if (placementId == devmEtReklam)
            {
                if (showResult == ShowResult.Finished)
                {
                    Debug.Log("AD " + placementId + " COMPLETE");
                    //reklam izlendimi 1 ise izlendi, 0ise izlenmedi
                    PlayerPrefs.SetInt("reklam izlendimi", 1);
                    adsPanel.SetActive(false);
                    gameData.GameState = true;
                }
                else if (showResult == ShowResult.Skipped)
                {
                    Debug.Log("Skipped");
                }
                else if (showResult == ShowResult.Failed)
                {
                    Debug.Log("Failed");
                }
            }
        }
    }
}