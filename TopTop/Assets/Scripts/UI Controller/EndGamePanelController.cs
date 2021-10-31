using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using TopTop.GameData;

namespace TopTop.UIController
{
    public class EndGamePanelController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI message;
        [SerializeField] private TextMeshProUGUI butonText;
        [SerializeField] private Data data;

        private void Start()
        {
            message.text = data.endGameMessage;
            butonText.text = data.endButonString;
        }

        public void stoped()
        {
            SceneManager.LoadScene("SampleScene");
        }

        public void tekrar()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}