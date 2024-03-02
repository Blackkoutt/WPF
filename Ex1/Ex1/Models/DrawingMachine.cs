using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex1.Models
{
    public class DrawingMachine
    {
        private List<string> tickets;
        public DrawingMachine()
        {
            tickets = new List<string>();
            AddTicket("abc");
            AddTicket("def");
        }
        public void AddTicket (string ticket)
        {
            if(ticket.Trim() == "")
            {
                throw new Exception("Ticket cannot contains only spaces or be empty!");           
            }
            else if (tickets.Contains(ticket))
            {
                throw new Exception("Such ticket already exists in drawing machine!");
            }
            else
            {
                tickets.Add(ticket);
            }
            
        }
        public string GetRandomTicket()
        {
            if (IsTicketsAvailable())
            {
                Random rnd = new Random();
                int randomIndex = rnd.Next(0, tickets.Count);
                string ticket = tickets[randomIndex];
                tickets.Remove(ticket);
                return ticket;
            }
            throw new Exception("List Is Empty !");
        }
        public bool IsTicketsAvailable()
        {
            return tickets.Any();
        }
        public List<string> Tickets
        {
            get { return tickets; }
        }
    }
}
