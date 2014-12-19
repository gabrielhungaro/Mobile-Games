using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Com.Globo.Sitio.MobileGames.Balloon
{
    public class ScoreController : MonoBehaviour
    {

        public static int _points;
        public static int _record;

        public void Start()
        {
            _record = 0;
            _record = PlayerPrefs.GetInt("Recorde", 0);
            _points = 0;
        }

        void Udate()
        {

        }

        public static void SetPoints(int value)
        {
            _points = value;
        }

        public static int GetPoints()
        {
            return _points;
        }

        public static void AddPoints(int value)
        {
            _points += value;
        }

        public static void RemovePoints(int value)
        {
            _points -= value;
            if (_points < 0)
            {
                _points = 0;
            }
        }

        public static void SetRecord(int value)
        {
            if (value > _record)
            {
                PlayerPrefs.SetInt("Recorde", value);
            }
        }

        public static int GetRecord()
        {
            return PlayerPrefs.GetInt("Recorde", 0);
        }
    }
}
