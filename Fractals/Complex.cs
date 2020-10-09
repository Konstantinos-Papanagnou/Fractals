using System;

namespace Fractals
{
    class Complex
    {
        double a;
        double b;
        public Complex(double a,double b)
        {
            this.a = a;
            this.b = b;
        }
        public void Square()
        {
            double temp = (a * a) - (b * b);
            b = 2.0 * a * b;
            a = temp;
        }
        public double Magnitude()
        {
            return Math.Sqrt((a * a) + (b * b));
        }
        public void Add(Complex c)
        {
            a += c.a;
            b += c.b;
        }
    }
}
