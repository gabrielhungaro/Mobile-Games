using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using AquelaFrameWork.Core.State;
using AquelaFrameWork.View;

namespace com.globo.sitio.mobilegames.QuebraCuca.Characters
{
    public class AnimationController : MonoBehaviour
    {

        private AFStatesController _animationController;
        private AMovieClip _hitedState;

        public void Initialize()
        {
            _animationController = this.gameObject.GetComponent<Character>().GetCharacterAnimation();
            _hitedState = _animationController.GetState(Character.STATE_HITED);
            this.gameObject.GetComponent<Character>().GetCharacterAnimation().GetState(Character.STATE_HITED).OnComplete.Add(CompleteHitedAnimation);
        }

        void CompleteHitedAnimation(bool value)
        {
            this.gameObject.GetComponent<Character>().GetCharacterAnimation().GoTo(Character.STATE_IDLE);
            this.gameObject.GetComponent<Character>().SetHitedAnimationIsComplete(true);
        }
    }
}
