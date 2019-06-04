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
        Debug.Log(Id + " " + Spelling + " " + Pos + " " + Meaning + " " + Syllable + " " + Proficient + " " + numberOfCorrect);
    }

    // method to dcrease the proficient from 0 to 100
    public void upProf() {

    }

    // method to dcrease the proficient from 100 to 0
    public void depreciateProf() {
        numberOfCorrect++;
        double v = 5 - (0.5 * numberOfCorrect);
        Proficient = 100 - (int)(100 / (1 + Math.Exp((float)v)));
        Debug.Log(Proficient);
    }
}
