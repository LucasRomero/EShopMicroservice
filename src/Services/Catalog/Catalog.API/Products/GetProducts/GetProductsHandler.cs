﻿
namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(): IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal sealed class GetProductsQueryHandler
    (IDocumentSession session, ILogger<GetProductsQueryHandler> logger) 
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {

        logger.LogInformation("GetProductsQueryHandler.Handle called with @{Query}", query);

        var product = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductsResult(product);

    }
}
