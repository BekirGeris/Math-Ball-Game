using System.Collections;
using System.Collections.Generic;
using TMPro;
using TopTop.GameData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopTop.UIController
{
    public class PanelController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI level;
        [SerializeField] private Data data;

        private void Start()
        {
            level.text = "Level " + data.Level;
        }
        public void started()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
