using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace implementation
{
    class BranchAndBoundSolverOffline : IOfflineSolver
    {

        public Solution solve(OfflineProblem problem)
        {
            //tak is lege solution, hier lower bound pakken met LP en upper bound met bruteforce oplossing/ heuristic --> of numPatients, stuk sneller
            // bij lower bound kijken of het een integer oplossing is, alle decision variables moeten integer zijn. 
            //als niet integer, dan verder zoeken
            //dan branchen door random 1 persoon in te vullen, dit gaat DF. Hier opnieuw LP en bruteforce, alleen dan geef je ze partial oplossing mee. 
            return LinearProgramming.Solve(problem);
        }

        /*        static public int PigeonHole(Job[] jobs, int limit)
                {
                    jobs = jobs.OrderBy(x => x.t1).ToArray();

                    int highest = 0;

                    for (int i = 0; i < jobs.Count(); ++i)
                    {
                        var lookahead = new List<Job>();
                        var t1 = jobs[i].t1;
                        var t2 = jobs[i].t2;
                        double s = jobs[i].p;

                        highest = Math.Max(highest, 1);

                        for (int j = 1; j < limit && j < jobs.Count() - i; ++j)
                        {
                            t2 = Math.Max(t2, jobs[j].t2);
                            s += jobs[j].p;

                            int highest2 = (int)Math.Ceiling(s / (t2 - t1));
                            highest = Math.Max(highest, highest2);
                        }
                    }

                    return highest;
                }*/
    }
}