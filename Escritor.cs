using System.Threading;

namespace buffercircular
{
    public class Escritor
    {
        private char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private Buffer buffer;
        private Thread thread;
        private int media;
        private int n;
        private int sigma;
        public Escritor(Buffer buffer, int n, int media, int sigma)
        {
            this.buffer = buffer;
            this.n = n;
            this.media = media;
            this.sigma = sigma;
        }

        public void Empieza()
        {
            this.thread = new Thread(() => this.Escribir());
            this.thread.Start();
        }

        public void Termina()
        {
            thread.Join();
        }

        private void Escribir()
        {
            char c;
            NormalDist normalDist = new NormalDist(media, sigma);
            for (int i = 0; i < this.n; i++)
            {
                Thread.Sleep((int)normalDist.GetValue());
                c = letters[normalDist.Random(letters.Length)];
                buffer.Escribir(c);
            }
        }
    }
}