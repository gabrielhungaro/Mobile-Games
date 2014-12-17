using System.Collections;

using UnityEngine;

namespace AquelaFrameWork.Core
{
    /// <summary>
    /// 
    /// 
    /// <para>This class was build to make easy to get a Singleton class in the Unity Engine System.</para>
    /// 
    /// <para>If would you want a single instance of a specific class, extends this class and get access </para>
    /// from it by the static function "Instance"
    /// 
    /// </summary>
    /// 
    public abstract class ASingleton<T> : AFObject where T : MonoBehaviour
    {
        protected static T m_instance;

        protected ASingleton()
        {
        }

        /// <summary>
        /// Method returns the single instance of the Object
        /// </summary>
        /// <return>Instance of this class</return>
        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = AFObject.Create<T>();
                    GameObject.DontDestroyOnLoad(m_instance);
                }
                return m_instance;
            }
        }

        /// <summary>
        /// Destroys the single instance of the Object in the system
        /// </summary>
        public static void DestroyInstance()
        {
            Destroy(m_instance.gameObject);
            m_instance = null;
        }
    }
}
