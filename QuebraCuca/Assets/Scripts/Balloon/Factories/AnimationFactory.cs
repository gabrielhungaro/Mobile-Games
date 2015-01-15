using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.View;
using AquelaFrameWork.Utils;


namespace com.globo.sitio.mobilegames.Balloon.Factories
{
    public class AnimationFactory : ASingleton<AnimationFactory>
    {
        public static readonly int FRAME_RATE = 60;

        public AFMovieClip BuildAnimation(string spritePath, string spriteName, string animationStateName)
        {
            AFMovieClip L_animation;
            AFTextureAtlas L_heroAtlas = AFAssetManager.Instance.Load<AFTextureAtlas>(spritePath);
            L_animation = AFObject.Create<AFMovieClip>(animationStateName);
            L_animation.Init(L_heroAtlas.GetSprites(spriteName + "_" + animationStateName), FRAME_RATE );
            return L_animation;
        }

    }
}
