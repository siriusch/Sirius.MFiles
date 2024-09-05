﻿using System.Text;
using System.Threading.Tasks;

using Ical.Net.Serialization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Sirius.VAF.AspNetCore {
	public class CalendarResultExecutor: IActionResultExecutor<CalendarResult> {
		public Task ExecuteAsync(ActionContext context, CalendarResult result) {
			var response = context.HttpContext.Response;
			response.StatusCode = 200;
			response.Headers.Add("Content-Type","text/calendar; encoding=utf-8");
			new CalendarSerializer().Serialize(result.Calendar, response.Body, Encoding.UTF8);
			return Task.CompletedTask;
		}
	}
}
