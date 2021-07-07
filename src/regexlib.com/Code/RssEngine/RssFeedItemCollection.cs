using System;
using System.Collections;

namespace RssEngine
{
	/// <summary>
	/// RssItemCollection represents a collection of <see cref="RssFeedItem"/> objects.  The <see cref="RssFeed"/>
	/// control has a property called <b>Items</b> which is of type RssFeedItemCollection, and allows programmatic
	/// access to the RssFeed control's RSS items.
	/// </summary>
	public class RssFeedItemCollection : ICollection
	{
		#region Private Member Variables
		// private member variables
		private ArrayList items;
		#endregion

		#region Constructors
		// make the default constructor private, so it can't be called
		private RssFeedItemCollection()  {}

		/// <summary>
		/// Creates a new RssItemCollection based on a populated ArrayList
		/// </summary>
		/// <param name="items">An ArrayList of RssItem members.</param>
		public RssFeedItemCollection(ArrayList items)
		{
			this.items = items;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Copies the elements of the RssItemCollection to an array, starting at a particular array index.
		/// </summary>
		/// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection. The Array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in array at which copying begins.</param>
		public virtual void CopyTo(Array array, int index)
		{
			items.CopyTo(array, index);
		}

		/// <summary>
		/// Returns an IEnumerator that can be used to enumerate through the RssItemCollection.
		/// </summary>
		/// <returns>An enumerator that can be used to enumerate through the RssItemCollection.</returns>
		public virtual IEnumerator GetEnumerator()
		{
			return items.GetEnumerator();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Returns the number of elements in the RssItemCollection.
		/// </summary>
		public virtual int Count
		{
			get
			{
				return items.Count;
			}
		}

		/// <summary>
		/// Retrieves a particular RssItem instance from the RssItemCollection.
		/// </summary>
		public virtual RssFeedItem this[int index]
		{
			get
			{
				return (RssFeedItem) items[index];
			}
		}

		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}
		#endregion
	}
}
