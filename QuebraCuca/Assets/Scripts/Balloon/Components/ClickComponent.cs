using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class ClickComponent : MonoBehaviour
    {

        public delegate void OnClickEvent(GameObject g);
        public event OnClickEvent OnClick;

        private bool _explodeByClick = true;

        void Start()
        {

        }

        public void OnMouseDown()
        {
            if (_explodeByClick == true)
                SoundManager.PlaySoundByName(this.gameObject.GetComponent<Balloon>().GetType());

            _explodeByClick = true;
            this.gameObject.GetComponent<Balloon>().SetIsTouching(true);
            this.gameObject.GetComponent<Balloon>().SetCanExplode(false);
            OnClick(this.gameObject);
        }

        public void SetExplodeByClick(bool value)
        {
            _explodeByClick = value;
        }

        public bool GetExplodeByClick()
        {
            return _explodeByClick;
        }
    }
}
