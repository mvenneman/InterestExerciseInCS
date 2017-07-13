using System.Collections.Generic;
using System.Linq;
using System;

namespace InterestExerciseNS
{
    public class Wallet : InterestBearer
    {
        public long walletId;
        public List<CreditCard> creditCards;

        public Wallet(long walletId)
        {
            this.walletId = walletId;
        }

        public void addCreditCard(CreditCard creditCard)
        {
            if (this.creditCards == null)
            {
                this.creditCards = new List<CreditCard>();
            }
            this.creditCards.Add(creditCard);
        }

        public decimal calculateCurrentInterestOwed()
        {
            return Math.Round(this.creditCards.Sum(card => card.calculateCurrentInterestOwed()), 2);
        }
    }
}