using UnityEngine;
using System.Collections;

public class PlayerInfo 
{
    /// <summary>
    /// This Variable name is used to store information about the status of player subscription at MDS in unity3d. 
    /// To access the value of this variable you should use:
    /// 
    /// <code>
    /// //Returns 0 to false or 1 to true
    /// PlayerPrefs.GetInt(PlayerInfo.PLAYERPREFS_IS_PLAYER_SUBSCRIBER);
    /// </code>
    /// 
    /// </summary>
    public static readonly string PLAYERPREFS_IS_PLAYER_SUBSCRIBER = "isPlayerSubscriber";


    // <summary>
    /// Returns if player is or not a subscriber.
    /// </summary>
    public static bool IsSubscriber{ get; set; }

}
