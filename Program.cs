
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Security.Cryptography;
using app;

List<User> users = new List<User>(); // gör en lista för Users
bool running = true; // en bool för att kunna stänga av programmet
User?  active_user = null; // den Usern som är inloggad är inget till för att veta om du e inloggad eller inte
while (running) //en loop för att programmet ska runna på repeat
{
    if (active_user == null) // if sats för att se om du e inloggad eller inte
    {
        Console.Clear();
        Console.WriteLine("Press 1 to create account");
        Console.WriteLine("Press 2 to log in ");
        Console.WriteLine("Press 3 to remove account"); //alla dessa är till för menyval
        string menu = Console.ReadLine()!; // för att läsa av vilket val som görs
        switch (menu) // respondar till valet som gjordes
        {
            case "1":
                Console.Clear();
                Console.WriteLine("Please type your username");
                string new_name = Console.ReadLine()!; //för att läsa av ditt skrivna användarnamn
                Console.Clear();
                Console.WriteLine("please type your password");
                String new_password = Console.ReadLine()!; //för att läsa av ditt skrivna lösenord
                users.Add(new User(new_name, new_password)); //gör en ny user med ditt namn och lösenord
                Console.WriteLine("account created"); //conformation att du har gjort det
                File.AppendAllText("./Account_names.txt", new_name + "\n"); // gör en fil som är kopplat till ditt konto namn 
                break;
            case "2":
                Console.Clear();
                Console.WriteLine("Please type your username"); 
                string existing_name = Console.ReadLine()!; // kollar om det du skriver redan finns
                Console.Clear();
                Console.WriteLine("please type your password"); // samma med lösenordet
                
                String existing_password = Console.ReadLine()!;
                foreach (User user in users) // kopplad till TryLogin som skapades i user.cs
                {
                    if (user.TryLogin(existing_name, existing_password)) // kollar så att det finns en user och i såfall blir den personen inloggad
                    {
                        active_user = user;
                        break;
                    }


                }
                break;

  }


}
    if (active_user != null) // detta kommer upp när det finns en aktiv user
    {
        Console.Clear(); // alla dessa är menyval
        Console.WriteLine("Hello " + active_user.Email); // .Email är för att få användarnamnet 
        Console.WriteLine("type logout to logout");
        Console.WriteLine("type remove to remove your account");
        Console.WriteLine("type removeitem to remove item from your inventory");
        Console.WriteLine("type Add to add an item in your inventory");
        Console.WriteLine("type Show inventory to show what you want to trade");
        Console.WriteLine("type Show item market to show everyone");
        Console.WriteLine("type Ritems to look at requested items");
        Console.WriteLine("Type Arequest to show accepted requests");
        string menu2 = Console.ReadLine()!;
        switch (menu2)
        {
            case "logout": //detta är för att göra user'n inaktiv vilket loggar ut dig
                active_user = null;
                break;

            case "remove": // detta är till för att tabort ditt konto om du inte vill ha kvar det
                Console.WriteLine("are you sure you want to delete your account, yes to delete, no to go back");
                string remove_account = Console.ReadLine()!;

                if (remove_account == "yes") // dubbel kollar så att du verkligen vill ta bort ditt konto
                {
                    Console.Clear();
                    users.Remove(active_user);
                    Console.WriteLine("your account is now deleted");
                    active_user = null;
                }

                else //om du inte vill så går du bara tillbaka till den vanliga menyn
                if (remove_account == "no")
                {
                    break;
                }
                break;
            case "Arequest": //Till för att kolla accepted trades, alla trades som acceptas skickas hit så du kan blädra igenom

                Console.Clear();
                string[] rows_4 = File.ReadAllLines("./Accepted_trades.txt"); // skapar en array för att kunna skriva ut alla rader i filen

                foreach (string line in rows_4) // skriver ut alla rader
                {

                    Console.WriteLine(line);

                }
                Console.WriteLine("Press enter to go back");
                Console.ReadLine();

                break;


            case "Add": // till för att lägga till items i din personliga fil som skapas (filen blir kopplat till activa userns användarnamn)

                Console.Clear();
                Console.WriteLine("please type what you want to add to your inventory");
                string item = Console.ReadLine()!; // läser vad du vill ha för items
                Console.WriteLine("please enter info about the item ");
                string info = Console.ReadLine()!; // läse vad du vill ha för description på itemet
                File.AppendAllText("./Personitems/" + active_user.Email + ".txt", item); // skriver in det i text filen på en ny rad
                File.AppendAllText("./Personitems/" + active_user.Email + ".txt", info + "\n"); // -''-
                File.AppendAllText("./Personitems/" + active_user.Email + ".txt", "------------------" + "\n");// för att skapa mellanrum mellan varorna i filen



                break;
            case "Show inventory": // visar din egna fil 
                Console.Clear();
                File.AppendAllText("./Personitems/" + active_user.Email + ".txt", ""); // till för att skapa en tom fil om den inte existerar redan
                string[] rows = File.ReadAllLines("./Personitems/" + active_user.Email + ".txt"); //för att kunna skriva ut alla raderna med hjälp av en array

                foreach (string line in rows)// looppar alla raderna i fil och skriver ut dem
                {

                    Console.WriteLine(line);

                }
                Console.WriteLine("do you want to accept any of the requests");
                string choise = Console.ReadLine()!; // för att läsa av om du vill acceptera några av dina requests
                if (choise == "yes") //om dem skriver ja händer detta
                {
                    Console.WriteLine("plase type what row you want to accept");
                    string row = Console.ReadLine()!; //Alla items är uppskrivna i termnialen och du skriver vilket rad på items du vill accepta requesten
                    string filepath = "./Personitems/" + active_user.Email + ".txt"; //För att underlätta och inte behöva skriva hela filvägen hårdkådat igen
                    int targetrow = int.Parse(row); // för att göra om det du skrev till en rad
                    List<string> lines = new List<string>(File.ReadAllLines(filepath)); //lista för att raderna i filen skapas här
                    if (targetrow > 0 && targetrow <= lines.Count) //kollar så att raden du skrev existerar i filen
                    {
                        lines[targetrow - 1] += " " + active_user.Email + " Has accepted this trade and are now rdy to meet up ";// lägger till på raden du valde att du har accepterat och dem kan se det senare i accepted trades filen
                        File.WriteAllLines(filepath, lines);// skriver ut det 

                    }
                    File.WriteAllText("./Accepted_trades.txt", lines[targetrow - 1] + "\n"); // skriver ut exakt samma i Accepted_trades.txt så att alla kan se vad som har tredats

                }
                else if (choise == "no")
                {
                    Console.Clear();
                    Console.WriteLine("do you want to deny a request"); //om du inte vill acceptera någon trade
                    string deny = Console.ReadLine()!;// kollar på ditt svar

                    if (deny == "yes")
                    {
                        Console.WriteLine("plase type what row you want to accept"); // samma som innan med att den försöker hitta raden du  väljer i filen för att sedan lägga till att du har deniat den
                        string row = Console.ReadLine()!;
                        string filepath = "./Personitems/" + active_user.Email + ".txt";
                        int targetrow = int.Parse(row);
                        List<string> lines = new List<string>(File.ReadAllLines(filepath));
                        if (targetrow > 0 && targetrow <= lines.Count)
                        {
                            lines[targetrow - 1] += " " + active_user.Email + " Has denied this trade";
                            File.WriteAllLines(filepath, lines);

                        }
                    }
                    else if (deny == "no") // om du inte vill denya heller 
                    {
                        break;

                    }

                }


                Console.WriteLine("Press enter to go back to the meny");
                Console.ReadLine();

                break;

            case "Show item market": // för att se vad som finns att byta med
                Console.Clear();
                string[] rows_1 = File.ReadAllLines("./Account_names.txt"); // alla inloggnings namn som skapas skrivs in i ett account names fil så att du kan blädra genom användare och gå in på dem specifikt

                foreach (string line in rows_1)
                {

                    Console.WriteLine(line);

                }
                Console.WriteLine("please enter one of the name to see thier items"); // förklarar att du skriver deras namn för att komma in och se deras "item fil"
                string names = Console.ReadLine()!;  //kollar vilket namn du skriver
                Console.Clear();
                Console.WriteLine("here is" + " " + names + "items"); // visar alla items (denna redan som skriv i terminalen är inte rad 1)
                File.AppendAllText("./Personitems/" + names + ".txt", "");  // visar den specifika filen
                string[] rows_2 = File.ReadAllLines("./Personitems/" + names + ".txt"); // gör en ny array för att kunna hitta den specifia raden i filen

                foreach (string line in rows_2)
                {

                    Console.WriteLine(line);

                }
                Console.WriteLine("do you want to request any lf this items?");
                Console.WriteLine("type yes if you want to and no if you dont");
                string request = Console.ReadLine()!; // kollar om du vill requesta något item
                if (request == "yes")
                {
                    Console.WriteLine("Write what item you want to give in this exhange");
                    string trade = Console.ReadLine()!;
                    Console.WriteLine("plase enter the row of items you want to request example row 1 is the one in the top");
                    string row = Console.ReadLine()!;
                    string filepath = "./Personitems/" + names + ".txt"; //rad 220 till 232 är till för att gör samma som på 166 till 172
                    int targetrow = int.Parse(row);


                    List<string> lines = new List<string>(File.ReadAllLines(filepath));
                    if (targetrow > 0 && targetrow <= lines.Count)
                    {
                        lines[targetrow - 1] += " " + active_user.Email + " and you get " + trade + " /Request";
                        File.WriteAllLines(filepath, lines);

                    }
                    File.WriteAllText("./Request_items.txt", lines[targetrow - 1] + "\n");




                }
                else if (request == "no") // om dem inte vill requesta händer detta istället
                {
                    break;
                }



                Console.WriteLine("Press enter to go back to the meny");
                Console.ReadLine();
                break;
            case "Ritems": // visar alla items som är requested t.ex det står stol, 3 ben inget bord, Max vill byta sin kniv för den här stolen /Request
                Console.Clear();
                string[] rows_3 = File.ReadAllLines("./Request_items.txt"); // gör en array för att skriva ut alla raderna i filen 249-260

                foreach (string line in rows_3)
                {

                    Console.WriteLine(line);

                }
                Console.WriteLine("Press enter to go back");
                Console.ReadLine();

                break;

            case "removeitem":
                            Console.Clear();
                File.AppendAllText("./Personitems/" + active_user.Email + ".txt", ""); // till för att skapa en tom fil om den inte existerar redan
                string[] rows_ = File.ReadAllLines("./Personitems/" + active_user.Email + ".txt"); //för att kunna skriva ut alla raderna med hjälp av en array
                                      
                foreach (string line in rows_)// looppar alla raderna i fil och skriver ut dem
                {

                    Console.WriteLine(line);

                }
                Console.WriteLine("do you want to accept any of the requests");
                string choise_1 = Console.ReadLine()!; // för att läsa av om du vill acceptera några av dina requests
                if (choise_1 == "yes") //om dem skriver ja händer detta
                {
                    Console.WriteLine("plase type what row you want to accept");
                    string row = Console.ReadLine()!; //Alla items är uppskrivna i termnialen och du skriver vilket rad på items du vill accepta requesten
                    string filepath = "./Personitems/" + active_user.Email + ".txt"; //För att underlätta och inte behöva skriva hela filvägen hårdkådat igen
                    int targetrow = int.Parse(row); // för att göra om det du skrev till en rad
                    List<string> lines = new List<string>(File.ReadAllLines(filepath)); //lista för att raderna i filen skapas här
                    if (targetrow > 0 && targetrow <= lines.Count) //kollar så att raden du skrev existerar i filen
                    {
                        lines[targetrow - 1] = " " + active_user.Email + "";// lägger till på raden du valde att du har accepterat och dem kan se det senare i accepted trades filen
                        File.WriteAllLines(filepath, lines);// skriver ut det 

                    }
                    File.WriteAllText("./Accepted_trades.txt", lines[targetrow - 1] + "\n"); // skriver ut exakt samma i Accepted_trades.txt så att alla kan se vad som har tredats

                }
                break;
        }
               

            
               
}
            



        }



    
    




