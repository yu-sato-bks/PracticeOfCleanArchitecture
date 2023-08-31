
using Domain.Application.Users;
using Domain.Domain.Users;
using InMemoryInfrastructure.Users;
using Lib;
using Microsoft.Extensions.DependencyInjection;
using UseCase;

namespace ConsoleApp;

public class Startup
{
    public static void Run()
    {
        SetUp();
        ServiceProvider.Build();
    }

        private static void SetUp() {
            var services = ServiceProvider.ServiceCollection;
            services.AddSingleton<IUserRepository, InMemoryUserRepository>();

            services.AddTransient<IUserCreatePresenter, ConsolePresenter>();
            // ファイルに出力してみたい場合は↑をコメントアウトして↓のコメントを外す
            // services.AddTransient<IUserCreatePresenter, FilePresenter>();

            // Busへの登録
            var busBuilder = new SyncUseCaseBusBuilder(services);
            busBuilder.RegisterUseCase<UserCreateRequest, UserCreateInteractor>();
            busBuilder.RegisterUseCase<UserGetListRequest, UserGetListInteractor>();

            var bus = busBuilder.Build();
            services.AddSingleton(bus);
        }
}
