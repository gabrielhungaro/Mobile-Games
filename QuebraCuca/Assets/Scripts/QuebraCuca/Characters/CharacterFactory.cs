using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Utils;
using AquelaFrameWork.View;
using AquelaFrameWork.Components;

using UnityEngine;
using com.globo.sitio.mobilegames.QuebraCuca.Controllers;
using com.globo.sitio.mobilegames.QuebraCuca.Constants;

namespace com.globo.sitio.mobilegames.QuebraCuca.Characters
{
    public class CharacterFactory : ASingleton<CharacterFactory>
    {
        private int _numberOfCharacters;

        public Character CreateCharacter()
        {
            try
            {
                Character character = AFObject.Create<Character>();

                character.gameObject.AddComponent<TimeController>();
                character.gameObject.AddComponent<BoxCollider>();
                character.gameObject.AddComponent<AnimationController>();

                //UIEventListener.Get(character.gameObject).onClick += OnClick;
                _numberOfCharacters++;
                
                return character;
            }
            catch( Exception ex )
            {
                //AFDebug.LogError("NÃO Criei o personagem.. Por que não quiz... S");
            }
           
            return null;
        }

        private void OnClick(GameObject go)
        {
            if (go.GetComponent<Character>().GetIsHited() == false && go.GetComponent<Character>().GetIsShowing() == true)
            {
                Character character =  go.GetComponent<Character>();
                character.SetIsHited(true);
                character.GetCharacterAnimation().GoTo(Character.STATE_HITED);
                SoundManager.PlaySoundByName(SoundConstants.SFX_CORRECT_HIT);
                //CharacterManager.Instance.HideCharacter(character);
                PointsController.AddPoints(PointsController.GetPointsToAdd());
            }
        }
    }
}
