using AppZodiac.Models;
using AppZodiac.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;

namespace AppZodiac.ViewModels
{
    public class DateViewModel
    {
        int _age;
        DateTime? _selectedDate;
        private RelayCommand? _confirmCommand;
        private DateTime today = DateTime.Today;
        public Action<DateTime>? OpenZodiacWindowAction { get; set; }

        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
            }
        }
        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                ConfirmCommand?.NotifyCanExecuteChanged();
            }
        }

        public RelayCommand? ConfirmCommand
        {
            get
            {
                _confirmCommand ??= new RelayCommand(Confirm);
                return _confirmCommand;
            }
        }

        private bool IsApropriate()
        {

            if (!SelectedDate.HasValue)
            {
                MessageBox.Show("Choose the date.");
                return false;
            }

            Age = AgeCalc.CalculateAge(SelectedDate.Value);

            if (SelectedDate.Value >= today)
            {
                MessageBox.Show("Choose the correct date.");
                return false;
            }

            if (Age >= 120 || Age <= 5)
            {
                MessageBox.Show($"I don't believe you're {Age} years old.\nChoose the correct date.");
                return false;
            }

            return true;
        }

        private void Confirm()
        {
            if (!IsApropriate())
            {
                return;
            }

            if (SelectedDate?.DayOfYear == today.DayOfYear)
            {
                MessageBox.Show($"Happy {Age}-th birthday you stunning stack of sunshine!");
            }

            OpenZodiacWindowAction?.Invoke(SelectedDate.GetValueOrDefault());
        }
    }
}
