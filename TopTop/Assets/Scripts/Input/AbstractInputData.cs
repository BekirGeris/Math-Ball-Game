using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopTop.Input
{
    public abstract class AbstractInputData : ScriptableObject
    {

        public bool isTouched = false;

        public float Horizontal;
        public float Vertical;
        public abstract void ProcessInput(bool isActive);
        public abstract void ProcessInput();
    }
}