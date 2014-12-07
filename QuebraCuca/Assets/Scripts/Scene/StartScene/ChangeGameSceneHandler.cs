using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeGameSceneHandler : MonoBehaviour 
{

    public static readonly string GAME_QUEBRA_CUCA = "QuebraCuca";
    public static readonly string GAME_CATA_BALOES = "CataBaloes";
    public static readonly string GAME_BATE_RETATE = "BateRebate";

    [SerializeField]
    private Button m_bateRebateButton;

    [SerializeField]
    private Button m_quebraCucaButton;

    [SerializeField]
    private Button m_cataBalaoButton;

	// Use this for initialization
	void Start () 
    {
        m_bateRebateButton = GameObject.Find("BateRebateButton").GetComponent<Button>();
        m_quebraCucaButton = GameObject.Find("QuebraCucaButton").GetComponent<Button>();
        m_cataBalaoButton = GameObject.Find("CataBalaoButton").GetComponent<Button>();

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
}
