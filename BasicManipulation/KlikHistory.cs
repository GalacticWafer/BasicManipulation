using System;

namespace BasicManipulation
{
    internal class KlikHistory
    {
        double X { get; set; }
        double Y {get; set;}
        
        public KlikHistory(double x, double y)
        {
            X = x;
            Y = Y;
        }

        public string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }
    }
}
    