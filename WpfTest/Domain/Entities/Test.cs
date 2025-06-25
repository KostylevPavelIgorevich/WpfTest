using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.Domain.Entities
{
    public class Test
    {
        public string Title { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
