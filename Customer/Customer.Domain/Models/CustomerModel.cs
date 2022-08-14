using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Models
{
    public class CustomerModel
    {
        private protected readonly char[] ForbiddenChars = { '!', '@', '#', '$', '%', '&', '*', '\\' };
        private protected readonly string NullErrorMessage = "顧客名が入力されていません";
        private protected readonly string ForbiddenErrorMessage = "顧客名に禁則文字が含まれています";
        private protected readonly string EmailErrorMessage = "E-Mailアドレスが正しくありません";

        public string CustomerId { get; }

        public string CustomerName { get; }

        public string Email { get; }


        public CustomerModel(string customerId, string customerName, string email)
        {
            this.CustomerId = customerId;

            CheckCustomerName(customerName);

            this.CustomerName = customerName;

            if (!CheckEmail(email))
                throw new ArgumentException(EmailErrorMessage);

            this.Email = email;
        }

        /// <summary>
        /// null, 空白, 禁則文字チェック
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        private void CheckCustomerName(string customerName)
        {
            if(string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentNullException(NullErrorMessage);

            if (customerName.Any(x => ForbiddenChars.Contains(x)))
                throw new ArgumentException(ForbiddenErrorMessage);
        }

        /// <summary>
        /// Emailチェック
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool CheckEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
                return false;

            try
            {
                var emailAddress = new System.Net.Mail.MailAddress(email);
                return emailAddress.Address == trimmedEmail;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }

        }
    }
}