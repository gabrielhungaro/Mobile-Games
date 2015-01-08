using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core;
using AquelaFrameWork.Sound;

namespace BateRebate
{
    public class BateGameState : AState
    {
        private GameObject ball;
        private GameObject bg;
        private GameObject touchRight;
        private GameObject touchLeft;
        private GameObject paddleRight;
        private GameObject paddleLeft;
        private GameObject pauseBtn;

        private GameObject scoreLeftOver;
        private GameObject scoreRightOver;

        private GameObject pauseScene;

        private string pauseScenePreFabUrl = "preFabs/PreFabPauseScene";
        private string pauseBtnAssetUrl = "Scenes/Game/pausebt";
        private string paddleRightAssetUrl = "Scenes/Game/gamePaddle2";
        private string paddleLeftAssetUrl = "Scenes/Game/gamePaddle";
        private string bgAsset = "Scenes/Game/gameBg";
        private string ballAsset = "Scenes/Game/gameBall";
        private string hitBoxAsset = "Scenes/Game/gameHitOk";

        private GameObject gameScene;
        public int playerNumber;
        private BateController main;
        private GameObject wallNorth;
        private GameObject wallSouth;
        private GameObject wallRight;
        private GameObject wallLeft;
        private bool changedPlayerNumber = false;

        protected override void Awake()
        {
            m_stateID = EGameState.GAME;
        }
        public override void BuildState()
        {
            main = BateController.Instance;
            gameScene = AFAssetManager.Instance.Instantiate<GameObject>(AFAssetManager.GetDirectoryOwner("preFabs/PreFabGameScene"));
            playerNumber = main.PlayerNumber;

            VerifyScreenRes();
            CreateBackGround();
            CreateWalls();
            CreateUI();
            CreateBall();
            CreatePaddles();
            CreateTouches();
            touchLeft.GetComponent<PaddleBehaviour>().AwakenPaddle();
            touchRight.GetComponent<PaddleBehaviour>().AwakenPaddle();
            ball.GetComponent<BallBehaviour>().AwakeBall();

            Add(gameScene);

            base.BuildState();
        }

        private void VerifyScreenRes()
        {
            bgAsset = main.AddPlatformAndQualityToUrl(bgAsset);
            pauseBtnAssetUrl = main.AddPlatformAndQualityToUrl(pauseBtnAssetUrl);
            ballAsset = main.AddPlatformAndQualityToUrl(ballAsset);
            hitBoxAsset = main.AddPlatformAndQualityToUrl(hitBoxAsset);
            paddleRightAssetUrl = main.AddPlatformAndQualityToUrl(paddleRightAssetUrl);
            paddleLeftAssetUrl = main.AddPlatformAndQualityToUrl(paddleLeftAssetUrl);
        }
        private void CreateWalls()
        {
            Vector3 bgSize = GetExtents();
            Vector3 bgPos = GetPos();

            wallNorth = new GameObject();
            wallNorth.name = "wallU";
            wallNorth.AddComponent<BoxCollider2D>();
            wallNorth.transform.localScale = new Vector3(bgSize.x * 2f, 1f, 1f);
            wallNorth.transform.position = new Vector3(bgPos.x, bgPos.y + bgSize.y + wallNorth.transform.collider2D.bounds.extents.y, 0f);
            Add(wallNorth);

            wallSouth = new GameObject();
            wallSouth.name = "wallD";
            wallSouth.AddComponent<BoxCollider2D>();
            wallSouth.transform.localScale = new Vector3(bgSize.x * 2f, 1f, 1f);
            wallSouth.transform.position = new Vector3(bgPos.x, bgPos.y - bgSize.y - wallSouth.transform.collider2D.bounds.extents.y, 0f);
            Add(wallSouth);

            wallLeft = new GameObject();
            wallLeft.name = "wallL";
            wallLeft.AddComponent<BoxCollider2D>();
            wallLeft.transform.localScale = new Vector3(1f, bgSize.y * 2f, 1f);
            wallLeft.transform.position = new Vector3(bgPos.x - bgSize.x - wallLeft.transform.collider2D.bounds.extents.x, bgPos.y, 0f);
            Add(wallLeft);

            wallRight = new GameObject();
            wallRight.name = "wallR";
            wallRight.AddComponent<BoxCollider2D>();
            wallRight.transform.localScale = new Vector3(1f, bgSize.y * 2f, 1f);
            wallRight.transform.position = new Vector3(bgPos.x + bgSize.x + wallRight.transform.collider2D.bounds.extents.x, bgPos.y, 0f);
            Add(wallRight);
        }
        private Vector3 GetExtents()
        {
            return bg.GetComponent<SpriteRenderer>().sprite.bounds.extents;
        }
        private Vector3 GetPos()
        {
            return bg.transform.position;
        }
        private void CreateBackGround()
        {
            bg = new GameObject();
            bg.name = "background";
            bg.AddComponent<SpriteRenderer>().sprite = AFAssetManager.Instance.Instantiate<Sprite>(bgAsset);
            bg.transform.position = new Vector3(0f, 0f, 10);
            Add(bg);
        }
        private void CreateUI()
        {
            pauseScene = AFAssetManager.Instance.Instantiate<GameObject>(AFAssetManager.GetDirectoryOwner(pauseScenePreFabUrl));
            pauseScene.name = "pauseTela";
            pauseScene.AddComponent<BatePauseState>().StartScene(this);
            pauseScene.SetActive(false);
            Add(pauseScene);

            scoreLeftOver = GameObject.Find("scoreLeftOver");
            scoreRightOver = GameObject.Find("scoreRightOver");
            Font typographyofcoop = Resources.Load<Font>(AFAssetManager.GetDirectoryOwner("Fonts/TypographyofCoop-Black"));
            scoreLeftOver.GetComponent<Text>().font = typographyofcoop;
            scoreLeftOver.GetComponent<Text>().fontSize = 230;
            scoreLeftOver.GetComponent<Text>().supportRichText = true;

            scoreRightOver.GetComponent<Text>().font = typographyofcoop;
            scoreRightOver.GetComponent<Text>().fontSize = 230;
            scoreRightOver.GetComponent<Text>().supportRichText = true;

            pauseBtn = GameObject.Find("pausebtn");
            pauseBtn.GetComponent<Image>().sprite = AFAssetManager.Instance.Instantiate<Sprite>(pauseBtnAssetUrl);
            pauseBtn.GetComponent<Image>().preserveAspect = true;
            pauseBtn.GetComponent<Button>().onClick.AddListener(OnClickPause);
        }

        private void CreateBall()
        {
            ball = new GameObject();
            ball.name = "ball";
            ball.AddComponent<SpriteRenderer>().sprite = AFAssetManager.Instance.Instantiate<Sprite>(ballAsset);
            ball.AddComponent<CircleCollider2D>();
            ball.AddComponent<Rigidbody2D>().mass = 0.00001f;
            ball.GetComponent<Rigidbody2D>().gravityScale = 0f;
            ball.collider2D.sharedMaterial = AFAssetManager.Instance.Instantiate<PhysicsMaterial2D>(AFAssetManager.GetDirectoryOwner("Materials/ballMaterial"));
            ball.transform.position = new Vector3(GetPos().x, GetPos().y, 0);
            ball.AddComponent<BallBehaviour>();
            ball.GetComponent<BallBehaviour>().scoreLeftOver = scoreLeftOver;
            ball.GetComponent<BallBehaviour>().scoreRightOver = scoreRightOver;
            Add(ball);
        }
        private void CreateTouches()
        {
            touchLeft = new GameObject();
            touchRight = new GameObject();
            touchLeft.name = "touchBoxLeft";
            touchRight.name = "touchBoxRight";
            touchLeft.AddComponent<BoxCollider2D>().isTrigger = true;
            touchRight.AddComponent<BoxCollider2D>().isTrigger = true;

            touchRight.transform.localScale = new Vector3(GetExtents().x * 0.7f, GetExtents().y * 2f, 1f);
            touchRight.transform.position = new Vector3(GetPos().x + GetExtents().x - touchRight.collider2D.bounds.extents.x, GetPos().y, -9f);

            touchLeft.transform.localScale = new Vector3(GetExtents().x * 0.7f, GetExtents().y * 2f, 1f);
            touchLeft.transform.position = new Vector3(GetPos().x - GetExtents().x + touchLeft.collider2D.bounds.extents.x, GetPos().y, -9f);

            touchRight.AddComponent<PaddleBehaviour>().enableTouch = (playerNumber == 2);
            touchRight.GetComponent<PaddleBehaviour>().paddleAsset = paddleRight;
            touchRight.GetComponent<PaddleBehaviour>().ball = ball;
            touchLeft.AddComponent<PaddleBehaviour>().enableTouch = true;
            touchLeft.GetComponent<PaddleBehaviour>().paddleAsset = paddleLeft;
            touchLeft.GetComponent<PaddleBehaviour>().ball = ball;

            Add(touchLeft);
            Add(touchRight);
        }
        private void CreatePaddles()
        {
            paddleRight = new GameObject();
            paddleLeft = new GameObject();
            paddleRight.name = "paddleRightAsset";
            paddleLeft.name = "paddleLeftAsset";
            paddleRight.AddComponent<SpriteRenderer>().sprite = AFAssetManager.Instance.Instantiate<Sprite>(paddleRightAssetUrl);
            paddleLeft.AddComponent<SpriteRenderer>().sprite = AFAssetManager.Instance.Instantiate<Sprite>(paddleLeftAssetUrl);
            paddleRight.AddComponent<BoxCollider2D>();
            paddleLeft.AddComponent<BoxCollider2D>();
            paddleRight.AddComponent<Rigidbody2D>().mass = 100000f;
            paddleRight.GetComponent<Rigidbody2D>().fixedAngle = true;
            paddleRight.GetComponent<Rigidbody2D>().gravityScale = 0f;
            paddleLeft.AddComponent<Rigidbody2D>().mass = 100000f;
            paddleLeft.GetComponent<Rigidbody2D>().fixedAngle = true;
            paddleLeft.GetComponent<Rigidbody2D>().gravityScale = 0f;
            paddleRight.transform.position = new Vector3(GetPos().x + GetExtents().x - paddleRight.collider2D.bounds.extents.x * 3f, GetPos().y, 9f);
            paddleLeft.transform.position = new Vector3(GetPos().x - GetExtents().x + paddleLeft.collider2D.bounds.extents.x * 3f, GetPos().y, 9f);
            Add(paddleLeft);
            Add(paddleRight);
        }
        public void OnClickPause()
        {
            if (!main.IsPaused)
            {
                ball.GetComponent<BallBehaviour>().SaveState();
                ball.GetComponent<BallBehaviour>().Pause();
                ball.GetComponent<BallBehaviour>().CancellBallInvoke();
                touchLeft.GetComponent<PaddleBehaviour>().StopDrag();
                touchRight.GetComponent<PaddleBehaviour>().StopDrag();
                pauseScene.SetActive(true);
                main.IsPaused = true;
            }
        }
        public void OnClickUnPause()
        {
            if (main.IsPaused)
            {
                main.IsPaused = false;
                pauseScene.SetActive(false);
                if (!this.changedPlayerNumber)
                {
                    ball.GetComponent<BallBehaviour>().UnPause();
                }
                else
                {
                    this.changedPlayerNumber = false;
                    this.touchLeft.GetComponent<PaddleBehaviour>().ResetPos();
                    this.touchRight.GetComponent<PaddleBehaviour>().ResetPos();
                    this.ball.GetComponent<BallBehaviour>().InvokeResetBall();
                }
            }
        }
        public void SinglePlayer()
        {
            main.PlayerNumber = 1;
            this.playerNumber = 1;
            touchRight.GetComponent<PaddleBehaviour>().enableTouch = false;
            ball.GetComponent<BallBehaviour>().ResetScore();
            this.changedPlayerNumber = true;
        }
        public void MultiPlayer()
        {
            main.PlayerNumber = 2;
            this.playerNumber = 2;
            touchRight.GetComponent<PaddleBehaviour>().enableTouch = true;
            this.changedPlayerNumber = true;
            ball.GetComponent<BallBehaviour>().ResetScore();
        }
    }
}