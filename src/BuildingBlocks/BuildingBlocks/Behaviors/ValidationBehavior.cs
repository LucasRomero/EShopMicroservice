using BuildingBlocks.CQRS;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BuildingBlocks.Behaviors;
public class ValidationBehavior<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // creamos el contexto de validacion de lo cual obtiene del request
        var context = new ValidationContext<TRequest>(request);

        // vamos a correr los validadores y los errores lo devolvemos, ponemos un Task.WhenAll, ya que recorrer la lista
        // y son todos asincronos, por lo que se ejecutan en paralelo y cuando termine devuelve los errores.
        ValidationResult[] validationResults =
            await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        // obtenemos los errores de la lista de validaciones
        var failures =
            validationResults
            .Where(r => !r.IsValid)
            .SelectMany(r => r.Errors)
            .ToList();

        if(failures.Any())
            throw new ValidationException(failures);

        return await next();
    }
}
