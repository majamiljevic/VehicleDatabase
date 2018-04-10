namespace VehicleDatabase.Repository.Common
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}