using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

using AquelaFrameWork.Core.Asset;
//using AquelaFrameWork.Server;

public class StartState : AquelaFrameWork.Core.State.AState
{

    public static readonly string GAME_QUEBRA_CUCA = "QuebraCuca";
    public static readonly string GAME_CATA_BALOES = "CataBaloes";
    public static readonly string GAME_BATE_RETATE = "BateRebate";

    [SerializeField]
    private GameObject m_btBateRebate;
    [SerializeField]
    private GameObject m_btCataBalao;
    [SerializeField]
    private GameObject m_btQuebraCuca;

    [SerializeField]
    private GameObject m_background;

    [SerializeField]
    private UnityEngine.GameObject m_interface;

    [SerializeField]
    private Button m_bateRebateButton;

    [SerializeField]
    private Button m_quebraCucaButton;

    [SerializeField]
    private Button m_cataBalaoButton;

    public override void Initialize()
    {
       base.Initialize();
 
#if UNITY_EDITOR
        AFAssetManager.SimulatedDPI = AFAssetManager.DPI_IPAD_3;
        AFAssetManager.SimulatePlatform = AFAssetManager.EPlataform.IOS;
#endif

        m_interface = GameObject.Find("ButtonsCanvas");
        m_btBateRebate = GameObject.Find("BateRebateButton");
        m_btCataBalao = GameObject.Find("CataBalaoButton");
        m_btQuebraCuca = GameObject.Find("QuebraCucaButton");
        m_background = GameObject.Find("Background");

        m_bateRebateButton = m_btBateRebate.GetComponent<Button>();
        m_quebraCucaButton = m_btQuebraCuca.GetComponent<Button>();
        m_cataBalaoButton = m_btCataBalao.GetComponent<Button>();

        SetButtonView(m_btBateRebate.GetComponent<Image>(), "Scenes/StartScene/BateRebate" );
        SetButtonView(m_btCataBalao.GetComponent<Image>(), "Scenes/StartScene/Button_CataBalao");
        SetButtonView(m_btQuebraCuca.GetComponent<Image>(), "Scenes/StartScene/QuebraCuca");
        SetButtonView(m_background.GetComponent<Image>(), "Scenes/StartScene/Background");

        m_bateRebateButton.onClick.AddListener(() => { OnBaterebateButtonCLickHandler(); });
        m_quebraCucaButton.onClick.AddListener(() => { OnQuebraCucaButtonCLickHandler(); });
        m_cataBalaoButton.onClick.AddListener(() => { OnCataBalaoButtonCLickHandler(); });
    }

    private void OnBaterebateButtonCLickHandler()
    {
        m_bateRebateButton.onClick.RemoveAllListeners();
        Application.LoadLevel(GAME_BATE_RETATE);
    }

    private void OnQuebraCucaButtonCLickHandler()
    {
        m_quebraCucaButton.onClick.RemoveAllListeners();
        Application.LoadLevel(GAME_QUEBRA_CUCA);

    }

    private void OnCataBalaoButtonCLickHandler()
    {
        m_cataBalaoButton.onClick.RemoveAllListeners();
        Application.LoadLevel(GAME_CATA_BALOES);
    }


    public void OnLoginButtonClickHandler()
    {
        //AFServer.Instance.Login(m_btBateRebate.text, m_btCataBalao.text);
    }

//     public void OnLoginHandler(Sfs2X.Core.BaseEvent ev)
//     {
//         //PlayerPrefs.SetInt(PlayerInfo.PLAYERPREFS_IS_PLAYER_SUBSCRIBER, 1);
//         //PlayerInfo.IsSubscriber = true;
//     }
// 
//     public void OnLoginErrorHandler(Sfs2X.Core.BaseEvent ev)
//     {
//        // PlayerPrefs.SetInt(PlayerInfo.PLAYERPREFS_IS_PLAYER_SUBSCRIBER, 0);
//        // PlayerInfo.IsSubscriber = false;
//     }

    public void SetButtonView(Image image , string path)
    {
        AFDebug.Log(AFAssetManager.GetPathTargetPlatformWithResolution() + "/" + path);

        Sprite sp = AFAssetManager.Instance.Load<Sprite>(AFAssetManager.GetPathTargetPlatformWithResolution() + "/" + path);

        if (sp != null)
            image.sprite = sp;
        else
            AFDebug.LogError("Problem to load asset");
    }
    
}
