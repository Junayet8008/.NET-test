using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkdayCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IWorkdayCalculator calendar = new WorkdayCalculator();
            calendar.SetWorkdayStartAndStop(8, 0, 16, 0);
            calendar.SetRecurringHoliday(5, 17);
            calendar.SetHoliday(new DateTime(2004, 5, 27));

            string format = "dd-MM-yyyy HH:mm";

            // Test Case 1
            var start1 = new DateTime(2004, 5, 24, 19, 3, 0);
            decimal increment1 = 44.723656m;
            var incrementedDate1 = calendar.GetWorkdayIncrement(start1, increment1);
            Console.WriteLine(
                start1.ToString(format) +
                " with an addition of " +
                increment1 +
                " work days is " +
                incrementedDate1.ToString(format));

            // Test Case 2
            var start2 = new DateTime(2004, 5, 24, 18, 3, 0);
            decimal increment2 = -6.7470217m;
            var incrementedDate2 = calendar.GetWorkdayIncrement(start2, increment2);
            Console.WriteLine(
                start2.ToString(format) +
                " with an addition of " +
                increment2 +
                " work days is " +
                incrementedDate2.ToString(format));

            // Test Case 3
            var start3 = new DateTime(2004, 5, 24, 8, 3, 0);
            decimal increment3 = 12.782709m;
            var incrementedDate3 = calendar.GetWorkdayIncrement(start3, increment3);
            Console.WriteLine(
                start3.ToString(format) +
                " with an addition of " +
                increment3 +
                " work days is " +
                incrementedDate3.ToString(format));

            // Test Case 4
            var start4 = new DateTime(2004, 5, 24, 7, 3, 0);
            decimal increment4 = 8.276628m;
            var incrementedDate4 = calendar.GetWorkdayIncrement(start4, increment4);
            Console.WriteLine(
                start4.ToString(format) +
                " with an addition of " +
                increment4 +
                " work days is " +
                incrementedDate4.ToString(format));
            // Test Case 4
            var start5 = new DateTime(2004, 5, 24, 18, 5, 0);
            decimal increment = -5.5m;
            var incrementedDate = calendar.GetWorkdayIncrement(start5, increment);
            Console.WriteLine(
            start5.ToString(format) +
            " with an addition of " +
            increment +
            " work days is " +
            incrementedDate.ToString(format));
        }
    }
}
