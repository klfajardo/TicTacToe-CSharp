// See https://aka.ms/new-console-template for more information

using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading.Channels;
using Dimensions;

internal class Program
{
    // VARIABLES
    
    // List of players
    // Each new player will be added
    private static List<Player> players = new List<Player>();

    // MAIN PROGRAM 
    public static void Main(string[] args)
    {
        // Variables
        bool exitMenu = false;
        int enteredOption = 0;
        
        // Create default players
        CreateDefaultPlayers();
        
        // Menu
        while (!exitMenu)
        {
            PrintMenu();
            // Select menu option
            if (Int32.TryParse(Console.ReadLine()?.Trim(), out enteredOption) && enteredOption >= 1 && enteredOption <= 4)
            {
                switch (enteredOption)
                {
                    case 1:
                        // Start new game
                        CreateNewGame();
                        break;
                    case 2:
                        // View stats
                        PrintStats();
                        Console.Write("Hit Enter to warp back to the main menu!");
                        Console.ReadKey();
                        break;
                    case 3:
                        // Create new player
                        CreatePlayerMenu();
                        break;
                    case 4:
                        // Exit menu (program)
                        System.Environment.Exit(1);
                        exitMenu = true;
                        break;
                    default: 
                        // useless
                        Console.WriteLine("Oops, A glitch in the matrix. Try again!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid option! Please choose a number between 1 and 4."); 
                Thread.Sleep(2000);
            }
        }
    }

    private static void PrintMenu()
    {
        Console.Clear();
        Console.WriteLine("✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧");
        Console.WriteLine("✧   Tic Tac Toe Menu   ✧");
        Console.WriteLine("✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧");
        Console.WriteLine("✧ 1. Start new game    ✧"); 
        Console.WriteLine("✧ 2. View stats        ✧");
        Console.WriteLine("✧ 3. Create player     ✧");
        Console.WriteLine("✧ 4. Exit              ✧");
        Console.WriteLine("✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧");
        Console.Write("Choose an option (1-4): ");
    }
    
    private static void PrintNewGameMenu()
    {
        Console.Clear();
        Console.WriteLine("✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧");
        Console.WriteLine("✧    Start New Game    ✧");
        Console.WriteLine("✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧");
        Console.WriteLine("Players");
        for (int i = 0; i < players.Count; i++)
        {
            Console.WriteLine("{0}. {1}", i + 1, players[i].GetName()); 
        }
        Console.WriteLine(" ");
        Console.WriteLine("Board Style");
        Console.WriteLine("1. Classic");
        Console.WriteLine(" ");
        Console.WriteLine("✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧");
    }

    private static void CreateNewGame()
    {
        // Variables
        int enteredOption;
        Player player1 = null!;
        Player player2 = null!;
        String boardStyle;
        bool successful = false;
        bool rotateMessage = false;
        
        // Print menu
        PrintNewGameMenu();
        
       // Select player 1
       while (!successful)
       { 
           // Clear console and update menu
           Console.Clear();
           PrintNewGameMenu();
           
           // Select player
           Console.Write("Select player 1: ");
           if (Int32.TryParse(Console.ReadLine()?.Trim(), out enteredOption) && enteredOption > 0 && enteredOption <= players.Count)
           {
               player1 = players[enteredOption - 1];
               successful = true;
           }
           else
            {
                Console.Write("Invalid option! Please choose a number between 1 and {0}.", players.Count); 
                Thread.Sleep(2000);
            } 
       }
        successful = false;
        
       // Select player 2
       while (!successful)
       { 
           // Clear console and update menu
           Console.Clear();
           PrintNewGameMenu();
           Console.WriteLine("Select player 1: {0}", player1.GetName());
           
           // Select player 
           Console.Write("Select player 2: ");
           if (Int32.TryParse(Console.ReadLine()?.Trim(), out enteredOption) && enteredOption > 0 && enteredOption <= players.Count)
           {
               if (players[enteredOption - 1] == player1)
               {
                   // Player already in use
                   Console.Write(rotateMessage == false ? "Oops! That player's already selected. Pick someone else." : "Hey, no clones allowed! Choose a different player.");
                   rotateMessage = rotateMessage == false ? true : false;
                   Thread.Sleep(3000);
               }
               else
               {
                   // Set player 2
                   player2 = players[enteredOption - 1]; 
                   successful = true;
               }
           }
           else
           {
               Console.Write("Invalid option! Please choose a number between 1 and {0}.", players.Count); 
               Thread.Sleep(2000);
           } 
       } 
       successful = false;
       
       // Select baord style (for now just 1 style) 
       while (!successful)
       { 
           // Update menu
           Console.Clear();
           PrintNewGameMenu();
           Console.WriteLine("Select player 1: {0}", player1.GetName());
           Console.WriteLine("Select player 2: {0}", player2.GetName());
           
           Console.Write("Select Board Style: ");
           if (Int32.TryParse(Console.ReadLine()?.Trim(), out enteredOption) && enteredOption > 0 && enteredOption <= players.Count)
           {
               boardStyle = "classic"; // this doesn't do anything for now
               successful = true;
           }
           else
           {
               Console.Write("Invalid option. Try again!"); 
               Thread.Sleep(2000);
           } 
       } 
       successful = false;
       
       // Create the game and show the game settings
       Console.WriteLine("\n=<>=================================<>=");
       Console.WriteLine("{0} VS {1} - Classic Board", player1.GetName(), player2.GetName());
       Console.WriteLine("=======================================");
       Thread.Sleep(2000);
       Console.WriteLine("Ready, set, play! A new game is about to start.");
       Thread.Sleep(2500);
       Console.Clear();
       Thread.Sleep(0200);
       // Create game object
       Game newGame = new Game(player1, player2); 
       newGame.Start();
    }


    private static void PrintCreatePlayerMenu(string playerName, string playerChar1, string playerChar2)
    {
        Console.Clear(); 
        Console.WriteLine("✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧"); 
        Console.WriteLine("✧    Create Player     ✧"); 
        Console.WriteLine("✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧");
        Console.WriteLine("1. Enter Player Name: {0}", playerName);
        Console.WriteLine("2. Choose Character 1: {0}", playerChar1);
        Console.WriteLine("3. Choose Character 2: {0}", playerChar2);
        Console.WriteLine("4. Confirm and save");
        Console.WriteLine("5. Cancel");
        Console.WriteLine("✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧");
        Console.Write("Choose an option (1-4): ");
    }
    
    private static void CreatePlayerMenu()
    {
        // Variables
        int enteredOption;
        string enteredString = null!;
        string playerName = null!;
        string playerChar1 = null!;
        string playerChar2 = null!;
        bool playerCreated = false;
        bool successful = false;
        bool rotateMessage = false;
        
        // Print menu
        while (!playerCreated) 
        { 
            PrintCreatePlayerMenu(playerName, playerChar1, playerChar2);
            if (Int32.TryParse(Console.ReadLine()?.Trim(), out enteredOption) && enteredOption >= 1 && enteredOption <= 5)
            {
                switch (enteredOption)
                {
                    case 1: 
                        while (!successful)
                        { 
                            // Print menu
                            PrintCreatePlayerMenu(playerName, playerChar1, playerChar2); 
                            // Choose name
                            Console.Write("\nName (Max 11 characters): "); 
                            enteredString = Console.ReadLine().Trim();
                            if (enteredString.Length > 0 && enteredString.Length <= 11)
                            {
                                playerName = enteredString;
                                successful = true;
                            }
                            else
                            {
                                Console.WriteLine(rotateMessage == false ? "Keep it snappy! Names can only be 11 characters." : "Woah, slow down Shakespeare! Names can only be 11 characters.");
                                rotateMessage = rotateMessage == false ? true : false;
                                Thread.Sleep(3000); 
                            }
                        }
                        successful = false; 
                        break;
                    case 2:
                        while (!successful)
                        { 
                            // Print menu
                            PrintCreatePlayerMenu(playerName, playerChar1, playerChar2); 
                            // Choose character 1
                            Console.Write("\nCharacter 1 (Available: X, /, Z, K, %): ");
                            enteredString = Console.ReadLine().Trim().ToUpper();
                            if (enteredString.Length == 1 && (enteredString == "X" || enteredString == "/" || enteredString == "Z" || enteredString == "K" || enteredString == "%"))
                            {
                                playerChar1 = enteredString;
                                successful = true;
                            }
                            else
                            {
                                Console.WriteLine("Nice try, but that's not in the armory! Pick from (X, /, Z, K, %)");
                                Thread.Sleep(3000); 
                            }
                        }
                        successful = false; // leave it in false again
                        break;
                    
                    case 3:
                        while (!successful)
                        { 
                            // Print menu
                            PrintCreatePlayerMenu(playerName, playerChar1, playerChar2); 
                            // Choose character 2
                            Console.Write("\nCharacter 2 (Available: O, 0, C, G, @): ");
                            enteredString = Console.ReadLine().Trim().ToUpper();
                            if (enteredString.Length == 1 && (enteredString == "0" || enteredString == "O" || enteredString == "C" || enteredString == "G" || enteredString == "@"))
                            {
                                playerChar2 = enteredString;
                                successful = true;
                            }
                            else
                            {
                                Console.WriteLine("Nice try, but that's not in the armory! Pick from (O, @, C, G, @)");
                                Thread.Sleep(3000); 
                            } 
                        }
                        successful = false; 
                        break;
                    case 4:
                        // Create player and add it to the players list 
                        if (playerName != null && playerChar1 != null && playerChar2 != null)
                        {
                           Player newPlayer = new Player(playerName, playerChar1, playerChar2);
                           players.Add(newPlayer);
                           
                           playerCreated = true;
                           Console.WriteLine("New player created. Let's roll!");
                           Console.Write("You can start playing with {0} or see your stats in the 'View Stats' menu.", playerName);
                           Thread.Sleep(4000); 
                        }
                        else
                        {
                            Console.Write("Time to get personal! Complete all the fields above.");
                            Thread.Sleep(3000); 
                        }
                        break; 
                    case 5:
                        // Cancel 
                        return;
                    default:
                        Console.Write("Oops! A glitch in the matrix. Try again!");
                        break;
                } 
            }
            else
            {
                Console.Write("Invalid option! Choose a number between 1 and 4."); 
                Thread.Sleep(2000);
            } 
        }
        return;
    }

    private static void PrintStats()
    {
        // Print stats
        Console.Clear();
        Console.WriteLine("✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧"); 
        Console.WriteLine("✧        Stats         ✧"); 
        Console.WriteLine("✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧");
        
        // Print indiivudal stats for each player
        foreach (Player player in players)
        {
            Console.WriteLine("Player: {0} ", player.GetName());
            Console.WriteLine(" Symbol 1: {0}, Symbol 2: {1}", player.GetChar1(), player.GetChar2());
            Console.WriteLine(" Wins: {0}", player._totalWins);
            Console.WriteLine(" Losses: {0}", player._totalLosses);
            Console.WriteLine(" Draws: {0}", player._totalDraws);
            Console.WriteLine(" Total Games Played: {0}", player._totalGamesPlayed);
            Console.WriteLine(" Winning Percentage: {0}", player._totalWinningPercentage);
            Console.WriteLine(" ");
        }
        Console.WriteLine("✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧✧");
    }
    
    private static void CreateDefaultPlayers()
    {
        // Two default players
        players.Add(new Player("Player1")); 
        players.Add(new Player("Player2")); 
    }
}