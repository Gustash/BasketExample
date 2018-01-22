using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BasketTask.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestBasket1()
        {
            string expectedFinalValue = "60.15";
            int expectedMessageNum = 0;

            Item hat = new Item("Hat", 1050, Categories.Clothing);
            Item jumper = new Item("Jumper", 5465, Categories.Clothing);

            GiftVoucher voucher = new GiftVoucher(500);

            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(hat, 1);
            cart.AddItem(jumper, 1);
            cart.ApplyVoucher(voucher);

            string actualFinalValue = cart.FinalValue;
            List<string> actualMessages = cart.Messages;

            Assert.AreEqual(expectedFinalValue, actualFinalValue);
            Assert.AreEqual(expectedMessageNum, actualMessages.Count);
        }

        [TestMethod]
        public void TestBasket2()
        {
            string expectedFinalValue = "51.00";
            int expectedMessageNum = 1;
            string expectedMessage = "There are no products in your basket applicable to voucher Voucher YYY-YYY.";

            Item hat = new Item("Hat", 2500, Categories.Clothing);
            Item jumper = new Item("Jumper", 2600, Categories.Clothing);

            OfferVoucher voucher = new OfferVoucher(500, 5000, Categories.HeadGear);

            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(hat, 1);
            cart.AddItem(jumper, 1);
            cart.ApplyVoucher(voucher);

            string actualFinalValue = cart.FinalValue;
            List<string> actualMessages = cart.Messages;

            Assert.AreEqual(expectedFinalValue, actualFinalValue);
            Assert.AreEqual(expectedMessageNum, actualMessages.Count);
            Assert.AreEqual(expectedMessage, actualMessages[0]);
        }

        [TestMethod]
        public void TestBasket3()
        {
            string expectedFinalValue = "51.00";
            int expectedMessageNum = 0;

            Item hat = new Item("Hat", 2500, Categories.Clothing);
            Item jumper = new Item("Jumper", 2600, Categories.Clothing);
            Item headLight = new Item("Head Light", 350, Categories.HeadGear);

            OfferVoucher voucher = new OfferVoucher(500, 5000, Categories.HeadGear);

            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(hat, 1);
            cart.AddItem(jumper, 1);
            cart.AddItem(headLight, 1);
            cart.ApplyVoucher(voucher);

            string actualFinalValue = cart.FinalValue;
            List<string> actualMessages = cart.Messages;

            Assert.AreEqual(expectedFinalValue, actualFinalValue);
            Assert.AreEqual(expectedMessageNum, actualMessages.Count);
        }

        [TestMethod]
        public void TestBasket4()
        {
            string expectedFinalValue = "41.00";
            int expectedMessageNum = 0;

            Item hat = new Item("Hat", 2500, Categories.Clothing);
            Item jumper = new Item("Jumper", 2600, Categories.Clothing);

            GiftVoucher voucher1 = new GiftVoucher(500);
            OfferVoucher voucher2 = new OfferVoucher(500, 5000);

            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(hat, 1);
            cart.AddItem(jumper, 1);
            cart.ApplyVoucher(voucher1);
            cart.ApplyVoucher(voucher2);

            string actualFinalValue = cart.FinalValue;
            List<string> actualMessages = cart.Messages;

            Assert.AreEqual(expectedFinalValue, actualFinalValue);
            Assert.AreEqual(expectedMessageNum, actualMessages.Count);
        }

        [TestMethod]
        public void TestBasket5()
        {
            string expectedFinalValue = "55.00";
            int expectedMessageNum = 1;
            string expectedMessage = "You have not reached the spend threshold for voucher YYY-YYY. Spend another £25.01 to receive £5.00 discount from your basket total.";

            Item hat = new Item("Hat", 2500, Categories.Clothing);
            Item giftVoucher = new Item("£30 Gift Voucher", 3000, Categories.Voucher);

            OfferVoucher voucher = new OfferVoucher(500, 5000);

            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(hat, 1);
            cart.AddItem(giftVoucher, 1);
            cart.ApplyVoucher(voucher);

            string actualFinalValue = cart.FinalValue;
            List<string> actualMessages = cart.Messages;

            Assert.AreEqual(expectedFinalValue, actualFinalValue);
            Assert.AreEqual(expectedMessageNum, actualMessages.Count);
            Assert.AreEqual(expectedMessage, actualMessages[0]);
        }

        [TestMethod]
        public void TestGiftVoucherNotApplyToPurchasedGiftVouchers()
        {
            string expectedFinalValue = "5.00";
            int expectedMessageNum = 0;
            
            Item giftVoucher = new Item("£5 Gift Voucher", 500, Categories.Voucher);

            GiftVoucher voucher = new GiftVoucher(500);

            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(giftVoucher, 1);
            cart.ApplyVoucher(voucher);

            string actualFinalValue = cart.FinalValue;
            List<string> actualMessages = cart.Messages;

            Assert.AreEqual(expectedFinalValue, actualFinalValue);
            Assert.AreEqual(expectedMessageNum, actualMessages.Count);
        }

        [TestMethod]
        public void TestOfferVoucherAppliedToOnlyVouchers()
        {
            string expectedFinalValue = "35.00";
            int expectedMessageNum = 1;
            string expectedMessage = "This voucher is most likely an error.";

            Item giftVoucher = new Item("£30 Gift Voucher", 3000, Categories.Voucher);
            Item hat = new Item("Hat", 500, Categories.Clothing);

            OfferVoucher voucher = new OfferVoucher(500, 500, Categories.Voucher);

            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(giftVoucher, 1);
            cart.AddItem(hat, 1);
            cart.ApplyVoucher(voucher);

            string actualFinalValue = cart.FinalValue;
            List<string> actualMessages = cart.Messages;

            Assert.AreEqual(expectedFinalValue, actualFinalValue);
            Assert.AreEqual(expectedMessageNum, actualMessages.Count);
            Assert.AreEqual(expectedMessage, actualMessages[0]);
        }

        [TestMethod]
        public void TestApplyingMoreThanOneOfferVoucher()
        {
            string expectedFinalValue = "30.00";
            int expectedMessageNum = 1;
            string expectedMessage = "One offer voucher was already applied.";

            Item hat = new Item("Hat", 5000, Categories.Clothing);

            OfferVoucher voucher1 = new OfferVoucher(2000, 4000);
            OfferVoucher voucher2 = new OfferVoucher(500, 1000);

            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(hat, 1);
            cart.ApplyVoucher(voucher1);
            cart.ApplyVoucher(voucher2);

            string actualFinalValue = cart.FinalValue;
            List<string> actualMessages = cart.Messages;

            Assert.AreEqual(expectedFinalValue, actualFinalValue);
            Assert.AreEqual(expectedMessageNum, actualMessages.Count);
            Assert.AreEqual(expectedMessage, actualMessages[0]);
        }

        [TestMethod]
        public void TestNegativeValueCartGiftVoucher()
        {
            string expectedFinalValue = "0.00";
            int expectedMessageNum = 0;

            Item hat = new Item("Hat", 500, Categories.Clothing);

            GiftVoucher voucher = new GiftVoucher(2000);

            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(hat, 1);
            cart.ApplyVoucher(voucher);

            string actualFinalValue = cart.FinalValue;
            List<string> actualMessages = cart.Messages;

            Assert.AreEqual(expectedFinalValue, actualFinalValue);
            Assert.AreEqual(expectedMessageNum, actualMessages.Count);
        }

        [TestMethod]
        public void TestNegativeValueCartOfferVoucher()
        {
            string expectedFinalValue = "0.00";
            int expectedMessageNum = 0;

            Item hat = new Item("Hat", 1000, Categories.Clothing);

            OfferVoucher voucher = new OfferVoucher(2000, 500);

            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(hat, 1);
            cart.ApplyVoucher(voucher);

            string actualFinalValue = cart.FinalValue;
            List<string> actualMessages = cart.Messages;

            Assert.AreEqual(expectedFinalValue, actualFinalValue);
            Assert.AreEqual(expectedMessageNum, actualMessages.Count);
        }

        [TestMethod]
        public void TestNegativeValueCartOfferAndGiftVoucher()
        {
            string expectedFinalValue = "0.00";
            int expectedMessageNum = 0;

            Item hat = new Item("Hat", 1500, Categories.Clothing);

            OfferVoucher voucher1 = new OfferVoucher(400, 1000);
            GiftVoucher voucher2 = new GiftVoucher(2000);

            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(hat, 1);
            cart.ApplyVoucher(voucher1);
            cart.ApplyVoucher(voucher2);

            string actualFinalValue = cart.FinalValue;
            List<string> actualMessages = cart.Messages;

            Assert.AreEqual(expectedFinalValue, actualFinalValue);
            Assert.AreEqual(expectedMessageNum, actualMessages.Count);
        }
    }
}
