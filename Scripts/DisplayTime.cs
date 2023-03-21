<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using WorldMapStrategyKit;
using TMPro;

public class DisplayTime : MonoBehaviour
{
    public int YearTime = 371;
    public int MonthTime = 1;
    public TMP_Text YearTimeText;
    public TMP_Text MonthTimeText;

    public void Start()
    {
        
    }

    public void FixedUpdate()
    {
        MonthTime = Convert.ToInt32((WMSK.instance.time / 4) + 1) % 13;
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
=======
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using WorldMapStrategyKit;
using TMPro;

public class DisplayTime : MonoBehaviour
{
    public int YearTime = 371;
    public int MonthTime = 1;
    public TMP_Text YearTimeText;
    public TMP_Text MonthTimeText;

    public void Start()
    {
        
    }

    public void FixedUpdate()
    {
        MonthTime = Convert.ToInt32((WMSK.instance.time / 4) + 1) % 13;
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
>>>>>>> 68f8efb2a846b970304649f1d187e665da684a14
