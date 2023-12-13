using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;

namespace YemaekHan
{
    public class GameSetting : MonoBehaviour
    {
        private WMSK map;
        private static Country[] mainCountry;

        private void Awake()
        {
            map = WMSK.instance;
            mainCountry = map.countries;
        }

        public static Country getCountry(int index)
        {
            return mainCountry[index];
        }

        public void accessRight(Country selectCountry)
        {
            for (int i = 0; i < mainCountry.Length; i++)
            {
                if (!mainCountry[i].attrib["isPlayer"])
                {
                    mainCountry[i].canCross = false;
                }
            }
        }
    }
}