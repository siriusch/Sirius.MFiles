using System;
using System.Threading.Tasks;

using MFiles.VAF.Common;

using MFilesAPI;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Sirius.VAF.AspNetCore {
	public class ObjVerExFileResult: ActionResult {
		public OneOf.OneOf<string, int, Func<ObjectFile, bool>> Filter {
			get;
		}

		public ObjVerEx Object {
			get;
		}

		public ObjVerExFileResult(ObjVerEx o, OneOf.OneOf<string, int, Func<ObjectFile, bool>>? filter) {
			Filter = filter ?? 0;
			Object = o;
		}

		public override Task ExecuteResultAsync(ActionContext context) {
			if (context == null) {
				throw new ArgumentNullException(nameof(context));
			}
			var executor = context.HttpContext.RequestServices.GetRequiredService<IActionResultExecutor<ObjVerExFileResult>>();
			return executor.ExecuteAsync(context, this);
		}
	}
}
