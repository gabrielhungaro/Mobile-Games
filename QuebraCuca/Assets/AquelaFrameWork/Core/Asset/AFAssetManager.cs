
using System;

using System.Collections.Generic;
using System.Collections;
using UnityEngine;

using AquelaFrameWork.Sound;

namespace AquelaFrameWork.Core.Asset
{

    public class AFAssetManager : ASingleton<AFAssetManager>
    {
        public static readonly int DEFAULT_SCREEN_DPI = 160;

#if UNITY_EDITOR
        public enum EPlataform
        {
            IOS = 0,
            ANDROID,
            WINDOWSP8,
            EDITOR
        }

        public static readonly int DPI_IPHONE_3 = 163;
        public static readonly int DPI_IPHONE_4_5 = 326;
        public static readonly int DPI_IPHONE_6 = 401;
        public static readonly int DPI_IPAD_3 = 264;
        public static readonly int DPI_IPAD_1_2 = 132;
        public static readonly int DPI_IPAD_RETINA = 324;
        public static readonly int DPI_GALAXY_S5 = 432;
        public static readonly int DPI_GALAXY_S4 = 441;
        public static readonly int DPI_LUMIA_720 = 217;
        public static readonly int DPI_LUMIA_520 = 233;
        
        public static int SimulatedDPI { get; set; }
        public static EPlataform SimulatePlatform { get; set; }
#endif

        public static string iphonePath = "IOS/";
        public static string androidPath = "Android/";
        public static string windowsPhone8Path = "WP8/";
        public static string commumPath = "Common/";
        public static string package = "com.globo.sitio.games";

        private static string DIRECTORY_OWNER = "QuebraCuca";

        public static readonly string DIRECTORY_NAME_ASSETS = "Assets";
        public static readonly string DIRECTORY_NAME_HIGH = "High/";
        public static readonly string DIRECTORY_NAME_XHIGH = "ExtraHigh/";
        public static readonly string DIRECTORY_NAME_LOW = "Low/";
        public static readonly string DIRECTORY_NAME_MEDIUM = "Medium/";
        public static readonly string DIRECTORY_NAME_SOUND = "Sounds/";
        public static readonly string DIRECTORY_NAME_DATA = "Data/";
        public static readonly string DIRECTORY_NAME_SCRIPTS = "Scripts";
        public static readonly string DIRECTORY_NAME_RESOURCES= "Resources";

        protected Dictionary<string, Texture> m_textures = new Dictionary<string,Texture>();
        protected Dictionary<string, AFTextureAtlas> m_texturesAtlas = new Dictionary<string, AFTextureAtlas>();
        protected Dictionary<string, AudioClip> m_sounds = new Dictionary<string, AudioClip>();
        protected Dictionary<string , GameObject> m_prefabs = new Dictionary<string,GameObject>();
        protected Dictionary<string , object> m_custom = new Dictionary<string,object>();

        protected Dictionary<string, AFPool> m_pool = new Dictionary<string, AFPool>();

//         public void Awake()
//         {
//             gameObject.transform.parent = AFEngine.Instance.gameObject.transform;
//         }

        public T Load<T>(string path) where T : UnityEngine.Object
        {
            return Load<T>(path, path);
        }

        public T Load<T>(string name, string path) where T : UnityEngine.Object
        {
            T res = null;

            try
            {
                res = GetAsset<T>(name);

                if ( AFObject.IsNull(res) )
                {

                    if ( typeof(T) == typeof(AFTextureAtlas) )
                    {
                        res = Add(name, new AFTextureAtlas(name, path, AFTextureAtlas.EFileType.kTextTypes_Csv)) as T;
                    }
                    else
                    {
                        res = Resources.Load<T>(path);

                        if (typeof(T) == typeof(Texture))
                            Add(name, res as Texture);
                        else if (typeof(T) == typeof(GameObject))
                            Add(name, res as GameObject);
                        else if (typeof(T) == typeof(Texture))
                            Add(name, res as Texture);
                        else if (typeof(T) == typeof(AudioClip))
                            Add(name, res as AudioClip);
                        else
                            Add(name, res);

                        Resources.UnloadUnusedAssets();
                    }

                    UnityEngine.Debug.Log("I'll store an object of: " + typeof(T).ToString());
                }
            }
            catch( NullReferenceException nullEx )
            {
                UnityEngine.Debug.LogError("The asset was not found: " + nullEx.Message);
            }
            catch( Exception ex )
            {
                //TODO: Discover what happens when unity throw this error
                UnityEngine.Debug.LogError("The asset was not found: " + ex.Message);
            }

            return res;
        }


        public AFPool CreatePool<T>( string name , string assetName , uint qtd , uint maxPoolObjects = 0) where T : UnityEngine.Object
        {
            AFPool pool = null;

            if( qtd > 1 )
            {

                T obj = GetAsset<T>(assetName);

                if ( obj )
                {

                    pool = AFObject.Create<AFPool>(name);
                    pool.Init(name, maxPoolObjects );

                    pool.transform.parent = this.gameObject.transform;

                    if( obj.GetType().IsSubclassOf( typeof(UnityEngine.Object) ) )
                    {
                        UnityEngine.Debug.Log("Creating a object from a instance");
                        pool.Create((UnityEngine.Object)obj, qtd);
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Creating a object from a Type");
                        pool.Create(obj.GetType(), (int)qtd );
                    }

                    m_pool.Add(name, pool);
                }
                else
                {
                    UnityEngine.Debug.LogError(" The asset not exists " + assetName);
                }
            }
            else
            {
                UnityEngine.Debug.LogError(" The pool should be more than one");
            }

            return pool;
        }

        public AFTextureAtlas Add(string name, AFTextureAtlas obj)
        {
            if (!AFObject.IsNull(obj))
             {
                m_texturesAtlas.Add(name, obj);
             }
             else
             {
                 UnityEngine.Debug.LogWarning("TextureAtlas could not be null");
             }

            return obj;
        }


        public object Add(string name, object obj)
        {
            if (!AFObject.IsNull(obj))
            {
                m_custom.Add(name , obj);
            }
            else
            {
                UnityEngine.Debug.LogWarning("Object could not be null");
            }

            return obj;
        }

        public AudioClip Add(string name, AudioClip sound)
        {
            if (!AFObject.IsNull(sound))
            {
                m_sounds.Add(name, sound);
            }
            else
            {
                UnityEngine.Debug.LogWarning("AFSound could not be null");
            }

            return sound;
        }

        public GameObject Add(string name, GameObject gameObject)
        {
            if (!AFObject.IsNull(gameObject))
            {
                m_prefabs.Add(name, gameObject);
            }
            else
            {
                UnityEngine.Debug.LogWarning("GameObject could not be null");
            }

            return gameObject;
        }

        public Texture Add(string name, Texture texture)
        {
            if (!AFObject.IsNull(texture))
            {
                m_textures.Add(name, texture);
            }
            else
            {
                UnityEngine.Debug.LogWarning("Texture could not be null");
            }

            return texture;
        }

        public Texture GetTexture( string name )
        {
            if( m_textures.ContainsKey( name ) )
            {
                return m_textures[name];
            }
            
            UnityEngine.Debug.LogWarning("Texture " + name + " was not found!");
            return null;
        }

        public GameObject GetPrefabs( string name )
        {
            if( m_prefabs.ContainsKey( name ) )
            {
                return m_prefabs[name];
            }
            
            UnityEngine.Debug.LogWarning( "Prefeb: " + name + " was not found!" );


            return null;
        }

        public AudioClip GetAudioClip(string name)
        {
            if( m_sounds.ContainsKey( name ) )
            {
                return m_sounds[name];
            }
            
            UnityEngine.Debug.LogWarning(" AFSound: " + name + " was not found!");

            return null;
        }

        public void Delete( string name )
        {
            if( m_textures.ContainsKey(name ) )
            {
                m_textures.Remove(name);
            }
            else if ( m_sounds.ContainsKey(name))
            {
                //m_sounds[name].Destroy();
                m_sounds.Remove(name);
            }
            else if( m_prefabs.ContainsKey(name ) )
            {
                GameObject.Destroy(m_prefabs[name]);
                m_prefabs.Remove(name);
            }
            else if( m_custom.ContainsKey(name ) )
            {
                m_custom.Remove(name);
            }
            else if (m_texturesAtlas.ContainsKey(name))
            {
                m_texturesAtlas.Remove(name);
            }
            else
            {
                UnityEngine.Debug.LogWarning("The asset: " + name + " was not found");
            }
        }

        public bool HasAsset( string name )
        {
            if ( m_textures.ContainsKey(name) || 
                m_sounds.ContainsKey(name) ||
                m_prefabs.ContainsKey(name) ||
                m_custom.ContainsKey(name))
            {
                return true;
            }
            
            return false;
        }

        private T GetAsset<T>(string name) where T : UnityEngine.Object
        {
            T obj = null;

            if (m_textures.ContainsKey(name))
            {
                obj = m_textures[name] as T;
            }
            else if(m_sounds.ContainsKey(name) )
            {
                obj = m_sounds[name] as T;
            }
            else if(m_prefabs.ContainsKey(name))
            {
                obj = m_prefabs[name] as T;
            }
            else if (m_texturesAtlas.ContainsKey(name))
            {
                obj = m_texturesAtlas[name] as T;
            }
            else if (m_custom.ContainsKey(name))
            {
                obj = m_custom[name] as T;
            }

            return obj;
        }

        public Sprite CreateSpriteFromTexture( string path )
        {
            Texture2D L_texture = Load<Texture2D>(path);

            if (L_texture != null)
            {
                return Sprite.Create(
                        L_texture,
                        new Rect(0, 0, L_texture.width, L_texture.height),
                        new Vector2(0.5f,0.5f) );
            }


            return null;
        }

        public void DisposeAll()
        {
            m_textures = new Dictionary<string,Texture>();
            m_sounds = new Dictionary<string, AudioClip>();
            m_prefabs = new Dictionary<string,GameObject>();
            m_custom = new Dictionary<string,object>();
        }

        public static string GetPathTargetPlatform()
        {
            #if UNITY_IPHONE
                return iphonePath;
            #elif UNITY_ANDROID
                return androidPath;
            #elif UNITY_WP8
                return windowsPhone8Path;
            #elif UNITY_EDITOR
                return GetEditorPath();
            #else
                return GetCommumPath();
            #endif  
        }

#if UNITY_EDITOR

        public static string GetEditorPath()
        {
            if( SimulatePlatform != null )
            {
                switch(SimulatePlatform)
                {
                    case EPlataform.IOS:
                        return iphonePath;
                    case EPlataform.ANDROID:
                        return androidPath;
                    case EPlataform.WINDOWSP8:
                        return windowsPhone8Path;
                }

            }
            return commumPath;
        }

#endif
        public static string GetPathTargetPlatformWithResolution(string file = "")
        {
            if (DIRECTORY_OWNER.Equals(""))
                return (GetPathTargetPlatform() + GetResolutionFolder() + file);
                
            return (DIRECTORY_OWNER + "/" + GetPathTargetPlatform() + GetResolutionFolder() + file);
        }


        public static string GetResolutionFolder()
        {
            UnityEngine.Debug.Log("DPI DA TELA: " + Screen.dpi);
            float DPI;

#if UNITY_EDITOR
            DPI = Screen.dpi <= 0 ? SimulatedDPI : Screen.dpi;
#else
            DPI = Screen.dpi <= 0 ? DEFAULT_SCREEN_DPI : Screen.dpi;
#endif //UNITY_EDITOR

            if (DPI > 290)
            {
                return DIRECTORY_NAME_HIGH;
            }
            else if (DPI > 200 && Screen.dpi <= 290)
            {
                return DIRECTORY_NAME_HIGH;
            }
            else if (DPI >= 150 && Screen.dpi <= 200)
            {
                return DIRECTORY_NAME_MEDIUM;
            }
            else if (DPI < 150)
            {
                return DIRECTORY_NAME_LOW;
            }

            return DIRECTORY_NAME_LOW;
        }

        public static string GetCommumPath()
        {
            return commumPath;
        }


        public T Instantiate<T>( string nameOrPath ) where T : UnityEngine.Object
        {
            T L_object = Load<T>(nameOrPath);

            if(AFObject.IsNull(L_object) )
            {
                AFDebug.LogError("Was not Possible to load or instantiate follow gameObject: " + name );
            }
            else
            {
                T L_objectInstantiated = Instantiate(L_object) as T;
                
                if (AFObject.IsNull(L_object))
                    AFDebug.LogError("Was not Possible to load or instantiate follow gameObject: " + name);

                return L_objectInstantiated;
            }

            return null;
        }

        public static string SetDirectoryOwner(string newOwner)
        {
            return (DIRECTORY_OWNER = newOwner);
        }
        public static string GetDirectoryOwner( string path )
        {
            if (DIRECTORY_OWNER.Equals(""))
                return path;

            return (DIRECTORY_OWNER + "/" + path);
        }

    }
}
