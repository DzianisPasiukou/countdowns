namespace Transfer
{
	using System.Collections.Generic;

	/// <summary>
	/// The instance of exercises data transfer object.
	/// </summary>
	public class ExercisesDto
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the identifier of exercise.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of exercise.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description of exercise.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the files of exercise.
		/// </summary>
		/// <value>
		/// The files.
		/// </value>
		public ICollection<FilesDto> Files { get; set; }

		#endregion
	}
}
