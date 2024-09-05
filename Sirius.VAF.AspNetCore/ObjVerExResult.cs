using System;
using System.Threading.Tasks;

using MFiles.VAF.Common;

using MFilesAPI;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Sirius.VAF.AspNetCore {
	public class ObjVerExResult: ActionResult {
		public Func<ObjectFile, bool> Filter {
			get;
		}

		public ObjVerEx Object {
			get;
		}

		public ObjVerExResult(ObjVerEx o, Func<ObjectFile, bool> filter) {
			Filter = filter ?? (_ => true);
			Object = o;
		}

		public override Task ExecuteResultAsync(ActionContext context) {
			if (context == null) {
				throw new ArgumentNullException(nameof(context));
			}
			var executor = context.HttpContext.RequestServices.GetRequiredService<IActionResultExecutor<ObjVerExResult>>();
			return executor.ExecuteAsync(context, this);
		}
	}
}
