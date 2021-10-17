﻿using System;
using System.IO;
using static implementation.Parser;
using System.Collections.Generic;


namespace implementation
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestOffline();
            //Benchmark();
            //MiniTestCase();
            TestOnline();
        }

        static void MiniTestCase()
        {
            ForwardMinimizeOnline f = new ForwardMinimizeOnline();

            int p1 = 2, p2 = 3, g = 0;
            List<Patient> p = new List<Patient>();
            p.Add(new Patient(0, 2, 0, 3, p1, p2, g));
            p.Add(new Patient(5, 10, 0, 3, p1, p2, g));
            OnlineProblem o = new OnlineProblem(p1, p2, g, p);

            Solution2D sol = f.solve(o);

            Console.WriteLine("NO ERRORS!");
            foreach(Doses d in sol.doses)
                Console.Write(d.t1 + ", " + d.t2 + "; ");
        }

        static void TestOnline()
        {
            ForwardMinimizeOnline f = new ForwardMinimizeOnline();


            var solvers = new List<IOnlineSolver>(new IOnlineSolver[] {
                new ForwardMinimizeOnline()
            });

            Benchmarker.RandomTestOnline(solvers.ToArray(), 10, 10, true);
        }

        static void Benchmark()
        {
            var solvers = new List<IOfflineSolver>(new IOfflineSolver[] {
                //new RecursiveBruteforce(),
                new IntSatSolver(),
            });

            /*String prologPath = "C:\\Program Files\\swipl\\bin\\swipl.exe";
            if (File.Exists(prologPath))
            {
                Console.WriteLine(Directory.GetCurrentDirectory());
                var clpfd = new CallableSolver(prologPath, new String[] { "..\\..\\..\\Callables\\constraint_programming.pl" });
                solvers.Add(clpfd);
            }*/


            var res = new Benchmarker(false, false).BenchmarkAll(solvers.ToArray(), 10.0);
            Console.WriteLine(res.ToString());
        }

        static void TestOffline()
        {
            var solvers = new List<IOfflineSolver>(new IOfflineSolver[] {
                new RecursiveBruteforce(), // TODO this one fails
                new IntSatSolver(),
            });

            Benchmarker.RandomTest(solvers.ToArray(), 10, 10);

            // validator test
            if (File.Exists("../data/offline/"))
            {
                List<string> problems = new List<string>
                {
                    "../data/offline/backtracker.txt",
                    "../data/offline/big_numbers.txt",
                    "../data/offline/three_quarters.txt",
                    "../data/online/Problem1.txt",
                    "../data/online/Problem2.txt"
                };

                List<string> solutions = new List<string>
                {
                    "../data/solutions/offline/backtracker.txt",
                    "../data/solutions/offline/big_numbers.txt",
                    "../data/solutions/offline/three_quarters.txt",
                    "../data/solutions/online/Solution1.txt",
                    "../data/solutions/online/Solution2.txt"
                };

                ValidateTestcases(problems, solutions);
            }
            else
            {
                Console.WriteLine("warning: can't find unit test path");
            }
        }

        static private void ValidateTestcases(List<string> problem_filenames, List<string> solution_filenames) {
            for (int i = 0; i < problem_filenames.Count; i++){
                OfflineProblem prob = ParseOfflineProblem(problem_filenames[i]);
                Solution2D sol = ParseSolution2D(solution_filenames[i]);
                OfflineValidator val = new OfflineValidator(prob);
                Console.WriteLine($"Validating {problem_filenames[i]} and {solution_filenames[i]}...");
                val.validate(sol);
            }
        }
    }
}
