namespace CountdownDataBaseLayer.Repo
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using CountdownDataBaseLayer.Comparer;

	/// <summary>
	/// Repository for files.
	/// </summary>
	public class FilesRepo : MyDbContainer<Files>, IDisposable
	{
		#region Public Methods

		/// <summary>
		/// Gets all files.
		/// </summary>
		/// <returns>
		/// IEnumerable of files.
		/// </returns>
		public override IEnumerable<Files> GetAll()
		{
			IEnumerable<Files> files = this.Container.Files;
			return files;
		}

		/// <summary>
		/// Gets the by identifier file.
		/// </summary>
		/// <param name="id">The identifier of file.</param>
		/// <returns>
		/// The files.
		/// </returns>
		public override Files GetById(int id)
		{
			Files file = this.Container.Files.Find(id);
			return file;
		}

		/// <summary>
		/// Updates the many files.
		/// </summary>
		/// <param name="existingEntities">The existing entities.</param>
		/// <param name="updatedEntities">The updated entities.</param>
		public override void UpdateMany(IEnumerable<Files> existingEntities, IEnumerable<Files> updatedEntities)
		{
			var addedFile = updatedEntities.Except(existingEntities, new CompareFiles());
			var deletedFile = existingEntities.Except(updatedEntities, new CompareFiles());
			var modifiedFile = updatedEntities.Except(addedFile, new CompareFiles());

			addedFile.ToList<Files>().ForEach(addFile =>
				{
					this.Container.Files.Add(addFile);
				});

			deletedFile.ToList<Files>().ForEach(delFile => this.Container.Files.Remove(this.Container.Files.Find(delFile.Id)));

			foreach (Files file in modifiedFile)
			{
				var existingFile = this.Container.Files.Find(file.Id);

				if (existingFile != null)
				{
					var fileEntry = this.Container.Entry(existingFile);
					fileEntry.CurrentValues.SetValues(file);
				}
			}

			this.Container.SaveChanges();
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing">Dispose state.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		#endregion
	}
}
