﻿/* Date: 3.12.2014, Time: 16:07 */
using System;
using System.Reflection;
using System.Reflection.Emit;
using IllidanS4.SharpUtils.Reflection.Emit;

namespace IllidanS4.SharpUtils.Reflection.TypeSupport
{
	/// <summary>
	/// Represents a derived byref type.
	/// </summary>
	public class ByRefType : TypeAppendConstruct
	{
		public override CorElementType CorElementType{
			get{
				return CorElementType.ByRef;
			}
		}
		
		public ByRefType(Type elementType) : base(elementType, UnderlyingByRefType(elementType))
		{
			
		}
		
		protected override string Append(string name)
		{
			return name+"&";
		}
		
		private static Type UnderlyingByRefType(Type elementType)
		{
			if(elementType is FunctionPointerType)
			{
				return Types.Generated.FnPtrByRef;
			}else try{
				return elementType.UnderlyingSystemType.MakeByRefType();
			}catch(NullReferenceException)
			{
				return Types.Void.MakeByRefType();
			}catch(TypeLoadException)
			{
				return Types.Void.MakeByRefType();
			}
		}
		
		protected override void AddSignature(SignatureHelper signature)
		{
			signature.AddElementType(CorElementType.ByRef);
			signature.AddArgumentSignature(ElementType);
		}
	}
}
