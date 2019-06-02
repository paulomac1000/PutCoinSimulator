namespace Models.AddingThread.Enums
{
    public enum FakeTransactionType
    {
        /// <summary>
        /// Generated block has a bad signature
        /// </summary>
        BadSignature,

        /// <summary>
        /// Generated duplicated blocks which double-charge sender of the transaction
        /// </summary>
        DoubleSpending,

        /// <summary>
        /// Generated block containing unproper transaction amount
        /// </summary>
        BadAmount
    }
}
