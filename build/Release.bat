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
dotnet pack src/Cosmos.Data/Cosmos.Data.csproj                                                               -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Abstractions/Cosmos.Data.Abstractions.csproj                                     -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Dependency/Cosmos.Data.Dependency.csproj                                     -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Statements/Cosmos.Data.Statements.csproj                                         -c Release -o nuget_packages --no-restore

::data dialects
dotnet pack src/Cosmos.Data.Extensions.MySql/Cosmos.Data.Extensions.MySql.csproj                             -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Extensions.MySqlConnector/Cosmos.Data.Extensions.MySqlConnector.csproj           -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Extensions.Oracle/Cosmos.Data.Extensions.Oracle.csproj                           -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Extensions.PostgreSql/Cosmos.Data.Extensions.PostgreSql.csproj                   -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Extensions.SqlServer/Cosmos.Data.Extensions.SqlServer.csproj                     -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Extensions.Sqlite/Cosmos.Data.Extensions.Sqlite.csproj                           -c Release -o nuget_packages --no-restore

::sql-kata
dotnet pack src/Cosmos.Data.Extensions.SqlKata/Cosmos.Data.Extensions.SqlKata.csproj                         -c Release -o nuget_packages --no-restore

::extra
dotnet pack src/Cosmos.Data.Extensions.AspectCoreInjector/Cosmos.Data.Extensions.AspectCoreInjector.csproj             -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Extensions.Autofac/Cosmos.Data.Extensions.Autofac.csproj                                   -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Data.Extensions.DependencyInjection/Cosmos.Data.ExExtensionstra.DependencyInjection.csproj           -c Release -o nuget_packages --no-restore

for /R "nuget_packages" %%s in (*symbols.nupkg) do (
    del "%%s"
)

echo.
echo.

::push nuget packages to server
for /R "nuget_packages" %%s in (*.nupkg) do ( 	
    dotnet nuget push "%%s" -s "Release" --skip-duplicate
	echo.
)

::get back to build folder
cd build