using System.Linq;
using System.Text;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;

namespace Marketplace.BackgroundJob.Hangfire.MSSQL;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
	private readonly string? _username;
	private readonly string? _password;

	public HangfireAuthorizationFilter(string? username, string? password)
	{
		_username = username;
		_password = password;
	}

	public bool Authorize(DashboardContext context)
	{
		var httpContext = context.GetHttpContext();

		var result = false;
		var authorization = httpContext.Request.Headers.Authorization.ToString();

		if (string.IsNullOrWhiteSpace(authorization) == false)
		{
			Encoding encoding = Encoding.GetEncoding("iso-8859-1");
			string usernamePassword = encoding.GetString(Convert.FromBase64String(authorization.Replace("Basic ", string.Empty)));

			if (string.IsNullOrWhiteSpace(usernamePassword) == false)
			{
				string? username = usernamePassword!.Split(":").FirstOrDefault();
				string? password = usernamePassword!.Split(":").LastOrDefault();

				if (string.IsNullOrWhiteSpace(username) == false && string.IsNullOrWhiteSpace(password) == false)
				{
					if (username == _username && password == _password)
					{
						result = true;
					}
				}
			}
		}

		if (result == false)
		{
			httpContext.Response.StatusCode = 401;
			httpContext.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Hangfire Dashboard\"");
		}

		return result;
	}
}