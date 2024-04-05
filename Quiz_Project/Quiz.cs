using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Quiz_Project
{
    internal interface IQuiz
    {
        public string section { get; set; }
    }
    [Serializable]
    internal class Quiz : IQuiz
    {
        public Quiz() { 
            questions = new List<IQuestion>();
        }
        public string section { get; set; }
        public List<IQuestion> questions;
        public User Start(User user, string section) {
           DateTime startTime = DateTime.Now;
           int score = 0;
            if (section == "AllSubjectsTest")
            {
                List<int> nums= new List<int>();
                for (int i = 0; i < 20; i++)
                {
                    Random random = new Random();
                    int index;
                    while (true)
                    {
                        bool SimilarIndex = false;
                        index = random.Next(0, questions.Count);
                        foreach(int num in nums)
                        {
                            if (num == index)
                            {
                                SimilarIndex = true;
                                break;
                            }
                        }
                        if (SimilarIndex) continue;
                        break;
                    }
                    nums.Add(index);
                    Console.Clear();
                    Console.WriteLine(questions[index].question);
                    string user_answer;
                    if (questions[index] is Question)
                    {
                        Console.WriteLine("Enter answer: ");
                        user_answer = Console.ReadLine();
                        if (user_answer == questions[index].answer)
                            score++;
                    }
                    else if (questions[index] is MultiQuestion)
                    {
                        MultiQuestion multiQuestion = (MultiQuestion)questions[index];
                        Console.WriteLine($"{string.Join("\t", multiQuestion.variant_answers)}");
                        Console.WriteLine("Enter answer(A B C): ");
                        user_answer = Console.ReadLine();
                        user_answer = user_answer.Replace(" ", "");
                        if (user_answer == multiQuestion.answer)
                            score++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < questions.Count; i++)
                {
                    Console.Clear();
                    Console.WriteLine(questions[i].question);
                    string user_answer;
                    if (questions[i] is Question)
                    {
                        Console.WriteLine("Enter answer: ");
                        user_answer = Console.ReadLine();
                        if (user_answer == questions[i].answer)
                            score++;
                    }
                    else if (questions[i] is MultiQuestion)
                    {
                        MultiQuestion multiQuestion = (MultiQuestion)questions[i];
                        Console.WriteLine($"{string.Join("\t", multiQuestion.variant_answers)}");
                        Console.WriteLine("Enter answer(A B C): ");
                        user_answer = Console.ReadLine();
                        user_answer = user_answer.Replace(" ", "");
                        if (user_answer == multiQuestion.answer)
                            score++;
                    }
                }
            }
           DateTime endTime = DateTime.Now;
           TimeSpan ResultTime = endTime - startTime;
           DateTime newTime = endTime.Subtract(ResultTime);
            user.stats.Add(new QuizStats() {score = score, time = newTime, section = section});
            Console.WriteLine(user.stats[user.stats.Count - 1].ToString());
           return user;
        }
        public void Save(string file)
        {
            using (Stream stream = File.Create(file))
            {
                new SoapFormatter()?.Serialize(stream, questions);
            }
        }
        public void Load(string file)
        {
            using (Stream stream = File.OpenRead(file))
            {
                questions = new SoapFormatter()?.Deserialize(stream) as List<IQuestion>;
            }
        }
    }
    [Serializable]
    class QuizStats : IQuiz
    {
        public string section { get; set; }
        public DateTime time;
        public int score;
        public override string ToString() => $"stats:\nsection - {section}| Time: {time} | Score: {score}/20 |";
    }
}

