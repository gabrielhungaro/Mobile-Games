using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquelaFrameWork.View
{
    public class AFMovieCLipNGUI : AMovieClip
    {
        public AFSpriteRendererNGUI UI2DSpriteRenderer { get; set; }


        public override void Init(UnityEngine.Sprite[] sprites, float fps = 12, string name = "")
        {
            AFSpriteRendererNGUI spngui = new AFSpriteRendererNGUI();
            UI2DSpriteRenderer = spngui;

            UpdateSpriteContainer();
            base.Init(sprites, fps, name);
        }

        public override void UpdateSpriteContainer()
        {
            UI2DSpriteRenderer.SpriteContainer = this.gameObject.AddComponent<UI2DSprite>();
            SetSpriteCotnainer(UI2DSpriteRenderer);
        }

    }
}
