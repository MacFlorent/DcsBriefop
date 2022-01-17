using System.Configuration;

namespace DcsBriefop.Configuration
{
	internal class BriefopRouteCollection : ConfigurationElementCollection
	{
		public BriefopRouteCollection() { }

		public BriefopRouteElement this[int index]
		{
			get { return (BriefopRouteElement)BaseGet(index); }
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		new public BriefopRouteElement this[string name]
		{
			get { return (BriefopRouteElement)BaseGet(name); }
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new BriefopRouteElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((BriefopRouteElement)element).FileName;
		}

		public int IndexOf(BriefopRouteElement element)
		{
			return BaseIndexOf(element);
		}

		public void Add(BriefopRouteElement element)
		{
			BaseAdd(element);
		}

		public void Remove(BriefopRouteElement element)
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
