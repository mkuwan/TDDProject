using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Customer.Domain.Models;
using Customer.Repository.Db;
using Customer.Repository.Dto;
using Customer.Repository.Entities;
using Customer.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Customer.Test
{
    /// <summary>
    /// 基本的にはRepositoryのCRUDのテストをしています
    /// </summary>
    public class CustomerRepositoryTest : IDisposable
    {
        // TDDをあまり丁寧にしようとすると、分かっていること、当たり前のことまでもTDDで作ろうとしてしまいます
        // しかし、実際はコーディングとテストを並行しながら作成していくことになります。
        // 特に、プロジェクトが進んでいくと、アーキテクチャや作成手順などは固定化されていくので
        // コーディングのスピードが上がっていくものです

        // 以上の理由から自分としては、まずModelとClassの作成は先に行う
        // 次にユーザーストーリーに即してメソッドを分解します
        // そして引数・戻り値を確定したメソッドを作成します
        // TDDとして、このメソッドの実装時にテストから作成して実装を作成するという手順で進めるのが良いでしょう

        // メソッドが完成していくことで、ユーザーストーリーが完成し
        // このユーザーストーリーをテストするために、統合テスト(Integration Test)・機能テスト(Functional Test)
        // を作成していく流れとなります
        // 統合テストか機能テストかは、ユーザーストーリーの規模によります。
        // 規模が小さければ機能テスト、規模が大きければ統合テストとなるでしょう

        // DDDでの集合(Aggregation)として、OrderAggregationでは機能テストとなるでしょう
        // ShopAggregationは統合テストとなる可能性が高いです

        // customer-02 ブランチは、CreateCustomerRepositoryTestとしてTDDで単体テストを作り、次に実装を行います
        // 手順としては
        // 1. CustomerRepositoryクラスの作成(DomainのICustomerRepositoryを継承)
        // 2. CustomerModel (Domain Model)の作成
        // 3. CustomerEntity (Repository Model)の作成
        // 4. Test: CreateCustomerをルールに従ってテストを作成します
        //      a) 最初は実装がないのでRed(失敗)となります
        //      b) Green(成功)となるように実装を作成します
        //      C) 必要に応じてリファクタリングをします

        // ここでユニットテストと機能テスト、統合テストとの違いを意識する必要があります
        // ユニットテストではDBへの保存、読み込みといった処理はMockを使用し、メソッドのみのテストに集中します
        // 機能テスト, 統合テストではDBへの保存、読み込みといった処理も含めますが、本番環境ではないため、InMemoryDBなどを使用します
        // 最終テスト(EtoE)などでは本番環境と同じDBでの処理をテストします

        private static DbContextOptions<CustomerDbContext> _options = new DbContextOptions<CustomerDbContext>();

        /// <summary>
        /// setup
        /// </summary>
        public CustomerRepositoryTest()
        {
            _options =
                new DbContextOptionsBuilder<CustomerDbContext>()
                    .UseInMemoryDatabase("TestCustomerDb")
                    .Options;
        }

        /// <summary>
        /// tear down
        /// </summary>
        public void Dispose()
        {
            
        }

        /// <summary>
        /// CustomerModelを作る
        /// </summary>
        [Fact]
        public void CustomerModelを作る_成功()
        {
            // Arrange
            string customerId = Guid.NewGuid().ToString();
            string customerName = "鈴木";
            string email = "suzuki@exsample.com";


            // Act
            object customer = new object();
            var exception = Record.Exception(() =>
            {
                customer = new CustomerModel(customerId, customerName, email);
            });

            // Assert
            Assert.IsType<CustomerModel>(customer);
            Assert.NotNull(customer);
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Customer名がnullや空白_エラーとなる(string customerName)
        {
            // Arrange
            string customerId = Guid.NewGuid().ToString();
            string email = "suzuki@exsample.com";

            // Act
            object customer = new object();
            var exception = Record.Exception(() =>
            {
                customer = new CustomerModel(customerId, customerName, email);
            });

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(typeof(ArgumentNullException), exception.GetType());
        }

        /// <summary>
        /// CustomerName: 禁則文字エラー
        /// </summary>
        [Theory]
        [InlineData('!')]
        [InlineData('#')]
        [InlineData('$')]
        [InlineData('%')]
        [InlineData('&')]
        [InlineData('*')]
        [InlineData('\\')]
        public void Customer名に禁則文字が含まれる_エラーとなる(char invalidCharacter)
        {
            // Arrange
            string customerId = Guid.NewGuid().ToString();
            string customerName = "鈴木" + invalidCharacter;
            string email = "suzuki@exsample.com";

            // Act
            object customer = new object();
            var exception = Record.Exception(() =>
            {
                customer = new CustomerModel(customerId, customerName, email);
            });

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(typeof(ArgumentException), exception.GetType());
            Assert.Equal("顧客名に禁則文字が含まれています", exception.Message);

            
            void Action() => new CustomerModel(customerId, customerName, email);
            var caughtException = Assert.Throws<ArgumentException>(Action);

        }

        [Theory]
        [InlineData("customerId001", "鈴木", "suzuki@example.com")]
        public void CustomerModelをCustomerEntityに変換する_成功(string id, string name, string email)
        {
            // Arrange
            var customer = new CustomerModel(id, name, email);

            // Act
            var entity = CustomerDto.FromCustomerModel(customer);

            // Assert
            Assert.Equal(id, entity.CustomerId);
            Assert.Equal(name, entity.CustomerName);
            Assert.Equal(email, entity.Email);
        }

        [Theory]
        [InlineData("customerId001", "鈴木", "suzuki@example.com")]
        public async Task CustomerをDbContextでDBに保存する_件数1が戻る(string id, string name, string email)
        {
            // Arrange
            CustomerEntity customer = new CustomerEntity();
            customer.CustomerId = id;
            customer.CustomerName = name;
            customer.Email = email;

            await using CustomerDbContext context = new CustomerDbContext(_options);

            // Act
            context.Add(customer);
            var count = await context.SaveChangesAsync();
            var entity = await context.Customers.FindAsync(id);
            
            // Assert
            Assert.Equal(1, count);
            Assert.IsType<CustomerEntity>(entity);
            Assert.Equal(customer.CustomerId, entity.CustomerId);

        }

        [Theory]
        [InlineData("customerId002", "鈴木さん", "suzuki@example.com")]
        public async Task CustomerModelをRepositoryでDBに保存する_件数1が戻る(string id, string name, string email)
        {
            // Arrange
            var customer = new CustomerModel(id, name, email);
            await using CustomerDbContext context = new CustomerDbContext(_options);
            CustomerRepository repository = new CustomerRepository(context);

            // Act
            var count = await repository.SaveCustomer(customer);

            // Assert
            Assert.Equal(1, count);

        }

        [Theory]
        [InlineData("customerId003-1", "鈴木最初", "suzuki@example.com")]
        [InlineData("customerId003-1", "鈴木更新", "suzuki@example.com")]
        public async Task CustomerModelを作成_新規と変更_RepositoryでDBに保存_正しい値となる(string id, string name, string email)
        {
            // Arrange
            var customer = new CustomerModel(id, name, email);
            await using CustomerDbContext context = new CustomerDbContext(_options);
            CustomerRepository repository = new CustomerRepository(context);

            // Act
            var count = await repository.SaveCustomer(customer);
            var savedCustomer = await context.Customers.FindAsync(id);
            var savedCount = await context.Customers.Where(x => x.CustomerId == id).CountAsync();

            // Assert
            Assert.Equal(1, count);
            Assert.Equal(1, savedCount);
            Assert.IsType<CustomerEntity>(savedCustomer);
            Assert.Equal(name, savedCustomer.CustomerName);
        }

        [Theory]
        [InlineData("customerId004-1", "鈴木", "suzuki@example.com")]
        public async Task CustomerModelをDbContextで削除_正しく削除される(string id, string name, string email)
        {
            // Arrange
            CustomerEntity customer = new CustomerEntity();
            customer.CustomerId = id;
            customer.CustomerName = name;
            customer.Email = email;

            await using CustomerDbContext context = new CustomerDbContext(_options);
            context.Add(customer);
            var count = await context.SaveChangesAsync();

            // Act
            var insCustomer = context.Customers.FindAsync(id);
            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
            var deletedCustomer = context.Customers.FindAsync(id);

            // Assert
            Assert.IsType<CustomerEntity>(insCustomer.Result);
            Assert.IsNotType<CustomerEntity>(deletedCustomer);
            Assert.Null(deletedCustomer.Result);

        }


        [Theory]
        [InlineData("customerId004-2", "鈴木", "suzuki@example.com")]
        public async Task CustomerModelをRepositoryで削除_正しく削除される(string id, string name, string email)
        {
            // Arrange
            CustomerEntity customer = new CustomerEntity();
            customer.CustomerId = id;
            customer.CustomerName = name;
            customer.Email = email;

            await using CustomerDbContext context = new CustomerDbContext(_options);
            context.Add(customer);
            var count = await context.SaveChangesAsync();
            CustomerRepository repository = new CustomerRepository(context);

            // Act
            await repository.Delete(customer.CustomerId);
            var deletedCustomer = context.Customers.FindAsync(customer.CustomerId);

            // Assert
            Assert.IsNotType<CustomerEntity>(deletedCustomer);
            Assert.Null(deletedCustomer.Result);

        }

    }
}
