using System;

namespace buffercircular
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Simula(4, 20, 300, 100, 500, 150);
            p.Simula(4, 20, 500, 150, 300, 100);
        }

        public void Simula(int t, int n, int m_w, int s_w, int m_r, int s_r)
        {
            Console.WriteLine("INICIO");
            Buffer buffer = new Buffer(t);
            Escritor escritor = new Escritor(buffer, n, m_w, s_w);
            Lector lector = new Lector(buffer, n, m_r, s_r);
            escritor.Empieza();
            lector.Empieza();
            escritor.Termina();
            lector.Termina();
            Console.WriteLine("FIN");
        }
    }
}
