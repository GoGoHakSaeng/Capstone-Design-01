using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using WorldMapStrategyKit;
using TMPro;

namespace YemaekHan
{
    public class DisplayTime : MonoBehaviour
    {
        public static DisplayTime instance;
        public int YearTime = 371;
        public int MonthTime = 1;
        public TMP_Text YearTimeText;
        public TMP_Text MonthTimeText;

        public int realTime = 0;
        private int RealTime
        {
            get
            {
                return realTime;
            }
            set
            {
                if (realTime != value)
                {
                    realTime = value;
                }
            }
        }

        private void Awake()
        {
            if (DisplayTime.instance == null)
            {
                DisplayTime.instance = this;
            }
        }

        public void FixedUpdate()
        {
            RealTime = Convert.ToInt32((WMSK.instance.time / 4) + 1);
            MonthTime = realTime % 13;
            if (MonthTime == 0)
            {
                MonthTime++;
                YearTime++;
                WMSK.instance.time = -1;
            }
            YearTimeText.text = Convert.ToString(YearTime);
            MonthTimeText.text = Convert.ToString(MonthTime);
        }
    }
    
}

