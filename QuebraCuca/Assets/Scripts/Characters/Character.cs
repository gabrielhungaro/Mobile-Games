using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Elements;
using Constants;
using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.View;
using AquelaFrameWork.Core.Asset;

namespace Characters
{
    public class Character : AFObject
    {
        public static readonly string STATE_IDLE = "idle";
        public static readonly string STATE_HITED = "hited";
        public static readonly string STATE_ANGRY = "angry";


        private string _imagePath;
        private int _characterDepth;
        private bool _hited;
        private bool _isShowing;
        private Vector3 _initialPosition;
        private AFStatesController _characterAnimations;

        public void Initialize()
        {
            _characterAnimations = AFObject.Create<AFStatesController>();
            _characterAnimations.Add(STATE_HITED , AnimationFactory.Instance.BuildAnimation(PathConstants.GetGameScenePath() + "cucaSprites", STATE_HITED ) ,false);
            _characterAnimations.Add(STATE_ANGRY, AnimationFactory.Instance.BuildAnimation(PathConstants.GetGameScenePath() + "cucaSprites", STATE_ANGRY), false);
            _characterAnimations.Add(STATE_IDLE, AnimationFactory.Instance.BuildAnimation(PathConstants.GetGameScenePath() + "cucaSprites", STATE_IDLE), true);
            _characterAnimations.gameObject.transform.parent = this.gameObject.transform;
        }

        public AFStatesController GetCharacterAnimation()
        {
            return _characterAnimations;
        }

        public override void AFUpdate(double time)
        {
            
            _characterAnimations.AFUpdate(time);
        }

        public void SetIsHited(bool value)
        {
            _hited = value;
        }

        public bool GetIsHited()
        {
            return _hited;
        }

        public void SetIsShowing(bool value)
        {
            _isShowing = value;
        }

        public bool GetIsShowing()
        {
            return _isShowing;
        }

        public void SetInitialPosition(Vector3 value)
        {
            _initialPosition = value;
        }

        public Vector3 GetInitialPosition()
        {
            return _initialPosition;
        }
    }
}
