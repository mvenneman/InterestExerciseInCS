using System.Collections.Generic;
using System.Linq;
using System;

namespace InterestExerciseNS
{
    public class Person : InterestBearer
    {
        public long personId;
        public IList<Wallet> wallets;

        public Person(long personId)
        {
            this.personId = personId;
        }

        public void addWallet(Wallet wallet)
        {
            if (this.wallets == null)
            {
                this.wallets = new List<Wallet>();
            }
            this.wallets.Add(wallet);
        }

        public decimal calculateCurrentInterestOwed()
        {
            return Math.Round(this.wallets.Sum(wallet => wallet.calculateCurrentInterestOwed()), 2);
        }
    }
}
