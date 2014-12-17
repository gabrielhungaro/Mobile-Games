using System;
using System.Collections.Generic;


using UnityEngine;

using AquelaFrameWork.Core;
using AquelaFrameWork.Sound;

using Signals;

namespace AquelaFrameWork.View
{
    public class AFMovieClip : AMovieClip
    {
        public AFSpriteRenderer SpriteRenderer { get; set; }

        public override void Init(UnityEngine.Sprite[] sprites, float fps = 12, string name = "")
        {
            SpriteRenderer = new AFSpriteRenderer();
            SpriteRenderer.SpriteRendererHolder = this.gameObject.AddComponent<SpriteRenderer>();
            SetSpriteCotnainer(SpriteRenderer);

            base.Init(sprites, fps, name);
        }

        public override void UpdateSpriteContainer()
        {
            SpriteRenderer.SpriteRendererHolder = this.gameObject.GetComponent<SpriteRenderer>();
        }
    }
}
