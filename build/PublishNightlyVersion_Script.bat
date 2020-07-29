@echo off

echo =======================================================================
echo Cosmos.Data
echo =======================================================================

::go to parent folder
cd ..

::create nuget_packages
if not exist nuget_packages (
    md nuget_packages
    echo Created nuget_packages folder.
)

::clear nuget_packages
for /R "nuget_packages" %%s in (*) do (
    del "%%s"
)
echo Cleaned up all nuget packages.
echo.

::start to package all projects
dotnet pack src/Cosmos.Data/Cosmos.Data._build.csproj                                                               -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Abstractions/Cosmos.Data.Abstractions._build.csproj                                     -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Statements/Cosmos.Data.Statements._build.csproj                                         -c Release -o nuget_packages --no-restore

::data dependency
dotnet pack src/Cosmos.Data.Extensions.Autofac/Cosmos.Data.Extensions.Autofac._build.csproj                         -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Extensions.AspectCoreInjector/Cosmos.Data.Extensions.AspectCoreInjector._build.csproj   -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Extensions.DependencyInjection/Cosmos.Data.Extensions.DependencyInjection._build.csproj -c Release -o nuget_packages --no-restore

::data dialects
dotnet pack src/Cosmos.Data.Extensions.MySql/Cosmos.Data.Extensions.MySql._build.csproj                             -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Extensions.MySqlConnector/Cosmos.Data.Extensions.MySqlConnector._build.csproj           -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Extensions.Oracle/Cosmos.Data.Extensions.Oracle._build.csproj                           -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Extensions.PostgreSql/Cosmos.Data.Extensions.PostgreSql._build.csproj                   -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Extensions.SqlServer/Cosmos.Data.Extensions.SqlServer._build.csproj                     -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Extensions.Sqlite/Cosmos.Data.Extensions.Sqlite._build.csproj                           -c Release -o nuget_packages --no-restore

::sql-kata
dotnet pack src/Cosmos.Data.Extensions.SqlKata/Cosmos.Data.Extensions.SqlKata._build.csproj                         -c Release -o nuget_packages --no-restore

for /R "nuget_packages" %%s in (*symbols.nupkg) do (
    del "%%s"
)

echo.
echo.

::push nuget packages to server
for /R "nuget_packages" %%s in (*.nupkg) do (
    dotnet nuget push "%%s" -s "Nightly" --skip-duplicate
	echo.
)

::get back to build folder
cd build