using System.Linq;
using System.Threading.Tasks;

using MFilesAPI;
using MFilesAPI.Extensions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;

namespace Sirius.VAF.AspNetCore {
	public class ObjVerExResultExecutor: IActionResultExecutor<ObjVerExResult> {
		public Task ExecuteAsync(ActionContext context, ObjVerExResult result) {
			var contentTypeProvider = context.HttpContext.RequestServices.GetRequiredService<IContentTypeProvider>();
			var response = context.HttpContext.Response;
			var vault = result.Object.Vault;
			var file = vault.ObjectFileOperations.GetFiles(result.Object.ObjVer).Cast<ObjectFile>().SingleOrDefault(result.Filter);
			if (file == null) {
				response.StatusCode = 404;
				return Task.CompletedTask;
			}
			response.StatusCode = 200;
			response.Headers.Add("Content-Type", contentTypeProvider.TryGetContentType('.'+file.Extension, out var contentType) ? contentType : "application/octet-stream");
			response.Headers.Add("Content-Disposition", $"inline; filename=\"{file.Title}.pdf\"");
			using var stream = vault.ObjectFileOperations.OpenFileForReading(file, vault);
			stream.CopyTo(response.Body);
			return Task.CompletedTask;
		}
	}
}