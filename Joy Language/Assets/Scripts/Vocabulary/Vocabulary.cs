using System.Collections.Generic;
using UnityEngine;
using System;

public class Vocabulary
{
    public int Id { get; }
    public string Spelling { get; }
    public string Pos { get; }
    public string Meaning { get; }
    public string Syllable { get; }
    public int Proficient;
    public int numberOfCorrect;
    public long lastTimeGetCorrect;

    public Vocabulary(int id, string spelling, string pos, string meaning, string syllable, int proficient, int correct)
    {
        Id = id;
        Spelling = spelling;
        Pos = pos;
        Meaning = meaning;
        Syllable = syllable;
        Proficient = proficient;
        numberOfCorrect = correct;
    }

    public void PrintV() {
        Debug.Log(Id + " " + Spelling + " " + Pos + " " + Meaning + " " + Syllable + " " + Proficient + " " + numberOfCorrect + " " + lastTimeGetCorrect);
    }

    // method to dcrease the proficient from 0 to 100
    public void upProf() {
        numberOfCorrect++;
        double v = 5 - (0.5 * numberOfCorrect);
        Proficient = (int)(100 / (1 + Math.Exp((float)v)));
        lastTimeGetCorrect = DateTime.Now.Ticks;
        Debug.Log(Id + " " + Spelling + " " + Pos + " " + Meaning + " " + Syllable + " " + Proficient + " " + numberOfCorrect + " " + lastTimeGetCorrect);
    }

    // method to dcrease the proficient from 100 to 0
    public void depreciateProf() {
        numberOfCorrect--;
        double v = -0.15 * (numberOfCorrect - 37);
        Proficient = (int)(100-Math.Exp((float)v));
        Debug.Log(Id + " " + Spelling + " " + Pos + " " + Meaning + " " + Syllable + " " + Proficient + " " + numberOfCorrect + " " + lastTimeGetCorrect);
    }
}
