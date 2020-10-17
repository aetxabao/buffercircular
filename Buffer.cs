using System;
using System.Text;
using System.Threading;

namespace buffercircular
{
    public class Buffer
    {
        private readonly object bloqueo = new object();
        private int tamano;
        private int ocupado;
        private int p_r;
        private int p_w;
        private char[] data;

        public Buffer(int tamano)
        {
            this.tamano = tamano;
            this.ocupado = 0;
            this.p_r = 0;
            this.p_w = 0;
            this.data = new char[tamano];
            for (int i = 0; i < tamano; i++)
            {
                this.data[i] = '_';
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < tamano; i++)
            {
                if ((i == p_r) && (i == p_w))
                {
                    if (ocupado == tamano)
                    {
                        sb.Append('\'');//lleno
                    }
                    else
                    {
                        sb.Append('.');//vacio
                    }
                }
                else if (i == p_r)
                {
                    sb.Append('¡');//puntero lectura
                }
                else if (i == p_w)
                {
                    sb.Append('!');//puntero escritura
                }
                else
                {
                    sb.Append('|');
                }
                sb.Append(data[i]);
            }
            sb.Append('|');
            return sb.ToString();
        }

        public void Escribir(char c)
        {
            lock (bloqueo)
            {
                while (true)
                {
                    if (ocupado < tamano)
                    {
                        data[p_w] = c;
                        ocupado++;
                        p_w = (p_w + 1) % tamano;
                        Console.WriteLine("-- {0} -->\t{1}", c, this);
                        Monitor.Pulse(bloqueo);
                        break;
                    }
                    else
                    {
                        Console.WriteLine(".. {0} ..|\t{1}", c, this);
                        Monitor.Wait(bloqueo);
                    }
                }
            }
        }
        public char Leer()
        {
            char c;
            lock (bloqueo)
            {
                while (true)
                {
                    if (ocupado > 0)
                    {
                        c = data[p_r];
                        ocupado--;
                        p_r = (p_r + 1) % tamano;
                        Console.WriteLine("        \t{0}\t----> {1}", this, c);
                        Monitor.Pulse(bloqueo);
                        return c;
                    }
                    else
                    {
                        Console.WriteLine("        \t{0}\t.......", this);
                        Monitor.Wait(bloqueo);
                    }
                }
            }
        }
    }
}