using Microsoft.Extensions.Logging;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price): ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

internal sealed class CreateProductCommandHandler
    (IDocumentSession session, ILogger<CreateProductCommandHandler> logger) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // 1. Create product entity from command object
        // 2. Save to database
        // 3. Return CreateProductResult result

        // 1. Create product entity from command object

        logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);

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
