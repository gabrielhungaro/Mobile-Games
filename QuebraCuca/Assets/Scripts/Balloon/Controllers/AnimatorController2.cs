// GENERATED CODE ==> EDITS WILL BE LOST AFTER NEXT GENERATION!
// Version for Mac / UNIX 

using UnityEngine;


namespace AnimatorAccess
{
    /// <summary>
    /// Convenience class to access Animator states and parameters.
    /// Edits will be lost when this class is regenerated. 
    /// Hint: Editing might be useful after renaming animator items in complex projects:
    ///  - Right click on an obsolete member and select Refactor/Rename. 
    ///  - Change it to the new name. 
    ///  - Delete this member to avoid comile error CS0102 ... already contains a definition ...''. 
    /// </summary>
    public class AnimatorController2 : BaseAnimatorAccess
    {

        /// <summary>
        /// Hash of Animator state Base Layer.Idle
        /// </summary>
        public readonly int stateIdIdle = 1432961145;

        public readonly int stateIdExplode = 0;


        public override int AllTransitionsHash
        {
            get { return 859155473; }
        }


        public void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public override void InitialiseEventManager()
        {
            //this.State(stateIdIdle).OnEnter
            StateInfos.Add(stateIdIdle, new StateInfo(stateIdIdle, 0, "Base Layer", "Base Layer.idle", "", 1f, false, false, "idle", 0.9999997f));
            StateInfos.Add(stateIdExplode, new StateInfo(stateIdExplode, 0, "Base Layer", "Base Layer.explode", "", 1f, false, false, "explode", 0.3166667f));
            
            TransitionInfos.Add(708569559, new TransitionInfo(708569559, "Base Layer.Walking -> Base Layer.Idle", 0, "Base Layer", -2010423537, 1432961145, true, 0.2068965f, false, 0f, false));
            
        }

        /// <summary>
        /// true if the current Animator state of layer 0 is  "Base Layer.Idle".
        /// </summary>
        public bool IsIdle()
        {
            return stateIdIdle == animator.GetCurrentAnimatorStateInfo(0).nameHash;
        }

        /// <summary>
        /// true if the given (state) nameHash equals Animator.StringToHash ("Base Layer.Idle").
        /// </summary>
        public bool IsIdle(int nameHash)
        {
            return nameHash == stateIdIdle;
        }

        private void FixedUpdate()
        {
            CheckForAnimatorStateChanges();
        }
    }
}