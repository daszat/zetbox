
namespace Kistl.App.Base
{

    /// <summary>
    /// Describes the multiplicities of objects on RelationEnds
    /// </summary>
    public enum Multiplicity
    {
		/// <summary>
		/// Required Element (exactly one)
		/// </summary>
		One = 2,

		/// <summary>
		/// Optional Element (zero or one)
		/// </summary>
		ZeroOrOne = 1,

		/// <summary>
		/// Optional List Element (zero or more)
		/// </summary>
		ZeroOrMore = 3,

	}
}