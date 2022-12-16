using AntDesign;
using Framework.Models;

namespace Framework.Toasts;

public class ToastMessage
{
	private readonly IMessageService _message;

	public ToastMessage(IMessageService Message)
	{
		_message = Message;
	}

	public async Task<bool> Show(Response response)
	{
		if (response.informationMessages is not null
			&& response.informationMessages.Any())
		{

			foreach (var infomsg in response.informationMessages)
			{
				await _message.Info(infomsg);
			}
		}

		if (response.status == "Succeeded")
		{
			await _message
			.Success(
				string.Concat(Resources.Messages.Successes.Success));

			return true;
		}
		else if (response.status == "Failed")
		{
			if (response.errorMessages is not null
				&& response.errorMessages.Any())
			{

				foreach (var errmsg in response.errorMessages)
				{
					await _message.Error(errmsg);
				}
			}
		}
		else if (response.status == "PartiallySucceeded")
		{
			if (response.errorMessages is not null
				&& response.errorMessages.Any())
			{
				foreach (var errmsg in response.errorMessages)
				{
					await _message.Error(errmsg);
				}
			}
			if (response.informationMessages is not null
				&& response.informationMessages.Any())
			{
				foreach (var errmsg in response.informationMessages)
				{
					await _message.Info(errmsg);
				}
			}
		}
		return false;
	}
}
