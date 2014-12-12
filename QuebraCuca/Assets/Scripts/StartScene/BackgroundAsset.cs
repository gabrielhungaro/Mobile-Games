using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using AquelaFrameWork.Core.Asset;

public class BackgroundAsset : MonoBehaviour 
{
#if UNITY_EDITOR



#endif //UNITY_EDITOR

    // Use this for initialization
	void Start () 
    {

#if UNITY_EDITOR
        AFAssetManager.SimulatedDPI = AFAssetManager.DPI_IPAD_3;
        AFAssetManager.SimulatePlatform = AFAssetManager.EPlataform.IOS;
        AFDebug.Log(AFAssetManager.GetPathTargetPlatformWithResolution());
#endif //UNITY_EDITOR
        
        Sprite sp = AFAssetManager.Instance.Load<Sprite>(AFAssetManager.GetPathTargetPlatformWithResolution() + "/" + "Scenes/Login/Background");
        Image L_img =  gameObject.GetComponent<Image>();
        L_img.sprite = sp;
    }
	
}
