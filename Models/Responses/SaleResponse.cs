namespace Models.Responses
{
    public sealed record SaleResponse(Guid UserId, string Title, string Description, decimal Price, DateTime ModifiedOn);

}
