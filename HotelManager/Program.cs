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
            //o programa irá fazer o gerenciamento de 15 quartos por padrão
            var qualities = new List<string> { "Economico", "Standard", "Turista", "Luxo" }; //Lista das qualidades disponiveis.
            int[] price = new int[15] {100,110,120,130,140,150,160,170,180,190,200,210,220,230,240 }; //vetor para o preço de cada quarto.
            string[] qrooms = new string[15] { 'Turista', 'Economico', 'Luxo', 'Luxo', 'Turista', 'Luxo', 'Turista', 'Economico', 'Luxo', 'Standard', 'Luxo', 'Luxo', 'Luxo', 'Luxo', 'Economico' }; //vetor para a qualidade do quarto.
            bool[] booked = new bool[15]; //vetor para endicar se o quarto está ocupado ou não.
            DateTime[,] time = new DateTime[15, 2]; // matriz para indicar quanto tempo um quero vai ficar ocupado.
            string op;

            SetRooms(ref price, ref qrooms, qualities);

            Console.WriteLine("Insira a opção desejada: ");
            op = Console.ReadLine();

            switch (op)
            {
                case "1":
                    // Mostrar todos os quartos
                    ShowRooms(qrooms, booked);
                    break;
                case "2":
                    // Ocupar quarto
                    BookRoom(ref booked, ref time);
                    break;
                case "3":
                    // Desocupa o quarto
                    /*UnbookRoom(ref booked, ref time);*/
                    break;
                case "4":
                    // Encontrar quarto por menor preço ou outras qualidades
                    FindRoom(price, qrooms, booked);
                    break;
                case "5":
                    // Editar quarto
                    ChangePrice(ref price);
                    break;
                /*case "6":
                    // Adicionar quarto
                    AddRoom(qualities, ref price, ref qrooms);
                    break;*/
                default:
                    Console.WriteLine("Opção inválida, digite novamente.");
                    break;
            }
            Console.ReadLine();
        }

        
        static void SetRooms(ref int[] price, ref string[] qrooms, List<string> qualities)
        {
            for (int i = 0; i < price.Length; i++)
            {
                Console.Write("Qual vai ser o preço do quarto número {0}: ", i + 1);
                do
                {
                    price[i] = Convert.ToInt32(Console.ReadLine());
                    if (price[i] <= 0) Console.WriteLine("O preço não pode ser menor ou igual que zero");
                } while (price[i] <= 0);
                Console.WriteLine("Qual a qualidade do quarto {0}? \nQualidades disponiveis: 'Economico', 'Standard', 'Turista' e 'Luxo'", i + 1);
                do
                {
                    qrooms[i] = Console.ReadLine();
                    if (!qualities.Contains(qrooms[i], StringComparer.OrdinalIgnoreCase)) Console.WriteLine("Valor errado, tente novamente");
                } while (!qualities.Contains(qrooms[i], StringComparer.OrdinalIgnoreCase));
                Console.Clear();
            }
        }
        static void BookRoom(ref bool[] booked, ref DateTime[,] time)
        {
            int i, i2;
            string check = "N";
            Console.WriteLine("Qual o quarto que quer marcar? \nOs quartos vão de 1 a 15");
            do
            {
                i = Convert.ToInt32(Console.ReadLine()) - 1;
                if (i < 0 || i > 14) Console.WriteLine("Quarto inválida, tente novamente");
                if (booked[i]) Console.WriteLine("Este quarto já foi marcado, tente outro quarto");
            } while ((i < 0 || i > 14) || booked[i]);
            time[i, 0] = DateTime.Now.Date;
            Console.WriteLine("Diga quantos dias quer reservar este quarto");
            do
            {
                i2 = Convert.ToInt32(Console.ReadLine());
                if (i2 <= 0) Console.WriteLine("Quantidade de dias invalida, tente novamente");
                else 
                { 
                Console.WriteLine("Queres mesmo reservar este quarto de {0} até {1}? \nEscreva 'S' para sim ou 'N' para não", time[i, 0].ToString("dd/mm/yyyy"), time[i, 0].AddDays((double)i2).ToString("dd/mm/yyyy"));
                check = Console.ReadLine();
                }
            } while (i2 <= 0 || check != "S");
            booked[i] = true;
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
        static void FindRoom(int[] price, string[] qrooms, bool[] booked)
        {
            int[] frooms = booked.Select((b, i) => !b ? i : -1).Where(i => i != -1).ToArray();
            int[] frprice = new int[frooms.Length];
            string[] fqrooms = new string[frooms.Length];
            for(int i = 0; i < frooms.Length; i++)
            {
                frprice[i] = price[frooms[i]];
                fqrooms[i] = qrooms[frooms[i]];
            }

            int i2;
            string input;
            Console.WriteLine("Escolha uma das opções. \n1 - Descobrir qual o quarto mais barato ou o mais caro disponivel \n2 - todos os quartos de uma certa qualidade disponiveis");
            i2 = Convert.ToInt32(Console.ReadLine());
            switch (i2)
            {
                case 1:
                    Console.WriteLine("Qual o tipo de preço que quer descobrir? \nUse 'barato' para descobrir o quarto mais barato disponivel ou"); // Escreve a mensagem a dizer qual é o preço a pesquisar
                    do {
                        input = Console.ReadLine();
                        if (input == "barato") Console.WriteLine("O número do quarto mais barato é {0}, ele custa {1}", frooms[Array.IndexOf(frprice, frprice.Min())] + 1, frprice.Min());
                        else if (input == "caro") Console.WriteLine("O número do quarto mais caro é {0}, ele custa {1}", frooms[Array.IndexOf(frprice, frprice.Max())] + 1, frprice.Max());
                        else Console.WriteLine("Opção invalida, tente novamente");
                    } while (input != "barato" || input != "caro");
                    break;
                case 2:
                    input = Console.ReadLine();
                    Console.WriteLine("Quartos {0}, qualidade {1}", fqrooms.Select((b, i) => b == input ? frooms[i] + 1 : -1).Where(i => i != -1).ToString(), input);
                    break;
                default:
                    break;
            }
        }
        static void ShowRooms(string[] qrooms, bool[] booked)
        {
            for (int i = 0; i < qrooms.Length; i++)
            {
                Console.WriteLine($"Quarto {i + 1}: {qrooms[i]}, Estado: {(booked[i] ? "Ocupado" : "Livre")}");
            }
        }
    }
}
