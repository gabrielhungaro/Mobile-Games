using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Com.Globo.Sitio.MobileGames.Balloon
{
    public class LifeController : MonoBehaviour
    {

        private static int _life;
        private static int _initialLife = 5;

        public void Start()
        {
            _life = _initialLife;
        }

        void Update()
        {
            if (_life <= 0)
            {
                GameController.SetEndedGame(true);
            }
        }

        public static int GetInitialLife()
        {
            return _initialLife;
        }

        public static void SetLife(int value)
        {
            _life = value;
        }

        public static int GetLife()
        {
            return _life;
        }

        public static void AddLife()
        {
            _life++;
        }

        public static void RemoveLife()
        {
            //_life--;
        }
    }
}