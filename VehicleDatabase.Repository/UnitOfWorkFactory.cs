using VehicleDatabase.Repository.Common;

namespace VehicleDatabase.Repository
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private IUnitOfWork Uow { get; set; }

        public UnitOfWorkFactory(IUnitOfWork uow)
        {
            Uow = uow;
        }

        public IUnitOfWork Create()
        {
            return Uow;
        }
    }
}
