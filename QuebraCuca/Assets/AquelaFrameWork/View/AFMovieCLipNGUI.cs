using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquelaFrameWork.View
{
    public class AFMovieCLipNGUI : AMovieClip
    {
        public AFSpriteRendererNGUI UI2DSpriteRenderer { get; set; }


        public override void Init(UnityEngine.Sprite[] sprites, float fps = 12)
        {
            AFSpriteRendererNGUI spngui = new AFSpriteRendererNGUI();
            spngui.SpriteContainer = this.gameObject.AddComponent<UI2DSprite>();
            SetSpriteCotnainer(spngui);

            UI2DSpriteRenderer = spngui;

            base.Init(sprites, fps);
        }
    }
}
