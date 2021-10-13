using System.Configuration;

namespace WcfSurveyConfiguration
{
	public class SqlScriptMonitoringConfigurationCollection : ConfigurationElementCollection
	{
		public SqlScriptMonitoringConfigurationCollection() { }

		public SqlScriptMonitoringConfigurationElement this[int index]
		{
			get { return (SqlScriptMonitoringConfigurationElement)BaseGet(index); }
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		new public SqlScriptMonitoringConfigurationElement this[string name]
		{
			get { return (SqlScriptMonitoringConfigurationElement)BaseGet(name); }
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new SqlScriptMonitoringConfigurationElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SqlScriptMonitoringConfigurationElement)element).Name;
		}

		public int IndexOf(SqlScriptMonitoringConfigurationElement element)
		{
			return BaseIndexOf(element);
		}

		public void Add(SqlScriptMonitoringConfigurationElement element)
		{
			BaseAdd(element);
		}

		public void Remove(SqlScriptMonitoringConfigurationElement element)
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
