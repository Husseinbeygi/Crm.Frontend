namespace Leads.Models;

public class LeadsViewModel : LeadsViewModelBase
{
	public Guid Id { get; set; }
    public int RowNumber { get; set; }
    public int VersionNumber { get; set; }

}
