using System;

namespace Models
{
    public class Transaction
    {
        public DateTime Time { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public double Amount { get; set; }
    }
}
