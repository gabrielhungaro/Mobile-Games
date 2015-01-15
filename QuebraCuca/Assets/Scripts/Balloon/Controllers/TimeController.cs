using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class TimeController : MonoBehaviour
    {
        public static int _seconds = 30;
        public static int _minutes = 0;
        private static string _time;
        private int _ticks;
        private int _frames;
        private float _frameAccum;
        private float _timeLeft;
        private float _updateInterval = .5f;
        private float _fps;
        private GameObject _gameControllerObj;
        private static bool _isTimeScaleActive;
        private static int _countWithScaledTime;
        private int _timeWithScaledTime = 10;
        private static float _originalTimeScale;
        private static float _originalMaximumDeltaTime;
        private static float _originalFixedDeltaTime;
        private static string _timeMode;
        public const string NORMAL_TIME = "normal";
        public const string FAST_TIME = "fast";
        public const string SLOW_TIME = "slow";
        private static float _timeScaleFactor = 1;


        public void Start()
        {
            _originalTimeScale = Time.timeScale;
            _originalMaximumDeltaTime = Time.maximumDeltaTime;
            _originalFixedDeltaTime = Time.fixedDeltaTime;

            GameController gameController = FindObjectOfType<GameController>();
            _gameControllerObj = gameController.gameObject;
            _seconds = 30;
            _minutes = 0;
            Time.timeScale = 1;
            _timeMode = NORMAL_TIME;
        }

        void Update()
        {
            if (!_gameControllerObj.GetComponent<GameController>().GetIsPaused())
            {
                _timeLeft -= Time.deltaTime;
                _frameAccum += Time.timeScale / Time.deltaTime;
                _frames++;
                if (_timeLeft <= 0)
                {
                    _fps = _frameAccum / _frames;
                    _timeLeft = _updateInterval;
                    _frameAccum = 0;
                    _frames = 0;
                }
                _ticks++;

                if (_ticks >= _fps)
                {
                    _ticks = 0;
                    _seconds--;
                }

                if (_seconds <= 0)
                {
                    if (_minutes <= 0)
                    {
                        UpdateTime();
                        EndGame();
                    }
                    else
                    {
                        _minutes--;
                        _seconds = 59;
                    }
                }

                if (_isTimeScaleActive == true)
                {
                    _countWithScaledTime++;
                    float timeToReturnScaledTime;
                    switch (_timeMode)
                    {
                        case FAST_TIME:
                            timeToReturnScaledTime = _timeWithScaledTime * _timeScaleFactor;
                            break;
                        case SLOW_TIME:
                            timeToReturnScaledTime = _timeWithScaledTime / _timeScaleFactor;
                            break;
                        default:
                            timeToReturnScaledTime = _timeWithScaledTime;
                            break;
                    }
                    if (_countWithScaledTime * Time.deltaTime > timeToReturnScaledTime)
                    {
                        Debug.Log("DESATIVANDO TIME SCALE");
                        _countWithScaledTime = 0;
                        DeactiveTimeScale();
                    }
                }

                UpdateTime();
            }
        }

        private void EndGame()
        {
            if (GameController.GetGameMode() == GameController.TIME_TRIAL)
                GameController.SetEndedGame(true);
        }

        private void UpdateTime()
        {
            string _sec = _seconds.ToString();
            string _min = _minutes.ToString();
            if (_seconds < 10)
            {
                _sec = "0" + _seconds.ToString();
            }
            if (_minutes < 10)
            {
                _min = "0" + _minutes.ToString();
            }
            _time = _min + ":" + _sec;
        }

        private static void UpdateStaticTime()
        {
            string _sec = _seconds.ToString();
            string _min = _minutes.ToString();
            if (_seconds < 10)
            {
                _sec = "0" + _seconds.ToString();
            }
            if (_minutes < 10)
            {
                _min = "0" + _minutes.ToString();
            }
            _time = _min + ":" + _sec;
        }

        public static void SetSeconds(int value)
        {
            _seconds = value;
        }

        public static int GetSeconds()
        {
            return _seconds;
        }

        public static void AddTime(int value)
        {
            _seconds += value;
            if (_seconds >= 60)
            {
                _seconds = _seconds - 60;
                _minutes++;
            }
        }

        public static void RemoveTime(int value)
        {
            _seconds -= value;
            if (_seconds <= 0)
            {
                if (_minutes <= 0)
                {
                    _seconds = 0;
                    UpdateStaticTime();
                    GameController.SetEndedGame(true);
                }
                else
                {
                    _seconds = 60 + _seconds;
                    _minutes--;
                }
            }
        }

        public static void SetMinutes(int value)
        {
            _minutes = value;
        }

        public static int GetMinutes()
        {
            return _minutes;
        }

        public static string GetTime()
        {
            return _time;
        }

        public static void ActiveSlowMotion(float timeFactor)
        {
            _timeMode = SLOW_TIME;
            _isTimeScaleActive = true;
            _timeScaleFactor = timeFactor;
            //_countWithScaledTime = 0;
//             Time.timeScale = _originalTimeScale / timeFactor;
//             Time.fixedDeltaTime = _originalFixedDeltaTime / timeFactor;
//             Time.maximumDeltaTime = _originalMaximumDeltaTime / timeFactor;
        }

        public static void ActiveFastFoward(float timeFactor)
        {
            _timeMode = FAST_TIME;
            _isTimeScaleActive = true;
            _timeScaleFactor = timeFactor;
            _countWithScaledTime = 0;
//             Time.timeScale = _originalTimeScale * timeFactor;
//             Time.fixedDeltaTime = _originalFixedDeltaTime * timeFactor;
//             Time.maximumDeltaTime = _originalMaximumDeltaTime * timeFactor;
        }

        public static void DeactiveTimeScale()
        {
            _timeMode = NORMAL_TIME;
            _isTimeScaleActive = false;
//             Time.timeScale = _originalTimeScale;
//             Time.fixedDeltaTime = _originalFixedDeltaTime;
//             Time.maximumDeltaTime = _originalMaximumDeltaTime;

            _timeScaleFactor = 1.0f;
        }

        public static bool GetTimeScaleActive()
        {
            return _isTimeScaleActive;
        }

        public static string GetTimeMode()
        {
            return _timeMode;
        }

        public static void SetTimeScaleFactor(float value)
        {
            _timeScaleFactor = value;
        }

        public static float GetTimeScaleFactor()
        {
            return _timeScaleFactor;
        }
    }
}
