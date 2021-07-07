using System;
using System.Collections;

namespace RssEngine.Engine
{
	/// <summary>
	/// Represents a strongly-typed collection of <see cref="RssItem"/> instances.
	/// </summary>
	public class RssItemList : CollectionBase
	{
		/// <summary>
		/// Adds a new RssItem to the end of the collection.
		/// </summary>
		/// <param name="value">An <see cref="RssItem"/> instance.</param>
		/// <returns>The index at which <i>value</i> has been added.</returns>
		public int Add(RssItem value)
		{
			return base.InnerList.Add(value);
		}

		/// <summary>
		/// Gets or sets the <see cref="RssItem"/> element at the specified index.
		/// </summary>
		public RssItem this[int index]
		{
			get
			{
				return (RssItem) base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}
	}
}
