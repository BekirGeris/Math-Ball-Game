using System;
using UnityEngine;

namespace TopTop.GameData
{
    [CreateAssetMenu(menuName = "TopTop/Timer/Timer Data")]
    public class Timer : ScriptableObject
    {
        public long firsTime = 0;
        public long time = 0;

        public void StartTimer()
        {
            time = 0;
            firsTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public long getTime()
        {
            if(firsTime > 0)
            {
                time = DateTimeOffset.Now.ToUnixTimeMilliseconds() - firsTime;
            }
            return time;
        }

        public void clearTimer()
        {
            time = 0;
            firsTime = 0;
        }

    }
}