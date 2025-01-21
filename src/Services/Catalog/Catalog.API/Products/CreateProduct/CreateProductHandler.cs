namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price): ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal sealed class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // 1. Create product entity from command object
        // 2. Save to database
        // 3. Return CreateProductResult result

        // 1. Create product entity from command object

        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // 2. Save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // 3. Return CreateProductResult result
        return new CreateProductResult(product.Id);

    }
}
