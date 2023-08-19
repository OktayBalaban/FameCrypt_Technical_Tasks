// See https://aka.ms/new-console-template for more information
using RobotProblem;

namespace MillerRabinTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Programdan 'Çıkış' yazarak çıkabilirsiniz");

            string inputString = "dummy";
            Random rand = new Random();


            while (true)
            {
                bool continueFlag = false; // Check boolean for returning to the beginning of while loop for some checkpoints
                List<string> results = new List<string>();

                Console.WriteLine("Lütfen pozitif bir tam sayı girin: ");

                inputString = Console.ReadLine();
                if (inputString == "Çıkış")
                {
                    break;
                }
            
                // First, check for input validity
                bool isValid = Int32.TryParse(inputString, out int number);
                while (!isValid || number <= 0)
                {
                    Console.WriteLine("Hatalı giriş! Lütfen pozitif bir sayı girin:");
                    inputString = Console.ReadLine();
                    isValid = Int32.TryParse(inputString, out number);
                }

                Console.WriteLine("Girilen sayı: " +  inputString);

                // Initial check for special cases of n equals to 1 or n is an even number
                if (number == 1 || number % 2 == 0)
                {
                    if (number == 2)
                    {
                        Console.WriteLine("2, asal bir sayıdır");

                        results.Add("Girilen sayı: " + inputString);
                        results.Add(inputString + ", asal bir sayıdır");

                        DataSaver.SaveResults(results);

                        continue;
                    }

                    Console.WriteLine(inputString + ", asal bir sayı değildir");

                    results.Add("Girilen sayı: " + inputString);
                    results.Add(inputString + ", asal bir sayı değildir");
                    DataSaver.SaveResults(results);

                    continue;
                }

                for (int i = 0; i < 20; i++) 
                {
                    // Greatest Common Divisor Test
                    if (getGreatestCommonDivisor(number) > 1)
                    {
                        Console.WriteLine(inputString + ", asal bir sayı değildir");
                        continueFlag = true;
                        break;
                    }
                }

                if(continueFlag) 
                {
                    results.Add("Girilen sayı: " + inputString);
                    results.Add(inputString + ", asal bir sayı değildir");
                    DataSaver.SaveResults(results);
                    continue;
                }

                // Find d and r from (n - 1 = d * 2^r) formula
                KeyValuePair<int, int> parameters = findFormulaParameters(number);

            
                for (int i = 0; i < 20; i++)
                {
                    int a = rand.Next(2, number - 2);

                    // x = a^d (mod n) formula test
                    if (!firstFormulaTest(number, parameters.Key, a))
                    {
                        if (!secondFormulaTest(number, parameters.Key, a, parameters.Value))
                        {
                            Console.WriteLine(inputString + ", asal bir sayı değildir");
                            continueFlag = true;
                            break;
                        }
                    }
                }

                if (continueFlag)
                {
                    results.Add("Girilen sayı: " + inputString);
                    results.Add(inputString + ", asal bir sayı değildir");
                    DataSaver.SaveResults(results);
                    continue;
                }

   
                results.Add("Girilen sayı: " + inputString);
                results.Add(inputString + ", asal bir sayıdır");

                DataSaver.SaveResults(results);

                Console.WriteLine(inputString + ", asal bir sayıdır");
            }
            return;
        }

        private static int getGreatestCommonDivisor(int number)
        {
            Random rand = new Random();
            int randomNumber = rand.Next(2, number - 2);

            // Euclidian algorithm is deployed
            while (randomNumber != 0)
            {
                int remainder = number % randomNumber;
                number = randomNumber;
                randomNumber = remainder;
            }
            return number;

        }

        private static KeyValuePair<int, int> findFormulaParameters(int number)
        {
            // In order for (n - 1 = d * 2^r) equation to hold true:
            // We already know that n is odd (even number is already returned as not prime) => (n - 1) is even => (d * 2^r) is even
            // We can find the d and r such that r is the largest power and d is odd (if it would be even, r is not the largest)

            number = number - 1;
            int r = 0;

            // Keep dividing by 2 until get an odd number (not the power of 2) so that we will find the largest r
            while ((number % 2) == 0) 
            {
                number = number / 2;
                r++;
            }

            // d is the number what remained
            int d = number;

            return new KeyValuePair<int, int>(d, r);
        }

        // x = a^d (mod n) Test
        private static bool firstFormulaTest(int number, int d, int a)
        {
            int x = modularExponentiation(a, d, number);

            if (x == 1 || x == (number - 1))
            {
                return true;
            }

            return false;
        }

        // Update x to (x^2 mod n) Test
        private static bool secondFormulaTest(int number, int d, int a, int r)
        {
            int x = modularExponentiation(a, d, number);

            if (x == number - 1)
            {
                return true;
            }

            for (int i = 1; i < r; i++)
            {
                x = (x * x) % number;

                if (x == number - 1)
                {
                    return true;
                }

            }

            return false;
        }

        // We need a better exponential modulus operation as we deal with large numbers
        private static int modularExponentiation(int baseValue, int exponent, int modulus)
        {
            int result = 1;
            baseValue = baseValue % modulus;

            while (exponent > 0)
            {
                if (exponent % 2 == 1)
                    result = (result * baseValue) % modulus;

                exponent = exponent / 2;
                baseValue = (baseValue * baseValue) % modulus;
            }

            return result;
        }
    }
}
