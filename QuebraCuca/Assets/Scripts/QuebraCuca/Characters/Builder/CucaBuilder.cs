using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using com.globo.sitio.mobilegames.QuebraCuca.Elements;
using com.globo.sitio.mobilegames.QuebraCuca.Controllers;
using com.globo.sitio.mobilegames.QuebraCuca.Constants;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.View;
using AquelaFrameWork.Core.Asset;


namespace com.globo.sitio.mobilegames.QuebraCuca.Characters
{
    public class CucaBuilder : ACharacterBuilder
    {
        public readonly string CUCA_SPRITES_PATH = PathConstants.GetGameScenePath() + "cucaSprites";

        public GameObject m_cuca;
        protected Character m_cucaCharacter;

        public override void CreateCharacterComponent()
        {
            m_cucaCharacter = m_cuca.AddComponent<Character>();
        }

        public override void CreateAnimationControllerComponent()
        {
            m_cucaCharacter.SetCharacterAnimation(AFObject.Create<AFStatesController>("CucaStates"));
        }

        public override void CreateAnimationController()
        {
            m_cuca.AddComponent<AnimationController>();
        }

        public override void CreateTimeController()
        {
            m_cuca.AddComponent<TimeController>();
        }

        public override void CreateCollider()
        {
            m_cuca.AddComponent<BoxCollider>();
        }

        public override void CreateColliderResizer()
        {
            m_cuca.AddComponent<BoxColliderResizer>().Initialize();
        }

        public override void CreateAnimationStateIdle()
        {
            CrateCharacterAnimation( Character.STATE_IDLE, m_cucaCharacter.GetCharacterAnimation() );
        }

        public override void CreateAnimationStateHited()
        {
            CrateCharacterAnimation(Character.STATE_HITED, m_cucaCharacter.GetCharacterAnimation());
        }

        public override void CreateAnimationStateAngry()
        {
            CrateCharacterAnimation(Character.STATE_ANGRY, m_cucaCharacter.GetCharacterAnimation());
        }

        protected void CrateCharacterAnimation(string spriteName, AFStatesController statesControler)
        {
            statesControler.Add(
               spriteName,
               AnimationFactory.Instance.BuildAnimation(CUCA_SPRITES_PATH, spriteName),
               false);
        }

        public override void CreateGameObject()
        {
            m_cuca = AFObject.Create<AFObject>("Cuca").gameObject;
        }

        public override GameObject GetGameObject()
        {
            return m_cuca;
        }

        override public Character GetCharacter()
        {
            return m_cucaCharacter;
        }

    }
}
