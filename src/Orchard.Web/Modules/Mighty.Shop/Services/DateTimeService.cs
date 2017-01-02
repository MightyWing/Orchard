using Mighty.Shop.Services;
using System;

namespace Mighty.Shop.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now
        {
            get { return DateTime.UtcNow; }
        }
    }
}