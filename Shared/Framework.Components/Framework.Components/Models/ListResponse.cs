namespace Framework.Models
{
	public class ListResponse<T>
	{
		public List<T> data { get; set; }
		public int count { get; set; }
		public int page { get; set; }
		public bool hasNextPage { get; set; }
		public List<object> errorMessages { get; set; }
		public List<object> hiddenMessages { get; set; }
		public List<object> informationMessages { get; set; }
		public string status { get; set; }
	}




}
