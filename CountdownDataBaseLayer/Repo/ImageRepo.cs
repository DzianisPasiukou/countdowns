namespace CountdownDataBaseLayer.Repo
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using CountdownDataBaseLayer.Comparer;

	/// <summary>
	/// The instance for image repository.
	/// </summary>
	public class ImageRepo : MyDbContainer<Images>, IDisposable
	{
		#region Public Methods

		/// <summary>
		/// Gets all images.
		/// </summary>
		/// <returns>
		/// IEnumerable of images.
		/// </returns>
		public override IEnumerable<Images> GetAll()
		{
			IEnumerable<Images> images = this.Container.Images;
			return images;
		}

		/// <summary>
		/// Gets the by identifier image.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		/// The image.
		/// </returns>
		public override Images GetById(int id)
		{
			Images image = this.Container.Images.Find(id);
			return image;
		}

		/// <summary>
		/// Updates the many images.
		/// </summary>
		/// <param name="existingEntities">The existing images.</param>
		/// <param name="updatedEntities">The updated images.</param>
		public override void UpdateMany(IEnumerable<Images> existingEntities, IEnumerable<Images> updatedEntities)
		{
			var addedImages = updatedEntities.Except(existingEntities, new CompareImages());
			var deletedImages = existingEntities.Except(updatedEntities, new CompareImages());
			var modifiedImages = updatedEntities.Except(addedImages, new CompareImages());

			addedImages.ToList<Images>().ForEach(addImage =>
			{
				this.Container.Images.Add(addImage);
			});

			deletedImages.ToList<Images>().ForEach(delImage => this.Container.Images.Remove(this.Container.Images.Find(delImage.Id)));

			foreach (Images image in modifiedImages)
			{
				var existingImage = this.Container.Images.Find(image.Id);

				if (existingImage != null)
				{
					var imageEntry = this.Container.Entry(existingImage);
					imageEntry.CurrentValues.SetValues(image);
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
