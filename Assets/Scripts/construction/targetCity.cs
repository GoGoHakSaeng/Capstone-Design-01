using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;

namespace YemaekHan
{
    public class TargetCity
    {
        private static City targetCity;
        private static JSONObject jsonObject;

        /// <summary>
        /// 플레이어가 지목한 도시 저장. 매개변수는 City형.
        /// </summary>
        /// <param name="city"></param>
        public static void setTargetCity(City city)
        {
            targetCity = city;
            jsonObject = targetCity.attrib["construction"];
        }

        /// <summary>
        /// 지정한 도시 반환. City형
        /// </summary>
        public static City getTargetCity
        {
            get
            {
                return targetCity;
            }
        }

        /// <summary>
        /// 지정한 도시의 JSON 반환. (WMSK)JSONObject형.
        /// </summary>

        public static JSONObject getTargetCityJSON
        {
            get
            {
                return jsonObject;
            }
        }

        /// <summary>
        /// 지정 도시 초기화. JSONObject도 초기화함.
        /// </summary>
        public static void clearTargetCity()
        {
            targetCity = null;
            jsonObject = null;
        }

        public static void EchoJSON()
        {
            foreach(KeyValuePair<string, string> kvp in jsonObject.ToDictionary())
            {
                Debug.Log("1: " + kvp.Key + ", " + kvp.Value);
            }
        }
    }
}