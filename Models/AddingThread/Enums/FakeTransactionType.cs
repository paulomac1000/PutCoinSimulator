namespace Models.AddingThread.Enums
{
    public enum FakeTransactionType
    {
        /// <summary>
        /// Generated transaction has a bad signature
        /// </summary>
        BadSignature = 1,

        /// <summary>
        /// Generated duplicated transaction which double-charge sender of the transaction
        /// </summary>
        DoubleSpending = 2,

        /// <summary>
        /// Generated transaction containing unproper transaction amount
        /// </summary>
        BadAmount = 3
    }
}
