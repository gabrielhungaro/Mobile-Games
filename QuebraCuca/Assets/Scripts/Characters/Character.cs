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
    public class Character : MonoBehaviour
    {

        private string _imagePath;
        private int _characterDepth;
        private bool _hited;
        private bool _isShowing;
        private Vector3 _initialPosition;
        private AFStatesController _characterAnimations;

        void Start()
        {
            _characterAnimations = AnimationFactory.Instance.BuildAnimation(PathConstants.GetGameScenePath() + "cucaSprites", "idle", true);
            _characterAnimations = AnimationFactory.Instance.BuildAnimation(PathConstants.GetGameScenePath() + "cucaSprites", "hited", false);
            _characterAnimations = AnimationFactory.Instance.BuildAnimation(PathConstants.GetGameScenePath() + "cucaSprites", "angry", false);
            //_characterAnimations.Add(
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
