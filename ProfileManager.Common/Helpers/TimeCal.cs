using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManager.Common.Helpers
{
    public static class TimeCal
    {
        public static string AsTimeAgo(this DateTime dateTime)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(dateTime);

            return timeSpan.TotalSeconds switch
            {
                <= 60 => $"{timeSpan.Seconds} Sec ago",

                _ => timeSpan.TotalMinutes switch
                {
                    <= 1 => "Min ago",
                    < 60 => $"{timeSpan.Minutes} Min ago",
                    _ => timeSpan.TotalHours switch
                    {
                        <= 1 => "Hr ago",
                        < 24 => $"{timeSpan.Hours} Hrs ago",
                        _ => timeSpan.TotalDays switch
                        {
                            <= 1 => "Yesterday",
                            <= 30 => $"{timeSpan.Days} Days ago",

                            <= 60 => "Month ago",
                            < 365 => $"{timeSpan.Days / 30} Months ago",

                            <= 365 * 2 => "Yr ago",
                            _ => $"{timeSpan.Days / 365} Yrs ago"
                        }
                    }
                }
            };
        }

        

    }

}
