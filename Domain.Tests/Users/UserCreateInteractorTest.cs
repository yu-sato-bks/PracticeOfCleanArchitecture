using Domain.Application.Users;
using Domain.Domain.Users;
using Moq;
using UseCase;

namespace Domain.Test;

[TestClass]
public class UserCreateInteractorTest
{
    [TestMethod]
    public void CreateUserTest()
    {
        // interacotrにはIUserRepositoryとIUserCreatePresenterが必要
        // ただ、UTとしては実際のものである必要はなくモックでよい
        // というかモックでテストするようにしたい。（実装クラスの変更による期待値変更を考慮したくない）
        // .NET だとMoqというパッケージがあるのでそれを使うことでモッククラスを簡単に作成できる。
        var repositoryMock = new Mock<IUserRepository>();
        var presenterMock = new Mock<IUserCreatePresenter>();
        // モックではメソッドが呼ばれたときに何を返すかを定義できる。
        // テストとして妥当なレスポンスを定義する。
        string savedUser = string.Empty;
        repositoryMock.Setup(x => x.Save(It.IsAny<User>())).Callback<User>(u => savedUser = u.UserName);
        // 例えばFindByNameで特定文字列の場合だけユーザーを返すようにすれば例外テストも出来る。
        repositoryMock.Setup(x => x.FindByUserName(It.IsAny<string>())).Returns((User)null);
        // 設定された進捗をテストするために保持する
        var progressResults = new List<int>();
        presenterMock.Setup(x => x.Progress(It.IsAny<int>()))
            .Callback<int>(progressResults.Add);
        presenterMock.Setup(x => x.Complete(It.IsAny<UserCreateResponse>()));

        // 引数としてモックを渡すことで、テストしたいクラスの処理だけを見ることが出来る。
        var interactor = new UserCreateInteractor(repositoryMock.Object, presenterMock.Object);
        interactor.Handle(new UserCreateRequest("test user"));

        Assert.IsTrue(savedUser == "test user");
        Assert.IsTrue(progressResults.Any());
        Assert.IsTrue(progressResults.Count == 4);
        Assert.IsTrue(progressResults.Contains(10));
        Assert.IsTrue(progressResults.Contains(30));
        Assert.IsTrue(progressResults.Contains(50));
        Assert.IsTrue(progressResults.Contains(90), "Progress 90"); // Fail
    }
}
