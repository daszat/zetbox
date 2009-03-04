
namespace Kistl.App.Base
{

    /// <summary>
    /// Storage Type of a 1:1 Releation.
    /// </summary>
    public enum StorageType
    {
		/// <summary>
		/// The relation information is stored on both sides of the Relation
		/// </summary>
		Replicate = 3,

		/// <summary>
		/// Deprecated alias for MergeIntoB
		/// </summary>
		Right = 2,

		/// <summary>
		/// Deprecated alias for MergeIntoA
		/// </summary>
		Left = 1,

		/// <summary>
		/// The relation information is stored with the A-side ObjectClass
		/// </summary>
		MergeIntoA = 1,

		/// <summary>
		/// The relation information is stored with the B-side ObjectClass
		/// </summary>
		MergeIntoB = 2,

		/// <summary>
		/// The relation information is stored in a separate entity
		/// </summary>
		Separate = 4,

	}
}