using System;
using System.Collections.Generic;
using System.Threading;

namespace SynchronisingThreads
{
    class Program
    {
        public class Philosopher
        {
            private int timeToEat = 10000;
            private int timeToThink = 3000;
            private int _philsophersNumber;
            private Fork _leftFork;
            private Fork _rightFork;
            public Philosopher(int philsophersNumber, Fork leftFork, Fork rightFork)
            {
                _philsophersNumber = philsophersNumber;
                _leftFork = leftFork;
                _rightFork = rightFork;
            }

            public void Eat()
            {
                lock (_leftFork)
                {
                    Console.WriteLine("Philosopher {0} picked up Fork {1}.", _philsophersNumber, _leftFork._forkNumber);

                    lock (_rightFork)
                    {
                        // Eat here 
                        Console.WriteLine("Philosopher {0} picked up Fork {1}.", _philsophersNumber, _leftFork._forkNumber);
                        Console.WriteLine("Philosopher {0} is eating.", _philsophersNumber);

                        Thread.Sleep(timeToEat); 
                    }

                    Console.WriteLine("   Philosopher {0} released {1} fork.", _philsophersNumber, _rightFork._forkNumber);
                }
                
                Console.WriteLine("Philosopher {0} released {1} fork.", _philsophersNumber, _leftFork._forkNumber);
                this.Think();
            }

            public void Think()
            {
                Console.WriteLine("Philosopher {0} is thinking", _philsophersNumber);
                Thread.Sleep(timeToThink);
                Eat();
            }
        }

        public class Fork
        {
            public int _forkNumber;
            public Fork(int forkNumber)
            {
                _forkNumber = forkNumber;
            }
        }
        static void Main(string[] args)
        {
            var count = 5;
            var philosophers = new List<Philosopher>();
            var forks = new List<Fork>();

            //create forks
            for (int i = 0; i < 5; i++)
            {
                forks.Add(new Fork(i));
            }

            //create philosophers and assign forks
            int fi = 1;
            foreach (Fork f in forks)
            {
                philosophers.Add(new Philosopher(fi, forks[fi - 1 == 0 ? 4 : fi - 1 ], forks[fi == 5 ? 1 : fi]));
                fi++;
            }

            //create and run threads
            foreach (Philosopher p in philosophers)
            {
                new Thread(() =>
                {
                    p.Eat();
                }).Start();
            }

        }
     

    }
}
