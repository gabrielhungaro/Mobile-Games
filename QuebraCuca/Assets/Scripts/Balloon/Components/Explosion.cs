using UnityEngine;
using System.Collections.Generic;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class Explosion : MonoBehaviour
    {
        private string _path;

        void Start()
        {
            this.gameObject.AddComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(_path + "/controllerExplosion");
            this.gameObject.AddComponent<SpriteRenderer>();

            AnimationEvent animationPickupEvent = new AnimationEvent();
            animationPickupEvent.functionName = "OnFinishedExplosion";

            UnityEditorInternal.AnimatorController animController = this.gameObject.GetComponent<Animator>().runtimeAnimatorController as UnityEditorInternal.AnimatorController;
            UnityEditorInternal.StateMachine stateMachine = animController.GetLayer(0).stateMachine;
            Debug.Log(animController.name);

            for (int i = 0; i < stateMachine.stateCount; i++)
            {
                UnityEditorInternal.State state = stateMachine.GetState(i);
                AnimationClip clip = state.GetMotion() as AnimationClip;
                Debug.Log(clip.name);
                if (clip.name == "explosion")
                {
                    Debug.Log("adicionando evento");
                    animationPickupEvent.time = clip.length;
                    animationPickupEvent.stringParameter = clip.name;
                    clip.AddEvent(animationPickupEvent);
                }
            }
            this.gameObject.GetComponent<Animator>().Play("explosion");

            DestroyOtherBalloons();
            FindObjectOfType<BalloonFactory>().SetCanCreateBalloon(false);
        }

        private void DestroyOtherBalloons()
        {
            List<Balloon> listOfBalloons = FindObjectOfType<BalloonFactory>().GetListOfBalloons();
            for (int i = 0; i < listOfBalloons.Count; i++)
            {
                Destroy(listOfBalloons[i]);
            }
        }

        void OnFinishedExplosion(string clipName)
        {
            GameObject.Destroy(this.gameObject);
            GameController.SetEndedGame(true);
        }

        public void SetPath(string value)
        {
            _path = value;
        }

    }
}
