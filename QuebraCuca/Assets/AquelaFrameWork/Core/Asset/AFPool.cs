using System;
using System.Collections.Generic;
using System.Collections;

using UnityEngine;

namespace AquelaFrameWork.Core.Asset
{
    public class AFPool : AFObject
    {
        private string m_name = "";

        private ArrayList m_pooledObjs;
        
        private uint m_qtdObjectsPooled = 0;
        private uint m_maxObjects = 0;

        public void Init( string name , uint maxObjects = 0)
        {
            m_name = name;
            m_maxObjects = maxObjects;
            m_pooledObjs = new ArrayList();
        }

        public void Create( UnityEngine.Object obj , uint qtd )
        {
            if ( m_maxObjects == 0 || m_maxObjects >= qtd )
            {
                int i = 0;

                for( i = 0; i < qtd ; ++i )
                {
                    if ( !Add(UnityEngine.Object.Instantiate(obj)) )
                    { 
                        return; 
                    }
                }
            }
            else
            {
                UnityEngine.Debug.LogError( " The quantity of objects to be created is not allowed! The max is : " + m_maxObjects );
            }
            
        }

        public void Create( Type type , int qtd)
        {
            if (m_maxObjects == 0 || m_maxObjects >= qtd)
            {
                int i = 0;

                for (i = 0; i < qtd; ++i)
                {
                    if ( !Add( Activator.CreateInstance(type) )){ return; }
                }
            }
            else
            {
                UnityEngine.Debug.LogError(" The quantity of objects to be created is not allowed!");
            }
        }


        public bool Add( object obj )
        {
            if (m_maxObjects == 0 || m_qtdObjectsPooled <= m_maxObjects)
            {
                m_pooledObjs.Add(obj);
                m_qtdObjectsPooled++;
            }
            else
            {
                UnityEngine.Debug.LogWarning("The object could not be added, because the pool reached your max limit of: " + m_maxObjects);
                return false;
            }

            return true;
        }

        public void Destroy()
        {
            m_qtdObjectsPooled = 0;
            m_pooledObjs.Clear();
            m_pooledObjs = null;
        }


    }
}
