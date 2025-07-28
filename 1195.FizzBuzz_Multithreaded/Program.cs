/*
 * It turns out my solution is not acceptable, you need to call each function
 * from the correct thread. However, in order to get things output in the correct order,
 * we're forced to run each thread locked in succession. This is multithreaded, but not parallel.
 * In my solution we fill the data in true multithreaded nature.
 *
 * Wait I could change the sendOutput to run from each thread.
 */

public class FizzBuzz {
    private const int NUM_THREADS = 4;

    private int n;
    private int max;
    bool isMutating = false;
    Mutex n_mutex = new Mutex();
    Mutex printMutex = new Mutex();

    // Represents all threads being complete
    private bool done = false;

    public FizzBuzz(int max) {
        this.n = 1;
        this.max = max;
    }

    public bool crit_compare_n_to_max() {
        while(isMutating) {}
        return this.n <= this.max;
    }

    public void critical_action(Action a) {
        printMutex.WaitOne();
        a();
        this.n++;

        if (this.n <= this.max) {

        }
        printMutex.ReleaseMutex();

    }

    // printFizz() outputs "fizz".
    public void Fizz(Action printFizz) {
        // This is hoping that reading/writing to an int is atomic, for legibility
        while (!this.done && this.crit_compare_n_to_max()) {
            if (this.n % 3 == 0 && this.n % 5 != 0) {
                critical_action(printFizz);
            }
        }
    }

    // printBuzzz() outputs "buzz".
    public void Buzz(Action printBuzz) {
        while (!this.done && this.crit_compare_n_to_max()) {
            if (this.n % 3 != 0 && this.n % 5 == 0) {
                printMutex.WaitOne();
                printBuzz();
                n++;
                printMutex.ReleaseMutex();
            }
        }
    }

    // printFizzBuzz() outputs "fizzbuzz".
    public void Fizzbuzz(Action printFizzBuzz) {
        while (!this.done && this.crit_compare_n_to_max()) {
            if (this.n % 3 == 0 && this.n % 5 == 0) {
                printMutex.WaitOne();
                printFizzBuzz();
                n++;
                printMutex.ReleaseMutex();
            }
        }
    }

    // printNumber(x) outputs "x", where x is an integer.
    public void Number(Action<int> printNumber) {
        while (!this.done && this.crit_compare_n_to_max()) {
            if (this.n % 3 != 0 && this.n % 5 != 0) {
                printMutex.WaitOne();
                printNumber(this.n);
                n++;
                printMutex.ReleaseMutex();
            }
        }
    }
}

namespace Solution {
    public class MainClass {
        public static void Main() {
            FizzBuzz fb = new FizzBuzz(15);

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

