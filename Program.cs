﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter Fighter 1: ");
            string fighterStr = Console.ReadLine();
            Fighter FighterOne = CreateFighterFromStr(fighterStr);
            while (FighterOne == null)
            {
                Console.WriteLine($"Could not create Fighter 1 from command {fighterStr}. Please enter Fighter 1: ");
                fighterStr = Console.ReadLine();
                FighterOne = CreateFighterFromStr(fighterStr);
            }

            Console.WriteLine("Please enter Fighter 2: ");
            string fighterStr2 = Console.ReadLine();
            Fighter FighterTwo = CreateFighterFromStr(fighterStr2);
            while (FighterTwo == null)
            {
                Console.WriteLine($"Could not create Fighter 2 from command {fighterStr2}. Please enter Fighter 2: ");
                fighterStr2 = Console.ReadLine();
                FighterTwo = CreateFighterFromStr(fighterStr2);
            }

            Dictionary<string, Fighter> FighterList = new Dictionary<string, Fighter>();
            FighterList.Add(FighterOne.getName(), FighterOne);
            FighterList.Add(FighterTwo.getName(), FighterTwo);

            Dictionary<string, Fighter> sleepingFighterList = new Dictionary<string, Fighter>();
            Dictionary<string, Fighter> paralysedFigthersList = new Dictionary<string, Fighter>();

            while (FighterList.Count > 1)
            {
                Console.WriteLine("Please enter another Fighter or let two Fighter battle: ");
                string command = Console.ReadLine();

                if (command.Contains(":")) // schützen vor Buchstaben
                {
                    Fighter newFighter = CreateFighterFromStr(command);

                    if (newFighter == null)
                    {
                        Console.WriteLine($"Could not create Fighter from Command {command}! ");
                    }
                    else if (FighterList.ContainsKey(newFighter.getName()))
                    {
                        Console.WriteLine("Figther already exsist! ");
                    }
                    else
                    {
                        FighterList.Add(newFighter.getName(), newFighter);
                    }
                }
                else if(command.Contains("+"))
                {
                    string[] FighterNames = command.Split('+');

                    if(!FighterList.ContainsKey(FighterNames[0]))
                    {
                        Console.WriteLine($"Fighter {FighterNames[0]} does not exsist! ");
                    }
                    else if(!FighterList.ContainsKey(FighterNames[1]))
                    {
                        Console.WriteLine($"Fighter {FighterNames[1]} does not exsist! ");
                    }
                    else if(FighterNames[0] == FighterNames[1])
                    {
                        Console.WriteLine("Fighter can't fight aganist themselves");
                    }
                    else
                    {
                        Fighter firstFighter = FighterList[FighterNames[0]];
                        Fighter secondFighter = FighterList[FighterNames[1]];
                        firstFighter.CountUpFight();
                        secondFighter.CountUpFight();

                        int orignalFirstFighterStrength = firstFighter.getStrength();
                        int orignalSecondFighterStrength = secondFighter.getStrength();
                        if (sleepingFighterList.ContainsKey(firstFighter.getName()))
                        {
                            firstFighter.setStrength(0);
                        }
                        if (sleepingFighterList.ContainsKey(secondFighter.getName()))
                        {
                            secondFighter.setStrength(0);
                        }

                        Fighter theWinner = null;
                        Fighter theLoser = null;

                        if(firstFighter.getStrength() > secondFighter.getStrength())
                        {
                            Console.WriteLine($"{firstFighter.getName()} wins, {secondFighter.getName()} loses! ");
                            theWinner = firstFighter;
                            theLoser = secondFighter;
                        }
                        else if(secondFighter.getStrength() > firstFighter.getStrength())
                        {
                            Console.WriteLine($"{secondFighter.getName()} wins, {firstFighter.getName()} loses! ");
                            theWinner = secondFighter;
                            theLoser = firstFighter;
                        }
                        else
                        {
                            Console.WriteLine("It's a draw!");
                            firstFighter.LevelUp(firstFighter.getStrength() / 2);
                            secondFighter.LevelUp(secondFighter.getStrength() / 2);
                        }

                        sleepingFighterList.Clear();

                        if (theWinner != null && theLoser != null)
                        {
                            FighterList.Remove(theLoser.getName());

                            State winnerstate = RandomState();
                            Console.WriteLine($"The Winner was given State: {winnerstate}!");

                            if (winnerstate == State.SLEEP)
                            {
                                sleepingFighterList.Add(theWinner.getName(), theWinner);
                            }
                            else if (winnerstate == State.PARALYSED)
                            {

                            }
                            else if (winnerstate == State.POISONED)
                            {

                            }
                            else
                            {
                                theWinner.LevelUp(theLoser.getStrength());
                            }
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Command unknown");
                }

                Console.WriteLine("Here are the available fighters:");

                foreach(KeyValuePair<string, Fighter> FighterInfo in FighterList)
                {
                    Console.WriteLine($"{FighterInfo.Key}: {FighterInfo.Value.getStrength()}");
                }

                Console.WriteLine();
            }


            Console.WriteLine($"{FighterList.First().Key} is the Champion with {FighterList.First().Value.getfightCount()} wins!");

            Console.ReadKey();
        }

        private static State RandomState()
        {
            return State.SLEEP;
        }

        private static Fighter CreateFighterFromStr(string FighterStr)
        {
            string[] fighterArr = FighterStr.Split(':');
            if (int.TryParse(fighterArr[1], out int strengthInt))
            {
                return new Fighter(fighterArr[0], strengthInt);
            }
            else
            {
                return null;
            }
        }
    }
}