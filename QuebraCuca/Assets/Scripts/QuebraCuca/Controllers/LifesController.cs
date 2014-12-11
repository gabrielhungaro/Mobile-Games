using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.globo.sitio.mobilegames.QuebraCuca.Controllers
{
    public class LifesController
    {

        private static volatile LifesController _instance;
        private static object _lock = new object();

        private static int _lifes;
        private static int _initialLife;

        static LifesController() { }

        public static LifesController Instance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null) _instance = new LifesController();
                }
            }
            return _instance;
        }

        private LifesController()
        {
            _initialLife = 3;
            _lifes = _initialLife;
        }

        public static int GetLifes()
        {
            return _lifes;
        }

        public static void SetLifes(int value)
        {
            _lifes = value;
        }

        public static void AddLife()
        {
            _lifes++;
        }

        public static void RemoveLife()
        {
            if(_lifes > 0){
                _lifes--;
            }
        }

        public static int GetInitialLifes()
        {
            return _initialLife;
        }
    }
}
