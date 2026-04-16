using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public class Route
    {

        public double Distance { get; private set; }

        public double Duration { get; private set; }


        private List<Location> points = new List<Location>();

        public IReadOnlyList<Location> Points
        {
            get { return points.AsReadOnly(); }
        }

        private Location start;

        public Location Start
        {
            get { return start; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Điểm bắt đầu không được null.");
                start = value;
            }
        }

        private Location end;

        public Location End
        {
            get { return end; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Điểm kết thúc không được null.");

                if (start != null && Location.Equals(start, value))
                    throw new ArgumentException("Start và End không được trùng.");

                end = value;
            }
        }

        public Route(Location start, Location end, double distance, double duration, List<Location> points)
        {
            Start = start;
            End = end;
            SetDistance(distance);
            SetDuration(duration);
            this.points = points ?? new List<Location>();
        }

        private void SetDistance(double d)
        {
            if (d < 0) throw new ArgumentException("Distance không hợp lệ.");
            Distance = d;
        }

        private void SetDuration(double d)
        {
            if (d < 0) throw new ArgumentException("Duration không hợp lệ.");
            Duration = d;
        }
    }
}
