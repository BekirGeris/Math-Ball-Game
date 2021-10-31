using System;
using System.Collections;
using System.Collections.Generic;
using TopTop.Input;
using UnityEngine;

namespace TopDownShooter.PlayerInput
{
    [CreateAssetMenu(menuName = "TopTop/Input/Input Data")]
    public class InputData : AbstractInputData
    {
        Touch theTouch;
        
        [Header("Input On/Off")]
        [SerializeField] private bool _isActive;

        [Header("Axis Base Control")] 
        [SerializeField] private bool _axisActive;
        [SerializeField] private string AxisNameHorizontal;
        [SerializeField] private string AxisNameVertical;

        [Header("Key Base Control")]
        [SerializeField] private bool _keyBaseHorizontalActive;
        [SerializeField] public KeyCode PositiveHorizontalKeyCode;
        [SerializeField] public KeyCode NegativeHorizontalKeyCode;
        [SerializeField] private bool _keyBaseVerticalActive;
        [SerializeField] public KeyCode PositiveVerticalKeyCode;
        [SerializeField] public KeyCode NegativeVerticalKeyCode;
        [SerializeField] private float _increaseAmount = 0.015f;

        [Header("Ekran Control")]
        [SerializeField] private bool _isEkranKontrol;
        [SerializeField] public KeyCode touchButton;

        public override void ProcessInput(bool _isActive)
        {
            isTouched = false;

            if (_isActive)
            {
                if (_isEkranKontrol)
                {
                    if(Input.touchCount > 0)
                    {
                        theTouch = Input.GetTouch(0);
                        if(theTouch.phase == TouchPhase.Began)
                        {
                            isTouched = true;
                        }

                    }

                    if (Input.GetKeyDown(touchButton))
                    {
                        isTouched = true;
                    }
                }
                else
                {
                    if (_axisActive)
                    {
                        Horizontal = Input.GetAxis(AxisNameHorizontal.ToString());
                        Vertical = Input.GetAxis(AxisNameVertical.ToString());
                    }
                    else
                    {
                        if (_keyBaseHorizontalActive)
                        {
                            KeyBaseAxisControl(ref Horizontal, PositiveHorizontalKeyCode, NegativeHorizontalKeyCode);
                        }
                        if (_keyBaseVerticalActive)
                        {
                            KeyBaseAxisControl(ref Vertical, PositiveVerticalKeyCode, NegativeVerticalKeyCode);
                        }
                    }
                }
            }
        }

        private Vector2 Vector2(double v1, double v2)
        {
            throw new NotImplementedException();
        }

        private Vector2 Vector3(double v1, double v2)
        {
            throw new NotImplementedException();
        }

        private void KeyBaseAxisControl(ref float value, KeyCode positive, KeyCode negative)
        {
            bool positiveActive = Input.GetKey(positive);
            bool negativeActive = Input.GetKey(negative);
            if (positiveActive)
            {
                value += _increaseAmount;
            }
            else if (negativeActive)
            {
                value -= _increaseAmount;
            }
            else
            {
                value = 0;
            }

            value = Mathf.Clamp(value, -1, 1);
        }

        public override void ProcessInput()
        {
            this.ProcessInput(_isActive);
        }
    }
}
