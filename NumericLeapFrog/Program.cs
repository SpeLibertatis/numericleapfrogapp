//Get close to the number without going over.
//Each guess is added to the previous guesses.
//When you are within 3 or go over the game is over.
//Get within 3 with as few guesses as possible.

using System;

//Create random number that the computer picks.

List<int> numbers = new List<int>();
for (int i = 1; i <= 999; i++)
{
    numbers.Add(i);
}
int userGuess = 0;
int totalUserGuess = 0;
Random.Shared.Shuffle(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(numbers));
int randomNumber = numbers.FirstOrDefault();

bool isGameOver = false;
Console.WriteLine("Welcome to Numeric Leap Frog.");
Console.WriteLine("Goal is to keep guessing numbers to guess what the computer picked.");
Console.WriteLine("Each guess will be added to the last. If you come within 3 of the number you win. Don't go over");
Console.WriteLine();
do
{ 
    Console.Write("Guess a number: ");
    userGuess = int.Parse(Console.ReadLine());
    totalUserGuess = totalUserGuess + userGuess;
    bool isWithinThree = Math.Abs(randomNumber - totalUserGuess) <= 3;
    if (isWithinThree == true)
    {
        Console.WriteLine("You're a winner");
        isGameOver = true;
    }
    else if (totalUserGuess > randomNumber)
    {
        isGameOver = true;
        Console.WriteLine();
        Console.WriteLine("I'm sorry you lost come back soon and play again!");
    }
    else
    {
        Console.WriteLine("Not quite there yet, keep guessing.");
        Console.WriteLine("Press enter to continue.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine($"Your total so far is: {totalUserGuess}");
    }
} while (isGameOver == false);
    Console.ReadLine();