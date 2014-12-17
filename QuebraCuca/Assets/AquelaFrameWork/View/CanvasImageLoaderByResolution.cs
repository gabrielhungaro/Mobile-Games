using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Asset;

namespace AquelaFrameWork.View
{
    public class CanvasImageLoaderByResolution : AFObject
    {
        public string path = "Scenes/Login/Background";

        void Start()
        {
            Sprite sp = AFAssetManager.Instance.Load<Sprite>(AFAssetManager.GetPathTargetPlatformWithResolution() + "/" + path);
            Image L_img = gameObject.GetComponent<Image>();

            if (L_img != null)
            {
                L_img.sprite = sp;
            }
        }

    }
}
