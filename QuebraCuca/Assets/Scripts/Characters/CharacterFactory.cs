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
using States;
using Controllers;
using Constants;

namespace Characters
{
    public class CharacterFactory : ASingleton<CharacterFactory>
    {
        private int _numberOfCharacters;

        public Character CreateCharacter()
        {
            try
            {
                Character character = AFObject.Create<Character>();
                //GameObject characterGO = AFObject.Create<Character>().gameObject;

                character.gameObject.AddComponent<TimeController>();
                character.gameObject.AddComponent<BoxCollider2D>();
                character.gameObject.AddComponent<AFBoxColider2DResizer>();
                character.gameObject.AddComponent<UIAnchor>();
                UIEventListener.Get(character.gameObject).onClick += OnClick;
                //character.gameObject.GetComponent<UIWidget>().MakePixelPerfect();
                _numberOfCharacters++;
                //FindObjectOfType<IndexController>().AddObjectToLIstByIndex(characterGO, 1);
                //character.GetComponent<Character>().SetImagePath(PathConstants.GetGameScenePath() + "cuca");
                //character.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "cuca");
                //character.AddComponent<Rigidbody2D>().use = false;
                // character.GetComponent<Rigidbody2D>().isKinematic = false;
                //character.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
                //character.GetComponent<UI2DSprite>().MakePixelPerfect();
                //adicionar anchor no character
                //character.GetComponent<SpriteRenderer>().alpha = 0;
                
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
            if (go.GetComponent<Character>().GetIsHited() == false)
            {
                Character character =  go.GetComponent<Character>();
                character.SetIsHited(true);
                character.GetCharacterAnimation().GoTo("hited");
                SoundManager.PlaySoundByName(SoundConstants.SFX_CORRECT_HIT);
                CharacterManager.Instance.HideCharacter(character);
                PointsController.AddPoints(PointsController.GetPointsToAdd());
            }
        }
    }
}
