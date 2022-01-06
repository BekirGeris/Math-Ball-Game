using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopTop.Bar
{ 
   
    public class CircleBar : MonoBehaviour
    {
        [SerializeField] private Image circleBar;
        private bool flag = true;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(0, 0, 6));

            if(flag)
            {
                circleBar.fillAmount += 0.01f;
                if(circleBar.fillAmount >= 1)
                {
                    flag = false;
                }
            }
            else
            {
                circleBar.fillAmount -= 0.01f;
                if (circleBar.fillAmount <= 0)
                {
                    flag = true;
                }
            }
        }
    }
}
