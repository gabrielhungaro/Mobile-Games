using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using com.globo.sitio.mobilegames.QuebraCuca.Elements;
using com.globo.sitio.mobilegames.QuebraCuca.Constants;
using com.globo.sitio.mobilegames.QuebraCuca.Controllers;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.View;
using AquelaFrameWork.Core.Asset;

namespace com.globo.sitio.mobilegames.QuebraCuca.Characters
{
    public class Character : AFObject
    {
        public static readonly string STATE_IDLE = "idle";
        public static readonly string STATE_HITED = "hited";
        public static readonly string STATE_ANGRY = "angry";

        private bool _hited = false;
        private bool _isShowing = false;
        private Vector3 _initialPosition;
        private AFStatesController _characterAnimations;
        private bool _isRotated = false;
        private Sprite _sprite;
        private bool _hitedAnimationIsComplete;
        private float _characterScale;

        public void Initialize()
        {
            _characterAnimations = AFObject.Create<AFStatesController>();
            _characterAnimations.Add(STATE_HITED , AnimationFactory.Instance.BuildAnimation(PathConstants.GetGameScenePath() + "cucaSprites", STATE_HITED ) ,false);
            _characterAnimations.Add(STATE_ANGRY, AnimationFactory.Instance.BuildAnimation(PathConstants.GetGameScenePath() + "cucaSprites", STATE_ANGRY), false);
            _characterAnimations.Add(STATE_IDLE, AnimationFactory.Instance.BuildAnimation(PathConstants.GetGameScenePath() + "cucaSprites", STATE_IDLE), true);
            _characterAnimations.gameObject.transform.parent = this.gameObject.transform;
            _sprite = _characterAnimations.GetCurrentState().GetSprite();
            this.gameObject.AddComponent<BoxColliderResizer>().Initialize();
            if(this.gameObject.GetComponent<AnimationController>()){
                this.gameObject.GetComponent<AnimationController>().Initialize();
            }
        }

        void OnMouseDown()
        {
            if (GetIsHited() == false && GetIsShowing() == true)
            {
                SetIsHited(true);
                GetCharacterAnimation().GoTo(Character.STATE_HITED);
                SoundManager.PlaySoundByName(SoundConstants.SFX_CORRECT_HIT);
                //CharacterManager.Instance.HideCharacter(character);
                PointsController.AddPoints(PointsController.GetPointsToAdd());
            }
        }

        public override void AFUpdate(double time)
        {
            
            _characterAnimations.AFUpdate(time);
        }

        public AFStatesController GetCharacterAnimation()
        {
            return _characterAnimations;
        }

        public void SetCharacterAnimation(AFStatesController value)
        {
            _characterAnimations = value;
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

        public void SetIsRotated(bool value)
        {
            _isRotated = value;
        }

        public bool GetIsRotated()
        {
            return _isRotated;
        }

        public Sprite GetSprite()
        {
            return _sprite;
        }

        public void SetHitedAnimationIsComplete(bool value)
        {
            _hitedAnimationIsComplete = value;
        }

        public bool GetHitedAnimationIsComplete()
        {
            return _hitedAnimationIsComplete;
        }

        public void SetScale(float value)
        {
            _characterScale = value;
        }

        public float GetScale()
        {
            return _characterScale;
        }
    }
}
