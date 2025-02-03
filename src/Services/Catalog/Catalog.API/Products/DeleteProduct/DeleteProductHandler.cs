namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id): ICommand<DeleteProductResult>;
public record DeleteProductResult(bool isSuccess);

internal sealed class DeleteProductHandler
    (IDocumentSession session, ILogger<DeleteProductHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductResult.Handle called with {@command}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if(product is null)
        {
            throw new ProductNotFoundException();
        }

        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
