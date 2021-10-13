using System.Configuration;

namespace DcsBriefop.Configuration
{
	internal class BriefopMarkerCollection : ConfigurationElementCollection
	{
		public BriefopMarkerCollection() { }

		public BriefopMarkerElement this[int index]
		{
			get { return (BriefopMarkerElement)BaseGet(index); }
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		new public BriefopMarkerElement this[string name]
		{
			get { return (BriefopMarkerElement)BaseGet(name); }
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new BriefopMarkerElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((BriefopMarkerElement)element).FileName;
		}

		public int IndexOf(BriefopMarkerElement element)
		{
			return BaseIndexOf(element);
		}

		public void Add(BriefopMarkerElement element)
		{
			BaseAdd(element);
		}

		public void Remove(BriefopMarkerElement element)
		{
			if (BaseIndexOf(element) >= 0)
				BaseRemove(element.FileName);
		}

		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}

		public void Remove(string name)
		{
			BaseRemove(name);
		}

		public void Clear()
		{
			BaseClear();
		}
	}
}
