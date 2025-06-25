using Microsoft.Win32;
using System.IO;
using System.Text.Json;
using System.Windows;
using WpfTest.Domain.Entities;
using WpfTest.Views;

namespace WpfTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CreateTest_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TestEditorView();
        }

        private void RunTest_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON файлы (*.json)|*.json"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string json = File.ReadAllText(openFileDialog.FileName);
                    var test = JsonSerializer.Deserialize<Test>(json);
                    if (test != null)
                    {
                        MainContent.Content = new TestRunnerView(test);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка загрузки теста.");
                    }
                }
                catch
                {
                    MessageBox.Show("Файл повреждён или неверного формата.");
                }
            }
        }
    }
}