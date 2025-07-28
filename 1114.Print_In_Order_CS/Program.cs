namespace Solution {
    public class Foo {
        private Semaphore FirstSemaphore = new Semaphore(0, 1);
        private Semaphore SecondSemaphore = new Semaphore(0, 1);

        public Foo() {}

        public void First(Action printFirst) {

            // printFirst() outputs "first". Do not change or remove this line.
            printFirst();

            FirstSemaphore.Release();
        }

        public void Second(Action printSecond) {

            this.FirstSemaphore.WaitOne();
            // printSecond() outputs "second". Do not change or remove this line.
            printSecond();

            this.SecondSemaphore.Release();
        }

        public void Third(Action printThird) {
            this.SecondSemaphore.WaitOne();
            // printThird() outputs "third". Do not change or remove this line.
            printThird();
        }
    }

}
