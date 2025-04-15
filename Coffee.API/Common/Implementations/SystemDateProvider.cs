using Coffee.API.Common.Interfaces;

namespace Coffee.API.Common.Implementations
{
    public class SystemDateProvider : IDateProvider
    {
        public DateTime Today => DateTime.Today;
    }
}
