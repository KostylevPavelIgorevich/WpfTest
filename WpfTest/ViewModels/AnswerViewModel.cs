using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.ViewModels
{
    class AnswerViewModel : INotifyPropertyChanged
    {
        private string _text = string.Empty;
        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
