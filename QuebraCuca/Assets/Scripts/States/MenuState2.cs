using UnityEngine;
using System.Collections;
using Elements;
using Constants;
using Controllers;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.View;

namespace States
{
    public class ManuState2 : AState
    {
        private GameObject _startButton;
        private GameObject _background;

        protected override void Awake()
        {
            m_stateID = AState.EGameState.MENU;
            Initialize(STATE_EVERYTHING);
        }

        public override void BuildState()
        {
            base.BuildState();
        }

        void Start()
        {
            _background = new GameObject();
            _background.name = "background";
            _background.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetStartScenePath() + "startScene");
            _background.GetComponent<UI2DSprite>().MakePixelPerfect();


            _startButton = new GameObject();
            _startButton.name = "startButton";
            _startButton.AddComponent<Button>().SetImagePath(PathConstants.GetStartScenePath() + "startButton");
            _startButton.GetComponent<Button>().OnClick += OnClick;
            _startButton.GetComponent<Button>().SetWithAnchor(true);
            _startButton.GetComponent<Button>().SetAnchor(GameStateFactory.GetUiRoot());
            _startButton.GetComponent<Button>().SetLeftAnchorPoint(-57);
            _startButton.GetComponent<Button>().SetRightAnchorPoint(57);
            _startButton.GetComponent<Button>().SetTopAnchorPoint(-720);
            _startButton.GetComponent<Button>().SetBottomAnchorPoint(-588);
        }

        private void OnClick()
        {
            //SceneManager.ChangeScene(SceneManager.GAME_SCENE);
            //Application.LoadLevel(SceneManager.GAME_SCENE);
        }

        // Update is called once per frame
        
    }
}