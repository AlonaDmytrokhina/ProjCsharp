using AppPerson.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppPerson.ViewModels
{
    internal class PersonViewModel : INotifyPropertyChanged
    {
        private Person person;
        private string _firstName;
        private string _lastName;
        private string? _email;
        private DateTime? _birthDate;

        public event PropertyChangedEventHandler? PropertyChanged;
        private RelayCommand _proceedCommand;
        public Action _toMainView { get; }

        public bool IsValid => !string.IsNullOrWhiteSpace(FirstName) &&
                       !string.IsNullOrWhiteSpace(LastName) &&
                       !string.IsNullOrWhiteSpace(Email) &&
                       BirthDate.HasValue;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                ProceedCommand.NotifyCanExecuteChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                ProceedCommand.NotifyCanExecuteChanged();
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                ProceedCommand.NotifyCanExecuteChanged();
            }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
                ProceedCommand.NotifyCanExecuteChanged();
            }
        }

        public RelayCommand ProceedCommand 
        { 
            get{
                _proceedCommand ??= new RelayCommand(Proceed, () => IsValid);
                return _proceedCommand;
            }
        }

        public PersonViewModel(Action toMainView)
        {
            _toMainView = toMainView;
        }

        private void Proceed()
        {

            person = new Person(FirstName, LastName, Email, BirthDate);

            if (!IsApropriate())
            {
                return;
            }

            if (person.IsBirthday())
            {
                MessageBox.Show($"Happy {person.Age}-th birthday!");
            }

            MessageBox.Show($"Login was successfull for user {FirstName} {LastName}");
           _toMainView();
        }

        private bool IsApropriate()
        {

            if (BirthDate >= DateTime.Today)
            {
                MessageBox.Show("Choose the correct date.");
                return false;
            }
            else if (person.Age >= 120)
            {
                MessageBox.Show($"I don't believe you're {person.Age} years old.\nChoose the correct date.");
                return false;
            }
            else if (!person.isAdult())
            {
                MessageBox.Show($"18+ access only!");
                return false;
            }


            return true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
