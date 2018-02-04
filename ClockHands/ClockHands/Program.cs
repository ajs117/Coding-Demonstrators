using System;

namespace ClockHands
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Please enter a 24 hour time i.e. 12:34 or 06:30");

            var time = Console.ReadLine();
            decimal hour = int.Parse(time.Substring(0, 2));
            decimal minute = int.Parse(time.Substring(3, 2));

            decimal fullCircle = 360;
            decimal minutesInHour = 60;
            decimal hoursInClock = 12;

            if (hour >= hoursInClock)
            {
                hour = hour - hoursInClock;
            }

            var angleOfMinute = minute * (fullCircle / minutesInHour);

            var angleOfHour = (hour * (fullCircle / hoursInClock)) + (angleOfMinute / hoursInClock);

            var angleDifference = Math.Abs(angleOfHour - angleOfMinute);

            if (angleDifference >= 180)
            {
                angleDifference = fullCircle - angleDifference;
            }

            Console.WriteLine("There is " + angleDifference + " degrees between the hour and minute hands.");

            Console.ReadLine();
        }
    }
}