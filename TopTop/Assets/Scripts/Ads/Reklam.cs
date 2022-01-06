using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TopTop.GameData;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using TopTop.Toast;

namespace TopTop.Reklam
{
    public class Reklam : MonoBehaviour, IUnityAdsListener
    {
        private string gameID = "4451675";
        private string devmEtReklam = "devamEtOdullu";
        private string banner = "bannerAna-EndSayfa";
        private bool testMode = false;
        private bool bannerActive = false;
        private bool flag;

        [SerializeField] private Data gameData;
        [SerializeField] private GameObject adsPanel;
        [SerializeField] private GameObject endPanel;
        [SerializeField] private Timer timer;
        [SerializeField] private ShowToast showToast;

        void Start()
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameID, testMode);

            flag = true;
            timer.clearTimer();
        }

        private void Update()
        {
            if (!bannerActive)
            {
                bannerShow();
            }

            if(timer.getTime() > 3000 && flag)
            {
                timer.clearTimer();
                adsPanel.SetActive(false);
                endPanel.SetActive(true);
                flag = false;
                showToast.MyShowToastMethod("Ad Failed to Show...");
            }

            if(timer.getTime() < 1000 && timer.getTime() > 0)
            {
                flag = true;
                odulluReklamShow();
            }
        }

        public void odulluReklamShow()
        {
            if (Advertisement.IsReady(devmEtReklam))
            {
                timer.clearTimer();
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
                    //reklam izlendimi 1 ise izlendi, 0 ise izlenmedi
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
                timer.clearTimer();
            }
        }
    }
}