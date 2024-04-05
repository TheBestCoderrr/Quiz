using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Project
{
    internal class Menu
    {
        public User Quiz(User player)
        {
            int user_choice = -1;
            string section = "";
            while (user_choice < 0 || user_choice > 3)
            {
                Console.Write("0 - Math\n1 - History\n2 - All");
                Console.Write("Enter variant: ");
                user_choice = int.Parse(Console.ReadLine());
                switch (user_choice)
                {
                    case 0:
                        section = "MathTest";
                        break;
                    case 1:
                        section = "HistoryTest";
                        break;
                    case 2:
                        section = "AllSubjectsTest";
                        break;
                    default:
                        Console.WriteLine("Invalid variant");
                        break;
                }
            }
            Quiz quiz = new Quiz();
            quiz.Load($"{section}.txt");
            player = quiz.Start(player, section);
            return player;
        }

        public void SecondMenu(User player, UsersList users)
        {
            int user_choice = -1;
            while (user_choice != 0)
            {
                Console.WriteLine("0 - Back\n1 - Start quiz\n2 - check results\n3 - check top-20 results\n4 - settings");
                Console.Write("Enter variant: ");
                user_choice = int.Parse(Console.ReadLine());
                switch (user_choice)
                {
                    case 0:
                        users.Save("Users.txt");
                        break;
                    case 1:
                        player = Quiz(player);
                        break;
                    case 2:
                        Console.Clear();
                        foreach(var item in player.stats)
                            Console.WriteLine(item.ToString());
                        break;
                    case 3:
                        users.PrintTop20();
                        break;
                    case 4:
                        Console.WriteLine("0 - login\n1 - password");
                        user_choice = int.Parse(Console.ReadLine());
                        if (user_choice == 0)
                        {
                            Console.Write("Enter new login: ");
                            player.login = Console.ReadLine();
                            user_choice = -1;
                        }
                        else if (user_choice == 1)
                        {
                            Console.Write("Enter new password: ");
                            player.password = Console.ReadLine();
                        }
                        else Console.WriteLine("Error");
                        break;
                    default:
                        Console.WriteLine("Invalid variant");
                        break;
                }
            }
        }
        public void menu()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            int user_choice = -1;
            while (user_choice != 0)
            {
                Console.Clear();
                UsersList users = new UsersList();
                users.Load("Users.txt");
                User player = new User();

                Console.WriteLine("0 - Exit\n1 - Sign in\n2 - Sign up");
                Console.Write("Enter variant: ");
                user_choice = int.Parse(Console.ReadLine());
                switch (user_choice)
                {
                    case 0:
                        Console.WriteLine("Exit");
                        break;
                    case 1:
                        user_choice = player.SignIn(users) ? 0 : -1;
                        if (user_choice == -1)
                        {
                            Console.WriteLine("User not found");
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Console.WriteLine("log in succes");
                            player = users.GetUser(player);
                            SecondMenu(player, users);
                            user_choice = -1;
                        }
                        break;
                    case 2:
                        player.SignUp();
                        if (users.FindUser(player) == false)
                        {
                            users.users.Add(player);
                            SecondMenu(player, users);
                        }
                        else
                        {
                            Console.WriteLine("Error.Similar user");
                            Thread.Sleep(2000);
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid variant");
                        break;
                }
            }
        }
    }
}
