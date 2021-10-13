using System.Configuration;

namespace WcfSurveyConfiguration
{
	public class SurveyConfigurationCollection : ConfigurationElementCollection
	{
		public SurveyConfigurationCollection() { }

		public SurveyConfigurationElement this[int index]
		{
			get { return (SurveyConfigurationElement)BaseGet(index); }
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		new public SurveyConfigurationElement this[string name]
		{
			get { return (SurveyConfigurationElement)BaseGet(name); }
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new SurveyConfigurationElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SurveyConfigurationElement)element).Name;
		}

		public int IndexOf(SurveyConfigurationElement element)
		{
			return BaseIndexOf(element);
		}

		public void Add(SurveyConfigurationElement element)
		{
			BaseAdd(element);
		}

		public void Remove(SurveyConfigurationElement element)
		{
			if (BaseIndexOf(element) >= 0)
				BaseRemove(element.Name);
		}

		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}

		public void Remove(string name)
		{
			BaseRemove(name);
		}

		/// <summary>
		/// Clears this collection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}
	}
}
