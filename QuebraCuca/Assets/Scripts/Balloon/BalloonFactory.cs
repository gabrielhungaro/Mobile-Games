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

        public Balloon CreateBalloon()
        {
            int chanceToAppearBalloon = Random.Range(0, 10);
            Balloon balloon = CreateEmptyBalloon();
            //switch (_balloonsConstants.GetListOfBalloonsType()[_randomBalloonNumber])

            switch (GetBalloonPercent())
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
                        CreateSimpleAddTime(balloon);
                    else
                        CreateSimpleRemovePoint(balloon);
                    break;
                default:
                    CreateSimpleAddPoint(balloon);
                    break;
            }

            string path = AFAssetManager.GetPathTargetPlatformWithResolution() + ConstantsPaths.GetBalloonAnimationsFolder();

            balloon.name = balloon.GetComponent<Balloon>().GetType();
            balloon.gameObject.GetComponent<Balloon>().SetSpritePath(path);
            balloon.gameObject.GetComponent<Balloon>().LoadSprite();
            //_balloon.AddComponent<BoxCollider>().size = new Vector3(_balloon.GetComponent<SpriteRenderer>().sprite.bounds.size.x, _balloon.GetComponent<SpriteRenderer>().sprite.bounds.size.y);

            //_balloon.AddComponent<AddPointComponent>().SetPointsToAdd(10);

            int randomXPoint = Random.Range(-Screen.width, Screen.width);
            float yPoint = Screen.height / 100f + balloon.GetComponent<Balloon>().GetSprite().bounds.size.y;
            balloon.transform.position = new Vector3(randomXPoint / 100f, -yPoint);
            _listOfBalloons.Add(balloon);
            return balloon;
        }

        public Balloon CreateEmptyBalloon()
        {
            Balloon balloon = AFObject.Create<Balloon>();
            balloon.gameObject.AddComponent<MoveComponent>().SetVelocity(.1f);
            MovementSystem.Instance.AddObjectToList(balloon.gameObject);
            balloon.gameObject.AddComponent<ClickComponent>();
            balloon.gameObject.AddComponent<DestroyerComponent>();

            return balloon;
        }

        public Balloon CreateSimpleAddPoint(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.gameObject.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_SIMPLE_ADD_POINT);
            balloon.gameObject.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_SIMPLE_ADD_POINT);
            balloon.gameObject.GetComponent<Balloon>().SetIsGood(true);
            balloon.gameObject.AddComponent<AddPointComponent>().SetPointsToAdd(10);

            return balloon;
        }

        public Balloon CreateSimpleRemovePoint(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.gameObject.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_SIMPLE_REMOVE_POINT);
            balloon.gameObject.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_SIMPLE_REMOVE_POINT);
            balloon.gameObject.GetComponent<Balloon>().SetIsGood(false);
            balloon.gameObject.AddComponent<RemovePointComponent>().SetPointsToRemove(10);

            return balloon;
        }

        public Balloon CreateSimpleSlowMotion(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.gameObject.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_SLOW_MOTION);
            balloon.gameObject.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_SLOW_MOTION);
            balloon.gameObject.GetComponent<Balloon>().SetIsGood(false);
            balloon.gameObject.AddComponent<SlowMotionComponent>().SetSlowMotionTime(10);

            return balloon;
        }


        public Balloon CreateSimpleFastFoward(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.gameObject.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_FAST_FOWARD);
            balloon.gameObject.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_FAST_FOWARD);
            balloon.gameObject.GetComponent<Balloon>().SetIsGood(false);
            balloon.gameObject.AddComponent<FastFowardComponent>().SetFastFowardTime(10);

            return balloon;
        }


        public Balloon CreateExplosive(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.gameObject.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_EXPLOSIVE);
            balloon.gameObject.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_EXPLOSIVE);
            balloon.gameObject.GetComponent<Balloon>().SetIsGood(false);
            balloon.gameObject.AddComponent<ExplosionComponent>();

            return balloon;
        }


        public Balloon CreateWord(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.gameObject.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_WORD);
            balloon.gameObject.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_WORD);
            balloon.gameObject.GetComponent<Balloon>().SetIsGood(false);
            balloon.gameObject.AddComponent<WordComponent>();

            return balloon;
        }


        public Balloon CreateNuclearExplosive(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.gameObject.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_NUCLEAR_EXPLOSIVE);
            balloon.gameObject.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_NUCLEAR_EXPLOSIVE);
            balloon.gameObject.GetComponent<Balloon>().SetIsGood(false);
            balloon.gameObject.AddComponent<NuclearExplosionComponent>();

            return balloon;
        }

        public Balloon CreateSimpleAddTime(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.gameObject.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_SIMLPE_ADD_TIME);
            balloon.gameObject.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_SIMLPE_ADD_TIME);
            balloon.gameObject.GetComponent<Balloon>().SetIsGood(true);
            balloon.gameObject.AddComponent<AddTimeComponent>().SetTimeToAdd(20);

            return balloon;
        }


        public Balloon CreateSimpleRemoveTime(Balloon balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.gameObject.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_SIMPLE_REMOVE_TIME);
            balloon.gameObject.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_SIMPLE_REMOVE_TIME);
            balloon.gameObject.GetComponent<Balloon>().SetIsGood(false);
            balloon.gameObject.AddComponent<RemoveTimeComponent>().SetTimeToRemove(10);

            return balloon;
        }

        private string GetBalloonPercent()
        {
            /*int random = Random.Range(0, 100);

            if (random <= ConstantsBalloons.LOW_CHANCE)
            {
                return ConstantsBalloons._listOfLowChanceBalloons[Random.Range(0, ConstantsBalloons._listOfLowChanceBalloons.Count)];
            }
            else if (random <= ConstantsBalloons.MEDIUM_CHANCE)
            {
                return ConstantsBalloons._listOfMediumChanceBalloons[Random.Range(0, ConstantsBalloons._listOfMediumChanceBalloons.Count)];
            }
            else if (random <= ConstantsBalloons.HIGH_CHANCE)
            {
                return ConstantsBalloons._listOfHighChanceBalloons[Random.Range(0, ConstantsBalloons._listOfHighChanceBalloons.Count)];
            }*/
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

