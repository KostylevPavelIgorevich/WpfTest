using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfTest.Domain.Entities;
using WpfTest.Infrastructure.Commands;

namespace WpfTest.ViewModels
{
    public class TestResultViewModel
    {
        public ObservableCollection<TestResultItem> Results { get; }

        public int TotalCorrect { get; }

        public int TotalQuestions => Results.Count;

        public ICommand CloseCommand { get; }

        public Action CloseAction { get; set; }

        public TestResultViewModel(List<TestResultItem> results)
        {
            Results = new ObservableCollection<TestResultItem>(results ?? new List<TestResultItem>());
            TotalCorrect = results.Count(r => r.IsCorrect);

            CloseCommand = new RelayCommand(_ => CloseAction?.Invoke());
        }
    }
}

