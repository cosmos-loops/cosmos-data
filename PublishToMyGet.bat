@echo off

::create nuget_pub
if not exist nuget_pub (
    md nuget_pub
)

::clear nuget_pub
for /R "nuget_pub" %%s in (*) do (
    del %%s
)

::Data
dotnet pack src/Cosmos.Data -c Release -o ../../nuget_pub
dotnet pack src/Cosmos.Data.Abstractions -c Release -o ../../nuget_pub
dotnet pack src/Cosmos.Data.Statements -c Release -o ../../nuget_pub

::Extensions for Data dependency
dotnet pack extensions/dependency/src/Cosmos.Data.Extensions.Autofac -c Release -o ../../../../nuget_pub
dotnet pack extensions/dependency/src/Cosmos.Data.Extensions.AspectCoreInjector -c Release -o ../../../../nuget_pub
dotnet pack extensions/dependency/src/Cosmos.Data.Extensions.DependencyInjection -c Release -o ../../../../nuget_pub

::Extensions for Data dialects
dotnet pack extensions/dialects/src/Cosmos.Data.Extensions.MySql -c Release -o ../../../../nuget_pub
dotnet pack extensions/dialects/src/Cosmos.Data.Extensions.MySqlConnector -c Release -o ../../../../nuget_pub
dotnet pack extensions/dialects/src/Cosmos.Data.Extensions.Oracle -c Release -o ../../../../nuget_pub
dotnet pack extensions/dialects/src/Cosmos.Data.Extensions.PostgreSql -c Release -o ../../../../nuget_pub
dotnet pack extensions/dialects/src/Cosmos.Data.Extensions.SqlServer -c Release -o ../../../../nuget_pub
dotnet pack extensions/dialects/src/Cosmos.Data.Extensions.Sqlite -c Release -o ../../../../nuget_pub

::Extensions for SqlKata
dotnet pack extensions/sqlkata/sql/Cosmos.Data.Extensions.SqlKata Release -o ../../../../nuget_pub

for /R "nuget_pub" %%s in (*symbols.nupkg) do (
    del %%s
)

echo.
echo.

set /p key=input key:
set source=https://www.myget.org/F/alexinea/api/v2/package

for /R "nuget_pub" %%s in (*.nupkg) do ( 
    call nuget push %%s %key% -Source %source%	
	echo.
)

pause