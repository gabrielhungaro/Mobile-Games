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
        public override void Init(UnityEngine.Sprite[] sprites, float fps = 12)
        {
            AFSpriteRenderer spr = new AFSpriteRenderer();
            spr.SpriteRendererHolder = this.gameObject.AddComponent<SpriteRenderer>();
            SetSpriteCotnainer(spr);
            base.Init(sprites, fps);
        }
    }
}
