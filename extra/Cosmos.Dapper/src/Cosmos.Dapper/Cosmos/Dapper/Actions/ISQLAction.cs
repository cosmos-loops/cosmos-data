namespace Cosmos.Dapper.Actions
{
    // ReSharper disable once InconsistentNaming
    public interface ISQLAction
    {
        string Dialect { get; }

        ActionKind Kind { get; }
        
        ActionCallingMode CallingMode { get; }

        DapperOptions Options { get; }

        bool IsExecuted { get; set; }
    }
}