using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O365PWSync_SyncService
{
	enum SyncProcessedStatus
	{
		PENDING = 0,
		IN_PROGRESS = 1,
		COMPLETE = 2,
		FAILED = 3
	}

	class DBUser
	{
		public long ID;
		public string Username;
		public string Password;
		public long Timestamp;
		public SyncProcessedStatus Processed;
		public DateTime TimestampDatetime
		{
			get
			{
				return new DateTime(Timestamp);
			}
		}
	}
}
