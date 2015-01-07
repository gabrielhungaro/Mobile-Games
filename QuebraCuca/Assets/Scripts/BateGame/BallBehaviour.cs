using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AquelaFrameWork.Sound;

namespace BateRebate
{
    public class BallBehaviour : MonoBehaviour
    {
        public float speed = 15f;
        public float timeToLaunch = 3f;
        public Vector3 posInit;

        public int pointsLeft = 0;
        public int pointsRight = 0;
        public GameObject scoreLeftOver;
        public GameObject scoreRightOver;

        private Vector2 lastVelocity;

        protected bool m_isStarted = false;

        public bool IsStarted()
        {
            return m_isStarted;
        }

        public void AwakeBall()
        {
            posInit = this.gameObject.transform.position;
            InvokeResetBall();
        }
        public void InvokeResetBall()
        {
            m_isStarted = false;

            if (!BateController.Instance.IsPaused)
            {
                this.gameObject.transform.position = posInit;
                rigidbody2D.velocity = Vector2.zero.normalized;
                CancelInvoke("StartBall");
                Invoke("StartBall", timeToLaunch);
            }
        }
        public void CancellBallInvoke()
        {
            if (BateController.Instance.IsPaused)
            {
                CancelInvoke("StartBall");
            }
        }
        public void ScorePoint(int player)
        {
            if (player == 1)
            {
                pointsLeft++;
                this.scoreLeftOver.GetComponent<Text>().text = pointsLeft.ToString();
            }
            else if (player == 2)
            {
                pointsRight++;
                this.scoreRightOver.GetComponent<Text>().text = pointsRight.ToString();
            }
        }
        public void ResetScore()
        {
            pointsLeft = 0;
            pointsRight = 0;
            this.scoreLeftOver.GetComponent<Text>().text = pointsLeft.ToString();
            this.scoreRightOver.GetComponent<Text>().text = pointsRight.ToString();
        }
        public void SaveState()
        {
            Debug.Log("SavedState: " + lastVelocity);
            lastVelocity = rigidbody2D.velocity;
        }
        public void Pause()
        {
            rigidbody2D.velocity = Vector2.zero.normalized;
        }
        public void UnPause()
        {
            Debug.Log("LastVel: " + lastVelocity);
            rigidbody2D.velocity = lastVelocity;

            if (!IsStarted())
                InvokeResetBall();
        }
        private void StartBall()
        {
            if (!BateController.Instance.IsPaused)
            {
                int randomX = Random.Range(0, 4);
                Vector2 vel = Vector2.one.normalized;
                switch (randomX)
                {
                    case 0:
                        vel.x *= -speed;
                        vel.y *= -speed;
                        break;
                    case 1:
                        vel.x *= speed;
                        vel.y *= speed;
                        break;
                    case 2:
                        vel.x *= speed;
                        vel.y *= -speed;
                        break;
                    case 3:
                        vel.x *= -speed;
                        vel.y *= speed;
                        break;
                }
                rigidbody2D.velocity = vel;
                transform.Rotate(Vector3.forward * -5);
                PlayCollisionWithPaddleSound();

                m_isStarted = true;
            }
        }
        private void PlayCollisionWithWallSound()
        {
            if (BateController.Instance.IsSounding)
            {
                if (BateController.Instance.playSounds) AFSoundManager.Instance.Play(BateController.Instance.somBallHitWall);
            }
        }
        private void PlayCollisionWithPaddleSound()
        {
            if (BateController.Instance.IsSounding)
            {
                if (BateController.Instance.playSounds) AFSoundManager.Instance.Play(BateController.Instance.somBallHitPaddle);
            }
        }
        private void PlayScoreSound(bool leftgoal = false)
        {
            if (BateController.Instance.IsSounding)
            {
                bool dontcheer = (leftgoal && BateController.Instance.PlayerNumber == 1);
                if (dontcheer)
                {
                    if (BateController.Instance.playSounds) AFSoundManager.Instance.Play(BateController.Instance.somIAScores);
                }
                else
                {
                    if (BateController.Instance.playSounds) AFSoundManager.Instance.Play(BateController.Instance.somPlayerScores);
                }
            }
        }
        void OnCollisionEnter2D(Collision2D col)
        {
            if (!BateController.Instance.IsPaused)
            {
                if (rigidbody2D.velocity == Vector2.zero.normalized)
                {
                    UnPause();
                }
                if (col.gameObject.name == "paddleLeft")
                {
                    PlayCollisionWithPaddleSound();
                    float y = hitFactor(transform.position, col.transform.position, ((BoxCollider2D)col.collider).size.y);
                    Vector2 dir = new Vector2(1, y).normalized;
                    rigidbody2D.velocity = dir * speed;
                }
                if (col.gameObject.name == "paddleRight")
                {
                    PlayCollisionWithPaddleSound();
                    float y = hitFactor(transform.position, col.transform.position, ((BoxCollider2D)col.collider).size.y);
                    Vector2 dir = new Vector2(-1, y).normalized;
                    rigidbody2D.velocity = dir * speed;
                }
                if (col.gameObject.name == "wallR")
                {
                    PlayScoreSound();
                    InvokeResetBall();
                    ScorePoint(1);
                    //P1 Scores!
                }
                if (col.gameObject.name == "wallL")
                {
                    PlayScoreSound(true);
                    InvokeResetBall();
                    ScorePoint(2);
                    //P2 Scores!
                }
                if (col.gameObject.name == "wallU" || col.gameObject.name == "wallD")
                {
                    PlayCollisionWithWallSound();
                }
            }
            else
            {
                Pause();
            }
        }
        float hitFactor(Vector2 ballPos, Vector2 paddlePos, float paddleHeight)
        {
            return (ballPos.y - paddlePos.y) / paddleHeight;
        }
    }
}
