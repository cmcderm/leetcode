namespace Solution {
    public class H2O {

        private int count = 0;
        private object hLock = new object();
        private SemaphoreSlim HydrogenSemaphore = new SemaphoreSlim(2);
        private SemaphoreSlim OxygenSemaphore = new SemaphoreSlim(1);

        public H2O() {}

        public void Hydrogen(Action releaseHydrogen) {
            HydrogenSemaphore.Wait();
            releaseHydrogen();

            // releaseHydrogen() outputs "H". Do not change or remove this line.
            lock(hLock) {
                count++;
                if (count == 2) {
                    count = 0;
                    OxygenSemaphore.Release();
                }
            }
        }

        public void Oxygen(Action releaseOxygen) {
            OxygenSemaphore.Wait();

            // releaseOxygen() outputs "O". Do not change or remove this line.
            releaseOxygen();
            HydrogenSemaphore.Release(2);
        }
    }
}
