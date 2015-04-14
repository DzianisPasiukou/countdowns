namespace LoggingManager
{
	using System.Globalization;
	using System.IO;
	using System.Xml.Serialization;

	using Newtonsoft.Json;

	/// <summary>
	/// The instance for serialization class object.
	/// </summary>
	public static class MySerialization
	{
		#region Public Methods

		/// <summary>
		/// Serializes to XML.
		/// </summary>
		/// <param name="value">The object.</param>
		/// <returns>String from serialization.</returns>
		public static string SerializeToXML(object value)
		{
			XmlSerializer serializer = new XmlSerializer(value.GetType());

			using (StringWriter writer = new StringWriter(CultureInfo.InvariantCulture))
			{
				serializer.Serialize(writer, value);

				return writer.ToString();
			}
		}

		/// <summary>
		/// Serializes to JSON.
		/// </summary>
		/// <param name="value">The object.</param>
		/// <returns>String from serialization.</returns>
		public static string SerializeToJSON(object value)
		{
			return JsonConvert.SerializeObject(value, Formatting.Indented);
		}

		#endregion
	}
}
