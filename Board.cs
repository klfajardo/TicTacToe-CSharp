using System.Diagnostics;

namespace Dimensions;
internal class Board
{
    // Member variable
    private string _name;
    private string[,] _matrix;
    private string _charWinner;

    // Default Constructor
    public Board()
    {
        _name = "DefaultBoard";
        _matrix = new string[,]
        {
            { "1", "2", "3" },
            { "4", "5", "6" },
            { "7", "8", "9" }
        }; 
        Debug.WriteLine($"Board {_name} was created.");
        _charWinner = "";
    }

    public void PrintBoard()
    {
       Console.WriteLine("     +   +   \n" +
                         "   {0} | {1} | {2} \n" +
                         " +---+---+---+ \n" +
                         "   {3} | {4} | {5} \n" +
                         " +---+---+---+ \n" +
                         "   {6} | {7} | {8} \n" +
                         "     +   +   ", _matrix[0, 0], _matrix[0, 1], _matrix[0, 2], 
                                              _matrix[1, 0], _matrix[1, 1], _matrix[1, 2], 
                                              _matrix[2, 0], _matrix[2, 1], _matrix[2, 2]);
       Debug.WriteLine("{0} board was printed.", _name);
    }
    
    public void ResetBoard()
    {
        int numberCount = 1;
        for (int i=0; i < _matrix.GetLength(0); i++)
        {
            for (int j = 0; j < _matrix.GetLength(1); j++)
            {
                _matrix[i, j] = numberCount.ToString();
                numberCount++;
            }
        }
        Debug.WriteLine("{0} board was reset.", _name); 
    }

    public bool ReplaceMatrixPosition(string playerChar, int[] position) 
    {
        int intMatrix;
        if ((int.TryParse(_matrix[position[0], position[1]], out intMatrix)))
        {
            // Successfully parsed
            _matrix[position[0], position[1]] = playerChar;
            Debug.WriteLine($"ReplaceMatrixPosition: value was: {intMatrix}, value now is {playerChar}.");
            return true;
        }
        else
        {
            // Position is not an integer (already occupied by a character)
            Debug.WriteLine("Value didn't change. Position already occupied.");
            return false;
        }
    }
    
    // BOARD CHECKER
   public bool Checker()
    {
        // CHECK HORIZONTAL
        for (int i = 0; i < _matrix.GetLength(0); i++)
        {
            if (_matrix[i, 1] == _matrix[i, 0] && _matrix[i, 2] == _matrix[i, 0])
            {
                _charWinner = _matrix[i, 1];
                Debug.WriteLine("Checker - status: Winner, type: horizontal");
                return true;
            }
        }
        // CHECK VERTICAL
        for (int i = 0; i < _matrix.GetLength(1); i++)
        {
            if (_matrix[1, i] == _matrix[0, i] && _matrix[2, i] == _matrix[0, i])
            {
                _charWinner = _matrix[1, i];
                Debug.WriteLine("Checker - status: winner, type: vertical");
                return true;
            }
        }
        // CHECK DIAGONAL
        if (_matrix[0, 0] == _matrix[1, 1] && _matrix[2, 2] == _matrix[1, 1] || 
            _matrix[0, 2] == _matrix[1, 1] && _matrix[2, 0] == _matrix[1, 1])
        {
            _charWinner = _matrix[1, 1];
            Debug.WriteLine("Checker - status: winner, type: diagonal");
            return true;
        }
        // CHECK DRAW
        for (int i = 0; i < _matrix.GetLength(0); i++)
        {
            for (int j = 0; j < _matrix.GetLength(1); j++)
            {
                if ((int.TryParse(_matrix[i, j], out _)))
                {
                    // No winner and no draw, board still in play
                    Debug.WriteLine("Checker - status: in-game, type: n/a"); 
                    return false; 
                }
            }
        }
        // Draw
        Debug.WriteLine("Checker - status: draw, type: all slots are full");
        return true;
    }

   public string GetCharWinner()
   {
       return _charWinner;
   }

}