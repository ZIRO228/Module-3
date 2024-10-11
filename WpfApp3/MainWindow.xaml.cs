using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TaskManagerApp
{
    // Делегат для выполнения задачи
    public delegate void TaskDelegate(string taskDescription);

    public partial class MainWindow : Window
    {
        private List<string> tasks;

        public MainWindow()
        {
            InitializeComponent();
            tasks = new List<string>(); // Инициализация списка задач
        }

        // Обработка нажатия кнопки "Добавить задачу"
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string taskDescription = TaskTextBox.Text; // Получение описания задачи
            if (!string.IsNullOrWhiteSpace(taskDescription)) // Проверка на пустоту
            {
                TaskDelegate taskDelegate = null; // Инициализация делегата

                // Определение делегата в зависимости от выбранного действия
                switch ((ActionComboBox.SelectedItem as ComboBoxItem)?.Content.ToString())
                {
                    case "Отправить уведомление":
                        taskDelegate = SendNotification;
                        break;
                    case "Записать в журнал":
                        taskDelegate = LogToJournal;
                        break;
                }

                // Выполнение задачи с использованием делегата
                taskDelegate?.Invoke(taskDescription);

                // Добавление задачи в список
                tasks.Add(taskDescription);
                TaskTextBox.Clear(); // Очистка текстового поля
                UpdateTaskList(); // Обновление списка задач
                StatusTextBlock.Text = "Задача добавлена и выполнена!";
            }
            else
            {
                StatusTextBlock.Text = "Введите описание задачи."; // Уведомление о пустом описании
            }
        }

        // Метод для отправки уведомления
        private void SendNotification(string taskDescription)
        {
            MessageBox.Show($"Уведомление: {taskDescription}"); // Показываем уведомление
        }

        // Метод для записи в журнал
        private void LogToJournal(string taskDescription)
        {
            // Запись в журнал (здесь просто вывод в консоль для демонстрации)
            Console.WriteLine($"Записано в журнал: {taskDescription}");
        }

        // Обновление списка задач в интерфейсе
        private void UpdateTaskList()
        {
            TaskListBox.Items.Clear(); // Очистка списка
            foreach (var task in tasks)
            {
                TaskListBox.Items.Add(task); // Добавление задач в список
            }
        }
    }
}