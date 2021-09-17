namespace implementation
{
    class Patient
    {
        int first_timeslot_first_dose;
        int last_timeslot_first_dose;
        int delay_between_doses;
        int second_dose_interval;
        public Patient(int first_timeslot_first_dose, int last_timeslot_first_dose, int delay_between_doses, int second_dose_interval)
        {
            this.first_timeslot_first_dose = first_timeslot_first_dose;
            this.last_timeslot_first_dose = last_timeslot_first_dose;
            this.delay_between_doses = delay_between_doses;
            this.second_dose_interval = second_dose_interval;
        }
        public override string ToString()
        {
            string part1 = "first_timeslot_first_dose: " + this.first_timeslot_first_dose + " ";
            string part2 = "last_timeslot_first_dose: " + this.last_timeslot_first_dose + " ";
            string part3 = "delay_between_doses: " + this.delay_between_doses + " ";
            string part4 = "second_dose_interval: " + this.second_dose_interval;
            return part1 + part2 + part3 + part4;
        }
    }
}
