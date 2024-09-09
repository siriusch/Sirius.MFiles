using System.Linq;
using System.Threading.Tasks;

using MFilesAPI;
using MFilesAPI.Extensions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;

namespace Sirius.VAF.AspNetCore {
	public class ObjVerExFileResultExecutor: IActionResultExecutor<ObjVerExFileResult> {
		public Task ExecuteAsync(ActionContext context, ObjVerExFileResult result) {
			var response = context.HttpContext.Response;
			var vault = result.Object.Vault;
			var files = vault.ObjectFileOperations.GetFiles(result.Object.ObjVer);
			var file = result.Filter.Match(
					filename => files.GetObjectFileByNameForFileSystem(filename), 
					index => index > 0 && index < files.Count ? files[index] : null, 
					filter => files.Cast<ObjectFile>().SingleOrDefault(filter));
			if (file == null) {
				response.StatusCode = 404;
				return Task.CompletedTask;
			}
			var contentTypeProvider = context.HttpContext.RequestServices.GetRequiredService<IContentTypeProvider>();
			response.StatusCode = 200;
			response.Headers.Add("Content-Type", contentTypeProvider.TryGetContentType('.'+file.Extension, out var contentType) ? contentType : "application/octet-stream");
			response.Headers.Add("Content-Disposition", $"inline; filename=\"{file.GetNameForFileSystem()}\"");
			using var stream = vault.ObjectFileOperations.OpenFileForReading(file, vault);
			stream.CopyTo(response.Body);
			return Task.CompletedTask;
		}
	}
}