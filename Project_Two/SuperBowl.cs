using System;
namespace Project_Two
{
    public class SuperBowl
    {
        public string date { get; set; }
        public string sb { get; set; }
        public int attendance { get; set; }
        public string winnerQB { get; set; }
        public string winnerCoach { get; set; }
        public string winner { get; set; }
        public int winnerPts { get; set; }
        public string loserQB { get; set; }
        public string loserCoach { get; set; }
        public string loser { get; set; }
        public int loserPts { get; set; }
        public string MVP { get; set; }
        public string stadium { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public SuperBowl(string date, string sb, int attendance, string winnerQB, 
            string winnerCoach, string winner, int winnerPts, string loserQB, string loserCoach, 
            string loser, int loserPts, string MVP, string stadium, string city, string state)
        {
            this.date = date;
            this.sb = sb;
            this.attendance = attendance;
            this.winnerQB = winnerQB;
            this.winnerCoach = winnerCoach;
            this.winner = winner;
            this.winnerPts = winnerPts;
            this.loserQB = loserQB;
            this.loserCoach = loserCoach;
            this.loser = loser;
            this.loserPts = loserPts;
            this.MVP = MVP;
            this.stadium = stadium;
            this.city = city;
            this.state = state;
        }
    }
}
