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
using System;
using System.Collections.Generic;

namespace Scio.CodeGeneration
{
	/// <summary>
	/// Defines a method with an arbitrary return type.
	/// </summary>
	public class GenericMethodCodeElement : MemberCodeElement, ICodeBlock
	{
		public override MemberTypeID MemberType {
			get { return MemberTypeID.Method; }
		}

		public List<ParameterCodeElement> Parameters = new List<ParameterCodeElement> ();
		List<string> code = new List<string> ();
		public List<string> Code {
			get { return code; }
		}

		public GenericMethodCodeElement (Type type, string name, AccessType access = AccessType.Public) : 
			base (type, name, access) {
		}

		public GenericMethodCodeElement (string type, string name, AccessType access = AccessType.Public) : 
			base (type, name, access) {
		}
		
		public void AddParameter (Type type, string name) {
			Parameters.Add (new ParameterCodeElement (type, name));
		}

		public void AddParameter (Type type, string name, object defaultValue) {
			Parameters.Add (new ParameterCodeElement (type, name, defaultValue));
		}

		public override string GetSignature () {
			string paramString = "";
			Parameters.ForEach ( (ParameterCodeElement param) => paramString += (string.IsNullOrEmpty (paramString) ? "" : ",") + param.ParameterType );
			string s = Name + "(" + paramString + ")";
			return s;
		}

		public override bool Equals (object obj) {
			if (obj is GenericMethodCodeElement) {
				return GetSignature () == ((GenericMethodCodeElement)obj).GetSignature ();
			} else {
				return base.Equals (obj);
			}
		}
		
		public override int GetHashCode () {
			return GetSignature ().GetHashCode ();
		}
		
		public override string ToString () {
			string str = "";
			Code.ForEach ((string s) => str += s + "\n");
			string pStr = ParameterCodeElement.ListToString (Parameters);
			return string.Format ("{0} ({1})\n{2}", base.ToString (), pStr, str);
		}
	}
	
	public class MethodCodeElement<T> : GenericMethodCodeElement 
	{
		public MethodCodeElement (string name, AccessType access = AccessType.Public) : 
			base (typeof (T), name, access)
		{
		}
	}

	/// <summary>
	/// Type void cannot be specified as generic Parameter so this is the convenience class.
	/// </summary>
	public class VoidMethodCodeElement : GenericMethodCodeElement
	{
		public VoidMethodCodeElement (string name, AccessType access = AccessType.Public) : 
			base ("void", name, access)
		{
		}
	}
}
