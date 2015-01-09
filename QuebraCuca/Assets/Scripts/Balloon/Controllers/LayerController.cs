using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class LayerController
    {

        private static LayerController instance;

        public int _layerHUD = 8;
        public int _maskHud;

        private LayerController()
        {

        }

        public static LayerController Instance()
        {

            if (instance == null)
            {
                instance = new LayerController();
                return instance;
            }
            else
            {
                return instance;
            }

        }

        public void Start()
        {
            _maskHud = 1 << _layerHUD;
        }


    }
}
