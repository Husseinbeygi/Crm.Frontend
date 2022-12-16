namespace Framework.Models;

public enum ResultStatus
{
	Succeeded = 0,
	Failed = 1,
	PartiallySucceeded = 2
}

public class Response
{
	public Response()
	{
		errorMessages = new();
		hiddenMessages = new();
		informationMessages = new();
	}

	public int count { get; set; }
	public int page { get; set; }
	public bool hasNextPage { get; set; }
	public List<string> errorMessages { get; set; }
	public List<string> hiddenMessages { get; set; }
	public List<string> informationMessages { get; set; }
	public string status { get; set; }
}
public class Response<T> : Response
{
	public T data { get; set; }
}
