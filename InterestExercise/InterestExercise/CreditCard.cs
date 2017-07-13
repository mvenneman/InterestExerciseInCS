namespace InterestExerciseNS
{
    public abstract class CreditCard : InterestBearer
    {
        public long cardId;
        public CreditCardType cardType;
        public decimal interestRate;
        public decimal balancePastDue;

        public CreditCard(long cardId, CreditCardType cardType, decimal interestRate)
        {
            this.cardId = cardId;
            this.interestRate = interestRate;
            this.balancePastDue = decimal.Zero;
        }

        public abstract decimal calculateCurrentInterestOwed();
    }
}