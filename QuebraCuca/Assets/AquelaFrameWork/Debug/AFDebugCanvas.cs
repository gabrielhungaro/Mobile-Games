using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AquelaFrameWork.Core;

using UnityEngine;
using UnityEngine.UI;

public class AFDebugCanvas : AFObject
{
    private GameObject m_textField;
    private GameObject m_debugCanvas;

    public void Awake()
    {
        this.gameObject.name = "AFDebugCanvas";
        RectTransform rectTrans = this.gameObject.AddComponent<RectTransform>();

        Canvas canvas = this.gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.pixelPerfect = true;

        this.gameObject.AddComponent<CanvasScaler>();
        this.gameObject.AddComponent<GraphicRaycaster>();

        m_textField = new GameObject("AFDebugText");
        this.m_textField.transform.parent = this.gameObject.transform;

        rectTrans = m_textField.AddComponent<RectTransform>();
        m_textField.AddComponent<CanvasRenderer>();
        m_textField.AddComponent<Text>().font = Resources.Load<Font>("Fonts/ANDALEMO");
        
        rectTrans.anchorMax = new Vector2(0, 1);
        rectTrans.anchorMin = new Vector2(0, 1);
        rectTrans.sizeDelta = new Vector2(800, 300);
        rectTrans.position = new Vector3(408, (Screen.height - (rectTrans.rect.size.y / 2) ), 1);
    }
}

