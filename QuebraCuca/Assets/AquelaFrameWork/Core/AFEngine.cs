/****************************************************************************|
/****************************************************************************|
		        *                                                           *
		        *  .=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-.       *
		        *   |                     ______                     |      *
		        *   |                  .-"      "-.                  |      *
		        *   |                 /            \                 |      *
		        *   |     _          |              |          _     |      *
		        *   |    ( \         |,  .-.  .-.  ,|         / )    |      *
		        *   |     > "=._     | )(__/  \__)( |     _.=" <     |      *
		        *   |    (_/"=._"=._ |/     /\     \| _.="_.="\_)    |      *
		        *   |           "=._"(_     ^^     _)"_.="           |      *
		        *   |               "=\__|IIIIII|__/="               |      *
		        *   |              _.="| \IIIIII/ |"=._              |      *
		        *   |    _     _.="_.="\          /"=._"=._     _    |      *
		        *   |   ( \_.="_.="     `--------`     "=._"=._/ )   |      *
		        *   |    > _.="                            "=._ <    |      *
		        *   |   (_/                                    \_)   |      *
		        *   |                                                |      *
		        *   '-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-='      *
		        *                      I warning you:                 *
		        *    Do not touch! Unless you know what you're doing        *
		        *************************************************************/

using System;
using System.Collections.Generic;

using Signals;

using AquelaFrameWork.Input;
using AquelaFrameWork.Sound;
using UnityEngine;

using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core.State;

namespace AquelaFrameWork.Core
{
    /// <summary>
    /// 
    /// The Main Class of the Aquela Framework. Here is start point! That class controls
    /// all the modules, input, sound manager, asset manager, application state and more. To get acess to it, 
    /// you should Extend the AFEngine to begin you project. All things should start from Initialize() method.
    /// <para>
    /// To the AFEngine works properly you should build some attributes like a: 
    /// <list type="bullet">
    ///     <item> 
    ///         <description><para><c>AFStateManager:</c> You should instanciate AFStateManager or build your own state manager extending the AStatemanager</para></description> 
    ///     </item> 
    /// 
    ///     <item> 
    ///         <description><para><c>IStateFactory:</c> You should build a state factory where all your states will be declared, and do not forget of implements IStateFacotry</para></description> 
    ///     </item> 
    /// 
    /// </list> 
    /// </para>
    /// 
    /// </summary>
    
    public class AFEngine : ASingleton<AFEngine> 
    {
        /// <summary>
        /// <para> 
        ///    The framework version
        /// </para>
        /// </summary>
        [SerializeField]
        public static readonly string VERSION = "0.0.10";

        /// <summary>
        /// <para> 
        ///     Aquela Framework frame rate
        /// </para>
        /// </summary>
        [SerializeField]
        public static readonly string FRAME_RATE = "60";


        /// <summary>
        /// <para> 
        ///     Show if the application is running or not
        /// </para>
        /// </summary>
        [SerializeField]
        private bool m_running = true;
        
        protected double m_startTime = 0;
        protected double m_time = 0;
        protected double m_deltaTime = 0;

        /// <summary>
        /// <para> 
        ///     This signal is dispatched when the Unity3D pause the system. Normally when the application is minimized by OS
        /// </para>
        /// </summary>
        public Signal<bool> OnPause = new Signal<bool>();


        /// <summary>
        /// <para> 
        ///     This signal is dispatched when the Unity3D pause the system. Normally when the application is minimized by OS
        /// </para>
        /// </summary>
        public Signal<bool> OnEngineReady = new Signal<bool>();

        /// <summary>
        /// <para> 
        ///     Occurs when the player gets or loses focus.
        /// </para>
        /// </summary>
        public Signal<bool> OnApplicationFocusChange = new Signal<bool>();

        /// <summary>
        /// <para> 
        ///     Signal sent before the application is quit
        /// </para>
        /// </summary>
        public Signal<bool> OnApplicationExit = new Signal<bool>();

        /// <summary>
        /// <para> 
        ///     This signal is dispatched before the AFEngine call destroy
        /// </para>
        /// </summary>
        public Signal<empty> OnApplicationDestroy = new Signal<empty>();

        protected AFInput m_input;

        protected AFSoundManager m_soundManager;

        protected AStateManager m_stateManager;


        /// <summary>
        /// <para> 
        ///     Return the current state manager
        /// </para>
        /// </summary>
        public AStateManager GetStateManger()
        {
            return m_stateManager;
        }


        protected void Awake()
        {
            SetRunning(false);
            m_instance = this;
            Initialize();
        }

        virtual public void ConsoleGetCommand( String name, String paramName )
        {
            //TODO: I have a dream.... Console command working for little test on the AF.
        }

        virtual public void ConsoleSetCommand( String name, String paramName, String value)
        {
            //TODO: I have a dream.... Console command working for little test on the AF.
        }


        /// <summary>
        /// <para> 
        ///     This method destroys all states and yours objects
        /// </para>
        /// </summary>
        virtual public void Destroy()
        {
            m_startTime = 0;
            m_time = 0;
            m_deltaTime = 0;

            OnPause.RemoveAll();
            OnPause = null;

            OnEngineReady.RemoveAll();
            OnEngineReady = null;

            OnApplicationFocusChange.RemoveAll();
            OnApplicationFocusChange = null;

            OnApplicationExit.RemoveAll();
            OnApplicationExit = null;

            OnApplicationDestroy.RemoveAll();
            OnApplicationDestroy = null;

            m_input.AFDestroy();
            m_input = null;

            m_soundManager.AFDestroy();
            m_soundManager = null;

            m_stateManager.AFDestroy();
            m_stateManager = null;

        }

        /// <summary>
        /// <para> 
        ///     Initialize the AFEngine
        /// </para>
        /// </summary>
        virtual public void Initialize()
        {
            AFAssetManager.Instance.gameObject.transform.parent = gameObject.transform;
            m_stateManager.gameObject.transform.parent = gameObject.transform;

            SetRunning( true );
        }

        /// <summary>
        /// <para> 
        ///     This method Pause the entire application
        /// </para>
        /// </summary>
        virtual public void Pause()
        {
            if(m_running)
            {
                SetRunning( false );
                m_stateManager.Pause();
                OnPause.Dispatch(m_running);
            }
        }

        /// <summary>
        /// <para> 
        ///     This method UnPause the entire application
        /// </para>
        /// </summary>
        virtual public void UnPause()
        {
            if (!m_running)
            {
                SetRunning( true );
                m_stateManager.Resume();
                OnPause.Dispatch(m_running);
            }
        }

        /// <summary>
        /// <para> 
        ///     Return if the application is running
        /// </para>
        /// </summary>
        public bool GetRunning()
        {
            return m_running;
        }

        private void SetRunning( bool value )
        {
            m_running = value;
        }

        public double GetDeltaTime()
        {
            return m_deltaTime;
        }

        public AFSoundManager GetSoundManager()
        {
            return m_soundManager;
        }

        public AFInput GetInput()
        {
            return m_input;
        }

        virtual protected void Update()
        {
            if( m_running )
            {
                double deltaTime = UnityEngine.Time.smoothDeltaTime;
                //m_input.Update(deltaTime);
                m_stateManager.AFUpdate(deltaTime);
            }
            //Debug.Log("New Resolution: " + AFAssetManager.GetPathTargetPlatformWithResolution());
        }


        // This function is called every fixed frame rate frame, if the MonoBehaviour is enabled (Since v1.0)
        void FixedUpdate()
        {

        }


        // LateUpdate is called every frame, if the Behaviour is enabled (Since v1.0)
        void LateUpdate()
        {

        }

        public void ShowConsole()
        {

        }

        public void HideConsole()
        {

        }

        void OnApplicationFocus(bool focus)
        {
            OnApplicationFocusChange.Dispatch(focus);
        }

        void OnApplicationQuit()
        {
            OnApplicationExit.Dispatch(true);
        }

         void OnApplicationPause(bool pauseStatus)
         {
             //SetRunning(pauseStatus);
            //OnPause.Dispatch(pauseStatus);
         }



        void OnDestroy()
        {
            OnApplicationDestroy.Dispatch();
        }
    }
}
