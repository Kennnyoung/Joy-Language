using System.Collections.Generic;

public class Vocabulary
{
    public int Id { get; }
    public string Spelling { get; }
    public string Pos { get; }
    public string Meaning { get; }
    public string Syllable { get; }

    public Vocabulary(int id, string spelling, string pos, string meaning, string syllable)
    {
        Id = id;
        Spelling = spelling;
        Pos = pos;
        Meaning = meaning;
        Syllable = syllable;
    }
}
