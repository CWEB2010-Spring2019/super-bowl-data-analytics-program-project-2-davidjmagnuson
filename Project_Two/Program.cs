//David J Magnuson
//Project 2
//CWEB 2010
//Spring 2019

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Project_Two
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath;
            Console.WriteLine("Enter the file path for 'Super_Bowl_Project.csv': ");
            filePath = Console.ReadLine();

            if (File.Exists(filePath))
            {

                List<SuperBowl> superBowlList = new List<SuperBowl>();

                FileStream CSVFile = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(CSVFile);

                var parentDirectory = Directory.GetParent(filePath);

                Console.WriteLine("Enter desired name for new .txt file:");
                string fileName = Console.ReadLine();

                string newFilePath = parentDirectory + "/" + fileName + ".txt";
                

                FileStream txtFile = new FileStream(newFilePath, FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(txtFile);

                string row = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    row = reader.ReadLine();
                    string[] aSuperBowl = row.Split(',');

                    SuperBowl theBigGame = new SuperBowl(aSuperBowl[0], aSuperBowl[1], Convert.ToInt32(aSuperBowl[2]), aSuperBowl[3], aSuperBowl[4], aSuperBowl[5], Convert.ToInt32(aSuperBowl[6]),
                                                        aSuperBowl[7], aSuperBowl[8], aSuperBowl[9], Convert.ToInt32(aSuperBowl[10]), aSuperBowl[11], aSuperBowl[12], aSuperBowl[13], aSuperBowl[14]);
                    superBowlList.Add(theBigGame);
                }

                //SB WINNER OUTPUT

                writer.WriteLine("Super Bowl Winners:");
                string sbNumber = "SB";
                string sbWinner = "Winner";
                string sbLoser = "Loser";
                string sbYear = "Year";
                string sbQB = "QB";
                string sbCoach = "Coach";
                string sbMVP = "MVP";
                string sbPtDiff = "Pt Diff";
                string sbCity = "City";
                string sbState = "State";
                string sbStadium = "Stadium";

                writer.WriteLine("{0,-8}{1,-21}{2,-5}{3,-27}{4,-16}{5,-26}{6,-7}",sbNumber,sbWinner,sbYear,sbQB,sbCoach,sbMVP,sbPtDiff);
                writer.WriteLine(new string('-', 110));
                foreach (SuperBowl winner in superBowlList)
                {
                double ptDifference = winner.winnerPts - winner.loserPts;
                writer.WriteLine($"{winner.sb,-8}{winner.winner,-20} '{winner.date.Substring(winner.date.Length - 2),-3} {winner.winnerQB,-26} {winner.winnerCoach,-15} {winner.MVP,-25} {ptDifference,-7}");
                }


                //TOP 5 ATTENDED SBs


                var top5AttendedSBsQ = (from superBowl in superBowlList
                                        orderby superBowl.attendance descending
                                        select superBowl).Take(5);

                writer.WriteLine("\nTop 5 attended super bowls:");
                writer.WriteLine("{0,-5}{1,-20}{2,-20}{3,-10}{4,-11}{5,-16}", sbYear, sbWinner, sbLoser, sbCity, sbState, sbStadium);
                writer.WriteLine(new string('-', 81));
              
                    foreach (SuperBowl superBowl in top5AttendedSBsQ)
                    {
                        writer.WriteLine("'{0,-4}{1,-20}{2,-20}{3,-10}{4,-11}{5,-16}", superBowl.date.Substring(superBowl.date.Length - 2), superBowl.winner, superBowl.loser, superBowl.city, superBowl.state, superBowl.stadium);
                    }
                
                // STATE/CITY/STADIUM THAT HOSTED THE MOST SUPER BOWLS

                var mostHostState = superBowlList
                    .GroupBy(state => state.state)
                    .OrderByDescending(group => group.Count())
                    .First().Key;

                var mostHostCity = superBowlList
                    .GroupBy(city => city.city)
                    .OrderByDescending(group => group.Count())
                    .First().Key;

                var mostHostStadium = superBowlList
                    .GroupBy(stadium => stadium.stadium)
                    .OrderByDescending(group => group.Count())
                    .First().Key;

                writer.WriteLine("\nState that hosted the most super bowls:\n\t{0}\n" +
                	              "City that hosted the most super bowls:\n\t{1}\n" +
                	              "Stadium that hosted the most super bowls:\n\t{2}",
                                  mostHostState, mostHostCity,mostHostStadium);

                // PLAYERS WHO WON MVP MORE THAN ONCE

                writer.WriteLine("\nPlayers who've won MVP multiple times:");
                var mvpGroup = from superBowl in superBowlList
                               group superBowl by superBowl.MVP into mvps
                               where mvps.Count() > 1
                               orderby mvps.Key
                               select mvps;
                /*
                var multiMVP = from mvp in mvpGroup
                               where mvpGroup.Count() > 1
                               select mvp;
                               */                              
                foreach(var player in mvpGroup)
                {
                    writer.WriteLine(player.Key);
                    foreach(var mvp in player)
                    {
                        writer.WriteLine("\tWon with {0} and beat {1}", mvp.winner, mvp.loser);
                    }
                }

                //1    //WHICH COACH LOST THE MOST AMOUNT OF SUPER BOWLSSS?

                var mostLossesCoach = superBowlList
                    .GroupBy(coach => coach.loserCoach)
                    .OrderByDescending(coach => coach.Count())
                    .First().Key;

                writer.WriteLine("\nWhich coach lost the most super bowls?\n\t{0}", mostLossesCoach);

                //2    //WHICH COACH WON THE MOST AMOUNT OF SUPER BOWLSSS?

                var mostWinsCoach = superBowlList
                    .GroupBy(coach => coach.winnerCoach)
                    .OrderByDescending(coach => coach.Count())
                    .First().Key;

                writer.WriteLine("\nWhich coach won the most super bowls?\n\t{0}",mostWinsCoach);

                //3    //WHICH TEAMS WON THE MOST SUPERBOWLS

                var mostWinsTeam = superBowlList
                    .GroupBy(team => team.winner)
                    .OrderByDescending(team => team.Count())
                    .First().Key;

                writer.WriteLine("\nWhich team has won the most super bowls?\n\t{0}",mostWinsTeam);

                //4    //WHICH TEAM LOST THE MOST SUPER BOWLS

                var mostLossesTeam = superBowlList
                    .GroupBy(team => team.loser)
                    .OrderByDescending(team => team.Count())
                    .First().Key;

                writer.WriteLine("\nWhich team has lost the most super bowls?\n\t{0}",mostLossesTeam);
               
                //5    //WHICH SUPER BOWL HAD THE GREATEST POINT DIFFERENCE

                var ptDiffQ = from superBowl in superBowlList
                              orderby (superBowl.winnerPts - superBowl.loserPts) descending
                              select superBowl;

                writer.WriteLine("\nWhich super bowl had the greatest point difference?\n\tSuper bowl {0}",ptDiffQ.First().sb);

                //6    //WHAT IS THE AVE ATTENDANCE OF ALL THE SBs

                int totalAttendance = 0;
                foreach(SuperBowl superBowl in superBowlList)
                {
                    totalAttendance = totalAttendance + superBowl.attendance;
                }
                int avgAttendance = totalAttendance / superBowlList.Count();

                writer.WriteLine("\nWhat is the average attendance of all the super bowls?\n\t{0} people",avgAttendance);

                CSVFile.Close();
                reader.Close();
                txtFile.Close();
            }
            else
            {
                Console.WriteLine("I can't seem to find that file :(\nPlease locate the file and restart the program !");
            }
            Console.WriteLine("Check directory of 'Super_Bowl_Project.csv' for the new file !\n\nPress 'Enter' to quit");
            Console.ReadLine();
        }
    }
}