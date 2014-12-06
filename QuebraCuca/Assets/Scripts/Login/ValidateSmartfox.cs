using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using AquelaFrameWork.Server;

public class ValidateSmartfox : MonoBehaviour {

	// Use this for initialization

    private InputField m_loginText;
    private InputField m_passText;
    private Button m_loginButton;

    public static readonly string IS_PLAYER_SUBSCRIBER = "isPlayerSubscriber";

    void Awake()
    {
        AFServer.Instance.InitSmarfoxServer();
        UnityEngine.Debug.Log(AFServer.Instance);
    }

	void Start () 
    {
        PlayerPrefs.SetInt(IS_PLAYER_SUBSCRIBER, 0);

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
        PlayerPrefs.SetInt(IS_PLAYER_SUBSCRIBER, 1);
    }

    public void OnLoginErrorHandler(Sfs2X.Core.BaseEvent ev)
    {
        PlayerPrefs.SetInt(IS_PLAYER_SUBSCRIBER, 0);
    }

	// Update is called once per frame
	void Update () {

        Debug.Log("Is Player Subscriber? => " + (PlayerPrefs.GetInt(IS_PLAYER_SUBSCRIBER) == 1 ? true : false));

	}
}
