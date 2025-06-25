using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTest.Domain.Entities;
using WpfTest.ViewModels;

namespace WpfTest.Views
{
    public partial class TestRunnerView : UserControl
    {
        public TestRunnerView(Test test)
        {
            InitializeComponent();
            DataContext = new TestRunnerViewModel(test);
        }
    }
}
