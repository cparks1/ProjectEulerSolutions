using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ProjectEulerPlayground
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //Console.WriteLine(EvenFibsSum(GenFibs()));
            //ulong n = 600851475143;
            //long n = 13195;
            //Console.WriteLine(LargestFactor(n, SieveOfEratosthenes((ulong)n/2)));
            //Console.WriteLine(SieveOfEratosthenes(n));
            //Console.WriteLine(BiggestProductPalindrome());
            //Console.WriteLine(SmallestEvenDivisibleBy(20));
            //Console.WriteLine(DiffBtwnFirstHundred());
            //Console.WriteLine(TenThousandAndFirstPrime());
            //Console.WriteLine(Adj13SumBigNum());
            //Console.WriteLine(pythagproduct());
            //Console.WriteLine(SumPrimesBelow2Mil());
            //Console.WriteLine(GreatestAdjProd());
            //Console.WriteLine(ProjEulerP12());
            //Console.WriteLine(ProjEulerP13().ToString());
            //Console.WriteLine(ProjEulerP14());
            //Console.WriteLine(ProjEulerP15());
            //Console.WriteLine(ProjEulerP16());
            //Console.WriteLine(ProjEulerP19());
            //Console.WriteLine(ProjEulerP20());
            //Console.WriteLine(ProjEulerP21());
            //Console.WriteLine(ProjEulerP22());
            //Console.WriteLine(ProjEulerP586((ulong)Math.Pow(10,15),40));
            //Console.WriteLine(isPermutation("tact", "tcta"));
            //Console.WriteLine(isPermutation("nascar", "nscara"));
            //Console.WriteLine(isPermutation("nascar", "nscar"));
            //ProjEulerP23();
            //Console.WriteLine()

            //Console.WriteLine(ProjEulerP586(new BigInteger(Math.Pow(10, 3)), 1));

            //Console.WriteLine(ProjEulerP586(new BigInteger(Math.Pow(10, 5)), 4));
            //Console.WriteLine(ProjEulerP586(new BigInteger(Math.Pow(10, 8)), 6));
            //Console.WriteLine(ProjEulerP586(BigInteger.Pow(new BigInteger(10), 15), 40));
            //Console.WriteLine(ProjEulerP583(10 * 10 * 10 * 10));
            //Console.WriteLine(ProjEulerP25(1000));
            //Console.WriteLine(ProjEulerP29());
            //Console.WriteLine(ProjEulerP30());
            //Console.WriteLine(ProjEulerP32());
            //Console.WriteLine(ProjEulerP34());
            //Console.WriteLine(ProjEulerP36());
            //Console.WriteLine(ProjEulerP39());
            //Console.WriteLine(ProjEulerP40());
            //Console.WriteLine(ProjEulerP45());
            //Console.WriteLine(ProjEulerP52());
            //Console.WriteLine(ProjEulerP55());
            Console.ReadKey();
        }

        static int ProjEulerP55()
        {
            int result = 0;
            for (int i = 10; i < 10000; i++)
                if (!isLychrel(i))
                    result++;
            return result;
        }

        static bool isLychrel(BigInteger n) // Taking input ulong because 9999 * 2 and then being multiplied by the resultant times to 50 times has a max value of 11 quintillion
        {
            BigInteger result = n + BigInteger.Parse(new string(n.ToString().Reverse().ToArray<char>())); // Add n by its reversal
            if (isPalindrome(result.ToString()))
                return true;
            for (int i=1; i<50; i++) // Start at 1 because we've done one iteration already
            {
                result += BigInteger.Parse(new string(result.ToString().Reverse().ToArray<char>())); // Add the result by its reversal
                if (isPalindrome(result.ToString()))
                    return true;
            }
            return false;
        }

        static int ProjEulerP52()
        {
            for(int i=1; true; i++)
            {
                string istr = i.ToString();
                bool isPermutedMultiples = true;
                for(int j=2; j<=6; j++)
                    if (!isPermutation(istr, (i * j).ToString()))
                        isPermutedMultiples = false;
                if (isPermutedMultiples)
                    return i;
            }
        }

        static long ProjEulerP45() // Bad solution, extremely memory intensive. Doesn't actually return correct answer, either
        {
            Dictionary<long, int> numbertrack = new Dictionary<long, int>(); // Keeps track of numbers found to be triangular, pentagonal, and hexagonal. The key "13" having a value of "1" means it is one of the three, "2" being two of the three, and "3" being that the key is triangular, pentagonal, AND hexagonal.
            for(int n=144; !numbertrack.ContainsValue(3); n++) // Keep increasing n until we have a number that is triangular, pentagonal, and hexagonal.
            {
                long Trikey = TriNum(n); // Generate the given n numbers
                long Pentagkey = PentagNum(n);
                long Hexagkey = HexagNum(n);

                if (numbertrack.ContainsKey(Trikey)) // Add the given n numbers to the dictionary
                    numbertrack[Trikey]++;
                else
                    numbertrack.Add(Trikey, 1);
                if (numbertrack.ContainsKey(Pentagkey))
                    numbertrack[Pentagkey]++;
                else
                    numbertrack.Add(Pentagkey, 1);
                if (numbertrack.ContainsKey(Hexagkey))
                    numbertrack[Hexagkey]++;
                else
                    numbertrack.Add(Hexagkey, 1);
            }
            foreach (KeyValuePair<long, int> kp in numbertrack) // Find the number after 40755 that is triangular, pentagonal, and hexagonal.
                if (kp.Value == 3)
                    return kp.Key;
            return -1; // Indicate error
        }

        static long TriNum(int n) // Generates the triangle number at given number n
        { return n * (n + 1) / 2; }
        static long PentagNum(int n) // Generates the pentagonal number at given number n
        { return n * (3 * n - 1) / 2; }
        static long HexagNum(int n) // Generates the hexagonal number at given number n
        { return n * (2*n - 1); }

        static long ProjEulerP40()
        {
            string dec = ".";
            int i = 1;
            while(dec.Length <= 1000000)
            {
                dec += i.ToString();
                i++;
            }
            long prod = 1;
            for (i = 1; i <= 1000000; i *= 10)
                prod *= long.Parse(dec[i].ToString());
            return prod;
        }

        static int ProjEulerP39()
        {
            Dictionary<int, int> perims = new Dictionary<int, int>(); // The perimeter is the key, and the number of solutions for that perimeter is the value
            for(int a=2; a<=1000; a++)
                for(int b=1; b<=a; b++)
                {
                    double c = Math.Sqrt(a * a + b * b); // Hypotenuse
                    if (c % 1 == 0)
                    {
                        int key = a + b + (int)c; // Perimeter
                        if (key <= 1000)
                            if (perims.ContainsKey(key))
                                perims[key]++;
                            else
                                perims.Add(key, 1);
                    }
                }
            return perims.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        }

        static int ProjEulerP36()
        {
            List<int> bpalin = new List<int>();
            for (int i = 1; i < 1000000; i += 2)
                if (isPalindrome(i.ToString()))
                    if (isPalindrome(Convert.ToString(i, 2)))
                        bpalin.Add(i);
            return bpalin.Sum();
        }

        /*static int ProjEulerP35()
        {
            List<int> cprimes = new List<int>();
            for (int i = 101; i < 1000000; i += 2)
                if(i%3!=0&&i%5!=0&&i%7!=0&&i%11!=0)
        }*/

        public static int ProjEulerP34()
        {
            int[] facts = new int[10];
            List<int> spec = new List<int>();
            for (int i = 0; i < facts.Length; i++)
                facts[i] = SimpleFactorial(i);
            for(int i=3; i<=int.MaxValue; i++)
                if (i.ToString().Sum(x => facts[x - 48]) == i)
                    spec.Add(i);
            return spec.Sum();
        }

        public static int SimpleFactorial(int n)
        {
            return (n <= 1) ? 1 : n * SimpleFactorial(n - 1);
        }

        public static int ProjEulerP32()
        {
            List<int> pandigs = new List<int>();
            for(int i=1; i<9900; i++)
                for(int j=1; j<9900; j++)
                {
                    int prod = i * j;
                    string pandigtest = i.ToString() + j.ToString() + prod.ToString();
                    if (pandigtest.Length == 9)
                        if (allUnique(pandigtest))
                            pandigs.Add(prod);
                }
            return pandigs.Distinct().Sum();
        }

        public static bool allUnique(string s)
        {
            int[] num = new int[127]; // Assuming only ASCII 
            for(int i=0; i<s.Length; i++)
                if (++num[s[i]] > 1)
                    return false;
            return num[48]<1;
        }

        public static int ProjEulerP30()
        {
            List<int> nums = new List<int>();
            int[] pow5 = new int[] { 0, 1, 32, 243, 1024, 3125, 7776, 16807, 32768, 59049 };
            for(int i=2; i<int.MaxValue; i++)
                if (i == i.ToString().Sum(x => pow5[x-48])) // Compares i with the sum of the fifth power of each digit in the number
                    nums.Add(i);
            return nums.Sum();
        }

        public static int ProjEulerP29()
        {
            Dictionary<double, int> distinct_nums = new Dictionary<double, int>();
            for (int a = 2; a <= 100; a++)
                for (int b = 2; b <= 100; b++)
                {
                    double num = Math.Pow(a, b);
                    if (distinct_nums.ContainsKey(num))
                        distinct_nums[num]++;
                    else
                        distinct_nums.Add(num, 1);
                }
            return distinct_nums.Count;
        }

        public static int ProjEulerP25(int n)
        {
            BigInteger fibmin1 = 89;
            BigInteger fib = 144;
            for (int i=13; i<int.MaxValue; i++)
            {
                BigInteger ifib = fib + fibmin1;
                fibmin1 = fib;
                fib = ifib;
                if (ifib.ToString().Length >= n)
                    return i;
            }
            return 0;
        }

        public static ulong ProjEulerP583(int n)
        {
            ulong sum = 0;
            for (int i=1; i<=n; i++) // i= AE, BD; BC = CD = 2i^2
            {
                double BC, CD, BD, AE;
                BD = AE = i; // BD, AE
                BC = CD = Math.Sqrt((BD*BD)/2); // ((BD^2)/2)^1/2
                double h = Math.Sqrt(BC * BC - (BD / 2) * (BD / 2)); // Height of the triangle of heron envelope
                bool isClean = h % 1 == 0 && BC%1==0;
                for (int j = i-1; j > h && isClean; j--)
                    sum += (ulong)(2 * j + AE + BD + BC + BD);


                for (int j = 1; j < i; j++) // Guess the sides of the isosceles
                {

                    for (int k = j; k < i; k++) // Guess the height of the rectangle
                    {

                    }
                }
            }

            return sum;
        }

        public static BigInteger ProjEulerP586(BigInteger n, int r)
        {
            Dictionary<BigInteger, int> dict = new Dictionary<BigInteger, int>();
            for (BigInteger a = 2; 3*a+a*a <= n; a++)
                for (BigInteger b = 1; b < a; b++)
                {
                    /*if (isPowerOfTen(a)) // Remove keys that definitely won't get touched again every power of 10
                    {
                        List<KeyValuePair<BigInteger, int>> keys = new List<KeyValuePair<BigInteger, int>>();
                        keys.AddRange(dict.Where(x => x.Value != r && x.Key < 3 * a + a * a));
                        foreach (KeyValuePair<BigInteger,int> kp in keys)
                            dict.Remove(kp.Key);
                    }*/
                    BigInteger key = a * a + b * b + 3 * a * b;
                    if (key <= n)
                    {
                        if (dict.ContainsKey(key))
                            dict[key]++;
                        else
                            dict.Add(key, 1);
                    }
                    else
                        break;
                }
            return dict.Count(x => x.Value == r);
        }

        public static int ProjEulerP586(int n, int r)
        {
            int[] ks = new int[n];
            for (int a = 1; a <= n; a++)
                for (int b = a - 1; b > 0; b++)
                {
                    int k = a * a + b * b + 3 * a * b;
                    if (k <= n)
                        ks[k - 1]++;
                }

            return ks.Count(x => x == r);
        }

        public static bool isPowerOfTen(BigInteger input)
        {
            return
              input == 1L
            || input == 10L
            || input == 100L
            || input == 1000L
            || input == 10000L
            || input == 100000L
            || input == 1000000L
            || input == 10000000L
            || input == 100000000L
            || input == 1000000000L
            || input == 10000000000L
            || input == 100000000000L
            || input == 1000000000000L
            || input == 10000000000000L
            || input == 100000000000000L
            || input == 1000000000000000L
            || input == 10000000000000000L
            || input == 100000000000000000L
            || input == 1000000000000000000L;
        }

        public static void ProjEulerP23()
        {
            List<int> abundantnumbers = new List<int>();
            List<int> unrepresentable = new List<int>();
            bool[] representables = new bool[28123];
            for (int i = 12; i <= 28123; i++)
                if (isAbundant(i))
                    abundantnumbers.Add(i);

            for (int i = 0; i < representables.Length; i++)
                representables[i] = false;
            for (int i = 0; i < abundantnumbers.Count; i++)
                for (int j = 0; j < abundantnumbers.Count; j++)
                {
                    int sum = abundantnumbers[i] + abundantnumbers[j];
                    if (sum > 28123)
                        break;
                    else
                        representables[sum - 1] = true;
                }
            int totsum = 0;
            for (int i = 0; i < representables.Length; i++)
            {
                if (!representables[i])
                {
                    totsum += i + 1;
                    Console.WriteLine(i + 1);
                }
            }
            Console.WriteLine(totsum);

        }

        public static bool isAbundant(int i)
        {
            return ProperDivisors(i).Sum() > i;
        }

        public static bool isPermutation(string a, string b)
        {
            if (a.Length == b.Length)
            {
                for (int i = 0; i < a.Length; i++)
                    if (b.Count(x=>x==a[i]) != a.Count(x=>x==a[i]))
                        return false;
                return true;
            }
            else
                return false;
        }

        public static int ProjEulerP586(ulong n, ulong r)
        { // Finds the number of integers k <= n that can be expressed as k = a^2 + 3*a*b + b^2
            // with a>b>0

            int numk = 0;
            for (ulong nn = 1; nn <= n; nn++)
            {
                ulong rdiff = 0;
                for (ulong a = 1; a <= nn; a++)
                    for (ulong b = 1; b <= nn; b++)
                    {
                        if (a * a + 3 * a * b + b * b == nn)
                            rdiff++;
                        if (rdiff > r)
                            break;
                    }
                if (rdiff == r)
                    numk++;
            }
            return numk;
        }

        public static ulong ProjEulerP22()
        { // -64 for letter's position in the alphabet
            List<string> names = File.ReadAllLines("p022_names.txt").ToList<string>();
            names.Sort();
            ulong totalscore = 0;
            for (int i = 0; i < names.Count; i++)
            {
                uint wordscore = 0;
                for (int j = 0; j < names[i].Length; j++)
                    wordscore += (uint)(names[i][j] - 64);
                wordscore *= (uint)(i + 1);
                totalscore += wordscore;
            }
            return totalscore;
        }

        public static long ProjEulerP21() // This function finds all amicable numbers under 10,000 and evaluates the sum of all the nubmers
        {
            List<long> AmicableNumbers = new List<long>();
            int[] PropDivSums = new int[9999];
            for (int i = 1; i < 10000; i++)
                PropDivSums[i - 1] = ProperDivisors(i).Sum();

            for (int i = 1; i < 10000; i++)
            {
                int a = i - 1;
                int d_a = PropDivSums[a];
                int b = PropDivSums[a];
                int d_b = (b - 1 <= 9998) ? PropDivSums[b - 1] : ProperDivisors(b).Sum();
                if (d_a == b && d_b == a + 1 && a + 1 != b)
                    AmicableNumbers.Add(i);
            }

            return new List<long>(AmicableNumbers.Distinct()).Sum();
        }

        public static List<int> ProperDivisors(int n)
        { // Returns a list of all the proper divisors of a number, excluding the number itself but including 1
            List<int> pd = new List<int>();
            pd.Add(1);
            for (int i = 2; i < n; i++)
                if (n % i == 0) pd.Add(i);
            return pd;
        }

        public static int ProjEulerP20()
        {
            BigInteger i = new BigInteger(1);
            for (int j = 100; j > 0; j--)
                i *= j;
            string s = i.ToString();
            int sum = 0;
            for (int j = 0; j < s.Length; j++)
                sum += int.Parse(s[j].ToString());
            return sum;
        }

        public static int ProjEulerP19()
        {
            int n = 0;
            for (int y = 1901; y <= 2000; y++)
                for (int m = 1; m <= 12; m++)
                    if (new DateTime(y, m, 1).DayOfWeek == DayOfWeek.Sunday) n++;
            return n;
        }

        public static int ProjEulerP16() // Incorrect
        {
            string[] nums = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninety", "hundred", "thousand" };
            string bigstring = "";
            for (int i = 1; i <= 1000; i++)
            {
                Console.WriteLine(NumToWord(i));
                bigstring += NumToWord(i);
            }
            return bigstring.Length;
        }

        public static string NumToWord(int i)
        { // If i is 1000, give "onethousand", 100-999 give first number plus next two digits. If middle digit is 0, give last digit number. If middle digit is not 1, give "teen", else give "twenty" .. "ninety" and last digit
            string[] nums = new string[] { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string[] teens = new string[] { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] enties = new string[] { "error", "error", "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            string hundred = "hundred";
            string thousand = "thousand";
            string s = i.ToString();
            switch(s.Length)
            {
                case 0:
                    return "";
                case 1:
                    return (i!=0)?nums[i]:"";
                case 2:
                    return (i < 20) ? teens[int.Parse(s[1].ToString())] : enties[int.Parse(s[0].ToString())] + nums[int.Parse(s[1].ToString())];
                case 3:
                    return nums[int.Parse(s[0].ToString())] + ((s[1]=='0'&&s[2]=='0')?hundred:hundred+"and") + NumToWord(int.Parse(s.Remove(0, 1)));
                case 4:
                    return "one" + thousand;
                default:
                    return "error";
            }
        }

        public static BigInteger ProjEulerP15()
        {
            BigInteger bigint = new BigInteger(1);
            for (int i = 0; i < 1000; i++) // Runs 1000 times
                bigint *= 2; // Add 1 to the exponent
            string s = bigint.ToString();
            ulong sum = 0;
            for (int i = 0; i < s.Length; i++)
                sum += ulong.Parse(s[i].ToString());
            return sum;
        }

        public static ulong ProjEulerP14()
        {
            ulong bigchain = 0; ulong chainsize = 0;
            for (ulong i = 1; i < 1000000; i++)
            {
                ulong csz = CollatzChain(i);
                if (csz > chainsize) { bigchain = i; chainsize = csz; }
            }
            return bigchain;
        }

        public static ulong CollatzChain(ulong n) // Returns the number of elements in the chain
        {
            ulong numinchain = 0;
            while(n!=1)
            {
                if (n % 2 == 0) // n is even
                    n /= 2;
                else // n is odd
                    n = 3 * n + 1;
                numinchain++;
            }
            return numinchain;
        }

        public static BigInteger ProjEulerP13()
        {
            BigInteger[] nums = new BigInteger[] {  BigInteger.Parse("37107287533902102798797998220837590246510135740250"),
                                                    BigInteger.Parse("46376937677490009712648124896970078050417018260538"),
                                                    BigInteger.Parse("74324986199524741059474233309513058123726617309629"),
                                                    BigInteger.Parse("91942213363574161572522430563301811072406154908250"),
                                                    BigInteger.Parse("23067588207539346171171980310421047513778063246676"),
                                                    BigInteger.Parse("89261670696623633820136378418383684178734361726757"),
                                                    BigInteger.Parse("28112879812849979408065481931592621691275889832738"),
                                                    BigInteger.Parse("44274228917432520321923589422876796487670272189318"),
                                                    BigInteger.Parse("47451445736001306439091167216856844588711603153276"),
                                                    BigInteger.Parse("70386486105843025439939619828917593665686757934951"),
                                                    BigInteger.Parse("62176457141856560629502157223196586755079324193331"),
                                                    BigInteger.Parse("64906352462741904929101432445813822663347944758178"),
                                                    BigInteger.Parse("92575867718337217661963751590579239728245598838407"),
                                                    BigInteger.Parse("58203565325359399008402633568948830189458628227828"),
                                                    BigInteger.Parse("80181199384826282014278194139940567587151170094390"),
                                                    BigInteger.Parse("35398664372827112653829987240784473053190104293586"),
                                                    BigInteger.Parse("86515506006295864861532075273371959191420517255829"),
                                                    BigInteger.Parse("71693888707715466499115593487603532921714970056938"),
                                                    BigInteger.Parse("54370070576826684624621495650076471787294438377604"),
                                                    BigInteger.Parse("53282654108756828443191190634694037855217779295145"),
                                                    BigInteger.Parse("36123272525000296071075082563815656710885258350721"),
                                                    BigInteger.Parse("45876576172410976447339110607218265236877223636045"),
                                                    BigInteger.Parse("17423706905851860660448207621209813287860733969412"),
                                                    BigInteger.Parse("81142660418086830619328460811191061556940512689692"),
                                                    BigInteger.Parse("51934325451728388641918047049293215058642563049483"),
                                                    BigInteger.Parse("62467221648435076201727918039944693004732956340691"),
                                                    BigInteger.Parse("15732444386908125794514089057706229429197107928209"),
                                                    BigInteger.Parse("55037687525678773091862540744969844508330393682126"),
                                                    BigInteger.Parse("18336384825330154686196124348767681297534375946515"),
                                                    BigInteger.Parse("80386287592878490201521685554828717201219257766954"),
                                                    BigInteger.Parse("78182833757993103614740356856449095527097864797581"),
                                                    BigInteger.Parse("16726320100436897842553539920931837441497806860984"),
                                                    BigInteger.Parse("48403098129077791799088218795327364475675590848030"),
                                                    BigInteger.Parse("87086987551392711854517078544161852424320693150332"),
                                                    BigInteger.Parse("59959406895756536782107074926966537676326235447210"),
                                                    BigInteger.Parse("69793950679652694742597709739166693763042633987085"),
                                                    BigInteger.Parse("41052684708299085211399427365734116182760315001271"),
                                                    BigInteger.Parse("65378607361501080857009149939512557028198746004375"),
                                                    BigInteger.Parse("35829035317434717326932123578154982629742552737307"),
                                                    BigInteger.Parse("94953759765105305946966067683156574377167401875275"),
                                                    BigInteger.Parse("88902802571733229619176668713819931811048770190271"),
                                                    BigInteger.Parse("25267680276078003013678680992525463401061632866526"),
                                                    BigInteger.Parse("36270218540497705585629946580636237993140746255962"),
                                                    BigInteger.Parse("24074486908231174977792365466257246923322810917141"),
                                                    BigInteger.Parse("91430288197103288597806669760892938638285025333403"),
                                                    BigInteger.Parse("34413065578016127815921815005561868836468420090470"),
                                                    BigInteger.Parse("23053081172816430487623791969842487255036638784583"),
                                                    BigInteger.Parse("11487696932154902810424020138335124462181441773470"),
                                                    BigInteger.Parse("63783299490636259666498587618221225225512486764533"),
                                                    BigInteger.Parse("67720186971698544312419572409913959008952310058822"),
                                                    BigInteger.Parse("95548255300263520781532296796249481641953868218774"),
                                                    BigInteger.Parse("76085327132285723110424803456124867697064507995236"),
                                                    BigInteger.Parse("37774242535411291684276865538926205024910326572967"),
                                                    BigInteger.Parse("23701913275725675285653248258265463092207058596522"),
                                                    BigInteger.Parse("29798860272258331913126375147341994889534765745501"),
                                                    BigInteger.Parse("18495701454879288984856827726077713721403798879715"),
                                                    BigInteger.Parse("38298203783031473527721580348144513491373226651381"),
                                                    BigInteger.Parse("34829543829199918180278916522431027392251122869539"),
                                                    BigInteger.Parse("40957953066405232632538044100059654939159879593635"),
                                                    BigInteger.Parse("29746152185502371307642255121183693803580388584903"),
                                                    BigInteger.Parse("41698116222072977186158236678424689157993532961922"),
                                                    BigInteger.Parse("62467957194401269043877107275048102390895523597457"),
                                                    BigInteger.Parse("23189706772547915061505504953922979530901129967519"),
                                                    BigInteger.Parse("86188088225875314529584099251203829009407770775672"),
                                                    BigInteger.Parse("11306739708304724483816533873502340845647058077308"),
                                                    BigInteger.Parse("82959174767140363198008187129011875491310547126581"),
                                                    BigInteger.Parse("97623331044818386269515456334926366572897563400500"),
                                                    BigInteger.Parse("42846280183517070527831839425882145521227251250327"),
                                                    BigInteger.Parse("55121603546981200581762165212827652751691296897789"),
                                                    BigInteger.Parse("32238195734329339946437501907836945765883352399886"),
                                                    BigInteger.Parse("75506164965184775180738168837861091527357929701337"),
                                                    BigInteger.Parse("62177842752192623401942399639168044983993173312731"),
                                                    BigInteger.Parse("32924185707147349566916674687634660915035914677504"),
                                                    BigInteger.Parse("99518671430235219628894890102423325116913619626622"),
                                                    BigInteger.Parse("73267460800591547471830798392868535206946944540724"),
                                                    BigInteger.Parse("76841822524674417161514036427982273348055556214818"),
                                                    BigInteger.Parse("97142617910342598647204516893989422179826088076852"),
                                                    BigInteger.Parse("87783646182799346313767754307809363333018982642090"),
                                                    BigInteger.Parse("10848802521674670883215120185883543223812876952786"),
                                                    BigInteger.Parse("71329612474782464538636993009049310363619763878039"),
                                                    BigInteger.Parse("62184073572399794223406235393808339651327408011116"),
                                                    BigInteger.Parse("66627891981488087797941876876144230030984490851411"),
                                                    BigInteger.Parse("60661826293682836764744779239180335110989069790714"),
                                                    BigInteger.Parse("85786944089552990653640447425576083659976645795096"),
                                                    BigInteger.Parse("66024396409905389607120198219976047599490197230297"),
                                                    BigInteger.Parse("64913982680032973156037120041377903785566085089252"),
                                                    BigInteger.Parse("16730939319872750275468906903707539413042652315011"),
                                                    BigInteger.Parse("94809377245048795150954100921645863754710598436791"),
                                                    BigInteger.Parse("78639167021187492431995700641917969777599028300699"),
                                                    BigInteger.Parse("15368713711936614952811305876380278410754449733078"),
                                                    BigInteger.Parse("40789923115535562561142322423255033685442488917353"),
                                                    BigInteger.Parse("44889911501440648020369068063960672322193204149535"),
                                                    BigInteger.Parse("41503128880339536053299340368006977710650566631954"),
                                                    BigInteger.Parse("81234880673210146739058568557934581403627822703280"),
                                                    BigInteger.Parse("82616570773948327592232845941706525094512325230608"),
                                                    BigInteger.Parse("22918802058777319719839450180888072429661980811197"),
                                                    BigInteger.Parse("77158542502016545090413245809786882778948721859617"),
                                                    BigInteger.Parse("72107838435069186155435662884062257473692284509516"),
                                                    BigInteger.Parse("20849603980134001723930671666823555245252804609722"),
                                                    BigInteger.Parse("53503534226472524250874054075591789781264330331690")};

            BigInteger sum = new BigInteger();
            for (int i = 0; i < nums.Length; i++)
                sum += nums[i];
            return sum;
        }

        public static ulong ProjEulerP12()
        {
            ulong sum = 0;
            for (ulong i = 1; i <= int.MaxValue; i++)
                if (NumFactors(sum+=i) > 500)
                    return sum;
            return 0;
        }

        public static int NumFactors(ulong n)
        {
            int nf = 0;
            nf = (n % 2 == 0) ? 4 : (n == 1) ? 1 : 0;
            for (ulong i = 3; i <= n/2; i++)
                if (n % i == 0) nf++;
            return nf;
        }

        /*public static ulong PrimeFactors(ulong n)
        { // Breaks a number down into its prime factorization form
            for(ulong i=n/2+1; i>0; i--)
            {
                if (n % i == 0) // If evenly divisible
                    PrimeFactors(n);
            }
        }*/

        public static ulong GreatestAdjProd()
        {
            ulong[,] numbers = new ulong[,]{{08, 02, 22, 97, 38, 15, 00, 40, 00, 75, 04, 05, 07, 78, 52, 12, 50, 77, 91, 08},
                                            {49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48, 04, 56, 62, 00},
                                            {81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30, 03, 49, 13, 36, 65},
                                            {52, 70, 95, 23, 04, 60, 11, 42, 69, 24, 68, 56, 01, 32, 56, 71, 37, 02, 36, 91},
                                            {22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80},
                                            {24, 47, 32, 60, 99, 03, 45, 02, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50},
                                            {32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70},
                                            {67, 26, 20, 68, 02, 62, 12, 20, 95, 63, 94, 39, 63, 08, 40, 91, 66, 49, 94, 21},
                                            {24, 55, 58, 05, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72},
                                            {21, 36, 23, 09, 75, 00, 76, 44, 20, 45, 35, 14, 00, 61, 33, 97, 34, 31, 33, 95},
                                            {78, 17, 53, 28, 22, 75, 31, 67, 15, 94, 03, 80, 04, 62, 16, 14, 09, 53, 56, 92},
                                            {16, 39, 05, 42, 96, 35, 31, 47, 55, 58, 88, 24, 00, 17, 54, 24, 36, 29, 85, 57},
                                            {86, 56, 00, 48, 35, 71, 89, 07, 05, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58},
                                            {19, 80, 81, 68, 05, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77, 04, 89, 55, 40},
                                            {04, 52, 08, 83, 97, 35, 99, 16, 07, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66},
                                            {88, 36, 68, 87, 57, 62, 20, 72, 03, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69},
                                            {04, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18, 08, 46, 29, 32, 40, 62, 76, 36},
                                            {20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74, 04, 36, 16},
                                            {20, 73, 35, 29, 78, 31, 90, 01, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57, 05, 54},
                                            {01, 70, 54, 71, 83, 51, 54, 69, 16, 92, 33, 48, 61, 43, 52, 01, 89, 19, 67, 48}};

            ulong bigprod = 0;
            for (int r = 0; r < 20; r++)
                for (int c = 0; c < 20; c++)
                {
                    ulong prod = 1;
                    if (r <= 16)
                    {
                        for (int i = 0; i < 4; i++)
                            prod *= numbers[r + i, c];
                        if (prod > bigprod) bigprod = prod;
                    }
                    prod = 1;
                    if(c<=16)
                    {
                        for (int i = 0; i < 4; i++)
                            prod *= numbers[r, c + i];
                        if (prod > bigprod) bigprod = prod;
                    }
                    prod = 1;
                    if(r<=16&&c<=16)
                    {
                        for (int i = 0; i < 4; i++)
                            prod *= numbers[r + i, c + i];
                        if (prod > bigprod) bigprod = prod;
                    }
                    prod = 1;
                    if(r<=16&&c>=4)
                    {
                        for (int i = 0; i < 4; i++)
                            prod *= numbers[r + i, c - i];
                        if (prod > bigprod) bigprod = prod;
                    }
                    prod = 1;
                    if(r>=4&&c<=16)
                    {
                        for (int i = 0; i < 4; i++)
                            prod *= numbers[r - i, c + i];
                        if (prod > bigprod) bigprod = prod;
                    }
                }

            for (int r = 19; r >= 0; r--)
                for (int c = 19; c >= 0; c--)
                {
                    ulong prod = 1;
                    if (r >= 4)
                    {
                        for (int i = 0; i < 4; i++)
                            prod *= numbers[r - i, c];
                        if (prod > bigprod) bigprod = prod;
                    }
                    prod = 1;
                    if (c >= 4)
                    {
                        for (int i = 0; i < 4; i++)
                            prod *= numbers[r, c - i];
                        if (prod > bigprod) bigprod = prod;
                    }
                    prod = 1;
                    if (r >= 4 && c >= 4)
                    {
                        for (int i = 0; i < 4; i++)
                            prod *= numbers[r - i, c - i];
                        if (prod > bigprod) bigprod = prod;
                    }
                }

            return bigprod;
        }

        public static ulong SumPrimesBelow2Mil()
        {
            ulong sum = 0;
            for (ulong i = 0; i < 2000000; i++)
                if (IsPrime(i))
                    sum += i;
            return sum;
        }

        public static int pythagproduct()
        {
            for(int a = 1; a<1000; a++)
                for(int b=1;b<1000;b++)
                    for(int c=1;c<1000;c++)
                        if (a + b + c == 1000)
                            if (a * a + b * b == c * c)
                                return a * b * c;
            return -1;
        }

        public static ulong Adj13SumBigNum()
        {
            string s = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";
            ulong bigprod = 1;
            for (int i = 0; i < s.Length - 13; i++)
            {
                ulong prod = 1;
                for (int j = 0; j < 13; j++)
                    prod *= (ulong)int.Parse(s[i + j].ToString());
                if (prod > bigprod) bigprod = prod;
            }
            return bigprod;
        }

        public static int TenThousandAndFirstPrime()
        {
            int primes = 0;
            for (ulong i = 0; i < int.MaxValue; i++)
                if (IsPrime(i))
                {
                    primes++;
                    if (primes == 10001)
                        return (int)i;
                }
            return 0;
        }

        public static int DiffBtwnFirstHundred()
        {//difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
            int sumsq = 0;
            int sum = 0;
            for (int i=1; i<=100; i++)
                sumsq += i * i;
            for (int i = 1; i <= 100; i++)
                sum += i;
            return sumsq - (sum * sum);
        }

        public static int SmallestEvenDivisibleBy(int n) // This function will find the smallest number evenly divisible by all numbers up to the given number
        {
            for (int i = 1; i < int.MaxValue; i++)
            {
                bool smallest = true;
                for (int j = 1; j <= n && smallest; j++)
                    if (i % j != 0)
                        smallest = false;
                if (smallest) return i;
            }
            return -1;
        }

        public static int BiggestProductPalindrome() // This function will find the biggest palindrome of the multiplication of 2 numbers that're 3 digits.
        {
            int bigpalin = 0;
            for (int i = 100; i <= 999; i++)
                for (int j = 100; j <= 999; j++)
                    if (isPalindrome((i * j).ToString()) && i*j>bigpalin)
                        bigpalin = i * j;
            return bigpalin;
        }

        public static bool isPalindrome(string p)
        {
            for (int i = 0; i < p.Length / 2; i++)
                if (p[i] != p[p.Length - (1 + i)])
                    return false;
            return true;
        }

        public static List<long> GenPrimes() // This function generates all primes up to 600851475143
        {
            List<long> primes = new List<long>();
            for (long i = 2; i <= 600851475143; i++)
            {
                bool isprime = true;
                for (long j = 2; j < i; j++)
                    if (i % j == 0)
                        isprime = false;
                if (isprime) primes.Add(i);
            }
            return primes;
        }

        static bool IsPrime(ulong number)
        {
            if (number <= 1) return false; // zero and one are not prime
            for (ulong i = 2; i * i <= number; i++)
                if (number % i == 0) return false;
            return true;
        }

        public static ulong SieveOfEratosthenes(ulong n) // This function will find the largest prime factor of a number, n
        { // This function is definitely not a sieve of eratosthenes and doesn't really efficiently find the prime factor of a number at all never use this again ok
            /*ulong largefac = 0;
            for (ulong i = 0; i < n/2; i++)
                if (i % 2 != 0 && i % 3 != 0 && i % 5 != 0 && i % 7 != 0)
                    if (n % i == 0)
                        if (i > largefac)
                            largefac = i;*/
            for (ulong i = 0; i < 1000000; i++)
                if (IsPrime(i))
                    if (n % i == 0) // If our number is divisible by the prime, i
                        if (IsPrime(n / i))
                            return n / i;

            for (ulong i = n/1000000+1; i > 0; i--)
                //if (i % 2 != 0 && i % 3 != 0 && i % 5 != 0 && i % 7 != 0)
                if (IsPrime(i))
                    if (n % i == 0)
                        return i;

            return 0;

            //return largefac;
            /*BigArray<long> sea = new BigArray<long>((ulong)n);
            //long[] sea = new long[n];
            //List<long> sea = new List<long>();
            for (ulong i = 2; i < n; i++)
                sea[i] = (long)i;//sea.Add(i);
            for (ulong i = 4; i < sea.Length; i += 2)
                sea[i] = 0;
            for (ulong i = 6; i < sea.Length; i += 3)
                sea[i] = 0;
            for (ulong i = 10; i < sea.Length; i += 5)
                sea[i] = 0;
            for (ulong i = 14; i < sea.Length; i += 7)
                sea[i] = 0;
            List<long> ret = new List<long>();
            for (ulong i = 0; i < sea.Length; i++)
                if (sea[i] != 0) ret.Add(sea[i]);
            return ret;*/
        }

        public static ulong LargestFactor(ulong n, List<ulong> primes) // Finds the largest prime factor of a number given a list of prime numbers and the number
        {
            ulong largefactor = 0;
            for (int i = 0; i < primes.Count; i++)
                if (n % primes[i] == 0 && primes[i] > largefactor)
                    largefactor = primes[i];
            return largefactor;
        }

        public static List<int> GenFibs() // This function will generate fibonacci numbers until they are greater than 4 million
        {
            List<int> fibs = new List<int>() { 0, 1, 1 };
            int nextfib = fibs[2] + fibs[1];
            fibs.Add(nextfib);
            int i = 3;
            while (nextfib < 4000000)
            {
                nextfib = fibs[i] + fibs[i - 1];
                if (nextfib < 4000000)
                    fibs.Add(nextfib);
                else
                    return fibs;
                i++;
            }
            return fibs;
        }

        public static long EvenFibsSum(List<int> fibs) // This function will generate the sum of all the even fibonacci numbers less than 4 million.
        {
            long sum = 0;
            foreach (int i in fibs.Where(x => x % 2 == 0))
                sum += i;
            return sum;
        }
    }
}