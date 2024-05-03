using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager
{
    class Program
    {
        static void Main(string[] args)
        {
            //o programa irá fazer o gerenciamento de 15 quartos
            uint[] price = new uint[15]; //vetor para o preço de cada quarto
            string[] qrooms = new string[15]; //vetor para a qualidade do quarto
            bool[] booked = new bool[15]; //vetor para endicar se o quarto está ocupado ou não
            DateTime[,] time = new DateTime[15, 2]; // matriz para endicar quanto tempo um quero vai ficar ocupado
        }
    }
}
