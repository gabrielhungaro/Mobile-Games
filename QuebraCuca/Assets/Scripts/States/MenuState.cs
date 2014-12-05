using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Controllers;
using Elements;
using Constants;

using AquelaFrameWork.Core.State;

namespace States
{
    public class MenuState : AState
    {

        private GameObject _startButton;
        private GameObject _background;
        private GameObject _camera;

        protected override void Awake()
        {
            m_stateID = AState.EGameState.MENU;
        }

        public override void BuildState()
        {
            _camera = new GameObject();
            _camera.name = "StateCam";
            _camera.AddComponent<MyCamera>();

            _background = new GameObject();
            _background.name = "background";
            _background.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetStartScenePath() + "startScene");
            _background.GetComponent<UI2DSprite>().MakePixelPerfect();

            _startButton = new GameObject();
            _startButton.name = "startButton";
            _startButton.AddComponent<Button>().SetImagePath(PathConstants.GetStartScenePath() + "startButton");
            _startButton.GetComponent<Button>().OnClick += OnClick;
            _startButton.GetComponent<Button>().SetWithAnchor(true);
            _startButton.GetComponent<Button>().SetAnchor(_camera);
            _startButton.GetComponent<Button>().SetLeftAnchorPoint(-57);
            _startButton.GetComponent<Button>().SetRightAnchorPoint(57);
            _startButton.GetComponent<Button>().SetTopAnchorPoint(-720);
            _startButton.GetComponent<Button>().SetBottomAnchorPoint(-588);

            base.BuildState();
        }

        private void OnClick()
        {
            m_engine.GetStateManger().GotoState(AState.EGameState.GAME);
        }

        override public void Destroy()
        {
            GameObject.Destroy(_camera);
            base.Destroy();
        }
    }
}
