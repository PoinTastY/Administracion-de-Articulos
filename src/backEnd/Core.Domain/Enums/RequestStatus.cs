namespace Core.Domain.Enums
{
    public enum RequestStatus
    {
        PreAproval = 1,
        Submitted = 2,
        Reviewing = 3,
        Approved = 9,
        Declined = 10,
    }

    public static class RequestStatusExtension
    {
        public static IEnumerable<RequestStatus> GetAll()
        {
            return Enum.GetValues(typeof(RequestStatus)).Cast<RequestStatus>();
        }

        public static string GetName(this RequestStatus status)
        {
            return status.ToString();
        }
    }
}
