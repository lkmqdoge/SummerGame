run:
    dotnet run --project ./SummerGame.Desktop/SummerGame.Desktop.csproj

content-raw:
    dotnet build --project ./SummerGame.Content/SummerGame.Content.csproj
    ./SummerGame.Content/bin/Debug/net9.0/SummerGame.Content
