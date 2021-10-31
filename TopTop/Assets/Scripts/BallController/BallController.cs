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
        [SerializeField] private GameObject level;
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI message;

        TextMeshPro pointTargetText;
        TextMeshPro levelText;

        List<TextMeshPro> numbers = new List<TextMeshPro>();

        Random random = new Random();

        List<Color> colors = new List<Color>();

        // Start is called before the first frame update
        void Start()
        {
            pointTargetText = pointTargetGameObject.GetComponent<TextMeshPro>();
            levelText = level.GetComponent<TextMeshPro>();

            if (data.endGameMessage == "" || data.endGameMessage == "Seviye Tamamlandý")
            {
                data.LastCount = data.Count;
                data.Level++;
                data.TargetCount += random.Next(10, 50);
            }
            else
            {
                data.Count = data.LastCount;
            }
            levelText.text = "Level " + data.Level;

            pointTargetText.text = data.Count + "/" + data.TargetCount;
            foreach(var numbersOfShape in numbersOfShapes)
            {
                TextMeshPro t = numbersOfShape.GetComponent<TextMeshPro>();
                numbers.Add(t);
            }

            foreach(var number in numbers)
            {
                number.text = random.Next(1, 10) + "";
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

            if (ballInput.isTouched && panel.activeSelf == false)
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
                        data.endGameMessage = "Hedefi ýskaladýnýz.";
                        data.endButonString = "Tekrar";
                        message.text = data.endGameMessage;
                        panel.SetActive(true);
                    }
                    else
                    {
                        panel.SetActive(false);
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
                data.endGameMessage = "Seviye Tamamlandý";
                data.endButonString = "Sonraki Seviye";
                SceneManager.LoadScene("EndGame");
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
            Debug.Log("Reklam gösterildi");
            panel.SetActive(false);
        }
    }
}