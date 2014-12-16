using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using com.globo.sitio.mobilegames.QuebraCuca.Characters;
using AquelaFrameWork.Components;
using AquelaFrameWork.View;

namespace com.globo.sitio.mobilegames.QuebraCuca.Characters
{
    public class BoxColliderResizer : MonoBehaviour
    {

        private Sprite sprite;

        public void Initialize()
        {
            if (this.gameObject.GetComponent<Character>())
            {
                sprite = this.gameObject.GetComponent<Character>().GetSprite();
            }
        }

        void Update()
        {
            if (this.gameObject.GetComponent<BoxCollider>() && sprite)
            {
                if (this.gameObject.GetComponent<Character>().GetIsRotated())
                {
                    this.gameObject.GetComponent<BoxCollider>().size = new Vector2(sprite.bounds.size.y, sprite.bounds.size.x);
                }
                else
                {
                    this.gameObject.GetComponent<BoxCollider>().size = new Vector2(sprite.bounds.size.x, sprite.bounds.size.y);
                }
            }
        }
    }
}
