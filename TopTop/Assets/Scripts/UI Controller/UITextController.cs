using UnityEngine;
using TopTop.Utiles;
using TMPro;

namespace TopTop.UIController { 
    public class UITextController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI startBtn;
        [SerializeField] private TextMeshProUGUI sharePanelBtn;
        [SerializeField] private TextMeshProUGUI shareBtn;
        [SerializeField] private TextMeshProUGUI keepPlayingBtn;
        [SerializeField] private TextMeshProUGUI skipBtn;
        [SerializeField] private TextMeshProUGUI homePageBtn;
        [SerializeField] private TextMeshProUGUI againBtn;
        [SerializeField] private TextMeshProUGUI sharePanelTxt;
        [SerializeField] private TextMeshProUGUI shareNameHint;

        // Start is called before the first frame update
        void Start()
        {
            startBtn.text = RuntimeHelper.selectStringByLanguage("Ba�la", "Start");
            sharePanelBtn.text = RuntimeHelper.selectStringByLanguage("Rekoru Payla�", "Share High Score");
            shareBtn.text = RuntimeHelper.selectStringByLanguage("Payla�", "Share");
            keepPlayingBtn.text = RuntimeHelper.selectStringByLanguage("Devam ET", "Keep Playing");
            skipBtn.text = RuntimeHelper.selectStringByLanguage("Ge�", "Skip");
            homePageBtn.text = RuntimeHelper.selectStringByLanguage("Ana Sayfa", "Home Page");
            againBtn.text = RuntimeHelper.selectStringByLanguage("Tekrar", "Again");
            sharePanelTxt.text = RuntimeHelper.selectStringByLanguage("Rekoru Payla�", "Share High Score");
            shareNameHint.text = RuntimeHelper.selectStringByLanguage("Ad giriniz...", "Enter Name...");
        }

    }
}