using System;
using System.Collections.Generic;
using System.Linq;

using MFiles.VAF.AppTasks;
using MFiles.VAF.Extensions;

using MFilesAPI;

namespace Sirius.VAF {
	public static class TaskManagerExtensions {
		public static bool IsDirectiveIdentical<T>(ObjIDTaskDirective<T[]> x, ObjIDTaskDirective<T[]> y) {
			return x.ObjectTypeID == y.ObjectTypeID && x.ObjectID == y.ObjectID && new HashSet<T>(x.Data).SetEquals(y.Data);
		}

		public static bool IsDirectiveIdentical<T>(ObjIDTaskDirective<T> x, ObjIDTaskDirective<T> y) where T: IEquatable<T> {
			return x.ObjectTypeID == y.ObjectTypeID && x.ObjectID == y.ObjectID && EqualityComparer<T>.Default.Equals(x.Data, y.Data);
		}

		public static void CancelIfTaskForSameObjectIsPending<T>(this TaskManager that, string queueId, string taskType, ITaskProcessingJob<T> job) where T: ObjIDTaskDirective {
			var hasWaitingTaskForSameObject = new TaskQuery()
					.Queue(queueId)
					.TaskType(taskType)
					.TaskState(MFTaskState.MFTaskStateWaiting)
					.FindTasks<T>(that)
					.Any(d => d.Directive.ObjectTypeID == job.Directive.ObjectTypeID && d.Directive.ObjectID == job.Directive.ObjectID);
			if (hasWaitingTaskForSameObject) {
				throw new AppTaskException(TaskProcessingJobResult.Cancel, $"Another {taskType} is pending for the same object");
			}
		}

		public static bool AddTaskIfDistinct<T>(this TaskManager that, Vault vault, string queueId, string taskType, T taskDirective, Func<T, T, bool> isIdentical) where T: TaskDirective {
			var hasWaitingIdenticalTask = new TaskQuery()
					.Queue(queueId)
					.TaskType(taskType)
					.TaskState(MFTaskState.MFTaskStateWaiting)
					.FindTasks<T>(that)
					.Any(d => isIdentical(d.Directive, taskDirective));
			if (hasWaitingIdenticalTask) {
				return false;
			}
			that.AddTask(
					vault,
					queueId,
					taskType,
					taskDirective
			);
			return true;
		}
	}
}
