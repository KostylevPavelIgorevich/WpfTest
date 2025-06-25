using System.Windows.Controls;
using WpfTest.ViewModels; // подключаем ViewModel

namespace WpfTest.Views
{
    public partial class TestEditorView : UserControl
    {
        public TestEditorView()
        {
            InitializeComponent();
            DataContext = new TestEditorViewModel(); 
        }
    }
}
