using UnityEngine;
using System.Collections;

public class ValidateIfPlayerIsSubscriber : MonoBehaviour 
{
	// Use this for initialization
	void Start () {
        PlayerInfo.IsSubscriber = PlayerPrefs.GetInt(PlayerInfo.PLAYERPREFS_IS_PLAYER_SUBSCRIBER) == 0 ? false : true;
	}
}
