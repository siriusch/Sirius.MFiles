using System;
using System.Threading.Tasks;

using Ical.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Sirius.VAF.AspNetCore {
	public class CalendarResult: ActionResult {
		public Calendar Calendar {
			get;
		}

		public CalendarResult(Calendar calendar) {
			Calendar = calendar;
		}

		public override Task ExecuteResultAsync(ActionContext context) {
			if (context == null) {
				throw new ArgumentNullException(nameof(context));
			}
			var executor = context.HttpContext.RequestServices.GetRequiredService<IActionResultExecutor<CalendarResult>>();
			return executor.ExecuteAsync(context, this);
		}
	}
}
