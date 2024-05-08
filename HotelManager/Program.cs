using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManager
{
    class Program
    {

        static string input;

        static float[] price = new float[15] { 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200, 210, 220, 230, 240 }; //vetor para o preço de cada quarto.

        static void Main(string[] args)
        {
            //o programa irá fazer o gerenciamento de 15 quartos por padrão
            var qualities = new List<string> { "Economico", "Standard", "Turista", "Luxo" }; //Lista das qualidades disponiveis.
            string[] qrooms = new string[15] { "Turista", "Economico", "Luxo", "Luxo", "Turista", "Luxo", "Turista", "Economico", "Luxo", "Standard", "Luxo", "Luxo", "Luxo", "Luxo", "Economico" }; //vetor para a qualidade do quarto.
            bool[] booked = new bool[15]; //vetor para endicar se o quarto está ocupado ou não.
            DateTime[] time = new DateTime[15]; // vetor para indicar quanto tempo um quero vai ficar ocupado.
            string op;

            //SetRooms(ref price, ref qrooms, qualities);
            do
            {
                AutoUnbook(ref booked, ref time);

                ExibirMenu();
                Console.Write("Insira a opção desejada: ");
                op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        // Mostrar todos os quartos
                        ShowRooms(qrooms, booked);
                        break;
                    case "2":
                        // Ocupar quarto
                        BookRoom(ref booked, ref time, qrooms);
                        break;
                    case "3":
                        // Desocupa o quarto
                        UnbookRoom(ref booked, ref time, qrooms);
                        break;
                    case "4":
                        // Encontrar quarto com menor preço, maior preço ou de certas qualidades
                        FindRoom(price, qrooms, booked, qualities);
                        break;
                    case "5":
                        // Editar quarto
                        ChangeRoom(ref price, ref qrooms, qualities, booked);
                        break;
                    case "6":
                        // Adicionar quarto
                        AddRoom(qualities, ref price, ref qrooms, ref time, ref booked);
                        break;
                    case "7":
                        // Remove quarto
                        DelRoom(ref price, ref qrooms, ref time, ref booked);
                        break;
                    case "8":
                        //Sair

                        break;
                    default:
                        Console.WriteLine("Opção inválida, digite novamente.");
                        break;
                }
            } while (op != "8");
            Console.ReadLine();
        }
        static void ExibirMenu()
        {
            Console.WriteLine("\n===== Menu =====");
            Console.WriteLine("1. Listar todos os quartos");
            Console.WriteLine("2. Ocupar quarto");
            Console.WriteLine("3. Desocupar quarto");
            Console.WriteLine("4. Encontrar quarto por menor preço ou outras qualidades");
            Console.WriteLine("5. Editar quarto");
            Console.WriteLine("6. Adicionar quarto");
            Console.WriteLine("7. Remover quarto");
            Console.WriteLine("8. Sair");
            Console.WriteLine("================\n");
        }

        static void AutoUnbook(ref bool[] booked, ref DateTime[] time)
        {
            for (int i = 0; i < booked.Length; i++)
            {
                if (booked[i]) if (DateTime.Compare(time[i], DateTime.Now.Date) <= 0)
                    {
                        booked[i] = false;
                        time = time.Where((val, idx) => idx != i).ToArray();
                    }
            }
        }

        static void UnbookRoom(ref bool[] booked, ref DateTime[] time, string[] qrooms)
        {
            if (Array.IndexOf(booked, true) == -1) Console.WriteLine("Todos os quartos estão livres");
            else
            {
                int i;
                ShowRooms(qrooms, booked);
                Console.WriteLine("Qual o quarto que gostaria de remover?");
                do
                {
                    input = Console.ReadLine();
                    Int32.TryParse(input, out i);
                    i--;
                    if (i < 0 || i > booked.Length - 1) Console.WriteLine("Quarto invalido, tente novamente");
                    else if (!booked[i]) Console.WriteLine("Esse quarto está livre, tente novamente");
                } while ((i < 0 || i > booked.Length - 1) || !booked[i]);
                booked[i] = false;
                time = time.Where((val, idx) => idx != i).ToArray();
            }
        }

        static void AddRoom(List<string> qualities, ref float[] price, ref string[] qrooms, ref DateTime[] time, ref bool[] booked)
        {
            float i;
            Array.Resize(ref price, price.Length + 1);
            Array.Resize(ref qrooms, qrooms.Length + 1);
            Array.Resize(ref time, time.Length + 1);
            Array.Resize(ref booked, booked.Length + 1);
            Console.WriteLine("Qual o preço para o quarto {0}", price.Length);
            do
            {
                input = Console.ReadLine();
                float.TryParse(input, out i);
                if (i <= 0) Console.WriteLine("Valor inválido tente novamente");
            } while (i <= 0);
            price[price.Length - 1] = i;
            ChangeQuality(ref qrooms, qualities, price.Length - 1);
        }

        static void DelRoom(ref float[] price, ref string[] qrooms, ref DateTime[] time, ref bool[] booked)
        {
            if (Array.IndexOf(booked, false) == -1) Console.WriteLine("Todos os quartos estão ocupados");
            else
            {
                int i2;
                Console.Write("Selecione o quarto que quer remover: ");
                do
                {
                    input = Console.ReadLine();
                    Int32.TryParse(input, out i2);
                    if (i2 < 1 || i2 > price.Length) Console.Write("Esse quarto não existe, tente novamente: ");
                    else if(booked[i2 - 1]) Console.WriteLine("Este quarto está ocupado");
                } while (i2 < 1 || i2 > price.Length || booked[i2 - 1]);
                for (int i = i2; i < price.Length; i++)
                {
                    price[i - 1] = price[i];
                    qrooms[i - 1] = qrooms[i];
                   time[i - 1] = time[i];
                    booked[i - 1] = booked[i];
                }
                Array.Resize(ref price, price.Length - 1);
                Array.Resize(ref qrooms, qrooms.Length - 1);
                Array.Resize(ref time, time.Length - 1);
                Array.Resize(ref booked, booked.Length - 1);
            }
        }

        static void SetRooms(ref float[] price, ref string[] qrooms, List<string> qualities)
        {
            for (int i = 0; i < price.Length; i++)
            {
                Console.Write("Qual vai ser o preço do quarto número {0}: ", i + 1);
                do
                {
                    input = Console.ReadLine();
                    float.TryParse(input, out price[i]);
                    if (price[i] <= 0) Console.WriteLine("O preço não pode ser menor ou igual que zero");
                } while (price[i] <= 0);
                ChangeQuality(ref qrooms, qualities, i);
                Console.Clear();
            }
        }

        static void BookRoom(ref bool[] booked, ref DateTime[] time, string[] qrooms)
        {
            if (Array.IndexOf(booked, false) == -1) Console.WriteLine("Todos os quartos estão ocupados");
            else
            {
                int i, i2;
                string check = "N";
                ShowRooms(qrooms, booked);
                Console.WriteLine("Qual o quarto que quer marcar? \nOs quartos vão de 1 a {0}", booked.Length);
                do
                {
                    input = Console.ReadLine();
                    Int32.TryParse(input, out i);
                    if (i < 1 || i > booked.Length ) Console.WriteLine("Quarto inválido, tente novamente");
                    else if (booked[i-1]) Console.WriteLine("Este quarto já foi marcado, tente outro quarto");
                } while (i < 1 || i > booked.Length || booked[i-1]);
                do
                {
                    Console.WriteLine("Diga quantos dias quer reservar este quarto");
                    input = Console.ReadLine();
                    Int32.TryParse(input, out i2);
                    if (i2 <= 0) Console.WriteLine("Quantidade de dias invalida, tente novamente");
                    else
                    {
                        Console.WriteLine("Queres mesmo reservar este quarto de {0} até {1}? \nEscreva '1' para sim ou '2' para não", DateTime.Now.Date.ToString("dd/MM/yyyy"), DateTime.Now.Date.AddDays((double)i2).ToString("dd/MM/yyyy"));
                        check = Console.ReadLine();
                    }
                } while (i2 <= 0 || check != "1");
                booked[i-1] = true;
                time[i-1] = DateTime.Now.Date.AddDays(i2);
            }
        }
        static void ChangeRoom(ref float[] price, ref string[] qrooms, List<string> qualities, bool[] booked)
        {
            int i;
            float nPrice;
            ShowRooms(qrooms, booked);
            Console.WriteLine("Que quarto que quer editar?");
            do
            {
                input = Console.ReadLine();
                Int32.TryParse(input, out i);
                i--;
                if (i < 0 || i > 14) Console.WriteLine("Quarto invalido, tente novamente");
            } while (i < 0 || i > 14);
            Console.WriteLine("Qual o novo preço para o quarto {0}? \nO preço atual é {1}", i + 1, price[i]);
            do
            {
                input = Console.ReadLine();
                float.TryParse(input, out nPrice);
                if (nPrice <= 0 || price[i] == nPrice) Console.WriteLine("Novo preço não pode ser igual ao preço antigo, ser negativo ou ser zero, tente novamente");
            } while (nPrice <= 0 || price[i] == nPrice);
            price[i] = nPrice;
            if (!booked[i])
            {
                Console.Write("Quer editar a qualidade do quarto? \nPrecione '1' para editar: ");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ChangeQuality(ref qrooms, qualities, i);
                        break;
                    default:
                        break;
                }
            }
        }

        static void ChangeQuality(ref string[] qrooms, List<string> qualities, int i)
        {
            string nQuality;
            int i2;
            Console.WriteLine("Qual vai ser a qualidade? \nQualidades disponiveis: '1' para 'Economico', '2' para 'Standard', '3' para 'Turista' e '4' para 'Luxo'");
            do
            {
                nQuality = Console.ReadLine();
                Int32.TryParse(nQuality, out i2);
                if (i2 == 0 || i2 < 1 || i2 > 4) Console.WriteLine("O valor inserido é inválido, tente novamente");
                else if (qrooms[i] == qualities[i2 - 1]) Console.WriteLine("Qualidade é igual a anterior, tente novamente");
            } while (i2 == 0 || i2 < 1 || i2 > 4 || qrooms[i] == qualities[i2 - 1]);
            qrooms[i] = qualities[i2 - 1];
        }

        static void FindRoom(float[] price, string[] qrooms, bool[] booked, List<string> qualities)
        {
            int[] frooms = booked.Select((b, idx) => !b ? idx : -1).Where(idx => idx != -1).ToArray();
            float[] frprice = new float[frooms.Length];
            string[] fqrooms = new string[frooms.Length];
            for (int idx = 0; idx < frooms.Length; idx++)
            {
                frprice[idx] = price[frooms[idx]];
                fqrooms[idx] = qrooms[frooms[idx]];
            }

            string input;
            int i;
            Console.WriteLine("Escolha uma das opções. \n1 - Descobrir qual o quarto mais barato ou o mais caro disponivel \n2 - todos os quartos de uma certa qualidade disponiveis \nQualquer outro valor lhe levará para o menú inicial");
            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine("Qual o tipo de preço que quer descobrir? \nUse '1' para descobrir o quarto mais barato disponivel ou use '2' para descobrir o quarto mais caro disponivel"); // Escreve a mensagem a dizer qual é o preço a pesquisar
                    do
                    {
                        input = Console.ReadLine();
                        if (input == "1") Console.WriteLine("O número do quarto mais barato é {0}, ele custa {1}", frooms[Array.IndexOf(frprice, frprice.Min())] + 1, frprice.Min());
                        else if (input == "2") Console.WriteLine("O número do quarto mais caro é {0}, ele custa {1}", frooms[Array.IndexOf(frprice, frprice.Max())] + 1, frprice.Max());
                        else Console.WriteLine("Opção invalida, tente novamente");
                    } while (input != "1" && input != "2");
                    break;
                case "2":
                    Console.WriteLine("Qual vai ser a qualidade? \nQualidades disponiveis: '1' para 'Economico', '2' para 'Standard', '3' para 'Turista' e '4' para 'Luxo'");
                    do
                    {
                        input = Console.ReadLine();
                        Int32.TryParse(input, out i);
                        i--;
                        if (i < 0 || i > 3) Console.WriteLine("O valor inserido é inválido, tente novamente");
                    } while (i < 0 || i > 3);
                    Console.WriteLine("Os quartos disponiveis com qualidade {1}, são: {0}", String.Join(", ", fqrooms.Select((b, idx) => b == qualities[i] ? frooms[idx] + 1 : -1).Where(idx => idx != -1).ToArray()), qualities[i]);
                    break;
                default:
                    break;
            }
        }
        static void ShowRooms(string[] qrooms, bool[] booked)
        {
            for (int i = 0; i < qrooms.Length; i++)
            {
                Console.WriteLine($"Quarto {i + 1}: {qrooms[i]}, Estado: {(booked[i] ? "Ocupado" : $"Livre, Preço: {price[i]}")}");
            }
        }
    }
}
