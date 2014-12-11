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
	/// Field of arbitrary type containing modifiers and optional initialisation code e.g. "public const bool b = true".
	/// s. FieldCodeElement<T> for template based fields.
	/// </summary>
	public class GenericFieldCodeElement : MemberCodeElement
	{
		public override MemberTypeID MemberType {
			get { return MemberTypeID.Field; }
		}

		public bool Const = false;
		public bool ReadOnly = false;

		public string Modifiers {
			get { 
				if (Const) {
					return "const";
				} else if (declaredStatic) {
					if (ReadOnly) {
						return "static readonly";
					} else {
						return "static";
					}
				} else if (ReadOnly) {
					return "readonly";
				}
				return "";
			}
		}

		public string InitialiserCode = "";

		public GenericFieldCodeElement (string type, string name , string init = "", AccessType access = AccessType.Public) : 
			base (type, name, access) {
			InitialiserCode = init;
		}
		public GenericFieldCodeElement (Type type, string name , string init = "", AccessType access = AccessType.Public) : 
			base (type, name, access) {
			InitialiserCode = init;
		}

	}

	public class FieldCodeElement<T> : GenericFieldCodeElement
	{
		public FieldCodeElement (string name, string init, AccessType access = AccessType.Public) : 
			base (CodeElementUtils.GetFormattedType (typeof (T)), name, init, access) 
		{
		}
		
	}
	
}