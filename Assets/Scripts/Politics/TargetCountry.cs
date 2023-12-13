using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;

namespace YemaekHan
{
    public class TargetCountry
    {
        private static Country targetCountry;
        private static int targetCountryIndex;
        private static string targetCountryName;
        private static string[] countryNames = { "고구려", "백제", "가야", "신라" };

        // 유저가 맵에서 선택한 국가를 저장함
        // 초기 값은 유저의 국가를 저장하고 있음.

        /// <summary>
        /// 플레이어가 지목한 국가 저장. 매개변수는 Country형.
        /// </summary>
        /// <param name="country"></param>
        public static void setTargetCountry(Country country)
        {
            targetCountry = country;
            targetCountryName = country.name;
            for (int i = 0; i< countryNames.Length; i++)
            {
                if (countryNames[i] == targetCountryName)
                {
                    targetCountryIndex = i;
                }
            }
        }

        /// <summary>
        /// 플레이어가 지목한 국가 저장. 매개변수는 int형.
        /// </summary>
        /// <param name="country"></param>
        public static void setTargetCountry(int countryIndex)
        {
            targetCountry = WMSK.instance.GetCountry(countryIndex);
            targetCountryName = targetCountry.name;
            targetCountryIndex = countryIndex;
        }

        /// <summary>
        /// 플레이어가 지목한 국가 저장. 매개변수는 string형.
        /// </summary>
        /// <param name="country"></param>
        public static void setTargetCountry(string countryName)
        {
            targetCountry = WMSK.instance.GetCountry(countryName);
            targetCountryName = countryName;
            for (int i = 0; i < countryNames.Length; i++)
            {
                if (countryNames[i] == countryName)
                {
                    targetCountryIndex = i;
                }
            }
        }

        /// <summary>
        /// 지정한 도시 반환. Country형
        /// </summary>
        public static Country getTargetCountry
        {
            get
            {
                return targetCountry;
            }
        }

        /// <summary>
        /// 지정한 도시 반환. int형
        /// </summary>
        public static int getTargetCountryIndex
        {
            get
            {
                return targetCountryIndex;
            }
        }

        /// <summary>
        /// 지정한 도시 반환. string형
        /// </summary>
        public static string getTargetCountryName
        {
            get
            {
                return targetCountryName;
            }
        }

        /// <summary>
        /// 지정 국가 초기화.
        /// </summary>
        public static void clearTargetCountry()
        {
            targetCountry = null;
            targetCountryIndex = -1;
            targetCountryName = null;
        }
    }
}