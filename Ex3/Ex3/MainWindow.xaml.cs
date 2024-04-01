using Ex3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ex3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Order order;
        public MainWindow()
        {
            order = new Order();
            InitializeComponent();
            UpdateOrderSummary();
        }
        private void LoadLabelTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;    
            try
            {
                order.ItemsQuantity = int.Parse(textbox.Text);
                UpdateOrderSummary();

            }
            catch (Exception)
            {
                return;
            }
        }
        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int newValue = (int)e.NewValue;
            double formatCost = 0.20;
            for (int i = 0; i<newValue; i++)
            {
                formatCost *= 2.5;
            }
            formatCost = Math.Round(formatCost, 2);
            order.Format = $"A{5 - newValue}";
            order.FormatCost = formatCost;

            if (formatCost < 1)
            {
                formatLabel.Content = $"A{5 - newValue} - cena {(int)(formatCost * 100)}gr/szt.";
            }
            else
            {
                formatLabel.Content = $"A{5 - newValue} - cena {formatCost}zł/szt.";
            }

            // podlicz cala cene 
            UpdateOrderSummary();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // do nothing
        }

        private void Grammage_RadioButton_Change(object sender, RoutedEventArgs e)
        {
           foreach(RadioButton radioButton in grammageStackPanel.Children)
           {
                if (radioButton.IsChecked == true && radioButton.Content!=null)
                {
                    string radioButtonTag = radioButton.Tag.ToString();
                    if(radioButtonTag == "one_and_half")
                    {
                        radioButtonTag = "1,5";
                    }
                        
                    double priceMultiplier = double.Parse(radioButtonTag);
                    order.Grammage = radioButton.Content.ToString().Split("-")[0].Trim();
                    if(priceMultiplier == 0)
                    {
                        order.GrammagePrice = 0;
                    }
                    else
                    {
                        order.GrammagePrice = Math.Round(order.FormatCost * priceMultiplier, 2);
                    }    

                    // podlicz cala cene 
                    UpdateOrderSummary();

                    break;
                }
            }
        }
        private void ColorfullPaperChecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null && checkBox.IsChecked == true) 
            {
                order.ColorfulPaperCost = order.FormatCost / 2;
            }
            else
            {
                order.ColorfulPaperCost = 0;
            }
            
            UpdateOrderSummary();
        }
        
        private void DeadlineOfRealizationRadioButtonChecked(object sender, RoutedEventArgs e)
        { 
            foreach(RadioButton radioButton in deadlineOfRealizationStackPanel.Children)
            {
                if(radioButton.IsChecked == true && radioButton.Content != null)
                {
                    order.DeadlineOfRealizationCost = double.Parse(radioButton.Tag.ToString());
                    order.DeadlineOfRealization = radioButton.Content.ToString().Split("-")[0].Trim();
                    UpdateOrderSummary();
                    break;
                }
            }
        }    
        private void PrintOptionsCheckboxChecked(object sender, RoutedEventArgs e)
        {
            double defaultPrice = order.FormatCost;
            double printOptionsCost = 0;
            List<string> printOptions = new List<string>();
            foreach (CheckBox checkbox in printOptionsStackPanel.Children)
            {
                if (checkbox.IsChecked == true && checkbox.Content!=null)
                {
                    int value;
                    try
                    {
                        printOptions.Add(checkbox.Content.ToString().Split("-")[0].Trim());
                        value = int.Parse(checkbox.Content.ToString().Split("+")[1].Split("%")[0]);
                        printOptionsCost += (defaultPrice / 100) * value;
                    }
                    catch(Exception)
                    {
                        return;
                    }
                }
            }
            order.PrintOptions = printOptions;
            order.PrintOptionsCost = printOptionsCost;
            UpdateOrderSummary();
        }

        private void Button_OK_Clicked(object sender, EventArgs e)
        {
            MessageBox.Show("Zamówienie zostało potwierdzone");
            // reest form
        }
        private void Button_Cancel_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateOrderSummary()
        {
            if(orderSummaryTextBox != null)
            {
                orderSummaryTextBox.Text = order.GetOrderSummary();
            }
            
        }


        // rabat 1% za każde 100 egzamplarzy
        // rabat max 10 %
    }
}
