namespace Models.Requests
{
    public sealed record CreateSaleRequest(Guid UserId, string Title, string Description, decimal Price);
     
}
