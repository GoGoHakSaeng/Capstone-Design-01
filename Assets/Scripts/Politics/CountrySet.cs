using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;

namespace YemaekHan
{
    public class CountrySet : MonoBehaviour
    {
        private static string countryName;
        private static int countryIndex;
        private static Country selectedCountry;

        //유저가 선택한 국가, 국가 소유자 등을 저장함

        private static WMSK map;
        private static string[] countryNames = { "ko", "ba", "ga", "si" };

        private void Awake()
        {
            map = WMSK.instance;
        }

        /// <summary>
        /// 유저의 국가를 지정함(매개변수로 country 사용)
        /// </summary>
        public static void setUserCountry(Country targetCountry)
        {
            if (selectedCountry != null)
            {
                selectedCountry.attrib["isPlayer"] = false;
            }

            selectedCountry = targetCountry;
            countryName = targetCountry.name;
            for (int i=0; i<countryNames.Length; i++)
            {
                if (countryNames[i] == countryName)
                {
                    countryIndex = i;
                }
            }
            selectedCountry.attrib["isPlayer"] = true;
        }

        /// <summary>
        /// 유저의 국가를 지정함(매개변수로 index 사용)
        /// </summary>
        public static void setUserCountry(int index)
        {
            if (selectedCountry != null)
            {
                selectedCountry.attrib["isPlayer"] = false;
            }
            countryIndex = index;
            selectedCountry = map.GetCountry(index);
            selectedCountry.attrib["isPlayer"] = true;

            countryName = countryNames[index];
        }
        /*
        /// <summary>
        /// 유저의 국가를 지정함(매개변수로 string 사용)
        /// </summary>
        public static void setUserCountry(string name)
        {
            if (selectedCountry != null)
            {
                selectedCountry.attrib["isPlayer"] = false;
            }

            countryName = name;
            selectedCountry = map.GetCountry(countryName);
            selectedCountry.attrib["isPlayer"] = true;

            for(int i=0; i<countryNames.Length; i++)
            {
                if (name == countryNames[i])
                {
                    countryIndex = i;
                    break;
                }
            }
        }*/

        /// <summary>
        /// 유저의 국가 정보를 가져옴. 매개변수 필요없음. 프로퍼티
        /// </summary>
        public static Country getUserCountry
        {
            get
            {
                return selectedCountry;
            }
        }

        /// <summary>
        /// 유저의 국가 이름을 가져옴. 매개변수 필요없음. 프로퍼티 
        /// </summary>
        public static string getUserCountryName
        {
            get
            {
                return countryName;
            }
        }

        /// <summary>
        /// 유저의 국가 번호를 가져옴. 매개변수 필요없음. 프로퍼티
        /// </summary>
        public static int getUserCountryIndex
        {
            get
            {
                return countryIndex;
            }
        }
    }
}


