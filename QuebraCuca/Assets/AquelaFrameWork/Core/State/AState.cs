using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using AquelaFrameWork.Sound;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core;
using AquelaFrameWork.Input;

using Signals;

namespace AquelaFrameWork.Core.State
{
    public abstract class AState : AFObject , IState
    {        
        readonly public static uint STATE_UPDATE = 0x1;
        readonly public static uint STATE_NOT_DESTROYABLE = 0x2;
        readonly public static uint STATE_EVENTS_SUPORT = 0x4;

        readonly public static uint STATE_EVERYTHING = STATE_EVENTS_SUPORT | STATE_UPDATE;

        public enum EGameState
        {
            NULL = 0,
            MENU,
            INTRO,
            TUTORIAL,
            RANKING,
            GAME,
            STATE_COUNT
        }

        [SerializeField]
        protected EGameState m_stateID = EGameState.NULL;

        protected AStateManager m_stateManger;
        protected AFSoundManager m_soundManager;
        protected AFAssetManager m_assetManager;
        protected AFInput m_input;
        protected AFEngine m_engine;

        [SerializeField]
        protected List<AFObject> m_objects;

        public Signal<empty> OnStart = new Signal<empty>();
        public Signal<empty> OnDestroy = new Signal<empty>();
        public Signal<empty> OnInitialized = new Signal<empty>();

        public Signal<bool> OnPause = new Signal<bool>();
        public Signal<AFObject> OnObjectAdded = new Signal<AFObject>();
        public Signal<AFObject> OnObjectRemoved = new Signal<AFObject>();

        [SerializeField]
        protected bool m_destroyable = true;
        [SerializeField]
        protected bool m_update = true;
        [SerializeField]
        protected bool m_hasEvents = true;
        [SerializeField]
        protected bool m_initialized = false;

        virtual protected void Awake()
        {
        }

        virtual public void Setup(uint flags)
        {
            if ((flags & STATE_UPDATE) == STATE_UPDATE)
            {
                m_update = true;
            }
            else
            {
                m_update = false;
            }

            if ((flags & STATE_EVENTS_SUPORT) == STATE_EVENTS_SUPORT)
            {
                m_hasEvents = true;
            }
            else
            {
                m_hasEvents = false;
            }

            if ((flags & STATE_NOT_DESTROYABLE) == STATE_NOT_DESTROYABLE)
            {
                m_destroyable = false;
            }
            else
            {
                m_destroyable = true;
            }
        }


        virtual public void Initialize()
        {
            if (m_hasEvents)
                OnStart.Dispatch();

            if (!m_initialized)
            { 
                m_engine = AFEngine.Instance;
                m_stateManger = AFEngine.Instance.GetStateManger();
                m_soundManager = AFSoundManager.Instance;
                m_assetManager = AFAssetManager.Instance;
                m_input = AFInput.Instance;
                m_objects = new List<AFObject>();

                if (m_hasEvents)
                    OnInitialized.Dispatch();
                BuildState();
                m_initialized = true;
            }
            else
            {
                Resume();
            }
        }

        virtual public void BuildState()
        {
 
        }

        public bool IsDestroyable()
        {
            return m_destroyable;
        }

        public void SetDestroyable( bool value )
        {
            m_destroyable = value;
        }

        public override void AFUpdate(double deltaTime)
        {
            if (m_objects.Count > 0)
                for (int i = 0; i < m_objects.Count; ++i)
                    m_objects[i].AFUpdate(deltaTime);

            base.AFUpdate(deltaTime);
        }

        virtual public string GetName()
        {
            return gameObject.name;
        }


        public EGameState GetStateID()
        {
            return m_stateID;
        }

        public void SetStateID(EGameState value)
        {
            m_stateID = value;
        }

        public int GetID()
        {
            return GetInstanceID();
        }

        virtual public void Pause()
        {
            m_engine.Pause();
            if (m_hasEvents)
                OnPause.Dispatch(true);
        }

        virtual public void Resume()
        {
            m_engine.UnPause();
            if (m_hasEvents)
                OnPause.Dispatch(false);
        }

        virtual public void Destroy()
        {
            if (m_destroyable)
            {
                m_stateManger = null;
                m_soundManager = null;
                m_input = null;
                m_engine = null;
                m_assetManager = null;

                for (int i = 0; i < m_objects.Count; ++i)
                    Destroy(m_objects[i].gameObject);

                m_objects.Clear();
                m_objects = null;
            }
            else
            {
                Debug.LogWarning("State could not be destroyed, set the destroyable value to true for complete the action");
            }
            
            if (m_hasEvents)
                OnDestroy.Dispatch();
        }

        virtual public AFObject Add(AFObject obj)
        {
            AFObject L_object = GetObjectByName( obj.name );

            if( L_object == null)
            {
                L_object = obj;
                m_objects.Add(L_object);
                L_object.gameObject.transform.parent = this.gameObject.transform;
            }
            else
            {
                UnityEngine.Debug.LogWarning("The object already in the State: " + L_object.name);
            }

            if (m_hasEvents)
                OnObjectAdded.Dispatch(L_object);

            return L_object;
        }

        virtual public void Add(GameObject go)
        {
            AFObject[] L_objList = go.GetComponents<AFObject>();

            if( L_objList != null )
            {
                for( int i = 0; i < L_objList.Length; ++i )
                {
                    Add(L_objList[i]);
                }
            }
        }

        virtual public Entity AddEntity(Entity entity, object view = null)
        {
            UnityEngine.Debug.LogError("FUNCTION NOT IMPLEMENTED");

            return null;
        }

        virtual public void Remove(AFObject obj)
        {
            if (m_objects.Contains(obj))
                m_objects.Remove(obj);

            if (m_hasEvents)
                OnObjectRemoved.Dispatch(obj);
        }

        virtual public AFObject GetObjectByName(string name)
        {
            foreach( AFObject L_object in m_objects )
            {
                if (L_object.name.Equals(name))
                    return L_object;
            }

            return null;
        }

        virtual public T GetFirstObjectByType<T>() where T : AFObject
        {
            Type L_type = typeof(T);

            for (int i = 0; i < m_objects.Count ; ++i )
            {
                if(L_type.Equals( m_objects[i].GetType() ) )
                    return m_objects[i] as T;
            }

            return null;
        }

        virtual public List<T> GetObjectsByType<T>(Type type) where T : AFObject
        {
            Type L_type = typeof(T);
            List<T> L_list = new List<T>();

            for (int i = 0; i < m_objects.Count; ++i)
            {
                if (L_type.Equals(m_objects[i].GetType()))
                   L_list.Add(m_objects[i] as T);
            }

            return L_list;
        }

        override public void AFDestroy()
        {

            m_stateManger = null;
            m_soundManager = null;
            m_assetManager = null;
            m_input = null;
            m_engine = null;

            for (int i = 0; i < m_objects.Count; ++i)
                m_objects[i].AFDestroy();

            m_objects.Clear();
            m_objects = null;

            OnStart.RemoveAll();
            OnStart = null;

            OnDestroy.RemoveAll();
            OnDestroy = null;
            
            OnInitialized.RemoveAll();
            OnInitialized = null;

            OnPause.RemoveAll();
            OnPause = null;

            OnObjectAdded.RemoveAll();
            OnObjectAdded = null;
            
            OnObjectRemoved.RemoveAll();
            OnObjectRemoved = null;
            
            base.AFDestroy();
        }

    }
}
