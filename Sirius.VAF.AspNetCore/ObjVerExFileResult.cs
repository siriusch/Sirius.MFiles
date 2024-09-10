using System;
using System.Threading.Tasks;

using MFiles.VAF.Common;

using MFilesAPI;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using OneOf;

namespace Sirius.VAF.AspNetCore {
	public class ObjVerExFileResult: ActionResult {
		public OneOf.OneOf<string, int, Func<ObjectFile, bool>> FileSpec {
			get;
		}

		public ObjVerEx Object {
			get;
		}

		public ObjVerExFileResult(ObjVerEx o): this(o, OneOf<string, int, Func<ObjectFile, bool>>.FromT1(0)) { }
		
		public ObjVerExFileResult(ObjVerEx o, string fileName): this(o, OneOf<string, int, Func<ObjectFile, bool>>.FromT0(fileName)) { }
		
		public ObjVerExFileResult(ObjVerEx o, int index): this(o, OneOf<string, int, Func<ObjectFile, bool>>.FromT1(index)) { }

		public ObjVerExFileResult(ObjVerEx o, Func<ObjectFile, bool> filter): this(o, OneOf<string, int, Func<ObjectFile, bool>>.FromT2(filter)) { }

		private ObjVerExFileResult(ObjVerEx o, OneOf.OneOf<string, int, Func<ObjectFile, bool>> fileSpec) {
			FileSpec = fileSpec;
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
