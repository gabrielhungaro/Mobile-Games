using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using AquelaFrameWork.Core;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class ConstantsBalloons : ASingleton<ConstantsBalloons>
    {
        public const string TYPE_SIMPLE_ADD_POINT = "AddPoint";
        public const string TYPE_SIMPLE_REMOVE_POINT = "RemovePoint";
        public const string TYPE_SIMLPE_ADD_TIME = "AddTime";
        public const string TYPE_SIMPLE_REMOVE_TIME = "RemoveTime";
        public const string TYPE_NUCLEAR_EXPLOSIVE = "NuclearExplosive";
        public const string TYPE_SLOW_MOTION = "SlowMotion";
        public const string TYPE_FAST_FOWARD = "FastFoward";
        public const string TYPE_EXPLOSIVE = "Explosive";
        public const string TYPE_WORD = "Word";

        public const int HIGH_CHANCE = 80;
        public const int MEDIUM_CHANCE = 50;
        public const int LOW_CHANCE = 10;

        public static List<String> _listOfHighChanceBalloons;
        public static List<String> _listOfMediumChanceBalloons;
        public static List<String> _listOfLowChanceBalloons;

        public static List<String> _listOfTypesNames;

        public void Initialize()
        {
            _listOfTypesNames = new List<String>();
            _listOfTypesNames.Add(TYPE_SIMLPE_ADD_TIME);
            _listOfTypesNames.Add(TYPE_SIMPLE_ADD_POINT);
            _listOfTypesNames.Add(TYPE_SIMPLE_REMOVE_POINT);
            _listOfTypesNames.Add(TYPE_SIMPLE_REMOVE_TIME);
            //_listOfTypesNames.Add(TYPE_NUCLEAR_EXPLOSIVE);
            _listOfTypesNames.Add(TYPE_FAST_FOWARD);
            _listOfTypesNames.Add(TYPE_SLOW_MOTION);

            _listOfHighChanceBalloons = new List<string>();
            _listOfHighChanceBalloons.Add(TYPE_SIMLPE_ADD_TIME);
            _listOfHighChanceBalloons.Add(TYPE_SIMPLE_ADD_POINT);

            _listOfMediumChanceBalloons = new List<string>();
            _listOfMediumChanceBalloons.Add(TYPE_SIMPLE_REMOVE_POINT);
            _listOfMediumChanceBalloons.Add(TYPE_SIMPLE_REMOVE_TIME);
            _listOfMediumChanceBalloons.Add(TYPE_SLOW_MOTION);
            _listOfMediumChanceBalloons.Add(TYPE_FAST_FOWARD);
            //_listOfMediumChanceBalloons.Add(TYPE_EXPLOSIVE);
            //_listOfMediumChanceBalloons.Add(TYPE_WORD);

            _listOfLowChanceBalloons = new List<string>();
            //_listOfLowChanceBalloons.Add(TYPE_NUCLEAR_EXPLOSIVE);
        }

        public List<String> GetListOfBalloonsType()
        {
            return _listOfTypesNames;
        }

        public MonoBehaviour GetComponentByName(string _name)
        {

            return null;
        }
    }
}

