using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
	/// <summary>
	///	 This object represents a sticker.
	/// </summary>
	public class Sticker : PhotoSize
	{
		/// <summary>
		///	 Sticker thumbnail in .webp or .jpg format
		/// </summary>
		[JsonProperty(PropertyName = "thumb")]
		public PhotoSize Thumb { get; set; }
	}
}