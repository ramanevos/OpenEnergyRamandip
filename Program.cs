
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Welcome to Ramandip Open Energy Market Technical Task \n");

        int gameModeChoice = await GetGameModeChoice();

        int? prevUserChoice = null;
        while (true)
        {
            Console.WriteLine(gameModeChoice == 1 ? "\nChoose your move: \n(1) Rock \n(2) Paper \n(3) Scissors \n(4) Quit" :
                                                    "\nChoose your move: \n(1) Rock \n(2) Paper \n(3) Scissors \n(4) Lizard \n(5) Spock \n(6) Quit");
            string[] moves = gameModeChoice == 1 ? new string[] { "Rock", "Paper", "Scissors" } :
                                                    new string[] { "Rock", "Paper", "Scissors", "Lizard", "Spock" };
            int userChoice = await GetUserChoice(gameModeChoice);

            if (userChoice == moves.Length)
            {
                Console.WriteLine("Thanks for playing!");
                break;
            }

            int computerChoiceRandom = GetComputerChoice(gameModeChoice);
            int computerChoicePrevUser = prevUserChoice ?? GetComputerChoice(gameModeChoice);

            DetermineWinner(userChoice, computerChoiceRandom, computerChoicePrevUser);

            prevUserChoice = userChoice;
        }
    }

    static async Task<int> GetGameModeChoice()
    {
        Console.WriteLine("Choose game mode: \n(1) Normal Rock, Paper, Scissors, \n(2) Rock, Paper, Scissors, Lizard, Spock");
        int choice;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out choice) && (choice == 1 || choice == 2))
            {
                return choice;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 1 or 2.");
            }
        }
    }

    static async Task<int> GetUserChoice(int gameModeChoice)
    {
        int choice;
        int maxChoice = (gameModeChoice == 1) ? 4 : 6;

        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= maxChoice)
            {
                return choice - 1;
            }
            else
            {
                Console.WriteLine("Invalid input, insert a number");
            }
        }
    }

    static int GetComputerChoice(int gameModeChoice)
    {
        Random rand = new Random();
        return gameModeChoice == 1 ? rand.Next(0, 3) : rand.Next(0, 5);
    }

    static void DetermineWinner(int userChoice, int computerChoiceRandom, int computerChoicePrevUser)
    {
        string[] moves = { "Rock", "Paper", "Scissors", "Lizard", "Spock" };
        Console.WriteLine($"You chose: {moves[userChoice]}");
        Console.WriteLine($"Computer 1 (Random) chose: {moves[computerChoiceRandom]}");
        Console.WriteLine($"Computer 2 (PrevUserChoice): {moves[computerChoicePrevUser]}");

        DetermineOutcome("Computer 1 (Random)", userChoice, computerChoiceRandom);
        DetermineOutcome("Computer 2 (PrevUserChoice)", userChoice, computerChoicePrevUser);
    }

    static void DetermineOutcome(string opponent, int userChoice, int opponentChoice)
    {
        ConsoleColor color = ConsoleColor.Yellow;
        string outcome = "It's a TIE";

        if ((userChoice == 0 && opponentChoice == 2) ||
            (userChoice == 1 && opponentChoice == 0) ||
            (userChoice == 2 && opponentChoice == 1) ||
            (userChoice == 3 && (opponentChoice == 1 || opponentChoice == 4)) ||
            (userChoice == 4 && (opponentChoice == 0 || opponentChoice == 3)))
        {
            color = ConsoleColor.Green;
            outcome = "You WIN";
        }
        else if (userChoice != opponentChoice)
        {
            color = ConsoleColor.Red;
            outcome = "You LOSE";
        }

        Console.ForegroundColor = color;
        Console.WriteLine($"{outcome} against {opponent}!");
        Console.ResetColor();
    }

}
