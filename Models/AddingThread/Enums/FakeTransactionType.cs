namespace Models.AddingThread.Enums
{
    public enum FakeTransactionType
    {
        /// <summary>
        /// Generated block has a bad signature
        /// </summary>
        BadSignature = 1,

        /// <summary>
        /// Generated duplicated blocks which double-charge sender of the transaction
        /// </summary>
        DoubleSpending = 2,

        /// <summary>
        /// Generated block containing unproper transaction amount
        /// </summary>
        BadAmount = 3
    }
}
