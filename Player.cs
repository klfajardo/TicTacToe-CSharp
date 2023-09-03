namespace Dimensions;

public class Player
{
    // PLAYER VARIABLES
    private string _name;
    private string _character1;
    private string _character2;
    
    // PLAYER STATS
    public int _totalWins { get; private set; }
    public int _totalLosses { get; private set; }
    public int _totalDraws { get; private set; }
    public int _totalGamesPlayed { get; private set; }
    public double _totalWinningPercentage { get; private set; }
    
    // Default constructors
    public Player()
    {
        _name = "default";
        _character1 = "X";
        _character2 = "O";

        _totalWins = 0;
        _totalLosses = 0;
        _totalDraws = 0;
        _totalGamesPlayed = 0;
        _totalWinningPercentage = 0;
    }

    public Player(string name)
    {
        _name = name;
        _character1 = "X";
        _character2 = "O";
        
        _totalWins = 0;
        _totalLosses = 0;
        _totalDraws = 0;
        _totalGamesPlayed = 0;
        _totalWinningPercentage = 0;
    }

    public Player(string name, string char1, string char2)
    {
        _name = name;
        _character1 = char1;
        _character2 = char2;
        
        _totalWins = 0;
        _totalLosses = 0;
        _totalDraws = 0;
        _totalGamesPlayed = 0;
        _totalWinningPercentage = 0;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetChar1()
    {
        return _character1;
    }
    
    public string GetChar2()
    {
        return _character2;
    }

    public void IncreaseWonGame()
    {
        _totalWins++;
    }
    
    public void IncreaseLost()
    {
        _totalLosses++;
    }
    
    public void IncreaseDraw()
    {
        _totalDraws++;
    }
    
    public void IncreaseTotalGamesPlayed()
    {
        _totalGamesPlayed++;
    }

    // Calculate the winning percentage
    public void CalculateWinningPercentage()
    {
        _totalWinningPercentage = (double)_totalWins / _totalGamesPlayed * 100;
    }
}