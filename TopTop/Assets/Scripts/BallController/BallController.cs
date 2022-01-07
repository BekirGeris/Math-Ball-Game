using System;
using System.Collections;
using System.Collections.Generic;
using TopTop.Input;
using TopTop.GameData;
using UnityEngine;
using TMPro;
using Random = System.Random;
using UnityEngine.SceneManagement;

namespace TopTop.BallController
{
    public class BallController : MonoBehaviour
    {
    
        [SerializeField] private AbstractInputData ballInput;
        [SerializeField] private Data data;
        [SerializeField] private BallControllerSettings _controllerSettings;
        [SerializeField] private List<GameObject> shapes;
        [SerializeField] private List<GameObject> numbersOfShapes;
        [SerializeField] private GameObject pointTargetGameObject;
        [SerializeField] private GameObject adsPanel;
        [SerializeField] private GameObject endPanel;        
        [SerializeField] private GameObject proBar;        
        [SerializeField] private GameObject circleBar;        
        [SerializeField] private TextMeshProUGUI messageAds;
        [SerializeField] private TextMeshProUGUI messageEnd;

        TextMeshPro pointTargetText;

        List<TextMeshPro> numbers = new List<TextMeshPro>();

        Random random = new Random();

        List<Color> colors = new List<Color>();

        // Start is called before the first frame update
        void Start()
        {
            circleBar.SetActive(false);

            pointTargetText = pointTargetGameObject.GetComponent<TextMeshPro>();

            data.Count = 0;

            pointTargetText.text = data.Count + "/" + data.TargetCount;
            foreach(var numbersOfShape in numbersOfShapes)
            {
                TextMeshPro t = numbersOfShape.GetComponent<TextMeshPro>();
                numbers.Add(t);
            }

            foreach(var number in numbers)
            {
                number.text = random.Next(1, 20) + "";
            }

            for(int i = 0;i <= shapes.Count; i++)
            {
                colors.Add(new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()));
            }
        }

        // Update is called once per frame
        void Update() //_rigidbody.transform.forward * ballInput.Horizontal * _controllerSettings.HorizontalSpeet
        {

            transform.position = transform.position + new Vector3(0, _controllerSettings.Speed, 0);

            if (ballInput.isTouched && data.GameState == true)
            {
                Vector3 ballTouchPosition = transform.position;
                _controllerSettings.Speed = -1 * _controllerSettings.Speed;

                float min = float.MaxValue;
                int index = -1;
                foreach (var shape in shapes)
                {
                    shape.gameObject.GetComponent<SpriteRenderer>().color = colors[random.Next(0, colors.Count - 1)];

                    if (Vector3.Distance(shape.transform.position, ballTouchPosition) <= min)
                    {
                        min = Vector3.Distance(shape.transform.position, ballTouchPosition);
                        if(min <= 0.4f)
                        {
                            index = shapes.IndexOf(shape);
                        }
                    }
                }
                
                if(index != -1)
                {
                    if ((data.Count + Int32.Parse(numbers[index].text.ToString())) > data.TargetCount)
                    {
                        if(data.Count > data.HighScore)
                        {
                            PlayerPrefs.SetInt("HighScore", data.Count);
                            PlayerPrefs.SetInt("highScoreIsCurrent", 1); //hg artýk güncel deðil
                        }

                        data.endGameMessage = "You missed the target.";
                        messageAds.text = data.endGameMessage;
                        messageEnd.text = data.endGameMessage;
                        data.LastCount = data.Count;
                        data.GameState = false;
                        if (PlayerPrefs.GetInt("reklam izlendimi", 0) == 0)
                        {
                            //reklam izlenecek
                            adsPanel.SetActive(true);
                            proBar.SetActive(true);
                            circleBar.SetActive(false);
                        }
                        else
                        {
                            //reklam zaten izlenmiþ
                            endPanel.SetActive(true);
                        }

                        StartCoroutine(checkInternetConnection((isConnected) => {
                            if (isConnected == false)
                            {
                                //internet baðlantýsý yok
                                endPanel.SetActive(true);
                            }
                        }));
                    }
                    else
                    {
                        adsPanel.SetActive(false);
                        data.Count += Int32.Parse(numbers[index].text.ToString());
                    }
                }
                
                foreach (var number in numbers)
                {
                    number.text = random.Next(1, 20) + "";
                }
                
            }

            pointTargetText.text = data.Count + "/" + data.TargetCount;

            if (data.Count == data.TargetCount)
            {
                data.TargetCount += random.Next(10, 50);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.name.Equals("WallUp") || collision.gameObject.name.Equals("WallDown"))
            {
                _controllerSettings.Speed = -1 * _controllerSettings.Speed;
            }
        }
        public void devamEt() //silinecek
        {
            data.Count = data.LastCount;
            proBar.SetActive(false);
            circleBar.SetActive(true);
            //adsPanel.SetActive(false);
            //data.GameState = true;
        }
            
        public void started()
        {
            data.GameState = true;
            data.Count = 0;
            data.TargetCount = 0;
        }

        IEnumerator checkInternetConnection(Action<bool> action)
        {
            WWW www = new WWW("http://google.com");
            yield return www;
            if (www.error != null)
            {
                action(false);
            }
            else
            {
                action(true);
            }
        }
    }
}