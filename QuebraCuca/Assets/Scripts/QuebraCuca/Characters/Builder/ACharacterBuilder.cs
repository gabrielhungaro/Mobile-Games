using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace com.globo.sitio.mobilegames.QuebraCuca.Characters
{
    public abstract class  ACharacterBuilder
    {
        public ACharacterBuilder()
        {

        }

        virtual public void CreateTimeController()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

        virtual public void CreateAnimationController()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

        virtual public void CreateCharacterComponent()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

        virtual public void CreateAnimationControllerComponent()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

        virtual public void CreateColliderResizer()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

        virtual public void CreateCollider()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

        virtual public void CreateAnimationStateWalk()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

        virtual public void CreateAnimationStateHited()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

        virtual public void CreateAnimationStateAngry()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

        virtual public void CreateAnimationStateStop()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

        virtual public void CreateAnimationStateIdle()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

        virtual public void CrateAnimationStateProvoke()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

        virtual public void CreateGameObject()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

        virtual public GameObject GetGameObject()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

        virtual public Character GetCharacter()
        {
            throw new InvalidOperationException("This Method must be overridden");
        }

    }
}
