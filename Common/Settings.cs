namespace Common
{
    public static class Settings
    {
        public static int NumbersOfClients = 50;
        public static string ApplicationName = "PutCoin Simulator";
        public static bool AppStarted = false;
        public static string FirstBlockPreviousBlockHashValue = "0000";
        public static string FirstBlockSenderName = "GENESIS";
        public static int HashingIterations = 100000; //10000000 for ~40s of generating time
        public static int GenerateTransactionMinDelayInSeconds = 10;
        public static int GenerateTransactionMaxDelayInSeconds = 20;
        public static int ChanceForGenerateFakeTransactionInPercent = 25;
    }
}
