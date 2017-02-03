using System;
using System.Collections.Specialized;
using System.Diagnostics;

namespace BenchmarkEvenStrategy
{
    class Program
    {
        static void Main(string[] args)
        {
            int loops = 1000;

            Console.WriteLine(new BenchmarkModulo(loops).Output());

            Console.WriteLine(new BenchmarkBytes(loops).Output());

            Console.WriteLine(new BenchmarkBitMask(loops).Output());

            Console.WriteLine(new BenchmarkBitVector(loops).Output());

            Console.ReadKey();

            //Subtract 1 and divide by 2?
        }

        abstract class Benchmark
        {
            protected string Name { get; set; }

            protected Stopwatch Time { get; set; }

            protected int Loops { get; set; }

            public Benchmark(int loops)
            {
                Time = new Stopwatch();
                Loops = loops;
            }

            public abstract bool IsEven(int number);

            public TimeSpan Bench()
            {
                Time.Start();

                for (int i = 1; i <= Loops; i++)
                {
                    IsEven(i);
                }

                Time.Stop();

                return Time.Elapsed;
            }

            public string Test()
            {
                string output = "";

                for (int i = 1; i <= Loops; i++)
                {
                    output += i + " = ";

                    if (IsEven(i))
                        output += "Even\n";
                    else
                        output += "Odd\n";
                }

                return output;
            }

            public string Output()
            {
                return Name + " time: " + Bench();
            }
        }

        class BenchmarkModulo : Benchmark
        {
            public BenchmarkModulo(int loops) : base(loops) { Name = "Modulo"; }

            public override bool IsEven(int number)
            {
                return (number % 2 == 0);
            }
        }

        class BenchmarkBytes : Benchmark
        {
            public BenchmarkBytes(int loops) : base(loops) { Name = "Bytes"; }

            public override bool IsEven(int number)
            {
                return !((number & 0x1) == 1);
            }
        }

        class BenchmarkBitMask : Benchmark
        {
            public BenchmarkBitMask(int loops) : base(loops) { Name = "BitMask"; }

            public override bool IsEven(int number)
            {
                return !((number & 1) == 1);
            }
        }

        class BenchmarkBitVector : Benchmark
        {
            public BenchmarkBitVector(int loops) : base(loops) { Name = "BitVector"; }

            public override bool IsEven(int number)
            {
                return !new BitVector32(number)[number - 1];
            }
        }
    }
}
