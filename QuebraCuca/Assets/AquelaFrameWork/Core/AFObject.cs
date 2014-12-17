using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace AquelaFrameWork.Core
{
    /// <summary>
    /// 
    /// All objects in the AquelaFramework are AFObject . 
    /// Is recommended you extend this class for your objects.
    /// 
    /// </summary>
    public class AFObject : MonoBehaviour
    {


        /// <summary>
        /// 
        /// Creates a AFObject by the type passed between "<>", it is the safe object creation at AquelaFrameWork. If you could, avoid "new" operator
        /// 
        /// </summary>
        public static T Create<T>( string name = "") where T : MonoBehaviour
        {
            if( name.Equals("") )
                return new GameObject(typeof(T).ToString()).AddComponent<T>();


            return new GameObject(name).AddComponent<T>();
        }


        virtual public void AFUpdate( double time )
        {

        }

        virtual public void AFDestroy()
        {
            if (this.gameObject)
                Destroy(this.gameObject);
        }

        public static bool IsNull(object obj)
        {
            return (obj == null);
        }

        public bool IsNull()
        {
            return ( (this as object) == null);
        }
    }
}
