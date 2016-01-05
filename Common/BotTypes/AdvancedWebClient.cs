using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;

namespace Telesharp.Common.BotTypes
{
	public class AdvancedWebClient : WebClient
	{
		public delegate void GetWebRequestEventHandler(object sender, GetWebRequestEventArgs e);

		/// <summary>
		///	 Create new Advanced WebClient
		/// </summary>
		public AdvancedWebClient() : this(2000)
		{
		}

		/// <summary>
		///	 Create new Advanced WebClient
		/// </summary>
		public AdvancedWebClient(int timeout)
		{
			Timeout = timeout;
		}

		/// <summary>
		///	 Timeout in milliseconds
		/// </summary>
		public int Timeout { get; set; }

		protected override WebRequest GetWebRequest(Uri address)
		{
			var request = base.GetWebRequest(address);
			if (request != null)
			{
				request.Timeout = Timeout;
			}

			var args = new GetWebRequestEventArgs(request);
			OnGetWebRequest(this, args);
			request = args.Request;

			return request;
		}

		/// <summary>
		///	 Send POST to url
		/// </summary>
		/// <param name="uri">Uri</param>
		/// <param name="fieldsAndValues">Fields and values</param>
		/// <returns></returns>
		public string SendPOST(string uri, Dictionary<string, string> fieldsAndValues)
		{
			var collection = new NameValueCollection();
			foreach (var item in fieldsAndValues)
			{
				collection.Add(item.Key, item.Value);
			}
			return Encoding.UTF8.GetString(UploadValues(uri, collection));
		}

		/// <summary>
		///	 Send GET to url
		/// </summary>
		/// <param name="uri">Uri</param>
		/// <param name="fieldsAndValues">Fields and values</param>
		/// <returns></returns>
		public string SendGET(string uri, Dictionary<string, string> fieldsAndValues)
		{
			var i = 0;
			foreach (var item in fieldsAndValues)
			{
				uri += i++ == 0 ? "?" : "&";
				uri += item.Key + "=" + HttpUtility.UrlEncode(item.Value);
			}
			return DownloadString(uri);
		}

		public event GetWebRequestEventHandler OnGetWebRequest = delegate { };

		public class GetWebRequestEventArgs : EventArgs
		{
			private WebRequest _request;

			public GetWebRequestEventArgs(WebRequest request)
			{
				if (request == null)
				{
					throw new ArgumentNullException();
				}
				_request = request;
			}

			public WebRequest Request
			{
				get { return _request; }
				set
				{
					if (value != null)
					{
						_request = value;
					}
					else
					{
						throw new ArgumentNullException();
					}
				}
			}
		}
	}
}