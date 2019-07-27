namespace Cosmos.Dapper.Actions
{
    public interface IHasRootActionBank
    {
        SQLActionSetBase RootActionBank { get; set; }
    }
}