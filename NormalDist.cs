using System;

namespace buffercircular
{
    public class NormalDist
    {
        private Random rand;
        public double mean;
        public double stdDev;
        public NormalDist(double mean, double stdDev)
        {
            this.mean = mean;
            this.stdDev = stdDev;
            this.rand = new Random();
        }
        public double Random()
        {
            return this.rand.NextDouble();
        }
        public int Random(int n)
        {
            return (int)(n * this.rand.NextDouble());
        }
        public double GetValue()
        {
            //https://stackoverflow.com/questions/218060/random-gaussian-variables
            double u1 = 1.0 - this.rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - this.rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + stdDev * randStdNormal;
            return randNormal;
        }
    }
}