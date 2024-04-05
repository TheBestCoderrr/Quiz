using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Project
{
    internal class UsersList
    {
        public UsersList() { 
            users = new List<User>();
        }
        public List<User> users;
        public bool FindUser(User user)
        {
            int index = users.FindIndex(0, u => u.login == user.login);
            return index > -1 ? true : false;
        }
        public User GetUser(User user)
        {
            int index = users.FindIndex(0, u => u.login == user.login);
            user = users[index];
            return user;
        }

        public void Sort()
        {
            users.Sort((user1, user2) => user2.getAVGstats().CompareTo(user1.getAVGstats()));
        }
        public void PrintTop20()
        {
            Sort();
            for (int i = 0; i < (users.Count > 20 ? 20 : users.Count); i++)
            {
                Console.WriteLine($"\tN{i+1}|Name: {users[i].login}");
                users[i].PrintStats();
            }
        }
        public void Save(string file)
        {
            using (Stream stream = File.Create(file))
            {
                new SoapFormatter()?.Serialize(stream, users);
            }
        }
        public void Load(string file)
        {
            using (Stream stream = File.OpenRead(file))
            {
                users = new SoapFormatter()?.Deserialize(stream) as List<User>;
            }
        }

    }
}
