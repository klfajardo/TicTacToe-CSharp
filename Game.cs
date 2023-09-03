using System.Diagnostics;
using System.Security.Principal;

namespace Dimensions;

internal class Game
{
    // VARIABLES
    // Counts
    private static int _turnCount;
    private static int _totalTurnCount;
    private static int _matchCount;
    
    // Constants
    private static int _totalMatch;
    
    // Game Definitions
    private static Player _player1;
    private static Player _player2;
    private static Player _currentPlayer;
    private static Board _board;

    // Win Variables
    private static Player _playerWinner; 
    private static Player _playerLoser;
    private static bool _theGameHasEnded; 
    private static int[] _wins = new int[2];
    
   
    public Game()
    {
        _player1 = new Player("Player1");
        _player2 = new Player("Player2");
        _board = new Board();
        _totalMatch = 5; 

        _playerWinner = null!;
        _playerLoser = null!;
        
        _turnCount = 1;
        _totalTurnCount = 0;
        _matchCount = 1;

        _theGameHasEnded = false;
        _wins[0] = 0;
        _wins[1] = 0;
    }

    public Game(Player player1, Player player2)
    {
        _player1 = player1;
        _player2 = player2;
        _currentPlayer = _player1;
        _board = new Board();
        _totalMatch = 5;

        _playerWinner = null!;
        _playerLoser = null!;
        
        _turnCount = 1;
        _totalTurnCount = 0;
        _matchCount = 1;

        _theGameHasEnded = false;
        _wins[0] = 0;
        _wins[1] = 0;
    }

    public Game(Player player1, Player player2, int totalMatch)
    {
        _player1 = player1;
        _player2 = player2;
        _currentPlayer = _player1;
        _board = new Board();
        _totalMatch = totalMatch;
        
        _playerWinner = null!;
        _playerLoser = null!;
        
        _turnCount = 1;
        _totalTurnCount = 0;
        _matchCount = 1;

        _theGameHasEnded = false;
        _wins[0] = 0;
        _wins[1] = 0;
    }
    
    public void Start() 
    {
        while(!_theGameHasEnded) 
        {
            PrintGame();
            PlayTurn();
            // Check if there is a winner
            if (_board.Checker()) 
            {
                // End match
                PrintGame();
                FinishMatch();
            }
            else
            {
                // Continue match
                PrintGame(); 
                ChangeTurn();
            }
        }
        // Exit game
        Thread.Sleep(4000);
        for (int i = 3; i > 0; i--)
        {
            PrintGame();
            Console.WriteLine("Going back to menu in {0}", i);
            Thread.Sleep(1000);
        } 
        return;
    }
    
    private static void PrintGame()
    { 
        // Print all the GUI
        Console.Clear(); 
        PrintStats();
        _board.PrintBoard(); 
        Console.WriteLine("__________________________________________");
        //Console.WriteLine("__  __  __  __  __  __  __  __  __  __  __");
    }

    private static void PlayTurn() 
    {
        // Variables
        int[] positionInMatrix = new int[2];
        int enteredPosition;
        string playerChar = _currentPlayer == _player1 ? _currentPlayer.GetChar1() : _currentPlayer.GetChar2();
        
        _turnCount += 1;
        Debug.WriteLine("Turn: {0}", _currentPlayer.GetName());
        
        do
        { 
            Console.Write("Position: ");
            if (Int32.TryParse(Console.ReadLine()?.Trim(), out enteredPosition) && enteredPosition >= 1 && enteredPosition <= 9)
            {
                positionInMatrix[0] = (enteredPosition - 1) / 3;
                positionInMatrix[1] = (enteredPosition - 1) % 3;
                if (_board.ReplaceMatrixPosition(playerChar, positionInMatrix))
                {
                    Debug.WriteLine("PlayTurn: SUCCESSFUL");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid move! Try another position.");
                    Debug.WriteLine("PlayTurn: TRYAGAIN");
                }
            }
            else
            {
                Console.WriteLine("Invalid move! Pick a number between 1 and 9.");
            }
            // Sleep the code, so the player thinks about it
            Thread.Sleep(2000);
        } while (true);
    }
    
    public void FinishMatch()
    {
        // Sleep the code at the end of the match
        Thread.Sleep(1000);
        // Double check winner
        if (CheckWinner() == _player1 && _player1 == _currentPlayer)
        {
            // Print winner and increase win
            Console.WriteLine("{0} has won this match!", _player1.GetName());
            _wins[0] += 1;
        }
        else if (CheckWinner() == _player2 && _player2 == _currentPlayer)
        {
            // Print winner and increase win
            Console.WriteLine("{0} has won this match!", _player2.GetName());
            _wins[1] += 1;
        }
        else
        {
            Console.WriteLine("DRAW!");
        }
        // REVERT MATCH STATS
        _totalTurnCount += _turnCount; 
        _turnCount = 1; 
        
        // VERIFY MATCHES LIMIT
        if (_matchCount < _totalMatch)
        {
            // Hasn't reached the match limit
            _matchCount += 1;
        }
        else
        {
            // Reached the match limit, finishing game
            Thread.Sleep(1000);
            FinishGame();
            return;
        }
        
        // Reset board to continue game
        _board.ResetBoard();
        Console.WriteLine("Hit Enter to continue the game!.");
        Console.ReadLine();
    }

    private void UpdateStats()
    {
        // Increase and update stats of players
        // Verify if there is a Winner, or it is a Draw
        if (_playerWinner != null && _playerLoser != null)
        { 
            // Increase wins of winner, and loses of losser
            _playerWinner.IncreaseWonGame(); 
            _playerLoser.IncreaseLost();
        }
        else
        {
            // Increase draw stats
            _player1.IncreaseDraw();
            _player2.IncreaseDraw();
        } 
        // Increase total games played
        _player1.IncreaseTotalGamesPlayed(); 
        _player2.IncreaseTotalGamesPlayed(); 
        
        // Calculate Total Winning percentage
        _player1.CalculateWinningPercentage();
        _player2.CalculateWinningPercentage();
    }

    private void FinishGame()
    {
        Console.Clear();
        PrintGame();
        if (_wins[0] > _wins[1])
        {
            // Player 1 Winner
            Console.WriteLine("{0} has won the series. Good game!", _player1.GetName());
            _theGameHasEnded = true;
            _playerWinner = _player1;
            _playerLoser = _player2;
            UpdateStats();
        } 
        else if (_wins[1] > _wins[0])
        {
            // Player 2 Winner
            Console.WriteLine("{0} has won the series. Good game!", _player2.GetName());
            _theGameHasEnded = true;
            _playerWinner = _player2;
            _playerLoser = _player1;
            UpdateStats();
        } 
        else
        {
            // Draw
            Console.WriteLine("The series is a draw! Nice game!");
            _theGameHasEnded = true;
            UpdateStats();
        }
        // Show final score
        //Console.WriteLine("SCORE {0}: {1} - {2}: {3}", _player1.GetName(), _wins[0], _player2.GetName(), _wins[1]);
        Thread.Sleep(5000);
    }
    
    private void ChangeTurn()
    {
        // Change turn
        _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;
    }

    private Player CheckWinner() // 
    {
        // Double check Winner making a correlation between the char winner and players char
        if (_board.GetCharWinner() == _player1.GetChar1())
        {
            return _player1;
        } 
        else if (_board.GetCharWinner() == _player2.GetChar2())
        {
            return _player2;
        }
        else
        {
            return null!;
        }
    }
    
    private static void PrintStats()
    {
        // Print stats of the game
        Console.WriteLine("=<>==================<>==================<>=");
        Console.WriteLine($"Turn {_turnCount}: {_currentPlayer.GetName()}    Match {_matchCount}/{_totalMatch}    Score: {_wins[0]}:{_wins[1]}");
        //Console.WriteLine($"Turn {_turnCount}: {_currentPlayer.GetName()} Symbol:{_currentPlayer}");
        Console.WriteLine("____________________________________________");
    }
}