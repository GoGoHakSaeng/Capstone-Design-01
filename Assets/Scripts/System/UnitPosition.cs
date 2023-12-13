using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YemaekHan
{
    public class UnitPosition : MonoBehaviour
    {
        public static UnitPosition instance;

        public List<UnitPositionInfo> provincesIndexes;
        

        private void Awake()
        {
            if (UnitPosition.instance == null)
            {
                UnitPosition.instance = this;
            }
        }

        private void Start()
        {
            provincesIndexes = new List<UnitPositionInfo>();
        }

        public void setUnitPosition(string country, int unitID, int provinceIndex)
        {
            provincesIndexes.Add(new UnitPositionInfo(country, unitID, provinceIndex));
        }

        public bool isNull(int targetProvinceIndex)
        {
            if(provincesIndexes.Find(x => x.provinceIndex == targetProvinceIndex) != null)
            {
                return false;
            } else
            {
                return true;
            }
        }
    }

    public class UnitPositionInfo
    {
        public string country { get; set; }
        public int unitID { get; set; }
        public int provinceIndex { get; set; }

        public UnitPositionInfo(string country, int unitID, int provinceIndex)
        {
            this.country = country;
            this.unitID = unitID;
            this.provinceIndex = provinceIndex;
        }

        public override string ToString()
        {
            return $"country : {country}, unitID : {unitID}, provinceIndex : {provinceIndex}";
        }
    }
}
