namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSucces);

public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketValidator()
    {
        RuleFor(x => x.UserName).NotNull().WithMessage("Username is required");
    }
}

public class DeleteBasketHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(
        DeleteBasketCommand command,
        CancellationToken cancellationToken
    )
    {
        //TODO: Find a basket in the database and delete it
        // Session.Delete<ShoppingCart>(command.UserName);

        return new DeleteBasketResult(true);
    }
}
