using System;
using System.Collections.Generic;

using MFiles.VAF.Configuration;

using MFilesAPI;

namespace Sirius.VAF {
	public static class VaultUserGroupOperationsExtensions {
		public static IEnumerable<int> GetUsersOfGroup(this VaultUserGroupOperations that, MFIdentifier groupId) {
			var users = new HashSet<int>();
			var queue = new Queue<int>();
			var visited = new HashSet<int>();
			queue.Enqueue(groupId);
			do {
				foreach (int member in that.GetUserGroup(queue.Dequeue()).Members) {
					if (member > 0) {
						users.Add(member);
					} else if (visited.Add(-member)) {
						queue.Enqueue(-member);
					}
				}
			} while (queue.Count > 0);
			return users;
		}
	}
}
