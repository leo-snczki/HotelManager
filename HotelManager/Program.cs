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
            var qualities = new List<string> { "Economico", "Standard", "Turista", "Luxo" }; //Lista das qualidades disponiveis

            int[] price = new int[15]; //vetor para o preço de cada quarto
            string[] qrooms = new string[15]; //vetor para a qualidade do quarto
            bool[] booked = new bool[15]; //vetor para endicar se o quarto está ocupado ou não
            DateTime[,] time = new DateTime[15, 2]; // matriz para endicar quanto tempo um quero vai ficar ocupado

            SetRooms(ref price, ref qrooms, qualities);
        }
        static void SetRooms(ref int[] price, ref string[] qrooms, List<string> qualities)
        {
            for (int i = 0; i < price.Length; i++)
            {
                Console.Write("Qual vai ser o preço do quarto número {0}", i + 1);
                do
                {
                    price[i] = Convert.ToInt32(Console.ReadLine());
                    if (price[i] <= 0) Console.WriteLine("O preço não pode ser menor ou igual que zero");
                } while (price[i] > 0);
                Console.WriteLine("Qual a qualidade do quarto {0}? \nQualidades disponiveis: 'Economico', 'Standard', 'Turista' e 'Luxo'");
                do
                {
                    qrooms[i] = Console.ReadLine();
                    if (!qualities.Contains(qrooms[i], StringComparer.OrdinalIgnoreCase)) Console.WriteLine("Valor errado, tente novamente");
                } while (!qualities.Contains(qrooms[i], StringComparer.OrdinalIgnoreCase));
            }
        }
        static void BookRoom(ref bool[] booked, ref DateTime[,] time)
        {
            int i;
            string check = "N";
            Console.WriteLine("Qual o quarto que quer marcar? \nOs quartos vão de 1 a 15");
            do
            {
                i = Convert.ToInt32(Console.ReadLine()) - 1;
                if (i < 0 || i > 14) Console.WriteLine("Quarto inválida, tente novamente");
                if (booked[i]) Console.WriteLine("Este quarto já foi marcado, tente outro quarto");
            } while ((i < 0 || i > 14) || booked[i]);
            time[i, 0] = new DateTime().Date;
            Console.WriteLine("Diga quantos dias quer reservar este quarto");
            do
            {
                i = Convert.ToInt32(Console.ReadLine());
                if (i <= 0) Console.WriteLine("Quantidade de dias invalida, tente novamente");
                else 
                { 
                Console.WriteLine("Queres mesmo reservar este quarto de {0} até {1}? \nEscreva 'S' para sim ou 'N' para não", time[i, 0].ToString("dd/mm/yyyy"), time[i, 0].AddDays((double)i).ToString("dd/mm/yyyy"));
                check = Console.ReadLine();
                }
            } while (i <= 0 && check != "S");
        }
        static void ChangePrice(ref int[] price)
        {
            int i;
            int nPrice;
            Console.WriteLine("Que quarto que quer editar?");
            do
            {
                i = Convert.ToInt32(Console.ReadLine()) - 1;
                if (i < 0 || i > 14) Console.WriteLine("Quarto invalido, tente novamente");
            } while (i < 0 || i > 14);
            Console.WriteLine("Qual o novo preço para o quarto {0}?", i);
            do
            {
                nPrice = Convert.ToInt32(Console.ReadLine());
                if (i <= 0 || price[i] == nPrice) Console.WriteLine("Novo preço não pode ser igual ao preço antigo, ser negativo ou ser zero, tente novamente");
            } while (i <= 0 || price[i] == nPrice);
            price[i] = nPrice;
        }
        static void ChangeQuality(ref string[] qrooms, List<string> qualities)
        {
            int i;
            string nQuality;
            Console.WriteLine("Que quarto que quer editar?");
            do
            {
                i = Convert.ToInt32(Console.ReadLine()) - 1;
                if (i < 0 || i > 14) Console.WriteLine("Quarto invalido, tente novamente");
            } while (i < 0 || i > 14);
            Console.WriteLine("Qual a nova qualidade? \nQualidades disponiveis: 'Economico', 'Standard', 'Turista' e 'Luxo'");
            do
            {
                nQuality = Console.ReadLine();
                if (!qualities.Contains(nQuality, StringComparer.OrdinalIgnoreCase) || qrooms[i] == nQuality) Console.WriteLine("Qualidade é inválida ou é igual a anterior, tente novamente");
            } while (!qualities.Contains(nQuality, StringComparer.OrdinalIgnoreCase) || qrooms[i] == nQuality);
            qrooms[i] = nQuality;
        }
    }
}
