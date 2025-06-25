using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.Domain.Entities
{
   public class TestResultItem
    {
        public string QuestionText { get; set; }
        public string SelectedAnswer { get; set; }
        public string CorrectAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
