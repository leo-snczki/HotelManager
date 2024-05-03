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
            int[] price = new int[15]; //vetor para o preço de cada quarto
            string[] qrooms = new string[15]; //vetor para a qualidade do quarto
            bool[] booked = new bool[15]; //vetor para endicar se o quarto está ocupado ou não
            DateTime[,] time = new DateTime[15, 2]; // matriz para endicar quanto tempo um quero vai ficar ocupado

            Prices(ref price);
        }
        static void Prices(ref int[] arr)
        {
            for(int i = 0; i < arr.Length; i++)
            {
                Console.Write("Qual vai ser o preço do quarto número {0}", i + 1);
                do
                {
                    arr[i] = Convert.ToInt32(Console.ReadLine());
                    if(arr[i] <= 0) Console.WriteLine("O preço não pode ser menor ou igual que zero");
                } while (arr[i] > 0);
            }
        }
    }
}
