using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using AquelaFrameWork.Core.State;
using AquelaFrameWork.View;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class AnimationController : MonoBehaviour
    {
        private AFStatesController _animationController;
        private Balloon _balloon;

        public void Initialize() 
        {
            _animationController = this.gameObject.GetComponent<Balloon>().GetBalloonAnimations();
            _balloon = this.gameObject.GetComponent<Balloon>();
            _animationController.GetState(Balloon.STATE_EXPLODE).OnComplete.Add(CompleteExplodeAnim);
        }

        void CompleteExplodeAnim(bool value)
        {
            _balloon.SetCanBeDestroyed(true);
        }

        void Update()
        {
            if (this.gameObject != null)
            {
                if (_balloon)
                {
                    if (_balloon.GetIsTouching() == true && _balloon.GetCanExplode() == false && _balloon.GetCanBeDestroyed() == false)
                    {
                        _balloon.SetIsTouching(false);
                        _animationController.GoTo(Balloon.STATE_EXPLODE);
                    }
                }
            }
        }
    }
}
