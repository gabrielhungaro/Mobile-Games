using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Utils;
using AquelaFrameWork.View;
using AquelaFrameWork.Components;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class BalloonFactory : ASingleton<BalloonFactory>
    {

        private List<Balloon> _listOfBalloons;
        private float _originalTimeToSpawn = 1;
        private float _timeToSpawnBalloon = 1;
        private int _seconds;
        private int _minutes;
        private int _ticks;
        private MovementSystem _movementSystem;
        private bool _canCreateBalloon;
        private int _numberOfBalloons;

        // Use this for initialization

        public void Initialize()
        {
            _canCreateBalloon = true;

            UnityEngine.Debug.Log("[ BALLOON_FACTORY ] - START");
            _listOfBalloons = new List<Balloon>();

            //this.addNew("", ConstantsBallooons.TYPE_SIMPLE_REMOVE_POINT);
        }

        // Update is called once per frame
        void Update()
        {
            _ticks++;
            //Debug.Log("Time.deltaTime: " + Time.deltaTime);
            if (TimeController.GetTimeScaleActive() == true)
            {
                switch (TimeController.GetTimeMode())
                {
                    case TimeController.FAST_TIME:
                        _timeToSpawnBalloon = _originalTimeToSpawn * TimeController.GetTimeScaleFactor();
                        break;
                    case TimeController.SLOW_TIME:
                        _timeToSpawnBalloon = _originalTimeToSpawn / TimeController.GetTimeScaleFactor();
                        break;
                    default:
                        _timeToSpawnBalloon = _originalTimeToSpawn;
                        break;
                }
            }
            else
            {
                _timeToSpawnBalloon = _originalTimeToSpawn;
            }
        }

        public Balloon CreateBalloon( string type = "" )
        {
            Balloon balloon = CreateEmptyBalloon();
            //switch (_balloonsConstants.GetListOfBalloonsType()[_randomBalloonNumber])

            string ballonType = type == "" ? GetBalloonPercent() : type;

            switch (ballonType)
            {
                case ConstantsBalloons.TYPE_SIMPLE_ADD_POINT:
                    CreateSimpleAddPoint(balloon);
                    break;
                case ConstantsBalloons.TYPE_SIMPLE_REMOVE_POINT:
                    CreateSimpleRemovePoint(balloon);
                    break;
                case ConstantsBalloons.TYPE_SLOW_MOTION:
                    CreateSimpleSlowMotion(balloon);
                    break;
                case ConstantsBalloons.TYPE_FAST_FOWARD:
                    CreateSimpleFastFoward(balloon);
                    break;
                case ConstantsBalloons.TYPE_EXPLOSIVE:
                    CreateExplosive(balloon);
                    break;
                case ConstantsBalloons.TYPE_WORD:
                    CreateWord(balloon);
                    break;
                case ConstantsBalloons.TYPE_NUCLEAR_EXPLOSIVE:
                    CreateNuclearExplosive(balloon);
                    break;
                case ConstantsBalloons.TYPE_SIMLPE_ADD_TIME:
                    if (GameController.GetGameMode() == GameController.TIME_TRIAL)
                        CreateSimpleAddTime(balloon);
                    else
                        CreateSimpleAddPoint(balloon);
                    break;

                case ConstantsBalloons.TYPE_SIMPLE_REMOVE_TIME:

                    if (GameController.GetGameMode() == GameController.TIME_TRIAL)
                        CreateSimpleRemoveTime(balloon);
                    else
                        CreateSimpleRemovePoint(balloon);
                    break;
                default:
                    CreateSimpleAddPoint(balloon);
                    break;
            }

            _numberOfBalloons++;

            string path = AFAssetManager.GetPathTargetPlatformWithResolution() + ConstantsPaths.GetBalloonAnimationsFolder();

            balloon.name = balloon.GetType() + _numberOfBalloons.ToString();
            balloon.SetSpritePath(path);
            //balloon.Initialize();

            _listOfBalloons.Add(balloon);

            balloon.Initialize();
            balloon.gameObject.SetActive(false);

            return balloon;
        }


        public List<Balloon> CreatePool( string type, int qtd )
        {
            List<Balloon> ballons = new List<Balloon>(qtd);

            for (int i = 0; i < qtd; ++i)
                ballons.Add(CreateBalloon(type));

            return ballons;
        }

        public Balloon CreateEmptyBalloon()
        {
            Balloon balloon = AFObject.Create<Balloon>();

            balloon.gameObject.AddComponent<MoveComponent>().SetVelocity(.05f);
            balloon.gameObject.AddComponent<ClickComponent>();
            balloon.gameObject.AddComponent<DestroyerComponent>();
            //balloon.gameObject.AddComponent<AnimationController>();
            MovementSystem.Instance.AddObjectToList(balloon.gameObject);

            return balloon;
        }

        public Balloon CreateSimpleAddPoint(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.SetType(ConstantsBalloons.TYPE_SIMPLE_ADD_POINT);
            balloon.SetSpriteName(ConstantsBalloons.TYPE_SIMPLE_ADD_POINT);
            balloon.SetIsGood(true);
            balloon.gameObject.AddComponent<AddPointComponent>().SetPointsToAdd(10);

            return balloon;
        }

        public Balloon CreateSimpleRemovePoint(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.SetType(ConstantsBalloons.TYPE_SIMPLE_REMOVE_POINT);
            balloon.SetSpriteName(ConstantsBalloons.TYPE_SIMPLE_REMOVE_POINT);
            balloon.SetIsGood(false);
            balloon.gameObject.AddComponent<RemovePointComponent>().SetPointsToRemove(10);

            return balloon;
        }

        public Balloon CreateSimpleSlowMotion(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.SetType(ConstantsBalloons.TYPE_SLOW_MOTION);
            balloon.SetSpriteName(ConstantsBalloons.TYPE_SLOW_MOTION);
            balloon.SetIsGood(false);
            balloon.gameObject.AddComponent<SlowMotionComponent>().SetSlowMotionTime(10);

            return balloon;
        }


        public Balloon CreateSimpleFastFoward(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.SetType(ConstantsBalloons.TYPE_FAST_FOWARD);
            balloon.SetSpriteName(ConstantsBalloons.TYPE_FAST_FOWARD);
            balloon.SetIsGood(false);
            balloon.gameObject.AddComponent<FastFowardComponent>().SetFastFowardTime(10);

            return balloon;
        }


        public Balloon CreateExplosive(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.SetType(ConstantsBalloons.TYPE_EXPLOSIVE);
            balloon.SetSpriteName(ConstantsBalloons.TYPE_EXPLOSIVE);
            balloon.SetIsGood(false);
            balloon.gameObject.AddComponent<ExplosionComponent>();

            return balloon;
        }


        public Balloon CreateWord(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.SetType(ConstantsBalloons.TYPE_WORD);
            balloon.SetSpriteName(ConstantsBalloons.TYPE_WORD);
            balloon.SetIsGood(false);
            balloon.gameObject.AddComponent<WordComponent>();

            return balloon;
        }


        public Balloon CreateNuclearExplosive(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.SetType(ConstantsBalloons.TYPE_NUCLEAR_EXPLOSIVE);
            balloon.SetSpriteName(ConstantsBalloons.TYPE_NUCLEAR_EXPLOSIVE);
            balloon.SetIsGood(false);
            balloon.gameObject.AddComponent<NuclearExplosionComponent>();

            return balloon;
        }

        public Balloon CreateSimpleAddTime(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.SetType(ConstantsBalloons.TYPE_SIMLPE_ADD_TIME);
            balloon.SetSpriteName(ConstantsBalloons.TYPE_SIMLPE_ADD_TIME);
            balloon.SetIsGood(true);
            balloon.gameObject.AddComponent<AddTimeComponent>().SetTimeToAdd(20);

            return balloon;
        }


        public Balloon CreateSimpleRemoveTime(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.SetType(ConstantsBalloons.TYPE_SIMPLE_REMOVE_TIME);
            balloon.SetSpriteName(ConstantsBalloons.TYPE_SIMPLE_REMOVE_TIME);
            balloon.SetIsGood(false);
            balloon.gameObject.AddComponent<RemoveTimeComponent>().SetTimeToRemove(10);

            return balloon;
        }

        public string GetBalloonPercent()
        {
            int random = Random.Range(0, 100);

            if (random <= ConstantsBalloons.LOW_CHANCE)
            {
                if (ConstantsBalloons._listOfLowChanceBalloons.Count > 0)
                    return ConstantsBalloons._listOfLowChanceBalloons[Random.Range(0, ConstantsBalloons._listOfLowChanceBalloons.Count - 1)];
            }
            else if (random <= ConstantsBalloons.MEDIUM_CHANCE)
            {
                return ConstantsBalloons._listOfMediumChanceBalloons[Random.Range(0, ConstantsBalloons._listOfMediumChanceBalloons.Count - 1)];
            }
            else if (random <= ConstantsBalloons.HIGH_CHANCE)
            {
                return ConstantsBalloons._listOfHighChanceBalloons[Random.Range(0, ConstantsBalloons._listOfHighChanceBalloons.Count - 1)];
            }
            return "";
        }

        public void SetListOfBalloons(List<Balloon> value)
        {
            _listOfBalloons = value;
        }

        public List<Balloon> GetListOfBalloons()
        {
            return _listOfBalloons;
        }

        public void SetCanCreateBalloon(bool value)
        {
            _canCreateBalloon = value;
        }

        public bool GetCanCreateBalloon()
        {
            return _canCreateBalloon;
        }
    }
}

