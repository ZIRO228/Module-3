using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DataFilteringApp
{
    // Делегат для фильтрации данных
    public delegate List<string> DataFilterDelegate(List<string> data, string filter);

    public partial class MainWindow : Window
    {
        private List<string> dataList; // Список данных

        public MainWindow()
        {
            InitializeComponent();
            dataList = new List<string>(); // Инициализация списка данных
        }

        // Обработка нажатия кнопки "Добавить данные"
        private void AddDataButton_Click(object sender, RoutedEventArgs e)
        {
            string inputData = DataInputTextBox.Text; // Получение данных из текстового поля
            if (!string.IsNullOrWhiteSpace(inputData)) // Проверка на пустоту
            {
                var items = inputData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                dataList.AddRange(items.Select(item => item.Trim())); // Добавление данных в список
                DataInputTextBox.Clear(); // Очистка текстового поля
                MessageBox.Show("Данные добавлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Введите данные для добавления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Обработка нажатия кнопки "Применить фильтр"
        private void FilterDataButton_Click(object sender, RoutedEventArgs e)
        {
            string filter = FilterTextBox.Text; // Получение фильтра из текстового поля

            // Применение фильтров и обновление отображаемых данных
            List<string> filteredData = ApplyFilter(dataList, filter, KeywordFilter);
            DisplayFilteredData(filteredData);
        }

        // Метод для фильтрации данных по ключевым словам
        private List<string> KeywordFilter(List<string> data, string filter)
        {
            return data.Where(d => d.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0).ToList(); // Фильтрация по ключевым словам
        }

        // Метод для отображения отфильтрованных данных
        private void DisplayFilteredData(List<string> filteredData)
        {
            FilteredDataTextBlock.Text = filteredData.Count > 0 ? string.Join(", ", filteredData) : "Нет данных для отображения."; // Обновление текстового блока
        }

        // Метод для применения фильтра
        private List<string> ApplyFilter(List<string> data, string filter, DataFilterDelegate filterDelegate)
        {
            return filterDelegate(data, filter); // Применение выбранного фильтра
        }
    }
}