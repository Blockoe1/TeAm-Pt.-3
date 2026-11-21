using System;
using System.IO;
using System.Linq.Expressions;
using UnityEngine;

public static class DeerNames 
{
    private const int PROBABILITY_OF_SPECIAL = 2;

    private static NameDatabase[] _namesDatabase;

    private static NameDatabase[] NamesDatabase
    {
        get
        {
            if (_namesDatabase == null)
            {
                _namesDatabase = new NameDatabase[] { new NameDatabase("FirstNames"), new NameDatabase("LastNames"), new NameDatabase("SpecialNames") };
            }
            return _namesDatabase;
        }
    }

    private class NameDatabase
    {
        private static string NamesPath => Path.Combine(Application.streamingAssetsPath, "Names");
        private readonly string[] names;
        internal int Length => names.Length;

        public NameDatabase(string category)
        {
            string filePath = Path.Combine(NamesPath, category + ".txt");
            using (StreamReader textReader = new StreamReader(filePath))
            {
                string allLines = textReader.ReadToEnd();
                names = allLines.Split('\n');
                // Filter out special characters.
                for(int i = 0; i < names.Length; i++)
                {
                    names[i] = names[i].Replace("\r", "");
                    names[i] = names[i].Replace("\n", "");
                    names[i] = names[i].Replace("\t", "");
                    names[i] = names[i].Replace(" ", "");
                }
            }
        }

        public string GetName(int index)
        {
            return names[index];
        }
    }


    public static string GetRandomName()
    {
        if(UnityEngine.Random.Range(0, 100) < PROBABILITY_OF_SPECIAL)
        {
            return NamesDatabase[2].GetName(UnityEngine.Random.Range(0, NamesDatabase[2].Length));
        }
        else
        {
            return NamesDatabase[0].GetName(UnityEngine.Random.Range(0, NamesDatabase[0].Length)) + " " 
                + NamesDatabase[1].GetName(UnityEngine.Random.Range(0, NamesDatabase[1].Length));

        }
    }
}
