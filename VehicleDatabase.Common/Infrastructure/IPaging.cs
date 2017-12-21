namespace VehicleDatabase.Common.Infrastructure
{
    public interface IPaging
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}