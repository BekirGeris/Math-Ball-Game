using UnityEngine;
using TMPro;
using TopTop.GameData;
using TopTop.Utiles;
using TopTop.Toast;

namespace TopTop.UIController
{ 
    public class GamePanelController : MonoBehaviour
    {
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject adsPanel;
        [SerializeField] private GameObject endPanel;
        [SerializeField] private GameObject sharePanel;
        [SerializeField] private Data gameData;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private ShowToast showToast;

        /*
        [SerializeField] private TextMeshProUGUI message;
        [SerializeField] private TextMeshProUGUI butonText;
        [SerializeField] private Data data;*/

        private void Start()
        {
            highScoreText.text = RuntimeHelper.selectStringByLanguage("Rekor: ", "High Score: ") + PlayerPrefs.GetInt("HighScore");
            gameData.HighScore = PlayerPrefs.GetInt("HighScore");
            menuPanelStart();
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
            highScoreText.text = RuntimeHelper.selectStringByLanguage("Rekor: ", "High Score: ") + PlayerPrefs.GetInt("HighScore");
            gameData.HighScore = PlayerPrefs.GetInt("HighScore");

            menuPanelStart();
        }

        public void tekrar()     
        {
            PlayerPrefs.SetInt("reklam izlendimi", 0);
            started();
        }

        public void menuPanelStart()
        {
            PlayerPrefs.SetInt("reklam izlendimi", 0);
            menuPanel.SetActive(true);
            adsPanel.SetActive(false);
            endPanel.SetActive(false);
            sharePanel.SetActive(false);
        }

        public void openSharePanel()
        {
            if (gameData.HighScore == 0)
            {
                showToast.MyShowToastMethod(RuntimeHelper.selectStringByLanguage("Rekor 0'ken paylaþýlamaz.", "Cannot be shared when high score is 0."));
            }
            else if (PlayerPrefs.GetInt("highScoreIsCurrent", 1) == 0)
            {
                showToast.MyShowToastMethod(RuntimeHelper.selectStringByLanguage("Zaten rekoru paylaþtýnýz.", "You have already shared the high score."));
            }
            else
            {
                sharePanel.SetActive(true);
            }
        }

        public void closeSharePanel()
        {
            sharePanel.SetActive(false);
        }
    }
}