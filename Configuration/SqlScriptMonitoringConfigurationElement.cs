using System.Configuration;

namespace WcfSurveyConfiguration
{
	public class SqlScriptMonitoringConfigurationElement : ConfigurationElement
	{
		public SqlScriptMonitoringConfigurationElement() { }

		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("connectionString", IsRequired = false, IsKey = false)]
		public string ConnectionString
		{
			get { return (string)this["connectionString"]; }
			set { this["connectionString"] = value; }
		}

		[ConfigurationProperty("emails", IsRequired = true, IsKey = false)]
		public string Emails
		{
			get { return (string)this["emails"]; }
			set { this["emails"] = value; }
		}

		[ConfigurationProperty("sqlScriptFullPath", IsRequired = true, IsKey = false)]
		public string SqlFileFullPath
		{
			get { return (string)this["sqlScriptFullPath"]; }
			set { this["sqlScriptFullPath"] = value; }
		}
		[ConfigurationProperty("sqlOutParam", IsRequired = false, IsKey = false)]
		public string SqlOutParam
		{
			get { return (string)this["sqlOutParam"]; }
			set { this["sqlOutParam"] = value; }
		}
		[ConfigurationProperty("frequency", IsRequired = false, IsKey = false)]
		public string Frequency
		{
			get { return (string)this["frequency"]; }
			set { this["frequency"] = value; }
		}
	}
}
