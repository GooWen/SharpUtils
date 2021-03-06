﻿/* Date: 6.9.2017, Time: 16:19 */
using System;
using System.Collections;
using System.IO;

namespace IllidanS4.SharpUtils.IO.FileSystems.DataExtensions
{
	using DataUri = DataFileSystem.DataUri;
	/// <summary>
	/// Provides extended information about a resource based on its content rather than metadata.
	/// </summary>
	public interface IDataExtension
	{
		bool SupportsType(string contentType);
		
		T GetProperty<T>(DataUri dataUri, ResourceProperty property);
	}
}
