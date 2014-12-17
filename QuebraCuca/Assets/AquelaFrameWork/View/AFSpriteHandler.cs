using System;
using System.Collections.Generic;

using UnityEngine;
using AquelaFrameWork.Core;

namespace AquelaFrameWork.View
{
    public class AFSpriteHandler : AFObject
    {
        static public Texture2D[] SpriteListFromUnityTextureAtlas(string path)
       {
           UnityEngine.Debug.Log("THE PATH IS: " + path);
           UnityEngine.Texture2D[] sprites = Resources.LoadAll<Texture2D>(path);
           UnityEngine.Debug.Log("Load: " + sprites.Length);

           for (int i = 0; i < sprites.Length; ++i)
               UnityEngine.Debug.Log("Sprite name: " + sprites[i].name);

           return sprites;
       }

    }
}
