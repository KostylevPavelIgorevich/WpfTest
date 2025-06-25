using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.Domain.Entities
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; } = new List<string>();
        public int CorrectAnswerIndex { get; set; }
    }
}
