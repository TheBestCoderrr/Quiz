using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Project
{
    [Serializable]
    internal class User
    {
        public User() {
            stats = new List<QuizStats>();
        }
        public string login { get; set; }
        public string password { get; set; }
        public DateTime dateOfBirth { get; set; }
        public List<QuizStats> stats;
        public void SignUp()
        {
            Console.WriteLine("Enter login: ");
            login = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            password = Console.ReadLine();
            bool incorrect = true;
            while (incorrect)
            {
                Console.WriteLine("Enter date of birth(dd.mm.yyyy): ");
                string[] txt = Console.ReadLine().Split(".");
                try
                {
                    dateOfBirth = new DateTime(int.Parse(txt[2]), int.Parse(txt[1]), int.Parse(txt[0]));
                }
                catch (Exception ex){
                    Console.WriteLine(ex.Message);
                }
                if (dateOfBirth < DateTime.Now) incorrect = false;
                else Console.WriteLine("Incorrect date.");
            }
        }
        public bool SignIn(UsersList users)
        {
            Console.WriteLine("Enter login: ");
            login = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            password = Console.ReadLine();
            foreach(User user in users.users)
            {
                if (login == user.login && password == user.password) return true;
            }
            return false;
        }
        public double getAVGstats()
        {
            double avgstats = 0;
            foreach(var item in stats)
            {
                avgstats += item.score;
            }
            avgstats /= stats.Count;
            return avgstats;
        }
        public void PrintStats()
        {
            foreach(var item in stats) Console.WriteLine(item.ToString());
        }
    }
}
