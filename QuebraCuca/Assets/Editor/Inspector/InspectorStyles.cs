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
using System.Collections;

namespace Scio.AnimatorAccessGenerator {
	
	public static class InspectorStyles
	{
		public static GUIStyle ButtonDisabled = null;
		public static GUIStyle MidMiniButtonHighLighted = null;
		public static GUIStyle RightMiniButtonHighLighted = null;
		public static GUIStyle ButtonRegular = null;
		public static GUIStyle LabelRegular = null;
		public static GUIStyle LabelRed = null;
		public static GUIStyle LabelHighLighted = null;

		static InspectorStyles () {
			ButtonDisabled = new GUIStyle ("button");
			ButtonDisabled.name = "ButtonDisabled";
			ButtonDisabled.normal.textColor = Color.gray;
			MidMiniButtonHighLighted = new GUIStyle ("miniButtonMid");
			MidMiniButtonHighLighted.name = "MidMiniButtonHighLighted";
			MidMiniButtonHighLighted.normal.textColor = Color.white;
			RightMiniButtonHighLighted = new GUIStyle ("miniButtonRight");
			RightMiniButtonHighLighted.name = "RightMiniButtonHighLighted";
			RightMiniButtonHighLighted.normal.textColor = Color.white;
			ButtonRegular = new GUIStyle ("button");
			LabelRegular = new GUIStyle ("label");
			LabelRed = new GUIStyle ("label");
			LabelRed.normal.textColor = Color.red;
			LabelHighLighted = new GUIStyle ("label");
			LabelHighLighted.normal.textColor = Color.white;
		}
	}
}

