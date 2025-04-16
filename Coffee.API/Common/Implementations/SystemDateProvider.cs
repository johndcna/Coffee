using Coffee.API.Common.Interfaces;

namespace Coffee.API.Common.Implementations
{
    public class SystemDateProvider : IDateProvider
    {
        public DateTime DateNow => DateTime.Now;
    }
}
