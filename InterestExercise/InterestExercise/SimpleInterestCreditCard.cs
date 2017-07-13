using System;

namespace InterestExerciseNS
{
    public class SimpleInterestCreditCard : CreditCard
    {
        public SimpleInterestCreditCard(long cardId, CreditCardType cardType, decimal interestRate) : base(cardId, cardType, interestRate) {}

        public override decimal calculateCurrentInterestOwed()
        {
            return Math.Round(balancePastDue * interestRate, 2);
        }
    }
}