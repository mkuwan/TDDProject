using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Test
{
    public class CustomerRepositoryTest
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

        // このサンプルプロジェクトでは、CreateCustomer, MakeCart, MakePaymentなどは機能テストとなります
        // DDDでの集合(Aggregation)として、OrderAggregationでは統合テストとなるでしょう

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
    }
}
