namespace VehicleDatabase.Service.Infrastructure
{
    public interface IPaging
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}