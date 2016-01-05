using System;

namespace Telesharp.Common.JsonConverters
{
	internal class UnixDateTimeConverterHelper
	{
		/// <summary>
		///	 Convert a long into a DateTime
		/// </summary>
		public static DateTime FromUnixTime(long self)
		{
			var ret = new DateTime(1970, 1, 1);
			return ret.AddSeconds(self);
		}

		/// <summary>
		///	 Convert a DateTime into a long
		/// </summary>
		public static long ToUnixTime(DateTime self)
		{
			if (self == DateTime.MinValue)
			{
				return 0;
			}

			var epoc = new DateTime(1970, 1, 1);
			var delta = self - epoc;

			if (delta.TotalSeconds < 0) throw new ArgumentOutOfRangeException(nameof(self));

			return (long) delta.TotalSeconds;
		}
	}
}