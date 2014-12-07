using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using AquelaFrameWork.Server;

public class ValidateSmartfox : MonoBehaviour {

	// Use this for initialization

    private InputField m_loginText;
    private InputField m_passText;
    private Button m_loginButton;

    void Awake()
    {
        AFServer.Instance.InitSmarfoxServer();
        UnityEngine.Debug.Log(AFServer.Instance);
    }

	void Start () 
    {
        //if( PlayerPrefs.GetInt(IS_PLAYER_SUBSCRIBER) != null && )
        //PlayerPrefs.SetInt(IS_PLAYER_SUBSCRIBER, 0);

        m_loginText = GameObject.Find("login_txt").GetComponent<InputField>();
        m_passText = GameObject.Find("password_txt").GetComponent<InputField>();
        m_loginButton = GameObject.Find("login_button").GetComponent<Button>();
        m_loginButton.onClick.AddListener(() => { OnLoginButtonClickHandler(); });


        AFServer.Instance.onLogin += OnLoginHandler;
        AFServer.Instance.onLoginError += OnLoginErrorHandler;
        AFServer.Instance.Connect();
	}
	
    public void OnLoginButtonClickHandler()
    {
        AFServer.Instance.Login(m_loginText.text, m_passText.text);
    }

    public void OnLoginHandler( Sfs2X.Core.BaseEvent ev )
    {
        PlayerPrefs.SetInt(PlayerInfo.PLAYERPREFS_IS_PLAYER_SUBSCRIBER, 1);
        PlayerInfo.IsSubscriber = true;
    }

    public void OnLoginErrorHandler(Sfs2X.Core.BaseEvent ev)
    {
        PlayerPrefs.SetInt(PlayerInfo.PLAYERPREFS_IS_PLAYER_SUBSCRIBER, 0);
        PlayerInfo.IsSubscriber = false;
    }
}
