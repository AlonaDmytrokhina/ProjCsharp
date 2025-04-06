using AppPersonList.ExceptionHandling;
using AppPersonList.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppPersonList.ViewModels
{
    internal class PersonViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person newPerson;
        private string _firstName;
        private string _lastName;
        private string? _email;
        private DateTime? _birthDate;

        public event PropertyChangedEventHandler? PropertyChanged;
        private RelayCommand _proceedCommand;
        private readonly Action _closeWindow;
        private bool IsValid => !string.IsNullOrWhiteSpace(FirstName) &&
                       !string.IsNullOrWhiteSpace(LastName) &&
                       !string.IsNullOrWhiteSpace(Email) &&
                       BirthDate.HasValue;
        private bool _isEnabled = true;
        private Visibility _loaderVisibile = Visibility.Collapsed;
        #endregion

        #region Properties
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
            get
            {
                _proceedCommand ??= new RelayCommand(Proceed, () => IsValid);
                return _proceedCommand;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public Visibility LoaderVisible
        {
            get
            {
                return _loaderVisibile;
            }
            set
            {
                _loaderVisibile = value;
                OnPropertyChanged(nameof(LoaderVisible));
            }
        }

        public Person NewPerson 
        {
            get
            {
                return newPerson;
            }
            set
            {
                newPerson = value;
                OnPropertyChanged(nameof(newPerson));
            }
        }

        public PersonViewModel(Action closeWindow)
        {
            _closeWindow = closeWindow;
        }
        #endregion

        private async void Proceed()
        {
            try
            {
                IsEnabled = false;
                LoaderVisible = Visibility.Visible;
                NewPerson = await Task.Run(() => new Person(FirstName, LastName, Email, BirthDate));
            }
            catch (FutureBirthDateException ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}");
                return;
            }
            catch (UnrealisticBirthDateException ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}");
                return;
            }
            catch (InvalidEmailException ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}");
                return;
            }
            catch (UnrealisticNameException ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}");
                return;
            }
            catch (IsNotAdultException ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}");
                return;
            }
            finally
            {
                IsEnabled = true;
                LoaderVisible = Visibility.Collapsed;
            }

            MessageBox.Show($"Login was successfull for user {FirstName} {LastName}");
            _closeWindow?.Invoke();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
