using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.Repository.Repositories;

namespace Customer.Test
{
    /// <summary>
    /// 顧客情報作成処理に関するテスト
    /// </summary>
    public class CreateCustomerSampleTest
    {
        /// <summary>
        /// クラスの作成ができること
        /// </summary>
        [Fact]
        public void CustomerRepositoryを作成_成功する()
        {
            CustomerRepositorySample repository = new CustomerRepositorySample();

            Assert.NotNull(repository);
        }

        /// <summary>
        /// まず最低限のメソッドのみを作ります
        /// Domain: ICustomerRepository
        /// Repository: CustomerRepository
        /// ここにCreateCustomerを作成しています
        /// </summary>
        [Fact]
        public void Customerを作成_成功する()
        {
            // Arrange
            CustomerRepositorySample repository = new CustomerRepositorySample();

            // Act
            var isCreated = repository.CreateCustomer01("高橋");

            // Assert
            Assert.True(isCreated);
        }

        /// <summary>
        /// 名前が空白・Nullのチェックをします
        /// </summary>
        [Fact]
        public void Customerを作成_EmptyOrNullチェックする_エラーとなる作りことができない()
        {
            // Arrange
            CustomerRepositorySample repository = new CustomerRepositorySample();

            // Act
            // step1
            //bool isCreatedByEmpty = repository.CreateCustomer02(string.Empty);
            //bool isCreatedByNull = repository.CreateCustomer02(null);
            // CreateCustomer02 は return true; となっているので、最初のテストは失敗します
            // そこでCreateCustomer02を修正します
            //          ↓
            //
            // step2 修正後
            bool isCreatedByEmpty = repository.CreateCustomer02_modified(string.Empty);
            bool isCreatedByBlank = repository.CreateCustomer02_modified("   ");
            bool isCreatedByNull = repository.CreateCustomer02_modified(null);

            // Assert
            Assert.False(isCreatedByEmpty);
            Assert.False(isCreatedByBlank);
            Assert.False(isCreatedByNull);
        }

        /// <summary>
        /// 名前に使えない文字がないかのチェックをします
        /// </summary>
        [Fact]
        public void Customerを作成_禁則文字チェックする_エラーとなる作りことができない()
        {
            // Arrange
            CustomerRepositorySample repository = new CustomerRepositorySample();
            char[] forbiddenChars = { '!', '@', '#', '$', '%', '&', '*', '\\'};

            // Act
            // step1 これは失敗します　そこで　修正します
            //bool isCreatedWithExclamation = repository.CreateCustomer02_modified(@"ユーザー!");
            //bool isCreatedWithAtt = repository.CreateCustomer02_modified(@"ユーザー@");
            //bool isCreatedWithSharp = repository.CreateCustomer02_modified(@"ユーザー#");
            //bool isCreatedWithDollar = repository.CreateCustomer02_modified(@"ユーザー$");
            //bool isCreatedWithPercent = repository.CreateCustomer02_modified(@"ユーザー%");
            //bool isCreatedWithAnd= repository.CreateCustomer02_modified(@"ユーザー&");
            //bool isCreatedWithAsterisk = repository.CreateCustomer02_modified(@"ユーザー*");
            //bool isCreatedWithSlash = repository.CreateCustomer02_modified(@"ユーザー\");
            // そこでCreateCustomer02_modifiedを修正します
            //          ↓
            //
            // step2 修正後
            bool isCreatedWithExclamation = repository.CreateCustomer02_modified02(@"ユーザー!");
            bool isCreatedWithAtt = repository.CreateCustomer02_modified02(@"ユーザー@");
            bool isCreatedWithSharp = repository.CreateCustomer02_modified02(@"ユーザー#");
            bool isCreatedWithDollar = repository.CreateCustomer02_modified02(@"ユーザー$");
            bool isCreatedWithPercent = repository.CreateCustomer02_modified02(@"ユーザー%");
            bool isCreatedWithAnd = repository.CreateCustomer02_modified02(@"ユーザー&");
            bool isCreatedWithAsterisk = repository.CreateCustomer02_modified02(@"ユーザー*");
            bool isCreatedWithSlash = repository.CreateCustomer02_modified02(@"ユーザー\");

            // Assert
            Assert.False(isCreatedWithExclamation);
            Assert.False(isCreatedWithAtt);
            Assert.False(isCreatedWithSharp);
            Assert.False(isCreatedWithDollar);
            Assert.False(isCreatedWithPercent);
            Assert.False(isCreatedWithAnd);
            Assert.False(isCreatedWithAsterisk);
            Assert.False(isCreatedWithSlash);
        }
    }
}

    