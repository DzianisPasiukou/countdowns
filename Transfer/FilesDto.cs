namespace Transfer
{
	using System.Collections.Generic;

	/// <summary>
	/// The instance of files data transfer object.
	/// </summary>
	public class FilesDto
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the identifier of file.
		/// </summary>
		/// <value>
		/// The identifier of file.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of file.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description of file.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the collection of images.
		/// </summary>
		/// <value>
		/// The images.
		/// </value>
		public ICollection<ImageDto> Images { get; set; }

		#endregion
	}
}
