using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterestExerciseNS;
using System.Linq;

namespace InterestExerciseTest
{
    [TestClass]
    public class InterestCalculationTests
    {
        //Hard-coded interest rates for testing
        //Might pull current interest rates from database via service in production system
        private static readonly decimal VISA_INTRST_RT = new decimal(0.10);
        private static readonly decimal MC_INTRST_RT = new decimal(0.05);
        private static readonly decimal DISC_INTRST_RT = new decimal(0.01);

        [TestMethod]
        /// <summary>
		/// 1 person has 1 wallet and 3 cards (1 Visa, 1 MC, 1 Discover)
		/// Each Card has a balance of $100
		/// 
		/// The interest for the Visa should be 10% of $100 or $10
		/// The interest for the MC should be 5% of $100 or $5
		/// The interest for the Discover should be 1% of $100 or $1
		/// 
		/// The interest for the Person should be the sum or $16
		/// </summary>
        public void onePersonOneWalletThreeCards()
        {
            Person person = new Person(1L);

            Wallet wallet = new Wallet(1L);

            CreditCard visa = new SimpleInterestCreditCard(1L, CreditCardType.VISA, VISA_INTRST_RT);
            visa.balancePastDue = new decimal(100);

            CreditCard mastercard = new SimpleInterestCreditCard(2L, CreditCardType.MASTERCARD, MC_INTRST_RT);
            mastercard.balancePastDue = new decimal(100);

            CreditCard discover = new SimpleInterestCreditCard(3L, CreditCardType.DISCOVER, DISC_INTRST_RT);
            discover.balancePastDue = new decimal(100);

            wallet.addCreditCard(visa);
            wallet.addCreditCard(mastercard);
            wallet.addCreditCard(discover);

            person.addWallet(wallet);

            //The Visa with ID# 1 should have $10 in interest
            CreditCard card = person.wallets[0].creditCards.Where(c => c.cardId == 1L).First();
            Assert.AreEqual(new decimal(10.00), card.calculateCurrentInterestOwed());

            //The MasterCard with ID# 2 should have $5 in interest
            card = person.wallets[0].creditCards.Where(c => c.cardId == 2L).First();
            Assert.AreEqual(new decimal(5.00), card.calculateCurrentInterestOwed());

            //The Discover with ID# 3 should have $1 in interest
            card = person.wallets[0].creditCards.Where(c => c.cardId == 3L).First();
            Assert.AreEqual(new decimal(1.00), card.calculateCurrentInterestOwed());

            //The person should have the sum interest of $16
            Assert.AreEqual(new decimal(16.00), person.calculateCurrentInterestOwed());
        }

        [TestMethod]
        /// <summary>
		/// 1 person has 2 wallets
		/// Wallet 1 has a Visa and Discover
		/// Wallet 2 has a MC
		/// Each card has $100 balance
		/// 
		/// The interest for Wallet 1 should be 10% of $100 (Visa) plus 1% of $100 (Discover) or $11
		/// The interest for Wallet 2 should be 5% of $100 (MasterCard) or $5
		/// 
		/// The interest for the Person should be the sum or $16
		/// </summary>
        public void onePersonTwoWallets()
        {
            Person person = new Person(1L);

            Wallet wallet1 = new Wallet(1L);
            Wallet wallet2 = new Wallet(2L);

            CreditCard visa = new SimpleInterestCreditCard(1L, CreditCardType.VISA, VISA_INTRST_RT);
            visa.balancePastDue = new decimal(100);

            CreditCard discover = new SimpleInterestCreditCard(2L, CreditCardType.DISCOVER, DISC_INTRST_RT);
            discover.balancePastDue = new decimal(100);

            wallet1.addCreditCard(visa);
            wallet1.addCreditCard(discover);

            person.addWallet(wallet1);

            CreditCard mastercard = new SimpleInterestCreditCard(3L, CreditCardType.MASTERCARD, MC_INTRST_RT);
            mastercard.balancePastDue = new decimal(100);

            wallet2.addCreditCard(mastercard);

            person.addWallet(wallet2);

            //Wallet 1 (ID# 1) should have an interest of $11
            Wallet wallet = person.wallets.Where(w => w.walletId == 1L).First();
            Assert.AreEqual(new decimal(11.00), wallet.calculateCurrentInterestOwed());

            //Wallet 2 (ID# 2) should have an interest of $5
            wallet = person.wallets.Where(w => w.walletId == 2L).First();
            Assert.AreEqual(new decimal(5.00), wallet.calculateCurrentInterestOwed());

            //The person should have the sum interest of $16
            Assert.AreEqual(new decimal(16.00), person.calculateCurrentInterestOwed());
        }

        [TestMethod]
        /// <summary>
		/// 2 people have 1 wallet each
		/// Person 1 has 1 wallet with 3 cards (1 MC, 1 Visa, 1 Discover)
		/// Person 2 has 1 wallet with 2 cards (1 Visa, 1 MC)
		/// All cards in all wallets for both people have a $100 balance
		/// 
		/// The interest for Person 1 should be $16.00
		/// The interest for Person 2 should be $15.00
		/// 
		/// Since each person has only the one wallet, 
		/// the interest for the Wallet should be the same as the total for the person.
		/// </summary>
        public void twoPeopleOneWalletEach()
        {
            Person person1 = new Person(1L);
            Person person2 = new Person(2L);

            Wallet wallet1 = new Wallet(1L);
            Wallet wallet2 = new Wallet(2L);

            CreditCard mastercard1 = new SimpleInterestCreditCard(1L, CreditCardType.MASTERCARD, MC_INTRST_RT);
            mastercard1.balancePastDue = new decimal(100);

            CreditCard visa1 = new SimpleInterestCreditCard(2L, CreditCardType.VISA, VISA_INTRST_RT);
            visa1.balancePastDue = new decimal(100);

            CreditCard discover = new SimpleInterestCreditCard(3L, CreditCardType.DISCOVER, DISC_INTRST_RT);
            discover.balancePastDue = new decimal(100);

            wallet1.addCreditCard(mastercard1);
            wallet1.addCreditCard(visa1);
            wallet1.addCreditCard(discover);

            person1.addWallet(wallet1);

            CreditCard visa2 = new SimpleInterestCreditCard(4L, CreditCardType.VISA, VISA_INTRST_RT);
            visa2.balancePastDue = new decimal(100);

            CreditCard mastercard2 = new SimpleInterestCreditCard(5L, CreditCardType.MASTERCARD, MC_INTRST_RT);
            mastercard2.balancePastDue = new decimal(100);

            wallet2.addCreditCard(visa2);
            wallet2.addCreditCard(mastercard2);

            person2.addWallet(wallet2);

            //Wallet 1 (ID# 1) should have an interest of $16
            Wallet wallet = person1.wallets.Where(w => w.walletId == 1L).First();
            Assert.AreEqual(new decimal(16.00), wallet.calculateCurrentInterestOwed());

            //Wallet 2 (ID# 2) should have an interest of $15
            wallet = person2.wallets.Where(w => w.walletId == 2L).First();
            Assert.AreEqual(new decimal(15.00), wallet.calculateCurrentInterestOwed());

            //Person 1 should have an interest of $16
            Assert.AreEqual(new decimal(16.00), person1.calculateCurrentInterestOwed());

            //Person 2 should have an interest of $15
            Assert.AreEqual(new decimal(15.00), person2.calculateCurrentInterestOwed());
        }
            
     }
}
