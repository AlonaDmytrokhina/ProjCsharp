using AppPersonList.Helpers;
using AppPersonList.Models;
using AppPersonList.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppPersonList.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Person> People { get; set; }
        public ObservableCollection<Person> FilteredPeople { get; set; }
        private string _filterText;
        public ICommand AddCommand { get; }
        public RelayCommand RemoveCommand { get; }
        private Person? _selectedPerson;

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                ApplyFilterAndSort();
            }
        }

        public Person? SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                if (_selectedPerson != value)
                {
                    _selectedPerson = value;
                    OnPropertyChanged(nameof(SelectedPerson));
                    RemoveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public MainViewModel()
        {
            var loaded = DataService.Load();
            if (loaded != null)
            {
                People = new ObservableCollection<Person>(loaded);
            }
            else
            {
                People = new ObservableCollection<Person>(GenerateRandomPeople(50));
                DataService.Save(People);
            }

            FilteredPeople = new ObservableCollection<Person>(People);

            AddCommand = new RelayCommand(AddPerson);
            RemoveCommand = new RelayCommand(RemovePerson, IsSelected);
        }

        private bool IsSelected()
        {
            return SelectedPerson != null;
        }

        private void AddPerson()
        {

            var window = new PersonWindow();
            PersonViewModel viewModel = new PersonViewModel(() => window.DialogResult = true);
            window.DataContext = viewModel;

            if (window.ShowDialog() == true)
            {
                if (viewModel.NewPerson != null)
                {
                    People.Add(viewModel.NewPerson);
                    ApplyFilterAndSort();
                    DataService.Save(People);
                }
            }
        }

        private void RemovePerson()
        {
            if (!IsSelected())
            {
                return;
            }

            People.Remove(SelectedPerson);
            ApplyFilterAndSort();
            DataService.Save(People);
        }

        public void SaveData() => DataService.Save(People);

        public void ApplyFilterAndSort()
        {
            var query = People.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(FilterText))
                query = query.Where(p =>
                    p.Name.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                    p.Surname.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                    p.Email.Contains(FilterText, StringComparison.OrdinalIgnoreCase));

            query = query.OrderBy(p => p.Surname).ThenBy(p => p.Name);

            FilteredPeople.Clear();
            foreach (var person in query)
                FilteredPeople.Add(person);
        }

        private List<Person> GenerateRandomPeople(int count)
        {
            var people = new List<Person>();
            var rnd = new Random();
            string[] names = { "Іван", "Марія", "Олег", "Анна", "Сергій", "Катерина",
                "Софія", "Михайло", "Дарія", "Арина", "Володимир", "Олександр"};
            string[] surnames = { "Іваненко", "Петренко", "Сидоренко", "Ковальчук", "Зозуля",
                "Коваленко", "Шевченко", "Харченко", "Заєць", "Прибиш"};

            for (int i = 0; i < count; i++)
            {
                people.Add(new Person
                {
                    Name = names[rnd.Next(names.Length)],
                    Surname = surnames[rnd.Next(surnames.Length)],
                    Email = $"user{i}@gmail.com",
                    BirthDate = DateTime.Today.AddYears(-rnd.Next(18, 70))
                });
            }

            return people;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
