using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace com.globo.sitio.mobilegames.QuebraCuca.Characters
{
    public class CucaBuilderDirector : ACharacterBuilderDirector
    {
        public CucaBuilderDirector(ACharacterBuilder builder)
            : base(builder)
        {

        }

        public Character Build()
        {

            m_builder.CreateGameObject();
            m_builder.CreateCharacterComponent();
            m_builder.CreateAnimationControllerComponent();
            m_builder.CreateAnimationStateIdle();
            m_builder.CreateAnimationStateHited();
            m_builder.CreateAnimationStateAngry();
            m_builder.CreateAnimationController();
            m_builder.CreateTimeController();
            m_builder.CreateCollider();

            return m_builder.GetCharacter();
        }

    }
}
