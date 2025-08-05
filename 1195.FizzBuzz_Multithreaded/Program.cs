
public class FizzBuzz {
    private int n;
    private int max;
    Mutex actionMutex = new Mutex();

    // Represents all threads being complete
    private bool done = false;

    public FizzBuzz(int max) {
        this.n = 1;
        this.max = max;
    }

    public bool crit_compare_n_to_max() {
        return this.n <= this.max;
    }

    public void critical_action(Action action) {
        actionMutex.WaitOne();
        if (!done) {
            action();
            this.n++;

            if (this.n > this.max) {
                done = true;
            }
        }
        actionMutex.ReleaseMutex();
    }

    // printFizz() outputs "fizz".
    public void Fizz(Action printFizz) {
        // This is hoping that reading/writing to an int is atomic, for legibility
        while (!this.done && this.crit_compare_n_to_max()) {
            if (!this.done && this.n % 3 == 0 && this.n % 5 != 0) {
                critical_action(printFizz);
            }
        }
    }

    // printBuzzz() outputs "buzz".
    public void Buzz(Action printBuzz) {
        while (!this.done && this.crit_compare_n_to_max()) {
            if (!this.done && this.n % 3 != 0 && this.n % 5 == 0) {
                critical_action(printBuzz);
            }
        }
    }

    // printFizzBuzz() outputs "fizzbuzz".
    public void Fizzbuzz(Action printFizzBuzz) {
        while (!this.done && this.crit_compare_n_to_max()) {
            if (!this.done && this.n % 3 == 0 && this.n % 5 == 0) {
                critical_action(printFizzBuzz);
            }
        }
    }

    // printNumber(x) outputs "x", where x is an integer.
    public void Number(Action<int> printNumber) {
        while (!this.done && this.crit_compare_n_to_max()) {
            if (this.n % 3 != 0 && this.n % 5 != 0) {
                int num_cpy = this.n;
                critical_action(() => printNumber(num_cpy));
            }
        }
    }
}

namespace Solution {
    public class MainClass {
        public static void Main(string[] args) {
            int target = 15;

            if (args.Count() >= 1) {
                if (!Int32.TryParse(args[0], out target)) {
                    Console.WriteLine("Invalid parameter given for target!");
                    return;
                }
            }

            FizzBuzz fb = new FizzBuzz(target);

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
                    fb.Number(x => Console.WriteLine(x));
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

