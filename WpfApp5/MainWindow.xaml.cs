using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SortingApp
{
    // Делегат для метода сортировки
    public delegate int[] SortingDelegate(int[] numbers);

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработка нажатия кнопки "Сортировать"
        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            string input = NumbersInputTextBox.Text; // Получение входных данных
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Введите числа для сортировки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Разделение строк на числа и преобразование их в массив
            int[] numbers = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                  .Select(n => int.Parse(n.Trim()))
                                  .ToArray();

            // Выбор метода сортировки
            SortingDelegate sortMethod = null;
            if (SortingMethodComboBox.SelectedItem is ComboBoxItem selectedMethod)
            {
                switch (selectedMethod.Content.ToString())
                {
                    case "Сортировка пузырьком":
                        sortMethod = BubbleSort;
                        break;
                    case "Быстрая сортировка":
                        sortMethod = QuickSort;
                        break;
                }
            }

            // Сортировка чисел и отображение результата
            if (sortMethod != null)
            {
                int[] sortedNumbers = sortMethod(numbers);
                SortedResultTextBlock.Text = $"Отсортированные числа: {string.Join(", ", sortedNumbers)}";
            }
        }

        // Метод сортировки пузырьком
        private int[] BubbleSort(int[] numbers)
        {
            int n = numbers.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        // Обмен элементов
                        int temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                    }
                }
            }
            return numbers;
        }

        // Метод быстрой сортировки
        private int[] QuickSort(int[] numbers)
        {
            QuickSort(numbers, 0, numbers.Length - 1);
            return numbers;
        }

        // Рекурсивный метод быстрой сортировки
        private void QuickSort(int[] numbers, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(numbers, low, high);
                QuickSort(numbers, low, pivotIndex - 1);
                QuickSort(numbers, pivotIndex + 1, high);
            }
        }

        // Метод разделения для быстрой сортировки
        private int Partition(int[] numbers, int low, int high)
        {
            int pivot = numbers[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (numbers[j] < pivot)
                {
                    i++;
                    // Обмен элементов
                    int temp = numbers[i];
                    numbers[i] = numbers[j];
                    numbers[j] = temp;
                }
            }

            // Обмен pivot с элементом на позиции i + 1
            int temp1 = numbers[i + 1];
            numbers[i + 1] = numbers[high];
            numbers[high] = temp1;

            return i + 1;
        }
    }
}