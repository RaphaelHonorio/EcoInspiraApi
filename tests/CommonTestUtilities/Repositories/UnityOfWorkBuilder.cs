using EcoInspira.Domain.Repositories;
using EcoInspira.Infrastructure.DataAccess;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UnityOfWorkBuilder
    {
        public static IUnitOfWork Build()
        {
            var mock = new Mock<UnityOfWork>();

            return mock.Object;
        }
    }
}