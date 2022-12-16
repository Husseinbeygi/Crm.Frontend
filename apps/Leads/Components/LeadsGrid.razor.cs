using AntDesign;
using Leads.Models.Leads;
using Microsoft.AspNetCore.Components;

namespace Leads.Components;

public partial class LeadsGrid
{
	bool _isloading = true;
	ColumnAlign columnAlign = ColumnAlign.Center;
	string _checkboxFix = "left";
	string _actionFix = "right";
	string fullnameFilter = string.Empty;

	[CascadingParameter]
	public Pages.Index Index { get; set; }

	IEnumerable<LeadsViewModel> selectedRows;
	public IEnumerable<LeadsViewModel> SelectedRows
	{
		get
		{
			return selectedRows;
		}
		set
		{
			selectedRows = value;
			Index.CheckforSelection();
		}
	}

	public List<LeadsViewModel> Leads { get; set; }

	List<LeadsViewModel> FilteredItems
	{
		get
		{
			return Leads?.Where(x => x.FullName.Contains(fullnameFilter, StringComparison.CurrentCultureIgnoreCase)).ToList();
		}
	}

	List<LeadsViewModel> _leadsViewModels = new();

	protected async override Task OnInitializedAsync()
	{
		Index.LeadsGrid = this;
		await base.OnInitializedAsync();
		await GetLeadsAsync();
		AdjustRightToLeft();
	}

	private void AdjustRightToLeft()
	{
		if (Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft)
		{
			string _checkboxFix = "right";
			string _actionFix = "left";
		}
	}

	public async Task GetLeadsAsync()
	{
		_isloading = true;

		var _leadsResponse = await _leadService
			.GetAsync<Models.ListResponse<Leads.Models.Leads.LeadsViewModel>>("Leads");

		Leads = _leadsResponse.data;

		int i = 1;
		foreach (var item in Leads)
		{
			item.RowNumber = i++;
		}
		_isloading = false;

	}

	public async Task GotoEdit(Guid id)
	{
		_navigationManager.NavigateTo($"/leads/{id}");
	}

	public async Task DeleteLeads()
	{
		if (!selectedRows.Any())
		{
			return;
		}

		_isloading = true;

		List<Guid> keys = new();

		foreach (var item in selectedRows)
		{
			keys.Add(item.Id);
		}

		await _leadService.
			DeleteAsync<Guid, Models.ListResponse<Leads.Models.Leads.LeadsViewModel>>("Leads", keys);

		await GetLeadsAsync();

		_isloading = false;

	}
}
