using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementSystem.Application.Behaviors.Validations
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationPipelineBehavior<TRequest, TResponse>> _logger;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationPipelineBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Short-circuit if no validators
            if (!_validators.Any())
            {
                return await next();
            }

            // Create the validation context only if there are validators
            var context = new ValidationContext<TRequest>(request);

            // Run validations in parallel and collect results
            var validationTasks = _validators.Select(v => v.ValidateAsync(context, cancellationToken));
            var validationResults = await Task.WhenAll(validationTasks);

            // Filter the errors lazily to avoid immediate collection
            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(error => error != null);

            if (failures.Any())
            {
                // Use a string builder for efficient concatenation of error messages
                var errorMessages = string.Join(";", failures.Select(error => $"{error.PropertyName}: {error.ErrorMessage}"));

                // Log the validation errors
                _logger.LogError("Validation errors occurred: {Errors}", errorMessages);

                throw new ValidationException(string.Join(";", errorMessages));
            }

            return await next();
        }
    }
}