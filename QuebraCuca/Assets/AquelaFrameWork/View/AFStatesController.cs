using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using AquelaFrameWork.Core;


namespace AquelaFrameWork.View
{
    public class AFStatesController : AFObject , IAnimatable
    {

#if UNITY_EDITOR
        [SerializeField]
        protected string currentStateName = "";
#endif //UNITY_EDITOR

        [SerializeField]
        protected int m_defaultStateID = 0;
        [SerializeField]
        protected int m_currentStateID = 0;

        [SerializeField]
        protected int m_lastStateID = 0;
        [SerializeField]
        protected int m_nextStateID = 0;

        protected AMovieClip m_currentState;
        [SerializeField]
        protected Dictionary<int, AMovieClip> m_states = new Dictionary<int, AMovieClip>();
        [SerializeField]
        protected bool m_update = true;

        public AMovieClip GetState(string name) { return GetState(name.GetHashCode()); }
        public AMovieClip GetState(int stateID) 
        {
            if (m_states.ContainsKey(stateID)) UnityEngine.Debug.LogWarning("The state was not here");
            return m_states[stateID]; 
            
        }

        public void Add(string name, AMovieClip state, bool defaultState = false)
        {
            Add(name.GetHashCode(), state, defaultState);
        }

        public void Add(int name, AMovieClip state, bool defaultState = false)
        {
            if (m_states.ContainsKey(name))
            {
                AFDebug.LogWarning("The State " + name + " already was created"); 
                
                return;
            }

            if (!defaultState)
            {
                state.Stop();
            }
            else
            {
                if (m_defaultStateID != 0)
                {
                    UnityEngine.Debug.LogWarning("The default state was changed to <" + name + ">");
                }

                m_defaultStateID = name;
                m_lastStateID = name;
                state.Play();
            }


            state.gameObject.SetActive(defaultState);
            m_currentState = state;
            m_currentStateID = name;
            m_states.Add(name, state);

#if UNITY_EDITOR
            SetNameOfCurrentState();
#endif //UNITY_EDITOR

            //Setting StatesController to owner this object
            state.gameObject.transform.parent = this.gameObject.transform;
        }

//This is for help with inpector in Unity3d Editor only
#if UNITY_EDITOR
        public void SetNameOfCurrentState()
        {
            currentStateName = m_currentState.name;
//                 string L_name = m_currentState.name;
// 
//                 if (L_name.Length < )
// 
//                 L_name = L_name.Substring(34);
//                 currentStateName = L_name;
           
        }
#endif //UNITY_EDITOR

        public void AdvanceTime(double time)
        {
            if (!m_update || !this.gameObject.activeSelf) return;

            if( m_nextStateID != 0 )
            {
                m_currentState.Stop();
                m_currentState.gameObject.SetActive(false);

                m_lastStateID = m_currentStateID;
                m_currentStateID = m_nextStateID;
                m_currentState = m_states[m_nextStateID];
                m_currentState.Play();
                m_currentState.gameObject.SetActive(true);
#if UNITY_EDITOR
                SetNameOfCurrentState();
#endif //UNITY_EDITOR

                m_nextStateID = 0;
            }
            
            m_currentState.AFUpdate(time);
        }

        public override void AFUpdate(double time)
        {
            AdvanceTime(time);
        }

        public void Remove( string name )
        {
            Remove(name.GetHashCode());
        }

        public void Remove(int stateID)
        {
            if (m_states.ContainsKey(stateID))
            {
                m_states.Remove(stateID);

                if (stateID == m_defaultStateID)
                {
                    m_defaultStateID = m_states.Keys.First() != null ? m_states.Keys.First() : 0;
                    UnityEngine.Debug.LogWarning("You are removing a default state. The default state was changed for the first state in the list. ID: " + m_defaultStateID);
                }

                if (stateID == m_currentStateID)
                {
                    GoToDefault();
                }
            }
        }

        public void Play()
        {
            m_update = true;
            m_currentState.Play();
        }

        public void Pause()
        {
            m_update = false;
        }

        public void Stop()
        {
            m_update = false;
            m_currentState.Stop();
        }

        public void GoTo( string name )
        {
            ChangeStateTo(name.GetHashCode());
        }

        public void GoTo(int hashName)
        {
            ChangeStateTo(hashName);
        }


        public void GoToDefault()
        {
            ChangeStateTo(m_defaultStateID);
        }

        private void ChangeStateTo( int stateID )
        {
            if (stateID == m_currentStateID) return;
            if ( !m_states.ContainsKey( stateID ) ) throw new Exception("State ID not valid");

            m_nextStateID = stateID;
        }

        public int GetDefaultStateID()
        {
            return m_defaultStateID;
        }

        public AMovieClip GetDefaultState()
        {
            return m_states[m_defaultStateID];
        }

        public AMovieClip GetLastState()
        {
            return m_states[m_lastStateID];
        }

        public AMovieClip GetCurrentState()
        {
            if(m_states.ContainsKey(m_currentStateID))
                return m_states[m_currentStateID];
            
            AFDebug.LogError("O ID: " + m_currentStateID + " Nao foi encontrado");
            return m_states[m_defaultStateID];
        }
        public int GetCurrentStateID()
        {
            return m_currentStateID;
        }

        public int GetLastStateID()
        {
            return m_lastStateID;
        }

        public void TakeContollOfAllAnimationChildren()
        {
             for (int i = 0; i < this.gameObject.transform.childCount; ++i)
             {
                 this.transform.GetChild(i).gameObject.SetActive(true);
             }

            AMovieClip[] L_list = this.gameObject.GetComponentsInChildren<AMovieClip>();
            AMovieClip movieclip;
            bool isDefault = false;
            for( int i = 0; i < L_list.Length; ++ i )
            {
                movieclip = L_list[i];
                if (i == 0)
                    isDefault = true;
                else
                    isDefault = false;

                Add(movieclip.gameObject.name, movieclip, isDefault);
            }

        }

    }
}
