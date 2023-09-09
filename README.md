# TicTacToe-CSharp
This is a console-based Tic Tac Toe game developed in C#. It is designed as a hands-on exercise for learning Object-Oriented Programming. The gameplay is turn-based and requires two players to alternate on a 3x3 grid board. The game incorporates additional elements like player statistics tracking and new players creation.

Note: This is a project I've been working on in my spare time, and is primarily aimed at practicing programming concepts as I learn them.

Any feedback is highly appreciated!

## Classes

### Program
The Program class serves as the main driver for the Tic-Tac-Toe game. It contains methods for displaying various menus that allow users to start a new game, view statistics, or create a new player. Upon launching, the program initializes with two default players and proceeds to the main menu where users can make different selections. The menu system is loop-based, capturing user inputs for the various functionalities until the user decides to exit the program. The class collaboratively works with other classes like Player and Game to facilitate gameplay and statistics tracking.

### Game
The Game class manages the logic and flow of a Tic-Tac-Toe game. It initializes players, keeps track of turns, matches, and game statistics, and orchestrates the game board. The class offers multiple constructors for flexibility, allowing you to start a game with default settings or specify player names and the total number of matches. It also contains methods for game management such as Start(), which drives the gameplay, and FinishMatch(), which handles the end of a match. These methods coordinate with helper methods to print the game board, switch turns, and determine winners, ultimately providing a complete Tic-Tac-Toe experience.

### Board
The Board class is designed to represent and manage a Tic-Tac-Toe game board. It provides functionalities for initializing the board with default values, printing the current state of the board, resetting it, and updating its cells based on player moves. Additionally, the class contains methods to check for game-ending conditions such as a win or a draw. Overall, it encapsulates the logic and state required for a simple Tic-Tac-Toe game, offering an interface for game operations and checks.

### Player
The Player class is designed to manage the attributes and statistics related to a player in a game, likely a Tic-Tac-Toe game given the context. The class includes member variables for the player's name and the characters they use in the game. It also maintains statistical data such as total wins, total losses, total draws, total games played, and winning percentage.

The class offers several constructors for initializing these attributes, including default constructors and parameterized ones. Methods are provided for retrieving player information and updating game statistics, like increasing the count of won or lost games and calculating the player's winning percentage. Overall, it encapsulates the state and operations related to a player in the game.

## Additional

### Future Additions
As I continue learning new topics in C#, I plan to keep updating this project. Some possible additions:
- Simple AI
- More Board Styles and game options

