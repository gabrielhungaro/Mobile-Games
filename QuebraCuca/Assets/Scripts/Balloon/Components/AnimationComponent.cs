using UnityEngine;
using System;
using System.Collections.Generic;
using AnimatorAccess;
//using UnityEditorInternal;
namespace Com.Globo.Sitio.MobileGames.Balloon
{

    public class AnimationComponent : BaseAnimatorAccess
    {

        public static string IDLE = "idle";
        public static string EXPLODE = "explode";
        public static List<string> _listOfAnimations;

        private bool _haveOtherAnimation;
        private int _numberOfAnimations;
        private string _path;
        private string _spriteName;
        private UnityEditorInternal.AnimatorController animController;

        private Animator _animator;

        void Start()
        {
            Debug.Log("START ANIMATION");
            _spriteName = this.gameObject.GetComponent<Balloon>().GetSpriteName();
            _path = this.gameObject.GetComponent<Balloon>().GetSpritePath();

            this.gameObject.AddComponent<Animator>();
            _animator = this.gameObject.GetComponent<Animator>();

            LoadAnimation();
        }

        public void UpdateAnimationSprite()
        {

        }

        public void LoadAnimation()
        {
            //Debug.Log(_path + "/controller");
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(_path + "controller");
            animController = _animator.runtimeAnimatorController as UnityEditorInternal.AnimatorController;
            //        _animator.GetCurrentAnimatorStateInfo(0).GetHashCode()
            InitialiseEventManager();
            //_animator.Play(IDLE);
            AddEvents();

        }

        public override void InitialiseEventManager()
        {
            int idleId = Animator.StringToHash("Base Layer.idle");
            int explodeId = Animator.StringToHash("Base Layer.explode");
            StateInfo stateExplode = new StateInfo(explodeId, 0, "Base Layer", "Base Layer.explode", "", 1f, false, false, "explode", 0.3166667f);
            StateInfo stateIdle = new StateInfo(idleId, 0, "Base Layer", "Base Layer.idle", "", 1f, false, false, "idle", 0.9999997f);
            if (StateInfos.ContainsKey(explodeId) == false)
            {
                StateInfos.Add(explodeId, stateExplode);
                StateInfos.Add(idleId, stateIdle);
            }
            //StateInfos.Add(stateIdExplode, new StateInfo(stateIdExplode, 0, "Base Layer", "Base Layer.explode", "", 1f, false, false, "explode", 0.3166667f));

            //TransitionInfos.Add(708569559, new TransitionInfo(708569559, "Base Layer.Walking -> Base Layer.Idle", 0, "Base Layer", -2010423537, 1432961145, true, 0.2068965f, false, 0f, false));
            this.State(explodeId).OnActive += Teste;
            //this.State(idleId).OnActive += Teste;

            this.OnStateChange += teste2;
            this.CheckForAnimatorStateChanges();
        }

        private void teste2(StateInfo info, LayerStatus status)
        {
            Debug.Log("state change teste: " + info.Name);
        }

        void GenerateStateInfos()
        {
            new StateInfo(_animator.GetCurrentAnimatorStateInfo(0).GetHashCode(), 0, "Base Layer", "Base Layer.idle", "", 1f, false, false, "idle", 0.9999997f);
        }

        private void Teste(StateInfo info, LayerStatus status)
        {
            Debug.Log("SAIU DO IDLE r15564341g5564r1g561455g4v5631v63451f31461f341438f91f863145616cv5341f861");
        }

        private void AddEvents()
        {
            AnimationEvent animationPickupEvent = new AnimationEvent();
            animationPickupEvent.functionName = "OnFinishedAnimation";

            UnityEditorInternal.StateMachine stateMachine = animController.GetLayer(0).stateMachine;

            for (int i = 0; i < stateMachine.stateCount; i++)
            {
                UnityEditorInternal.State state = stateMachine.GetState(i);
                AnimationClip clip = state.GetMotion() as AnimationClip;
                if (clip != null)
                {
                    if (clip.name == EXPLODE)
                    {
                        animationPickupEvent.time = clip.length;
                        animationPickupEvent.stringParameter = clip.name;
                        clip.AddEvent(animationPickupEvent);
                    }
                }
            }
        }

        void Update()
        {
            if (this.gameObject.GetComponent<Balloon>().GetIsTouching() == true && this.gameObject.GetComponent<Balloon>().GetCanExplode() == false && this.gameObject.GetComponent<Balloon>().GetCanBeDestroyed() == false)
            {
                this.gameObject.GetComponent<Balloon>().SetIsTouching(false);
                _animator.Play(EXPLODE);
            }
            //_animator.runtimeAnimatorController.
            /*if (_animator.GetCurrentAnimatorStateInfo(0).IsName(EXPLODE))
            {
                Debug.Log("testando " + _animator.GetCurrentAnimatorStateInfo(0) + "tamanho " + (_animator.GetCurrentAnimatorStateInfo(0).length * 10f));
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= (_animator.GetCurrentAnimatorStateInfo(0).length * 10f))
                {
                    Debug.Log("terminou de explodir");
                    this.gameObject.SetActive(false);
                }
            }
            else
            {
            
            }*/

        }

        void OnFinishedAnimation(string clipName)
        {
            _animator.enabled = false;
            this.enabled = false;
            this.gameObject.GetComponent<Balloon>().SetCanBeDestroyed(true);
        }

        public void Play(string anim)
        {
            foreach (string _anim in _listOfAnimations)
            {
                if (_anim != "")
                {
                    if (_anim == anim)
                    {
                        _animator.Play(anim);
                        return;
                    }
                }
            }
            Debug.LogError("Não existe nenhuma animação: " + anim + " cadastrada.");
        }

        public void SetHaveOtherAnimation(bool value)
        {
            _haveOtherAnimation = value;
        }

        public bool GetHaveOtherAnimation()
        {
            return _haveOtherAnimation;
        }

        public void SetNumberOfAnimation(int value)
        {
            _numberOfAnimations = value;
        }

        public int GetNumberOfAnimation()
        {
            return _numberOfAnimations;
        }

        public void SetPath(string value)
        {
            _path = value;
        }

        public string GetPath()
        {
            return _path;
        }

        public UnityEditorInternal.AnimatorController GetAnimationController()
        {
            return animController;
        }

        public void AddAnimation(string value)
        {
            _listOfAnimations.Add(value);
        }
    }
}