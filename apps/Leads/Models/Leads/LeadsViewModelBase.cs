using Leads.Models.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Leads.Models.Leads
{
	public class LeadsViewModelBase
	{
		public LeadsViewModelBase()
		{
			Salutaion = new ValueObject();
			Industry = new ValueObject();
			LeadSource = new ValueObject();
			LeadStatus = new ValueObject();
			Rating = new ValueObject();
		}

		public decimal? AnnualRevenue { get; set; }
		public string? City { get; set; }

		[Display
			(ResourceType = typeof(Resources.DataDictionary),
			Name = nameof(Resources.DataDictionary.CompanyName))]
		[Required(ErrorMessageResourceType = typeof(Resources.Messages.Validations),
				ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
		public string Company { get; set; }
		public string? Country { get; set; }
		public long? CreatedAt { get; set; }
		public Guid? CreatedById { get; set; }
		public string? Description { get; set; }

		[Display
			(ResourceType = typeof(Resources.DataDictionary),
			Name = nameof(Resources.DataDictionary.EmailAddress))]
		public string? Email { get; set; }

		[Display(ResourceType = typeof(Resources.DataDictionary),
			Name = nameof(Resources.DataDictionary.FirstName))]
		public string? FirstName { get; set; }

		[Display(ResourceType = typeof(Resources.DataDictionary),
			 Name = nameof(Resources.DataDictionary.FullName))]
		public string? FullName { get; set; }


		[Display(ResourceType = typeof(Resources.DataDictionary),
		 Name = nameof(Resources.DataDictionary.LastName))]

		[Required(ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
		public string LastName { get; set; }
		public ValueObject Industry { get; set; }
		public ValueObject LeadSource { get; set; }
		public ValueObject LeadStatus { get; set; }
		[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Mobile))]
		public string? Mobile { get; set; }
		public long? ModifiedAt { get; set; }
		public Guid? ModifiedById { get; set; }
		public int? NumberOfEmployees { get; set; }
		public Guid? OwnerId { get; set; }
		public string? Phone { get; set; }
		public string? PostalCode { get; set; }
		public string? Address { get; set; }
		public ValueObject Rating { get; set; }
		[Display(ResourceType = typeof(Resources.DataDictionary),
				Name = nameof(Resources.DataDictionary.Salutation))]
		public ValueObject Salutaion { get; set; }
		public string? State { get; set; }
		public string? Street { get; set; }
		public Guid? TenantId { get; set; }
		[Display(ResourceType = typeof(Resources.DataDictionary),
			Name = nameof(Resources.DataDictionary.Title))]

		public string? Title { get; set; }
		public string? Website { get; set; }
	}
}