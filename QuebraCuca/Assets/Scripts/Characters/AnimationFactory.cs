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
        AFMovieClip animation;
        AFStatesController m_heroStates;

        public AFStatesController BuildAnimation(string spritePath, string animationStateName, bool isDefaultState)
        {
            Debug.Log("path: " + spritePath);
            AFTextureAtlas heroAtlas = AFAssetManager.Instance.Load<AFTextureAtlas>(spritePath);
            m_heroStates = AFObject.Create<AFStatesController>("Hero Animation Controller");

            animation = AFObject.Create<AFMovieClip>(animationStateName);
            animation.Init(heroAtlas.GetSprites(animationStateName));
            m_heroStates.Add(animationStateName, animation, isDefaultState);

            if(m_heroStates.gameObject.GetComponent<Rigidbody2D>() == null){
                m_heroStates.gameObject.AddComponent<Rigidbody2D>();
            }
            //m_heroStates.transform.localScale = new UnityEngine.Vector3(3, 3, 3);

            return m_heroStates;
        }

        public AFMovieClip BuildMovieClipAnimation(string spritePath, string animationStateName, bool isDefaultState)
        {
            AFTextureAtlas heroAtlas = AFAssetManager.Instance.Load<AFTextureAtlas>(spritePath);

            animation = AFObject.Create<AFMovieClip>(animationStateName);
            animation.Init(heroAtlas.GetSprites(animationStateName));

            m_heroStates.Add(animationStateName, animation, isDefaultState);

            return animation;
        }

    }
}
