using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.View;
using AquelaFrameWork.Utils;

using Constants;
using UnityEngine;

namespace Characters
{
    public class AnimationFactory : ASingleton<AnimationFactory>
    {

        public AFMovieClip BuildAnimation(string spritePath, string animationStateName)
        {
            AFMovieClip L_animation;
            AFTextureAtlas L_heroAtlas = AFAssetManager.Instance.Load<AFTextureAtlas>(spritePath);
            L_animation = AFObject.Create<AFMovieClip>(animationStateName);
            L_animation.Init(L_heroAtlas.GetSprites(animationStateName));
            return L_animation;
        }

    }
}
