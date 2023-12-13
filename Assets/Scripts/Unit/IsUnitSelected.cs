using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YemaekHan
{
    public class IsUnitSelected : MonoBehaviour
    {
        public static IsUnitSelected instance;

        public bool isSelected { get; set; }

        private void Awake()
        {
            if (IsUnitSelected.instance == null)
            {
                IsUnitSelected.instance = this;
                IsUnitSelected.instance.isSelected = false;
            }
            
        }
    }
}

