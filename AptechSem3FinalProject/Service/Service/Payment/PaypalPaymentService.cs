using System;
using System.Collections.Generic;
using Context.Database;
using PayPal.Api;

namespace Service.Service.Payment
{
    public class PaypalPaymentService
    {
        private PayPal.Api.Payment _payment;
        private string _currency;

        public PayPal.Api.Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution {payer_id = payerId};
            _payment = new PayPal.Api.Payment {id = paymentId};
            return _payment.Execute(apiContext, paymentExecution);
        }

        public PayPal.Api.Payment CreatePayment(APIContext apiContext, string redirectUrl, List<Course> courses, string currencyCode)
        {
            long? totalPrice = 0;
            _currency = currencyCode;
            var itemList = new ItemList(){ items = new List<Item>() };
            foreach (var course in courses)
            {
                var item = new Item();
                item.name = course.Title;
                item.currency = _currency;
                item.price = course.Price.ToString();
                item.quantity = "1";
                item.sku = course.Id.ToString();
                totalPrice += course.Price;
                itemList.items.Add(item);
            }                

            var payer = new Payer {payment_method = "paypal"};

            // Configure Redirect Urls here with RedirectUrls object
            var redirectUrls = new RedirectUrls
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            // Adding Tax, shipping and Subtotal details
            var details = new Details
            {
                tax = "1",
                shipping = "1",
                subtotal = "1"
            };

            //Final amount with details
            var amount = new Amount
            {
                currency = _currency,
                total = totalPrice.ToString()
                //details = details
            };

            var transactionList = new List<Transaction>
            {
                new Transaction
                {
                    description = "Transaction description",
                    invoice_number = Guid.NewGuid().ToString("N"),
                    amount = amount,
                    item_list = itemList
                }
            };

            _payment = new PayPal.Api.Payment
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirectUrls
            };
            return _payment.Create(apiContext);
        }
    }
}