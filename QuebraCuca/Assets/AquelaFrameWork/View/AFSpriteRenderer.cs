using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace AquelaFrameWork.View
{
    public class AFSpriteRenderer : ISpriteContainer
    {
        public SpriteRenderer SpriteRendererHolder{ get; set; }

        public Sprite sprite
        {
            get
            {
                return SpriteRendererHolder.sprite;
            }
            set
            {
                SpriteRendererHolder.sprite = value;
            }
        }
    }
}
