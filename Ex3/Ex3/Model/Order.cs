using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex3.Model
{
    public class Order
    {      
        private string format;
        private string grammage;
        private int itemsQuantity;
        private double formatCost;
        private double grammagePrice;
        private double colorfulPaperCost;
        private double printOptionsCost;
        private double deadlineOfRealizationCost;
        private List<string> printOptions;
        private string deadlineOfRealization;
        private double priceBeforeDiscount;
        private int discountPercentage;
        private double priceAfterDiscount;

        public Order()
        {        
            format = "A5";           
            printOptions = new List<string>();
            itemsQuantity = 0;
            formatCost = 0.20;
            grammagePrice = 0;
            grammage = "80 g/m2";
            //printOptionsCost = (formatCost / 100) * 30;
           // printOptions.Add("druk w kolorze");
            deadlineOfRealizationCost = 0;
            deadlineOfRealization = "Standard";
            colorfulPaperCost = 0;
            

            discountPercentage = 0;

            priceBeforeDiscount = 0;
            priceAfterDiscount = 0;
        }
        public string GetOrderSummary()
        {
            CalculatePrice();

            return $"Przedmiot zamówienia: {itemsQuantity} szt., format {format}, {grammage}," +
                    $"{(printOptions.Any() ? " " + string.Join(", ", printOptions) + "," : "")} {(colorfulPaperCost == 0 ? "" : "kolorowy papier, ")}" +
                    $"{deadlineOfRealization}\n"+
                    $"Cena przed rabatem: {priceBeforeDiscount} zł\n" +
                    $"Naliczony rabat: {discountPercentage}% \n" +
                    $"Cena po rabacie: {priceAfterDiscount} zł";
        }
        private void CalculateDiscount(double price)
        {
            int items_temp = itemsQuantity;
            discountPercentage = 0;
            while (items_temp >= 100)
            {
                discountPercentage += 1;
                items_temp -= 100;
            }
            priceAfterDiscount = Math.Round(price - ((price/100) * discountPercentage),2);
        }
        private void CalculatePrice()
        {
            double helo = deadlineOfRealizationCost;

            priceBeforeDiscount = Math.Round((itemsQuantity * (formatCost + grammagePrice + printOptionsCost
                                                     + colorfulPaperCost)) + deadlineOfRealizationCost, 2);
            int a = 1;
            CalculateDiscount(priceBeforeDiscount);
        }
        public double DeadlineOfRealizationCost
        {
            get { return deadlineOfRealizationCost; }
            set { deadlineOfRealizationCost = value; }
        }
        public string DeadlineOfRealization
        {
            get { return deadlineOfRealization; }
            set { deadlineOfRealization = value; }
        }
        public double PrintOptionsCost
        {
            get { return printOptionsCost; }
            set { printOptionsCost = value; }
        }
        public double ColorfulPaperCost
        {
            get { return colorfulPaperCost; }
            set { colorfulPaperCost = value; }
        }
        public double GrammagePrice
        {
            get { return grammagePrice; } 
            set { grammagePrice=value; } 
        }
        public int ItemsQuantity 
        {
            get { return itemsQuantity; } 
            set { itemsQuantity = value; }
        }
        public string Format
        {
            get { return format; }
            set { format = value; }
        }
        public string Grammage
        {
            get { return grammage; }
            set { grammage = value; }
        }
        public List<string> PrintOptions
        {
            get { return printOptions; }
            set { printOptions = value; }
        }
        public int DiscountPercentage
        {
            get { return discountPercentage; }
            set { discountPercentage = value; }
        }
        public double FormatCost
        {
            get { return formatCost; }
            set { formatCost = value; }
        }
        public double PriceBeforeDiscount
        {
            get { return priceBeforeDiscount; }
            set { priceBeforeDiscount = value; }
        }
        public double PriceAfterDiscount
        {
            get { return priceAfterDiscount; }
            set { priceAfterDiscount = value; }
        }
    }
}
