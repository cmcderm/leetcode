public class FizzBuzz {
    private int max;
    Mutex output_mutex = new Mutex();
    private string[] output;

    private Action printFizz;
    private Action printBuzz;
    private Action printFizzBuzz;
    private Action printNumber;

    public FizzBuzz(int max) {
        this.max = max;
        output = new string[max];
    }

    public addOutput(int i, Action o) {
        output_mutex.WaitOne();

        output[i] = o;

        output_mutex.ReleaseMutex();
    }

    public SendOutput() {
        foreach(Action a in output) {a()}
    }

    // printFizz() outputs "fizz".
    public void Fizz(Action printFizz) {
        this.printFizz = printFizz;
        for (int i = 1; i < this.max; i += 3) {
            addOutput(i, "")
        }
    }

    // printBuzzz() outputs "buzz".
    public void Buzz(Action printBuzz) {
        this.printBuzz = printBuzz;
        printBuzz();
    }

    // printFizzBuzz() outputs "fizzbuzz".
    public void Fizzbuzz(Action printFizzBuzz) {
        this.printFizzBuzz = printFizzBuzz;
        printFizzBuzz();
    }

    // printNumber(x) outputs "x", where x is an integer.
    public void Number(Action<int> printNumber) {
        for (int i = 15; i < this.max; i += 15) {
            this.addOutput(i, () => printNumber(i))
        }
    }
}

namespace Solution {
    public class MainClass {
        public static void Main() {
            FizzBuzz fb = new FizzBuzz(15);

            //List<Thread> threads = new();

            Console.WriteLine("Hello World!");

            Thread threadA = new(() => {
                try {
                    fb.Fizz(() => Console.WriteLine("fizz"));
                } catch (Exception e) {
                    Console.WriteLine(e);
                }
            });

            Thread threadB = new(() => {
                try {
                    fb.Buzz(() => Console.WriteLine("buzz"));
                } catch (Exception e) {
                    Console.WriteLine(e);
                }
            });

            Thread threadC = new(() => {
                try {
                    fb.Fizzbuzz(() => Console.WriteLine("fizzbuzz"));
                } catch (Exception e) {
                    Console.WriteLine(e);
                }
            });

            Thread threadD = new(() => {
                try {
                    fb.Number(i => Console.WriteLine(i));
                } catch (Exception e) {
                    Console.WriteLine(e);
                }
            });

            threadA.Start();
            threadB.Start();
            threadC.Start();
            threadD.Start();

            threadA.Join();
            threadB.Join();
            threadC.Join();
            threadD.Join();
        }
    }
}

