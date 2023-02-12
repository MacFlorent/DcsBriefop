using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPart
	{
		public string Code { get; set; }
		public virtual string BuildHtmlContent()
		{
			return "";
		}

	}

	internal class BopBriefingPartText : BopBriefingPart
	{
		string Text { get; set; }
	}

	internal class BopBriefingPartText2 : BopBriefingPart
	{
		string sText { get; set; }
	}
}
