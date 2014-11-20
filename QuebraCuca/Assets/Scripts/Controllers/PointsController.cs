using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controllers
{
    public class PointsController
    {

        private static volatile PointsController _instance;
        private static object _lock = new object();

        private static int _points;
        private static int _pointsToAdd;

        static PointsController() { }

        public static PointsController Instance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null) _instance = new PointsController();
                }
            }
            return _instance;
        }

        private PointsController()
        {
            _pointsToAdd = 10;
        }

        public static int GetPoints()
        {
            return _points;
        }

        public static void SetPoints(int value)
        {
            _points = value;
        }

        public static void AddPoints(int value)
        {
            _points += value;
        }

        internal static int GetPointsToAdd()
        {
            return _pointsToAdd;
        }
    }
}
