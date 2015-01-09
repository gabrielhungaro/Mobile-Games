using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using AquelaFrameWork.Core.Asset;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class BalloonFactory : MonoBehaviour
    {

        private List<GameObject> _listOfBalloons;
        private float _originalTimeToSpawn = 1;
        private float _timeToSpawnBalloon = 1;
        private int _seconds;
        private int _minutes;
        private int _ticks;
        private ConstantsBalloons _balloonsConstants;
        private MovementSystem _movementSystem;
        private bool _canCreateBalloon;

        // Use this for initialization

        public void Start()
        {
            _canCreateBalloon = true;
            _balloonsConstants = new ConstantsBalloons();
            _balloonsConstants.Start();

            _movementSystem = this.gameObject.GetComponent<MovementSystem>();//FindObjectOfType<MovementSystem>();

            UnityEngine.Debug.Log("[ BALLOON_FACTORY ] - START");
            _listOfBalloons = new List<GameObject>();

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
            //Debug.Log("Time.timeScale: " + Time.timeScale + " || Time.deltaTime: " + Time.deltaTime + "_timeToSpawnBalloon: " + _timeToSpawnBalloon);
            if (_ticks * Time.deltaTime > _timeToSpawnBalloon && _canCreateBalloon)
            {
                _ticks = 0;
                createBalloon();
            }
        }

        void createBalloon()
        {
            int chanceToAppearBalloon = Random.Range(0, 10);
            GameObject balloon = CreateEmptyBalloon();
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
            balloon.GetComponent<Balloon>().SetSpritePath(path);
            balloon.AddComponent<AnimationComponent>();
            balloon.GetComponent<Balloon>().LoadSprite();
            //_balloon.AddComponent<BoxCollider>().size = new Vector3(_balloon.GetComponent<SpriteRenderer>().sprite.bounds.size.x, _balloon.GetComponent<SpriteRenderer>().sprite.bounds.size.y);

            //_balloon.AddComponent<AddPointComponent>().SetPointsToAdd(10);

            int randomXPoint = Random.Range(-Screen.width, Screen.width);
            float yPoint = Screen.height / 100f + balloon.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
            balloon.transform.position = new Vector3(randomXPoint / 100f, -yPoint);
            _listOfBalloons.Add(balloon);
        }

        public GameObject CreateEmptyBalloon()
        {
            GameObject balloon = new GameObject();
            balloon.AddComponent<Balloon>();
            balloon.AddComponent<MoveComponent>().SetVelocity(.1f);
            _movementSystem.AddObjectToList(balloon);
            balloon.AddComponent<ClickComponent>();
            balloon.AddComponent<DestroyerComponent>();

            return balloon;
        }

        public GameObject CreateSimpleAddPoint(GameObject balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_SIMPLE_ADD_POINT);
            balloon.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_SIMPLE_ADD_POINT);
            balloon.GetComponent<Balloon>().SetIsGood(true);
            balloon.AddComponent<AddPointComponent>().SetPointsToAdd(10);

            return balloon;
        }

        public GameObject CreateSimpleRemovePoint(GameObject balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_SIMPLE_REMOVE_POINT);
            balloon.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_SIMPLE_REMOVE_POINT);
            balloon.GetComponent<Balloon>().SetIsGood(false);
            balloon.AddComponent<RemovePointComponent>().SetPointsToRemove(10);

            return balloon;
        }

        public GameObject CreateSimpleSlowMotion(GameObject balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_SLOW_MOTION);
            balloon.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_SLOW_MOTION);
            balloon.GetComponent<Balloon>().SetIsGood(false);
            balloon.AddComponent<SlowMotionComponent>().SetSlowMotionTime(10);

            return balloon;
        }


        public GameObject CreateSimpleFastFoward(GameObject balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_FAST_FOWARD);
            balloon.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_FAST_FOWARD);
            balloon.GetComponent<Balloon>().SetIsGood(false);
            balloon.AddComponent<FastFowardComponent>().SetFastFowardTime(10);

            return balloon;
        }


        public GameObject CreateExplosive(GameObject balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_EXPLOSIVE);
            balloon.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_EXPLOSIVE);
            balloon.GetComponent<Balloon>().SetIsGood(false);
            balloon.AddComponent<ExplosionComponent>();

            return balloon;
        }


        public GameObject CreateWord(GameObject balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_WORD);
            balloon.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_WORD);
            balloon.GetComponent<Balloon>().SetIsGood(false);
            balloon.AddComponent<WordComponent>();

            return balloon;
        }


        public GameObject CreateNuclearExplosive(GameObject balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_NUCLEAR_EXPLOSIVE);
            balloon.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_NUCLEAR_EXPLOSIVE);
            balloon.GetComponent<Balloon>().SetIsGood(false);
            balloon.AddComponent<NuclearExplosionComponent>();

            return balloon;
        }

        public GameObject CreateSimpleAddTime(GameObject balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_SIMLPE_ADD_TIME);
            balloon.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_SIMLPE_ADD_TIME);
            balloon.GetComponent<Balloon>().SetIsGood(true);
            balloon.AddComponent<AddTimeComponent>().SetTimeToAdd(20);

            return balloon;
        }


        public GameObject CreateSimpleRemoveTime(GameObject balloon = null)
        {
            if (balloon == null)
                balloon = CreateEmptyBalloon();

            balloon.GetComponent<Balloon>().SetType(ConstantsBalloons.TYPE_SIMPLE_REMOVE_TIME);
            balloon.GetComponent<Balloon>().SetSpriteName(ConstantsBalloons.TYPE_SIMPLE_REMOVE_TIME);
            balloon.GetComponent<Balloon>().SetIsGood(false);
            balloon.AddComponent<RemoveTimeComponent>().SetTimeToRemove(10);

            return balloon;
        }

        private string GetBalloonPercent()
        {
            int random = Random.Range(0, 100);

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
            }
            return "";
        }

        public void SetListOfBalloons(List<GameObject> value)
        {
            _listOfBalloons = value;
        }

        public List<GameObject> GetListOfBalloons()
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

