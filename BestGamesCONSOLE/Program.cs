using BestGamesCONSOLE;

List<Game> games = new();
Random rnd = new();

using StreamReader sr = new(@"..\..\..\RES\bestgames.csv");
while (!sr.EndOfStream) games.Add(new Game(sr.ReadLine()));

Console.WriteLine($"f1: összesen {games.Count} játék szerepel a listában");

var group = games.GroupBy(g => g.ReleaseYear)
    .Where(gp => gp.Count() > 10)
    .OrderByDescending(gp => gp.Count());
Console.WriteLine("f2: ezekben az években került több, mint 10 cím a listára:");
foreach (var kvp in group) Console.WriteLine($"\t{kvp.Key}: {kvp.Count()}db");

#region LINQ nélkül
//Dictionary<int, int> dic = new();
//foreach (var game in games)
//{
//    if (dic.ContainsKey(game.ReleaseYear)) dic[game.ReleaseYear]++;
//    else dic.Add(game.ReleaseYear, 1);
//}
//var kvpList = dic.ToList();
//for (int i = 0; i < kvpList.Count - 1; i++)
//{
//    for (int j = i; j < kvpList.Count; j++)
//    {
//        if (kvpList[i].Value < kvpList[j].Value)
//            (kvpList[i], kvpList[j]) = (kvpList[j], kvpList[i]);
//    }
//}
//int k = 0;
//while (kvpList[k].Value > 10)
//{
//    Console.WriteLine($"\t{kvpList[k].Key}: {kvpList[k].Value}db");
//    k++;
//}
#endregion

List<Game> FPSGames = new();
foreach (var game in games)
    if (game.Genre == "First-person shooter")
        FPSGames.Add(game);
Console.WriteLine($"f3: összesen {FPSGames.Count} db FPS került fel a listára");
Console.WriteLine($"\tpéldául a(z): {FPSGames[rnd.Next(FPSGames.Count)].Title}");