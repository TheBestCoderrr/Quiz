using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Project
{
    internal interface IQuestion
    {
        string question { get; set; }
        string answer { get; set; }
    }
    [Serializable]
    internal class Question : IQuestion
    {
        public string question { get; set; }
        public string answer { get; set; }
    }
    [Serializable]
    internal class MultiQuestion : IQuestion
    {
        public string question { get; set; }
        public string answer { get; set; }
        public List<string> variant_answers;
    }
}
