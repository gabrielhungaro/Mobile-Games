using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

using UnityEngine;

namespace AquelaFrameWork.Core.State
{
    public class AFSingleTransition : AFObject , IStateTransition
    {
        private static readonly int SPRITE_SORT_LAYER = 1100;

        //private System.Threading.Thread m_thread;
        private GameObject m_trasition;
        private AFObject[] m_updatableComponents;
        private AStateManager m_stateManager;

        private bool m_isStateLoading = false;

        public void AddTrasitionView( GameObject transitionGO )
        {
            m_trasition = transitionGO;
            m_updatableComponents = m_trasition.GetComponents<AFObject>();
            //Todo: IMPLEMENT MULTHREAD

            SpriteRenderer[] spRenderer = m_trasition.GetComponents<SpriteRenderer>();

            for (int i = 0; i < spRenderer.Length; ++i )
                if (!AFObject.IsNull(spRenderer[i]))
                    spRenderer[i].sortingOrder = SPRITE_SORT_LAYER;

            if (m_updatableComponents.Length > 0)
                CreateThread();

            Hide();
        }

        public void CreateThread()
        {
//             if (m_thread == null)
//             {
//                 m_thread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadUpdate));
//             }
//             else
//                 AFDebug.LogWarning("The transition can not have more than one Thread");
        }

        public void ThreadUpdate()
        {
            this.AFUpdate(UnityEngine.Time.smoothDeltaTime);
            //Thread.Sleep(0);
        }

        public override void AFUpdate(double time)
        {
            if (m_updatableComponents != null )
                for (int i = 0; i < m_updatableComponents.Length; ++i)
                    m_updatableComponents[i].AFUpdate(time);
        }
        public IStateTransition Show()
        {
            //if (m_thread != null)
               // m_thread.Start();
            m_trasition.SetActive(true);
            return this;
        }

        public IStateTransition Hide()
        {
            //if (m_thread != null)
               // m_thread.Abort();
            m_trasition.SetActive(false);

            return this;
        }

        public void Initialize(AStateManager stateManager)
        {
            m_stateManager = stateManager;
            AddTrasitionView(this.gameObject);
        }

        public void Begin()
        {
            Show();
        }

        public void End()
        {
            Hide();
        }

        public void Remove()
        {
//             if (m_thread != null)
//             {
//                 m_thread.Suspend();
//                 m_thread = null;
//             }
            
        }
    }
}
