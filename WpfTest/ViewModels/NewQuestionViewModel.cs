using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.ViewModels
{
    class NewQuestionViewModel : INotifyPropertyChanged
    {
        private string _text;
        private int _correctAnswerIndex;

        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(); }
        }

        public ObservableCollection<AnswerViewModel> Answers { get; set; } = new ObservableCollection<AnswerViewModel>();

        public int CorrectAnswerIndex
        {
            get => _correctAnswerIndex;
            set { _correctAnswerIndex = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
