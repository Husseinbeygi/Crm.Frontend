using System.Text.Json.Serialization;

namespace Leads.Models.ValueObjects;

public class ValueObject
{
	[JsonConstructor]
	public ValueObject()
	{
	}

	public ValueObject(int key, string value)
	{
		Value = key;
		Name = value;
	}

	public int Value { get; set; }
	public string? Name { get; set; }
}
