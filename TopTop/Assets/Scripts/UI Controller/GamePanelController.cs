using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using TopTop.GameData;

namespace TopTop.UIController
{ 
    public class GamePanelController : MonoBehaviour
    {
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject adsPanel;
        [SerializeField] private GameObject endPanel;
        [SerializeField] private Data gameData;
        [SerializeField] private TextMeshProUGUI highScoreText;

        /*
        [SerializeField] private TextMeshProUGUI message;
        [SerializeField] private TextMeshProUGUI butonText;
        [SerializeField] private Data data;*/

        private void Start()
        {
            highScoreText.text = "HighScore:" + PlayerPrefs.GetInt("HighScore");
            gameData.HighScore = PlayerPrefs.GetInt("HighScore");

            menuPanel.SetActive(true);
            adsPanel.SetActive(false);
            endPanel.SetActive(false);
        }

        public void stoped()
        {
            menuPanel.SetActive(false);
            adsPanel.SetActive(true);
            endPanel.SetActive(false);
        }

        public void started()
        {
            menuPanel.SetActive(false);
            adsPanel.SetActive(false);
            endPanel.SetActive(false);
            gameData.GameState = true;
        }

        public void goHomePage()
        {
            highScoreText.text = "HighScore:" + PlayerPrefs.GetInt("HighScore");
            gameData.HighScore = PlayerPrefs.GetInt("HighScore");

            menuPanel.SetActive(true);
            adsPanel.SetActive(false);
            endPanel.SetActive(false);
        }

        public void tekrar()
        {
            started();
        }
    }
}