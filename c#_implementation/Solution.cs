using System;
using System.Collections.Generic;
using System.Linq;


namespace implementation
{
    class Solution
    {
        public int machines;
        public List<Doses> doses;

        public Solution(int machines, List<Doses> sol)
        {
            this.machines = machines;
            this.doses = sol;
        }

        public override string ToString()
        {
            string part1 = "machines: " + this.machines + "\n";
            string part2 = "";

            for (int i = 0; i < this.doses.Count; i++)
            {
                part2 += this.doses[i].ToString() + "\n";
            }

            return part1 + part2;
        }

        public Solution2D To2D(IProblem problem)
        {
            int[] hospitals = new int[this.machines];

            var regs2 = this.doses.Select<Doses, (Dose2D, Dose2D)>(Dose2D.Split);

            var appointments = regs2.SelectMany<(Dose2D, Dose2D), Dose2D>(x => new Dose2D[] { x.Item1, x.Item2 });
            appointments = appointments.OrderBy(x => x.t);

            foreach (var app in appointments)
            {
                for (int j = 0; j < hospitals.Count(); ++j) // TODO right now N * H, could probably be N * log(H) if we sort hospitals
                {
                    if (hospitals[j] < app.t)
                    {
                        app.h = j;
                        hospitals[j] = app.t + problem.p1;
                    }
                }
            }

            var regs3 = regs2.Select<(Dose2D, Dose2D), Doses2D>(Dose2D.To2D);

            return new Solution2D(this.machines, regs3.ToList());
        }
    }

    // wrapper class for tuple of two timeslots
    class Doses
    {
        public int t1;
        public int t2;
        public Doses(int t1, int t2)
        {
            this.t1 = t1;
            this.t2 = t2;
        }
        public override string ToString()
        {
            string part1 = "timeslot_first_dose: " + this.t1 + " ";
            string part2 = "timeslot_second_dose: " + this.t2;
            return part1 + part2;
        }
    }

    // wrapper class for tuple of timeslot and hospital id
    class Dose2D
    {
        public int t;
        public int h;

        public Dose2D(int t, int h)
        {
            this.t = t;
            this.h = h;
        }

        public static (Dose2D, Dose2D) Split(Doses r)
        {
            return (new Dose2D(r.t1, 0), new Dose2D(r.t2, 0));
        }

        public static Doses2D To2D((Dose2D, Dose2D) doses)
        {
            var a = doses.Item1;
            var b = doses.Item2;

            return new Doses2D(a.t, a.h, b.t, b.h);
        }
    }

    // wrapper class for tuple of timeslot1, hospital1, timeslot2, hospital2
    class Doses2D
    {
        public int t1;
        public int h1;
        public int t2;
        public int h2;
        public Doses2D(int t1, int h1, int t2, int h2)
        {
            this.t1 = t1;
            this.h1 = h1;
            this.t2 = t2;
            this.h2 = h2;
        }

        override public string ToString()
        {
            var tuple = new int[] { this.t1, this.h1, this.t2, this.h2 };

            return String.Join(", ", tuple.Select<int, string>(x => x.ToString()));
        }

        // forget the hospitals
        public Doses Forget()
        {
            return new Doses(this.t1, this.t2);
        }
    }

    class Solution2D : Solution
    {
        public List<Doses2D> hospitals;

        public Solution2D(int machines, List<Doses2D> hospitals) : base(machines, hospitals.Select(x => x.Forget()).ToList())
        {
            this.hospitals = hospitals;
        }

        public override string ToString()
        {
            var ret = String.Join('\n', this.hospitals.Select<Doses2D, string>(x => x.ToString()));
            ret += "\n" + this.machines.ToString();
            return ret;
        }
    }
}
