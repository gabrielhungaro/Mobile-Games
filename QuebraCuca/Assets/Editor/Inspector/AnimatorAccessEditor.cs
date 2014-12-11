// The MIT License (MIT)
// 
// Copyright (c) 2014 by SCIO System-Consulting GmbH & Co. KG. All rights reserved.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using Scio.CodeGeneration;
using AnimatorAccess;

namespace Scio.AnimatorAccessGenerator 
{
	/// <summary>
	/// Custom inspector for all generated AnimatorAccess classes i.e. that are derived from BaseAnimatorAccess.
	/// </summary>
	[ExecuteInEditMode]
	[CustomEditor(typeof(AnimatorAccess.BaseAnimatorAccess), true)]
	public class AnimatorAccessEditor : Editor 
	{
		const string InspectorIconsDir = "/Editor/Inspector/Icons/";

		static bool updateCheckFoldOutState = true;

		static Texture iconRemove = null;
		static Texture iconAdd = null;
		static Texture iconObsolete = null;

		/// <summary>
		/// Class constructor is called after every AssetDatabase.Refresh as the whole assembly is reloaded.
		/// </summary>
		static AnimatorAccessEditor () {
			string dir = "Assets/" + Manager.SharedInstance.InstallDir + InspectorIconsDir;
			iconRemove = AssetDatabase.LoadAssetAtPath (dir + "icon_remove.png", typeof(Texture)) as Texture;
			iconObsolete = AssetDatabase.LoadAssetAtPath (dir + "icon_obsolete.png", typeof(Texture)) as Texture;
			iconAdd = AssetDatabase.LoadAssetAtPath (dir + "icon_add.png", typeof(Texture)) as Texture;
		}

		/// <summary>
		/// The result of the last update check i.e. a class comparison between current class version and the one that
		/// will be generated next. This will be updated every Preferences.Key.AutoRefreshInterval seconds.
		/// </summary>
		List<ClassMemberCompareElement> updateCheck = null;
		/// <summary>
		/// Set after button Update or Undo was pressed to highlight the Refresh button.
		/// </summary>
		bool dirty = false;
		/// <summary>
		/// The last check timestamp to calculate the next update time according to Preferences.Key.AutoRefreshInterval.
		/// </summary>
		static double lastCheckTimestamp = 0;

		void OnEnable () {
			CheckForUpdates (false);
		}

		public override void OnInspectorGUI()
		{
			AnimatorAccess.BaseAnimatorAccess myTarget = (AnimatorAccess.BaseAnimatorAccess)target;
			Attribute[] attrs = Attribute.GetCustomAttributes(myTarget.GetType (), typeof (GeneratedClassAttribute), false);
			string version = "Version: ";
			if (attrs.Length == 1) {
				GeneratedClassAttribute a = (GeneratedClassAttribute)attrs[0];
				version += a.CreationDate;
			} else {
				version += " not available";
			}
			GUILayout.Label (version);
			EditorGUILayout.BeginHorizontal ();
			if(GUILayout.Button("Check", EditorStyles.miniButtonLeft)) {
				CheckForUpdates (true);
			}
			if(GUILayout.Button("Update", (updateCheck != null && updateCheck.Count > 0 ? InspectorStyles.MidMiniButtonHighLighted : EditorStyles.miniButtonMid))) {
				Manager.SharedInstance.Update (Selection.activeGameObject);
				updateCheck = null;
				dirty = true;
			}
			if(GUILayout.Button("Refresh" + (dirty ? "*" : ""), (dirty ? InspectorStyles.RightMiniButtonHighLighted : EditorStyles.miniButtonRight))) {
				Manager.SharedInstance.Refresh ();
				dirty = false;
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Separator ();
			if (EditorApplication.isCompiling || EditorApplication.isUpdating) {
				EditorGUILayout.BeginVertical ();
				EditorGUILayout.LabelField ("Loading, please wait ...");
				EditorGUILayout.EndVertical ();
			} else {
				if (updateCheck != null) {
					CheckForUpdates (false);
					if (updateCheck.Count > 0) {
						EditorGUILayout.BeginVertical ();
						List<ClassMemberCompareElement> errors = updateCheck.FindAll ((element) => element.result == ClassMemberCompareElement.Result.Error);
						List<ClassMemberCompareElement> infos = updateCheck.FindAll ((element) => element.result > ClassMemberCompareElement.Result.Error);
						// if there are severe errors, show them first above the foldout GUI element
						if (errors.Count > 0) {
							string errorHintTooltip = "If one of the members is marked as obsolete, it will be removed during generation to avoid compiler errrors.\n\n" +
								"If this is not the case, you probably have used the same name for an Animator state and for a parameter too.\n\n" +
								"To use identical names for Animator states and parameters, go to settings and define prefixes for states and/or parameters.";
							EditorGUILayout.LabelField (new GUIContent (errors.Count + " Naming Conflict(s)", errorHintTooltip), InspectorStyles.LabelRed);
							foreach (ClassMemberCompareElement error in errors) {
								string errorTooltip = error.Message;
								string errorLabel = string.Format ("{0} : {1}", error.Member, errorTooltip);
								EditorGUILayout.LabelField (new GUIContent (errorLabel, errorTooltip), InspectorStyles.LabelHighLighted);
							}
							EditorGUILayout.Separator ();
						}
						updateCheckFoldOutState = EditorGUILayout.Foldout (updateCheckFoldOutState, updateCheck.Count + " class member(s) to update");
						if (updateCheckFoldOutState) {
							// compare elements are sorted already: new, obsolete, removed members
							foreach (ClassMemberCompareElement c in infos) {
								string label = string.Format ("{0}", c.Signature);
								string tooltip = "";
								switch (c.result) {
								case ClassMemberCompareElement.Result.New:
									tooltip = string.Format ("{0} {1} {2} will be added", c.memberType, c.ElementType, c.Signature);
									EditorGUILayout.LabelField (new GUIContent (label, iconAdd, tooltip));
									break;
								case ClassMemberCompareElement.Result.Obsolete:
									tooltip = string.Format ("{0} {1} {2} will be marked as obsolete", c.memberType, c.ElementType, c.Signature);
									EditorGUILayout.LabelField (new GUIContent (label, iconObsolete, tooltip));
									break;
								case ClassMemberCompareElement.Result.Remove:
									tooltip = string.Format ("{0} {1} {2} will be removed", c.memberType, c.ElementType, c.Signature);
									EditorGUILayout.LabelField (new GUIContent (label, iconRemove, tooltip));
									break;
								default:
									break;
								}
							}
						}
						EditorGUILayout.EndVertical ();
					} else {
						EditorGUILayout.BeginVertical ();
						EditorGUILayout.LabelField (myTarget.GetType ().Name + " is up to date");
						EditorGUILayout.EndVertical ();
					}
				} else {
					EditorGUILayout.BeginVertical ();
					if (dirty) {
						EditorGUILayout.LabelField ("Press 'Refresh' to load updated component " + myTarget.GetType ().Name);
					} else {
						EditorGUILayout.LabelField ("Press 'Check' to get update information about " + myTarget.GetType ().Name);
					}
					EditorGUILayout.EndVertical ();
				}
			}
			EditorGUILayout.Separator ();
			EditorGUILayout.BeginHorizontal ();
			if (Manager.SharedInstance.HasBackup (myTarget)) {
				EditorGUILayout.LabelField ("Saved version " + Manager.SharedInstance.GetBackupTimestamp (myTarget));
				if (GUILayout.Button ("Undo")) {
					Manager.SharedInstance.Undo (myTarget);
					updateCheck = null;
					dirty = true;
				}
			} else {
				EditorGUILayout.LabelField ("No backup available");
				GUILayout.Button ("Undo", InspectorStyles.ButtonDisabled);
			}
			EditorGUILayout.EndHorizontal ();
		}

		/// <summary>
		/// Updates are performed every Preferences.Key.AutoRefreshInterval seconds to avoid heavy processing load.
		/// </summary>
		/// <param name="forceCheck">If set to <c>true</c> force check.</param>
		void CheckForUpdates (bool forceCheck) {
			if (!forceCheck) {
				int checkInterval = Preferences.GetInt (Preferences.Key.AutoRefreshInterval);
				if (checkInterval <= 0 || EditorApplication.timeSinceStartup - lastCheckTimestamp < checkInterval) {
					return;
				}
			}
			updateCheck = Manager.SharedInstance.CheckForUpdates (Selection.activeGameObject);
			updateCheck.Sort ((x, y) =>  {
				if (!forceCheck && x.result == ClassMemberCompareElement.Result.Error) {
					// unfold results if there are errors but not if this is an automatic check
					updateCheckFoldOutState = false;
				}
				if (x.result != y.result) {
					return x.result - y.result;
				}
				return x.Member.CompareTo (y.Member);
			});
			lastCheckTimestamp = EditorApplication.timeSinceStartup;
		}
	}
}


