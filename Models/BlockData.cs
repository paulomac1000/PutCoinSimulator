﻿namespace Models
{
    /// <summary>
    /// Data about transaction used in block
    /// </summary>
    public class BlockData
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public int Amount { get; set; }
    }
}