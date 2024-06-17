﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Classes;

public class PlayerData
{
    public string Name { get; private set; }
    public int NGames { get; private set; }
    int totalGuess;


    public PlayerData(string name, int guesses)
    {
        Name = name;
        NGames = 1;
        totalGuess = guesses;
    }

    public void Update(int guesses)
    {
        totalGuess += guesses;
        NGames++;
    }

    public double Average()
    {
        return (double)totalGuess / NGames;
    }

    // Currently not in use. Uncomment if needed for hash-based collections or comparisons.
    //public override bool Equals(Object p)
    //{
    //    return Name.Equals(((PlayerData)p).Name);
    //}


    //public override int GetHashCode()
    //{
    //    return Name.GetHashCode();
    //}
}
