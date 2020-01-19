using System;
using Epic.Core;
using UnityEngine;

namespace Epic
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true)]
	public class UPropertyAttribute : Attribute
	{
		public UPropertyAttribute(UP property, string category)
		{
			
		}
	}
}