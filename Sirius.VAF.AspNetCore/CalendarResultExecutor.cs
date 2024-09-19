using System.Text;
using System.Threading.Tasks;

using Ical.Net.Serialization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Sirius.VAF.AspNetCore {
	public class CalendarResultExecutor: IActionResultExecutor<CalendarResult> {
		public Task ExecuteAsync(ActionContext context, CalendarResult result) {
			var response = context.HttpContext.Response;
			response.StatusCode = 200;
			response.Headers.Add("Content-Type",string.IsNullOrEmpty(result.Calendar.Method) 
					? "text/calendar; charset=\"utf-8\"" 
					: $"text/calendar; charset=\"utf-8\"; method=\"{result.Calendar.Method}\"");
			new CalendarSerializer().Serialize(result.Calendar, response.Body, Encoding.UTF8);
			return Task.CompletedTask;
		}
	}
}
