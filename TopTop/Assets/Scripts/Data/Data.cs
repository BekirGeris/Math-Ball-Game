using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopTop.GameData
{
    [CreateAssetMenu(menuName = "TopTop/Data/Game Data")]
    public class Data : ScriptableObject
    {
        public string endGameMessage = "";
        public string endButonString = "";
        public int Count;
        public int TargetCount;
        public int LastCount;
        public int Level;
    }
}
