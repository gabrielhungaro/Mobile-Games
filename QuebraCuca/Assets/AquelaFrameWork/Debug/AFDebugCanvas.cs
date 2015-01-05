using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AquelaFrameWork.Core;

using UnityEngine;
using UnityEngine.UI;

public class AFDebugCanvas : AFObject
{
    private GameObject m_textFieldGameObj;
    private GameObject m_debugCanvas;

 
    private Text m_textField;

    public void Awake()
    {
        this.gameObject.name = "AFDebugCanvas";

        RectTransform rectTrans = this.gameObject.AddComponent<RectTransform>();

        rectTrans.transform.position = new Vector3(0, 0, -10.0f) ;

        Canvas canvas = this.gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.pixelPerfect = true;

        this.gameObject.AddComponent<CanvasScaler>();
        //this.gameObject.AddComponent<GraphicRaycaster>();

//        GameObject go = GameObject.Find("Canvas");
//        this.gameObject.transform.parent = go.transform;
        
        m_textFieldGameObj = new GameObject("AFDebugText");


        this.m_textFieldGameObj.transform.parent = this.gameObject.transform;

         rectTrans = m_textFieldGameObj.AddComponent<RectTransform>();
         CanvasRenderer cvr = m_textFieldGameObj.AddComponent<CanvasRenderer>();
         //cvr.transform.position = new Vector3(0, 0, -1);
 
         m_textField = m_textFieldGameObj.AddComponent<Text>();
         m_textField.font = Resources.Load<Font>("Fonts/ANDALEMO");
         m_textField.color = Color.gray;
         
         rectTrans.anchorMax = new Vector2(0, 1);
         rectTrans.anchorMin = new Vector2(0, 1);
         rectTrans.sizeDelta = new Vector2(800, 300);
         rectTrans.position = new Vector3(408, (Screen.height - (rectTrans.rect.size.y / 2) ), -10);

         GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public Text GettextFild()
    {
        return m_textField;
    }

    public void SetTextColor( Color color )
    {
        m_textField.color = color;
    }

}

