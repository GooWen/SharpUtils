﻿/* Date: 3.4.2015, Time: 20:53 */
using System;
using System.Collections.Generic;
using System.Reflection;
using IllidanS4.SharpUtils.Interop;

namespace IllidanS4.SharpUtils.Accessing
{
	/// <summary>
	/// Basic accessor methods.
	/// </summary>
	public static class Accessor
	{
		/// <summary>
		/// Returns an accessor to an array element.
		/// </summary>
		public static ArrayAccessor<T> Access<T>(this T[] array, int index)
		{
			return new ArrayAccessor<T>(array, index);
		}
		
		/// <summary>
		/// Returns a read-only accessor to a list element.
		/// </summary>
		public static ReadListAccessor<T> Access<T>(this IList<T> list, int index)
		{
			if(list.IsReadOnly)
			{
				return new ReadListAccessor<T>(list, index);
			}else{
				return new ListAccessor<T>(list, index);
			}
		}
		
		
		/// <summary>
		/// Returns a read-only accessor to a dictionary element.
		/// </summary>
		public static ReadDictionaryAccessor<TKey, TValue> Access<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
		{
			if(dictionary.IsReadOnly)
			{
				return new ReadDictionaryAccessor<TKey, TValue>(dictionary, key);
			}else{
				return new DictionaryAccessor<TKey, TValue>(dictionary, key);
			}
		}
		
		/// <summary>
		/// Returns a read-only accessor to an indexable object.
		/// </summary>
		public static IndexGetAccessor<TKey, TValue> Access<TKey, TValue>(this IIndexGet<TKey, TValue> indexable, TKey key)
		{
			return new IndexGetAccessor<TKey, TValue>(indexable, key);
		}
		
		/// <summary>
		/// Returns a write-only accessor to an indexable object.
		/// </summary>
		public static IndexSetAccessor<TKey, TValue> Access<TKey, TValue>(this IIndexSet<TKey, TValue> indexable, TKey key)
		{
			return new IndexSetAccessor<TKey, TValue>(indexable, key);
		}
		
		/// <summary>
		/// Returns an accessor to an indexable object.
		/// </summary>
		public static IndexGetSetAccessor<TKey, TValue> Access<TKey, TValue>(this IIndexGetSet<TKey, TValue> indexable, TKey key)
		{
			return new IndexGetSetAccessor<TKey, TValue>(indexable, key);
		}
		
		/// <summary>
		/// Returns a read-only accessor to a field.
		/// </summary>
		public static ReadFieldAccessor<T> Access<T>(this FieldInfo field, object target)
		{
			if(field.IsInitOnly)
			{
				return new ReadFieldAccessor<T>(field, target);
			}else{
				return new FieldAccessor<T>(field, target);
			}
		}
		
		/// <summary>
		/// Obtains a reference a "ref" accessor.
		/// </summary>
		public static void GetReference<T>(this IRefReference<T> r, Reference.RefAction<T> act)
		{
			r.GetReference<Unit>((ref T rf)=>{act(ref rf); return 0;});
		}
		
		/// <summary>
		/// Obtains a reference an "out" accessor.
		/// </summary>
		public static void GetReference<T>(this IOutReference<T> r, Reference.OutAction<T> act)
		{
			r.GetReference<Unit>((out T rf)=>{act(out rf); return 0;});
		}
		
		/// <summary>
		/// Obtains a safe reference from an accessor.
		/// </summary>
		public static void GetReference(this ITypedReference tr, Action<SafeReference> act)
		{
			tr.GetReference<Unit>(r => {act(r); return 0;});
		}
		
		/// <summary>
		/// Creates a temporary reference to an accessor.
		/// </summary>
		public static void GetTempReference<T>(this IReadWriteAccessor<T> acc, Reference.RefAction<T> act)
		{
			T temp = acc.Item;
			act(ref temp);
			acc.Item = temp;
		}
		
		/// <summary>
		/// Creates a temporary reference to an accessor.
		/// </summary>
		public static void GetTempReference(this IReadWriteAccessor acc, Reference.RefAction<object> act)
		{
			var temp = acc.Item;
			act(ref temp);
			acc.Item = temp;
		}
	}
}
