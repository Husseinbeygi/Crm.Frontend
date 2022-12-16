namespace Leads.Models.ValueObjects;

public class Options
{
	public Options()
	{
		leadSource = new();
		leadStatus = new();
		industry = new();
		rating = new();
		salutation = new();
	}

	public List<ValueObject> leadSource { get; set; }
	public List<ValueObject> leadStatus { get; set; }
	public List<ValueObject> industry { get; set; }
	public List<ValueObject> rating { get; set; }
	public List<ValueObject> salutation { get; set; }
}
