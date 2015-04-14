namespace CountdownDataBaseLayer.Repo
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using CountdownDataBaseLayer.Comparer;

	/// <summary>
	/// Repository for exercises.
	/// </summary>
	public class ExercisesRepo : MyDbContainer<Exercises>, IDisposable
	{
		#region Public Methods

		/// <summary>
		/// Gets all exercises.
		/// </summary>
		/// <returns>
		/// IEnumerable of exercises.
		/// </returns>
		public override IEnumerable<Exercises> GetAll()
		{
			IEnumerable<Exercises> exercises = this.Container.Exercises;
			return exercises;
		}

		/// <summary>
		/// Gets the by identifier exercises.
		/// </summary>
		/// <param name="id">The identifier of exercise.</param>
		/// <returns>
		/// Entity of exercises
		/// </returns>
		public override Exercises GetById(int id)
		{
			Exercises exercise = this.Container.Exercises.Find(id);
			return exercise;
		}
		
		/// <summary>
		/// Updates the many exercises.
		/// </summary>
		/// <param name="existingEntities">The existing exercises.</param>
		/// <param name="updatedEntities">The updated exercises.</param>
		public override void UpdateMany(IEnumerable<Exercises> existingEntities, IEnumerable<Exercises> updatedEntities)
		{
			var addedExercise = updatedEntities.Except(existingEntities, new CompareExercise());
			var deletedExercise = existingEntities.Except(updatedEntities, new CompareExercise());
			var modifiedExercise = updatedEntities.Except(addedExercise, new CompareExercise());

			addedExercise.ToList<Exercises>().ForEach(addExercise => 
				{
					this.Container.Exercises.Add(addExercise);
				});

			deletedExercise.ToList<Exercises>().ForEach(delExercise => this.Container.Exercises.Remove(this.Container.Exercises.Find(delExercise.Id)));

			foreach (Exercises exercise in modifiedExercise)
			{
				var existingExercise = this.Container.Exercises.Find(exercise.Id);

				if (existingExercise != null)
				{
					var exerciseEntry = this.Container.Entry(existingExercise);
					exerciseEntry.CurrentValues.SetValues(exercise);
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
