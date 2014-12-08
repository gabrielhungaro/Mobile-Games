using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AquelaFrameWork.View
{
    public class AFSpriteRendererNGUI : ISpriteContainer
    {

        public UI2DSprite SpriteContainer { get; set; }

        public Sprite sprite
        {
            get
            {
                return SpriteContainer.sprite2D;
            }

            set
            {
                SpriteContainer.sprite2D = value;
            }

        }


    }
}
