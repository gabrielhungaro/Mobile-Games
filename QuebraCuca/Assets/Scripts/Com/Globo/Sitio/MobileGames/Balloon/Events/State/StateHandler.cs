// // Created by Kay
// // Copyright 2013 by SCIO System-Consulting GmbH & Co. KG. All rights reserved.
//
using UnityEngine;
using System.Collections.Generic;

namespace AnimatorAccess
{
	/// <summary>
	/// Animator state handlers are called to check if an event related to a state change should be raised. Implementing 
	/// classes should check for changes in LayerStatus[] and raise an event if their specific condition is met. Note 
	/// that Perform () is called on every FixedUpdate or Update and thus should be carefully implemented regarding 
	/// peformance.  
	/// State handlers are stored in a dictionary with GetKeyString () as key to optimise reusing. Implementors not 
	/// deriving from AbstractStateHandler have to provide their own GetHashCode () method accordingly.
	/// </summary>
	public interface StateHandler
	{
		/// <summary>
		/// Checks for changes and if so raises an event.
		/// </summary>
		/// <param name="statuses">Layer statuses to check.</param>
		/// <param name="stateInfos">State infos to look up for callbacks.</param>
		void Perform (LayerStatus [] statuses, Dictionary<int, StateInfo> stateInfos);
		/// <summary>
		/// Key string to build hash code so that every handler can be identified within the dictionary. 
		/// Naming convention is:
		/// ClassName[:LayerIndex[:StateHashId]]
		/// </summary>
		/// <returns>Key string as ClassName[:LayerIndex[:StateHashId]].</returns>
		string GetKeyString ();
	}

	/// <summary>
	/// Implements some basic functionality of StateHandler expecially GetHashCode ().
	/// </summary>
	public abstract class AbstractStateHandler : StateHandler
	{
		public abstract void Perform (LayerStatus [] statuses, Dictionary<int, StateInfo> stateInfos);

		public override int GetHashCode () {
			return GetKeyString ().GetHashCode ();
		}

		public virtual string GetKeyString () {
			return this.GetType ().Name;
		}

		protected StateInfo GetStateInfo (int stateId, Dictionary<int, StateInfo> stateInfos) {
			StateInfo info = null;
			if (stateInfos.ContainsKey (stateId)) {
				info = stateInfos [stateId];
			} else {
				Debug.LogWarning ("No state info found for state [" + stateId + "]! Seems like the AnimatorAccess component needs to be updated.");
			}
			return info;
		}
	}

	/// <summary>
	/// EXPERIMENTAL!!! Dynamic state handler meant to supply its Perform implementation as delegate.
	/// </summary>
	public class DynamicStateHandler : AnyStateHandler
	{
		System.Action<Dictionary<int, StateInfo>, LayerStatus []> performAction;

		public DynamicStateHandler (System.Action<Dictionary<int, StateInfo>, LayerStatus []> performAction) {
			this.performAction = performAction;
		}

		public override void Perform (LayerStatus [] statuses, Dictionary<int, StateInfo> stateInfos) {
			if (performAction != null) {
				performAction (stateInfos, statuses);
			} else {
				Debug.LogWarning ("No performAction supplied!");
			}
		}

		public override string GetKeyString () {
			return base.GetKeyString () + ":" + performAction.ToString ();
		}
	}
}