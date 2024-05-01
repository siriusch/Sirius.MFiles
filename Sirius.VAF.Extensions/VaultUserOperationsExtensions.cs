using MFilesAPI;

namespace Sirius.VAF {
	public static class VaultUserOperationsExtensions {
		public static string GetUserName(this VaultUserOperations that, int userId) {
			var loginAccount = that.GetLoginAccountOfUser(userId);
			return string.IsNullOrEmpty(loginAccount.FullName) ? loginAccount.UserName : loginAccount.FullName;
		}
	}
}
