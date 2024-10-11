using System;
using System.Windows;

namespace NotificationApp
{
    // Класс, реализующий уведомления
    public class Notification
    {
        // События для разных типов уведомлений
        public event Action НаСообщениеОтправлено;
        public event Action НаЗвонокСовершён;
        public event Action НаПисьмоОтправлено;

        // Метод для отправки сообщения
        public void ОтправитьСообщение(string message)
        {
            // Здесь можно добавить логику для отправки сообщения
            Console.WriteLine($"Сообщение отправлено: {message}"); // Лог для демонстрации
            НаСообщениеОтправлено?.Invoke();
        }

        // Метод для совершения звонка
        public void СовершитьЗвонок()
        {
            НаЗвонокСовершён?.Invoke();
        }

        // Метод для отправки электронного письма
        public void ОтправитьПисьмо()
        {
            НаПисьмоОтправлено?.Invoke();
        }
    }

    public partial class MainWindow : Window
    {
        // Экземпляр класса Notification
        private Notification уведомление;

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация уведомлений
            уведомление = new Notification();

            // Регистрация обработчиков событий
            уведомление.НаСообщениеОтправлено += Уведомление_НаСообщениеОтправлено;
            уведомление.НаЗвонокСовершён += Уведомление_НаЗвонокСовершён;
            уведомление.НаПисьмоОтправлено += Уведомление_НаПисьмоОтправлено;
        }

        // Обработчик события отправки сообщения
        private void Уведомление_НаСообщениеОтправлено()
        {
            StatusTextBlock.Text = "Сообщение успешно отправлено!";
        }

        // Обработчик события совершения звонка
        private void Уведомление_НаЗвонокСовершён()
        {
            StatusTextBlock.Text = "Звонок успешно совершен!";
        }

        // Обработчик события отправки письма
        private void Уведомление_НаПисьмоОтправлено()
        {
            StatusTextBlock.Text = "Письмо успешно отправлено!";
        }

        // Кнопка для отправки сообщения
        private void MessageButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageTextBox.Text; // Получение текста из текстового поля
            if (!string.IsNullOrWhiteSpace(message)) // Проверка на пустоту
            {
                уведомление.ОтправитьСообщение(message); // Отправка сообщения
            }
            else
            {
                StatusTextBlock.Text = "Введите сообщение перед отправкой."; // Уведомление о пустом сообщении
            }
        }

        // Кнопка для совершения звонка
        private void CallButton_Click(object sender, RoutedEventArgs e)
        {
            уведомление.СовершитьЗвонок();
        }

        // Кнопка для отправки электронного письма
        private void EmailButton_Click(object sender, RoutedEventArgs e)
        {
            уведомление.ОтправитьПисьмо();
        }
    }
}