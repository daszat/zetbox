namespace NMock2.Syntax
{
    public interface ISetIndexerSyntax
    {
        IValueSyntax this[params object[] args] { get; }
    }
}
