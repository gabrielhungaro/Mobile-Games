using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Controllers;
using Constants;

namespace Elements
{
    public class Button : MonoBehaviour
    {
        private string _imagePath;
        public delegate void OnClickEvent();
        public event OnClickEvent OnClick;
        private GameObject _anchorTarget;
        private int _bottomAnchorPoint;
        private int _topAnchorPoint;
        private int _rightAnchorPoint;
        private int _leftAnchorPoint;
        private bool _withAnchors = false;

        void Start()
        {
            if (this.gameObject.GetComponent<UI2DSprite>() != true)
            {
                this.gameObject.AddComponent<UI2DSprite>();
                this.gameObject.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(_imagePath);
            }
            
            this.gameObject.GetComponent<UI2DSprite>().MakePixelPerfect();
            this.gameObject.AddComponent<BoxCollider>().size = new Vector3(this.gameObject.GetComponent<UI2DSprite>().width,
                                                                           this.gameObject.GetComponent<UI2DSprite>().height);
            //this.gameObject.AddComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
            if (_withAnchors)
            {
                this.gameObject.GetComponent<UI2DSprite>().SetAnchor(_anchorTarget);
                this.gameObject.GetComponent<UI2DSprite>().leftAnchor.absolute = _leftAnchorPoint;
                this.gameObject.GetComponent<UI2DSprite>().rightAnchor.absolute = _rightAnchorPoint;
                this.gameObject.GetComponent<UI2DSprite>().topAnchor.absolute = _topAnchorPoint;
                this.gameObject.GetComponent<UI2DSprite>().bottomAnchor.absolute = _bottomAnchorPoint;
                this.gameObject.GetComponent<UI2DSprite>().UpdateAnchors();
                this.gameObject.GetComponent<UI2DSprite>().MakePixelPerfect();
            }
            UIEventListener.Get(this.gameObject).onClick += OnMouseDown;
        }

        public void OnMouseDown(GameObject g)
        {
            SoundManager.PlaySoundByName(SoundConstants.SFX_BUTTON);
            OnClick();
        }

        public void SetImagePath(string value)
        {
            _imagePath = value;
        }

        public string GetImagePath()
        {
            return _imagePath;
        }

        internal void SetAnchor(GameObject value)
        {
            _anchorTarget = value;
        }

        internal void SetLeftAnchorPoint(int value)
        {
            _leftAnchorPoint = value;
        }

        internal void SetRightAnchorPoint(int value)
        {
            _rightAnchorPoint = value;
        }

        internal void SetTopAnchorPoint(int value)
        {
            _topAnchorPoint = value;
        }

        internal void SetBottomAnchorPoint(int value)
        {
            _bottomAnchorPoint = value;
        }

        internal void SetWithAnchor(bool value)
        {
            _withAnchors = value;
        }
    }
}
