using Ex1.Models;
using System;
using System.Windows;
using System.Windows.Media;

namespace Ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DrawingMachine drawingMachine;
        public MainWindow()
        {
            drawingMachine = new DrawingMachine();
            InitializeComponent();
        }

        private void TakeButton_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                string randomTicketValue = drawingMachine.GetRandomTicket();
                MainOutputLabel.Content = $"Drawn value: {randomTicketValue}";
                MainOutputLabel.Foreground = Brushes.Green;
                OutputLabel.Text = $"Remaining tickets: {string.Join(",", drawingMachine.Tickets)}";
            }
            catch (Exception ex) 
            {
                MainOutputLabel.Content = ex.Message;
                MainOutputLabel.Foreground = Brushes.Red;
            }       
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string ticketValue = TicketNameInput.Text;
            try
            {
                drawingMachine.AddTicket(ticketValue);
                MainOutputLabel.Content = $"Ticket {ticketValue} added succesfully!";
                MainOutputLabel.Foreground = Brushes.Green;
                OutputLabel.Text = $"Tickets in drawing machine: {string.Join(",", drawingMachine.Tickets)}";
            }
            catch(Exception ex)
            {
                MainOutputLabel.Content = ex.Message;
                MainOutputLabel.Foreground = Brushes.Red;
            }           
        }
    }
}
