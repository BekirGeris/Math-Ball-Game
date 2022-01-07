using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopTop.GameData
{
    [CreateAssetMenu(menuName = "TopTop/Data/Game Data")]
    public class Data : ScriptableObject
    {
        public string endGameMessage = "";
        public int Count = 0;
        public int TargetCount = 0;
        public int LastCount = 0;
        public bool GameState = false;
        public int HighScore = 0;
    }
}
