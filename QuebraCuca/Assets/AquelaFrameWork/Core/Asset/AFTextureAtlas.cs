using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using UnityEngine;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Utils;

//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

namespace AquelaFrameWork.Core.Asset
{
    public class AFTextureAtlas : UnityEngine.Object
    {
        public enum EFileType
        {
            kTextTypes_Json = 0,
            kTextTypes_Csv,
            kTextTypes_Count
        }

        private string m_name;
        private string m_path;
        public Texture2D m_texture;
        private Dictionary<string, AFTextureInfo> m_texturesInfo;
        private String[] m_textureNames;

        public AFTextureAtlas(string name, Texture2D texture, string path, EFileType fileType)
        {
            m_name = name;
            m_texturesInfo = new Dictionary<string, AFTextureInfo>();
            m_texture = texture;

            ParseFile(path, fileType);
        }

        public AFTextureAtlas(string name, string path, EFileType fileType)
        {
            m_texture = Resources.Load<Texture2D>(path);

            if(m_texture == null)
            {
                throw new FileLoadException("Texture not found");
            }

            m_name = name;
            m_texturesInfo = new Dictionary<string, AFTextureInfo>();
            ParseFile(path, fileType);
        }

        private void ParseFile(string path, EFileType fileType)
        {
            if (fileType == EFileType.kTextTypes_Json)
            {
                ParseJsonFile(path);
            }
            else if (fileType == EFileType.kTextTypes_Csv)
            {
                ParseCVSFile(path);
            }
            else
            {
                throw new Exception("The parse was not implemented yet");
            }
        }

       

        public Sprite[] GetSprites( string prefix )
        {
            string[] names = GetNames(prefix);

            if (names.Length == 0)
            {
                throw new FileNotFoundException("The sprites with prefix name <" + prefix +"> was not found!");
            }

            Sprite[] sprites = new Sprite[names.Length];

            for (int i = 0; i < names.Length; ++i)
            {
                sprites[i] = GetSprite(names[i]);
            }

            return sprites;
        }

        public String[] GetNames( string prefix )
        {

            if( m_textureNames == null )
            {
                
                m_textureNames = new String[m_texturesInfo.Count];
                int index = 0;
                foreach( KeyValuePair<string,AFTextureInfo> name in m_texturesInfo )
                { 
                    m_textureNames[index] = name.Key;
                    ++index;
                }
                Array.Sort(m_textureNames, StringComparer.Ordinal);
            }

            List<string> names = new List<string>();
            foreach (string name in m_textureNames)
            {
                if (name.IndexOf(prefix) == 0)
                    names.Add(name);
            }

            return names.ToArray();
        }

        public Sprite GetSprite(string name)
        {
            AFTextureInfo txInfo = m_texturesInfo[name];

            if (txInfo == null)
            {
                UnityEngine.Debug.LogWarning("THE SPRITE WAS NOT FOUND: " + name);
                return null;
            }
           
            Sprite L_sprite = Sprite.Create(
                m_texture ,
                txInfo.GetFrame(),
                txInfo.GetPivot(),
                100.0f);

            return L_sprite;
        }

        private void ParseCVSFile(String path)
        {
            UnityEngine.Debug.Log("PATH: " + Application.dataPath + "/Resources/" + path);

            if ( (Path.GetExtension(Path.GetExtension(path)) == "") )
            {
                path += ".aff";
            }

            AFCsvFileReader reader = new AFCsvFileReader(Application.dataPath + "/Resources/" + path);
            AFCsvRow row = new AFCsvRow();
            AFTextureInfo info = null;

            UnityEngine.Rect rect;
            UnityEngine.Vector2 pivot = UnityEngine.Vector2.zero;

            float x = 0.0f;
            float y = 0.0f;
            float w = 0.0f;
            float h = 0.0f;
            float px = 0.0f;
            float py = 0.0f;

            string name;

            while (reader.ReadRow(row))
            {
                info = new AFTextureInfo();

                name = row[0].Replace("/", "_");

                x = float.Parse(row[1]);
                y = float.Parse(row[2]);
                w = float.Parse(row[3]);
                h = float.Parse(row[4]);
                px = float.Parse(row[5]);
                py = float.Parse(row[6]);

                rect = new UnityEngine.Rect(x, y, w, h);
                pivot = new UnityEngine.Vector2(px, py);

                //TODO: Verificar bounds no sprites
//                 UnityEngine.Debug.Log("---------------------");
//                 UnityEngine.Debug.Log("Name: " + name);
//                 UnityEngine.Debug.Log("Rect: " + rect);
//                 UnityEngine.Debug.Log("Pivot: " + pivot);
//                 UnityEngine.Debug.Log("---------------------");

                info.SetName(name);
                info.SetFrame(rect);
                info.SetPivot(pivot);

                AddTextureInfo(info);
            }
           
        }

        private void ParseJsonFile(string path)
        {
//             TextAsset json = Resources.Load<TextAsset>(path);
// 
//             if(json != null)
//             { 
//                 AFTextureInfo info;
//                 JObject obj = JObject.Parse(json.text);
//                 JArray arr = obj.GetValue("frames").ToObject<JArray>();
//                 JObject textureObject;
// 
//                 int i;
//                 int hashValue;
//                 int count = arr.Count;
// 
//                 for (i = 0; i < count; ++i)
//                 {
//                     info = new AFTextureInfo();
//                     textureObject = arr[i].ToObject<JObject>();
// 
//                     foreach (KeyValuePair<string, JToken> property in textureObject)
//                     {
//                         hashValue = Utility.Hash(property.Key);
// 
//                         if ( AFTexturePackerProperties.FILE_NAME == hashValue )
//                         {
//                             info.SetName(property.Value.ToString());
//                         }
//                         else if (AFTexturePackerProperties.FRAME == hashValue)
//                         {
//                             JObject frame = JObject.Parse(property.Value.ToString());
//                             info.SetFrame( new Rect( 
//                                 (float)frame.GetValue("x") , 
//                                 (float)frame.GetValue("y") , 
//                                 (float)frame.GetValue("w") , 
//                                 (float)frame.GetValue("h")) );
//                         }
//                         else if (AFTexturePackerProperties.ROTATED == hashValue)
//                         {
//                             info.SetRotated(property.Value.ToString() == "false" ? false : true);
//                         }
//                         else if (AFTexturePackerProperties.PIVOT == hashValue)
//                         {
//                             JObject pivot = JObject.Parse(property.Value.ToString());
//                             info.SetPivot(new Vector2(
//                                 float.Parse(pivot.GetValue("x").ToString()),
//                                 float.Parse(pivot.GetValue("y").ToString()) ) );
//                         }
// 
//                         AddTextureInfo(info);
// 
//                         //Debug.Log(property.Key);
//                         //Debug.Log(property.Value.ToString());
//                         /**
//                          * IF SOME DAY USE THE FOLLOW PROPERTIES 
//                          * 
//                         else if (AFTexturePackerProperties.SPRITE_SOURCE_SIZE == hashValue)
//                         {
// 
//                         }
//                         else if (AFTexturePackerProperties.TRIMED == hashValue)
//                         {
// 
//                         }
// 
//                         else if (AFTexturePackerProperties.SOURCE_SIZE == hashValue)
//                         {
// 
//                         }**/
//                     }
//                     /*
//                     info = new AFTextureInfo();
//                     info.SetName(consult.GetValue(AFTexturePackerProperties.FILE_NAME).ToString());
//                     info.SetRotated(consult.GetValue(AFTexturePackerProperties.ROTATED).ToString() == "false " ? false : true);
//                 
//                     consult.GetValue(AFTexturePackerProperties.FILE_NAME)
//                     info.SetFrame(new Rect( ))
//                     */
//                 }
//             }
//             else
//             {
//                 throw new FileLoadException("O arquivo nao foi encontrado!!!");
//             }
        }

        private void AddTextureInfo( AFTextureInfo info )
        {
            m_texturesInfo[info.GetName()] = info;
        }

    }
}
