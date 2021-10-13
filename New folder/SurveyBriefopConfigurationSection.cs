using System.Configuration;

namespace WcfSurveyConfiguration
{
	public class SurveyConfigurationSection : ConfigurationSection
	{
		public SurveyConfigurationSection() { }

		[ConfigurationProperty("frequency", IsRequired = true)]
		public int Frequency
		{
			get { return (this["frequency"] as int?).GetValueOrDefault(0); }
			set { this["frequency"] = value; }
		}

		[ConfigurationProperty("activityPeriod", IsRequired = true)]
		public string ActivityPeriod
		{
			get { return this["activityPeriod"] as string; }
			set { this["activityPeriod"] = value; }
		}

		[ConfigurationProperty("activityDays", IsRequired = true)]
		public string ActivityDays
		{
			get { return this["activityDays"] as string; }
			set { this["activityDays"] = value; }
		}

		[ConfigurationProperty("emailFrom", IsRequired = false)]
		public string EmailFrom
		{
			get { return this["emailFrom"] as string; }
			set { this["emailFrom"] = value; }
		}

		[ConfigurationProperty("SurveyedConfigurations", IsDefaultCollection = false)]
		[ConfigurationCollection( typeof(SurveyConfigurationCollection), AddItemName = "add")]
		public SurveyConfigurationCollection SurveyedConfigurations
		{
			get
			{
				SurveyConfigurationCollection servicesCollection = this["SurveyedConfigurations"] as SurveyConfigurationCollection;
				return servicesCollection;
			}
		}

		[ConfigurationProperty("SqlScriptMonitoring", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(SqlScriptMonitoringConfigurationCollection), AddItemName = "add")]
		public SqlScriptMonitoringConfigurationCollection SqlScriptMonitoring
		{
			get
			{
				SqlScriptMonitoringConfigurationCollection servicesCollection = this["SqlScriptMonitoring"] as SqlScriptMonitoringConfigurationCollection;
				return servicesCollection;
			}
		}

	}
}
