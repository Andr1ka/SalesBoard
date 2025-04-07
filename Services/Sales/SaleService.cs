using Domain;
using Domain.Exceptions;
using LanguageExt.Common;
using Persistanse;
namespace Services.Sales
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUserRepository _userRepository;

        public SaleService(ISaleRepository saleRepository, IUserRepository userRepository)
        {
            _saleRepository = saleRepository;
            _userRepository = userRepository;
        }


        public async Task<Result<Sale>> CreateAsync(Guid userId, string title, string description, decimal price, CancellationToken cancellationToken)
        {
            ///refactor this
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user == null)
            {
                return new Result<Sale>(new InvalidFieldValue(nameof(userId)));
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                return new Result<Sale>(new FieldIsRequiredException(nameof(title)));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return new Result<Sale>(new FieldIsRequiredException(nameof(description)));
            }

            if (price <= 0)
            {
                return new Result<Sale>(new InvalidFieldValue(nameof(price)));
            }

            var sale = new Sale()
            {
                UserId = userId,
                Title = title,
                Description = description,
                Price = price,
            };
            var result = await _saleRepository.CreateAsync(sale, cancellationToken);
            return new Result<Sale>(result);
        }


        public Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _saleRepository.GetAllAsync(cancellationToken);
        }
    }
}
