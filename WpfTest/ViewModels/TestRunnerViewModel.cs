using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfTest.Domain.Entities;
using WpfTest.Infrastructure.Commands;
using WpfTest.Views;

namespace WpfTest.ViewModels
{
    class TestRunnerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int currentQuestionIndex;
        private int selectedAnswerIndex;

        public Test CurrentTest { get; set; }

        public int CurrentQuestionIndex
        {
            get => currentQuestionIndex;
            set
            {
                if (value < 0 || value >= CurrentTest?.Questions.Count)
                    return;

                if (SetProperty(ref currentQuestionIndex, value))
                {
                    OnPropertyChanged(nameof(CurrentQuestion));
                    SelectedAnswerIndex = UserAnswers[currentQuestionIndex];
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public Question? CurrentQuestion =>
            CurrentTest?.Questions != null && CurrentQuestionIndex >= 0 && CurrentQuestionIndex < CurrentTest.Questions.Count
                ? CurrentTest.Questions[CurrentQuestionIndex]
                : null;
        public int SelectedAnswerIndex
        {
            get => selectedAnswerIndex;
            set => SetProperty(ref selectedAnswerIndex, value);
        }

        public ObservableCollection<int> UserAnswers { get; set; }

        public ICommand NextCommand { get; }
        public ICommand PreviousCommand { get; }
        public ICommand FinishCommand { get; }

        public TestRunnerViewModel(Test test)
        {
            CurrentTest = test ?? throw new ArgumentNullException(nameof(test));

            if (CurrentTest.Questions == null || CurrentTest.Questions.Count == 0)
                throw new ArgumentException("Тест не содержит вопросов", nameof(test));

            UserAnswers = new ObservableCollection<int>(Enumerable.Repeat(-1, CurrentTest.Questions.Count));

            currentQuestionIndex = 0;
            selectedAnswerIndex = -1;

            NextCommand = new RelayCommand(_ => GoNext(), _ => true);
            PreviousCommand = new RelayCommand(_ => GoBack(), _ => CurrentQuestionIndex > 0);
            FinishCommand = new RelayCommand(_ => ShowResult());
        }

        private void GoNext()
        {
            SaveAnswer();

            if (CurrentQuestionIndex < CurrentTest.Questions.Count - 1)
            {
                CurrentQuestionIndex++;
                SelectedAnswerIndex = UserAnswers[CurrentQuestionIndex];
            }
            else
            {
                ShowResult();
            }
        }

        private void GoBack()
        {
            SaveAnswer();
            CurrentQuestionIndex--;
        }

        private void SaveAnswer()
        {
            if (CurrentQuestionIndex >= 0 && CurrentQuestionIndex < UserAnswers.Count)
                UserAnswers[CurrentQuestionIndex] = SelectedAnswerIndex;
        }

        private List<TestResultItem> GetTestResults()
        {
            var results = new List<TestResultItem>();

            for (int i = 0; i < CurrentTest.Questions.Count; i++)
            {
                var question = CurrentTest.Questions[i];
                int selectedIndex = UserAnswers[i];

                int correctIndex = question.CorrectAnswerIndex - 1;

                results.Add(new TestResultItem
                {
                    QuestionText = question.Text,

                    SelectedAnswer = selectedIndex >= 0 && selectedIndex < question.Answers.Count
                        ? question.Answers[selectedIndex]
                        : "Не выбран",

                    CorrectAnswer = correctIndex >= 0 && correctIndex < question.Answers.Count
                        ? question.Answers[correctIndex]
                        : "Некорректный индекс",

                    IsCorrect = selectedIndex == correctIndex
                });
            }

            return results;
        }

        private void ShowResult()
        {
            SaveAnswer();

            var results = GetTestResults();
            var resultViewModel = new TestResultViewModel(results);
            var resultWindow = new TestResultWindow(resultViewModel);
            resultWindow.Show();

            // Закрыть текущее окно, если нужно:
            Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext == this)
                ?.Close();
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
