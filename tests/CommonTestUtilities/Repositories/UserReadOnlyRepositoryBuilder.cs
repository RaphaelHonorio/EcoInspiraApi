using Moq;
using EcoInspira.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private readonly Mock<IUserReadOnlyRepository> _repositoty;

        public UserReadOnlyRepositoryBuilder() => _repositoty = new Mock<IUserReadOnlyRepository>();

        public void ExistActiveUserWithEmail(string email)
        {
            _repositoty.Setup(repostory => repostory.ExistActiveUserWithEmail(email)).ReturnsAsync(true);

        }

        public IUserReadOnlyRepository Build() => _repositoty.Object;
    }
}
