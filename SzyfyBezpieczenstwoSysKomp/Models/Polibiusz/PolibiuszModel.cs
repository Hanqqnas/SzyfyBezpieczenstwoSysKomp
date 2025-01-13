namespace SzyfyBezpieczenstwoSysKomp.Models
{
    public class PolibiuszModel
    {
        public static readonly char[] PolishAlphabet = 
        {
            'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 
            'l', 'ł', 'm', 'n', 'ń', 'o', 'ó', 'p', 'q', 'r', 's', 'ś', 't', 'u', 
            'v', 'w', 'x', 'y', 'z', 'ź', 'ż'
        };

        public char[,] GenerateRandomGrid()
        {
            var shuffled = PolishAlphabet.OrderBy(x => Guid.NewGuid()).ToArray();
            var grid = new char[5, 7];
            for (int i = 0; i < 35; i++)
            {
                grid[i / 7, i % 7] = shuffled[i];
            }
            return grid;
        }

        public string EncryptWithPolibius(string input, char[,] grid)
        {
            var encrypted = new List<string>();
            foreach (var letter in input.ToLower())
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (grid[i, j] == letter)
                        {
                            encrypted.Add($"{i + 1}{j + 1}");
                            break;
                        }
                    }
                }
            }
            return string.Join(" ", encrypted);
        }

        public int CalculateY(int x)
        {
            Random random = new Random();
            int a = random.Next(1, 11); 
            int b = random.Next(1, 11); 
            return a * x * x + b;
        }
        
    }
}