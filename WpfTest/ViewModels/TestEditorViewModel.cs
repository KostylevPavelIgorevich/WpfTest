using System.Collections.ObjectModel;
using WpfTest.Domain.Entities;
using WpfTest.Infrastructure.Commands;
using System.Windows.Input;
using Microsoft.Win32;
using System.Text.Json;
using System.Windows;
using System.IO;
using System.Text.Encodings.Web;

namespace WpfTest.ViewModels
{
    class TestEditorViewModel : ViewModelBase
    {
        public string TestTitle { get; set; } = "Новый тест";

        public ObservableCollection<Question> Questions { get; set; } = new();

        private NewQuestionViewModel _newQuestion = new NewQuestionViewModel();
        public NewQuestionViewModel NewQuestion
        {
            get => _newQuestion;
            set => SetProperty(ref _newQuestion, value);
        }

        public ICommand AddQuestionCommand { get; }
        public ICommand AddAnswerCommand { get; }
        public ICommand SaveTestCommand { get; }

        public TestEditorViewModel()
        {
            AddQuestionCommand = new RelayCommand(_ => AddQuestion());
            AddAnswerCommand = new RelayCommand(_ => AddAnswer());
            SaveTestCommand = new RelayCommand(_ => SaveTest());
        }

        private void AddQuestion()
        {
            if (string.IsNullOrWhiteSpace(NewQuestion.Text) || NewQuestion.Answers.Count < 2 || NewQuestion.Answers.Any(a => string.IsNullOrWhiteSpace(a.Text)))
            {
                MessageBox.Show("Введите текст вопроса и как минимум два непустых варианта ответа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Questions.Add(new Question
            {
                Text = NewQuestion.Text,
                Answers = NewQuestion.Answers.Select(a => a.Text).ToList(),
                CorrectAnswerIndex = NewQuestion.CorrectAnswerIndex
            });

            NewQuestion = new NewQuestionViewModel();
        }

        private void AddAnswer()
        {
            NewQuestion.Answers.Add(new AnswerViewModel());
        }

        private void SaveTest()
        {
            var test = new Test
            {
                Title = TestTitle,
                Questions = Questions.ToList()
            };

            var dialog = new SaveFileDialog
            {
                Filter = "JSON файл (*.json)|*.json",
                FileName = $"{TestTitle}.json"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    };

                    string json = JsonSerializer.Serialize(test, options);
                    System.IO.File.WriteAllText(dialog.FileName, json);

                    MessageBox.Show("Тест успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении теста:\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}


