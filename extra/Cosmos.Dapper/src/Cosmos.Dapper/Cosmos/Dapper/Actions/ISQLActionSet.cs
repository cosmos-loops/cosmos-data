namespace Cosmos.Dapper.Actions
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLActionSet
    {
        ActionCallingMode CallingMode { get; }
        DapperOptions Options { get; }
    }
}