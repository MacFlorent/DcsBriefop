using System.Configuration;
using System.ServiceModel.Configuration;

namespace WcfSurveyConfiguration
{
	public class SurveyConfigurationElement : ConfigurationElement
	{
		public SurveyConfigurationElement() { }

		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("database", IsRequired = true, IsKey = false)]
		public string Database
		{
			get { return (string)this["database"]; }
			set { this["database"] = value; }
		}

		[ConfigurationProperty("emails", IsRequired = true, IsKey = false)]
		public string Emails
		{
			get { return (string)this["emails"]; }
			set { this["emails"] = value; }
		}

		[ConfigurationProperty("endpoint", IsRequired = true, IsKey = false)]
		public ChannelEndpointElement Endpoint
		{
			get { return (ChannelEndpointElement)this["endpoint"]; }
			set { this["endpoint"] = value; }
		}

		[ConfigurationProperty("endpointNotif", IsRequired = false, IsKey = false)]
		public ChannelEndpointElement EndpointNotif
		{
			get { return (ChannelEndpointElement)this["endpointNotif"]; }
			set { this["endpointNotif"] = value; }
		}
	}
}
