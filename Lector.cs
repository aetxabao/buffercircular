using System;
using System.Text;
using System.Threading;

namespace buffercircular
{
    public class Lector
    {
        private StringBuilder sb;
        private Buffer buffer;
        private Thread thread;
        private int media;
        private int n;
        private int sigma;
        public Lector(Buffer buffer, int n, int media, int sigma)
        {
            this.sb = new StringBuilder();
            this.buffer = buffer;
            this.n = n;
            this.media = media;
            this.sigma = sigma;
        }

        public void Empieza()
        {
            this.thread = new Thread(() => this.Leer());
            this.thread.Start();
        }

        public void Termina()
        {
            thread.Join();
        }

        private void Leer()
        {
            char c;
            NormalDist normalDist = new NormalDist(media, sigma);
            for (int i = 0; i < this.n; i++)
            {
                Thread.Sleep((int)normalDist.GetValue());
                c = buffer.Leer();
                sb.Append(c);
            }
            Console.WriteLine(sb.ToString());
        }
    }
}