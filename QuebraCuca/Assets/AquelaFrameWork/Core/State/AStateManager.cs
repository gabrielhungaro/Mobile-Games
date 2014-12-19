using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Factory;

namespace AquelaFrameWork.Core.State
{
    public abstract class AStateManager : AFObject
    {
        ///////////////////////
        // PROPERTIES
        ///////////////////////

        protected AFEngine m_engine;

        [SerializeField]
        protected AState.EGameState currentStateID = AState.EGameState.NULL;
        [SerializeField]
        protected IState m_currentState;
        [SerializeField]
        protected AState.EGameState m_nextStateID;


        protected IStateFactory m_factory;

        protected IStateTransition m_transition;

        virtual public void Initialize( IStateFactory factory )
        {
            //TODO: Verify if factory is null case yes throw some error
            m_factory = factory;
            m_engine = AFEngine.Instance;

            AddTransition(new AFDefaultStateTransition());
        }

        virtual public void AddTransition( IStateTransition transition )
        {
            if (typeof(AFObject) == transition.GetType())
                ((AFObject)transition).gameObject.transform.parent = gameObject.transform;

            m_transition = transition;
            m_transition.Initialize(this);
        }

        virtual public void AFUpdate(double deltaTime)
        {
            if( m_nextStateID != null && !m_nextStateID.Equals(AState.EGameState.NULL) )
                ChangeState();
            //UnityEngine.Debug.Log(m_currentState);
            if (m_currentState != null && !currentStateID.Equals(AState.EGameState.NULL) )
                m_currentState.AFUpdate(deltaTime);
        }

        private void ChangeState()
        {
            if (!currentStateID.Equals(AState.EGameState.NULL))
            {
                if (m_currentState.IsDestroyable())
                {
                    m_currentState.AFDestroy();

                    if (m_currentState is AState)
                        Destroy( ( m_currentState as AState).gameObject );
                }
                else
                {
                    m_currentState.Pause();
                }
            }

            currentStateID = m_nextStateID;
            m_currentState = GetState(m_nextStateID);
            m_nextStateID = AState.EGameState.NULL;

            if (m_currentState is AState)
                (m_currentState as AState).transform.parent = gameObject.transform;

            m_currentState.Initialize();

            m_transition.End();
        }

        virtual public void GotoState( AState.EGameState newStateID )
        {
            m_transition.Begin();

            if ( newStateID == currentStateID )
                return;

            m_nextStateID = newStateID;
        }

        virtual public IState GetState(AState.EGameState newStateID)
        {
            return m_factory.CreateStateByID(newStateID);
        }

        virtual public void Pause() { m_currentState.Pause(); }
        virtual public void Resume() { m_currentState.Resume(); }


        public override void AFDestroy()
        {
            if( m_currentState != null )
            {
                m_currentState.AFDestroy();
            }

            base.AFDestroy();
        }
    }
}
