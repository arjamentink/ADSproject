using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


namespace implementation
{
    public class TroubleMaker // more commonly known as Arbitrary
    {
        public static OfflineProblem generate(int n, (int, int) p1B, (int, int) p2B, (int, int) gB, (int, int) rShiftB, (int, int) dd1B, (int, int) dd2B, (int, int) xB) {
            var rand = new Random();
            
            var p1 = rand.Next(p1B.Item1, p1B.Item2);
            var p2 = rand.Next(p2B.Item1, p2B.Item2);
            var g = rand.Next(gB.Item1, gB.Item2);
            
            var r = 0;

            var ps = new List<Patient>();

            for (int i = 0; i < n; ++i) {
                var r1i = r;
                // TODO remove + p1 here when all solvers are fixed to interpret [r1, d1] as the startable interval (not total interval)
                var d1i = r1i + p1 + rand.Next(dd1B.Item1, dd1B.Item2);
                
                var xi = rand.Next(xB.Item1, xB.Item2);

                var inter2 = p2 + rand.Next(dd2B.Item1, dd2B.Item2);

                var patient = new Patient(r1i, d1i, xi, inter2, p1, p2, g);

                ps.Add(patient);
            }

            OfflineProblem p = new OfflineProblem(p1, p2, g, n, ps);

            return p;
        }

        public static OfflineProblem generateSimple(int n) {
            var p1B = (1, 6);
            var p2B = (1, 6);
            var gB = (1, 6);
            var rShiftB = (0, 3);
            var dd1B = (1, 10);
            var dd2B = (1, 10);
            var xB = (0, 10);

            return generate(n, p1B, p2B, gB, rShiftB, dd1B, dd2B, xB);
        }
    }

    class Benchmarker
    {
        public static double dryRun(ISolverOffline solver, int n) {
            var timer = new Stopwatch();
            var p = TroubleMaker.generateSimple(1);

            timer.Start();
            for (int i = 0; i < n; ++i)
            {
                solver.solve(p);
            }
            timer.Stop();

            return timer.Elapsed.TotalSeconds / n;
        }

        public static void test(ISolverOffline[] solvers, int n, int m) {

            for (int i = 0; i < n; ++i)
            {
                var p = TroubleMaker.generateSimple(m);
                foreach (var solver in solvers) {
                    var sol = solver.solve(p);
                    new OfflineValidator(p, sol).validate();
                }
            }
        }

        public static Benchmark benchmark(ISolverOffline[] solvers, double tMin)
        {   // benchmark, until any solver uses at least tMin seconds
            double[] ts = new double[solvers.Count()];
            double[] dry = new double[solvers.Count()];

            List<double>[] result = new List<double>[solvers.Count()];
            List<OfflineProblem> cases = new List<OfflineProblem>();

            int n = 2;
            var timer = new Stopwatch();

            for (int i = 0; i < solvers.Count(); ++i)
            {
                result[i] = new List<double>();
                dry[i] = Benchmarker.dryRun(solvers[i], 10);
            }

            while (ts.All<double>(t => t < tMin))
            {
                var p = TroubleMaker.generateSimple(n);
                cases.Add(p);

                for (int i = 0; i < ts.Count(); ++i)
                {
                    timer.Start();
                    var sol = solvers[i].solve(p);
                    timer.Stop();

                    var validator = new OfflineValidator(p, sol);

                    try {
                        validator.validate();
                    }
                    catch (Exception e) {
                        Console.WriteLine("exception in " + solvers[i].GetType().ToString());
                        throw e;
                    }

                    double dt = timer.Elapsed.TotalSeconds;
                    dt -= dry[i];

                    timer.Reset();
                    ts[i] += dt;
                    result[i].Add(dt);
                }

                n += 1; // or *= 2 if you're daring
            }

            return new Benchmark(solvers, result, cases);
        }

        public class Benchmark
        {
            public ISolverOffline[] solvers;
            public List<double>[] result;
            public List<OfflineProblem> cases;


            public Benchmark(ISolverOffline[] solvers, List<double>[] result, List<OfflineProblem> cases)
            {
                this.solvers = solvers;
                this.result = result;
                this.cases = cases;
            }

            public override string ToString()
            {
                var str = $"number: " + String.Join(" ", cases.Select(c => c.number_of_patients)) + "\n";

                for (int i = 0; i < this.result.Count(); ++i)
                {
                    var r = this.result[i];
                    str += $"solver {i + 1}: " + String.Join(" ", r.Select<double, String>(v => v.ToString("F2"))) + "\n";
                }

                str += "\n\n";

                for (int i = 0; i < this.solvers.Count(); ++i)
                {
                    str += $"solver {i + 1} = " + this.solvers[i].GetType().ToString() + "\n";
                }

                return str;
            }
        }
    }
}
