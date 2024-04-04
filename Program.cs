// See https://aka.ms/new-Console-template for more information
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class AnagramSorter
{
    static void Main(string[] args)
    {
        try
        {
            if (args.Length == 0) { throw new Exception("No input file path provided"); }

            string inputFilePath = args[0];
            Console.WriteLine("Specified Input File Path: " + inputFilePath);

            if (File.Exists(inputFilePath))
            {
                // store each line in array of strings 
                Console.WriteLine("Reading Input File...");
                string[] lines = File.ReadAllLines(inputFilePath);

                // ensure InputOutput directory exists
                System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\InputOutput");

                // delete old output file if it exists
                string outputFilePath = System.IO.Directory.GetCurrentDirectory() + "\\InputOutput\\output.txt";
                Console.WriteLine("Ensuring Output File is cleared: " + outputFilePath);
                if (File.Exists(outputFilePath))
                {
                    System.IO.File.Delete(outputFilePath);
                }

                // check for invalid input characters
                foreach (string line in lines)
                {
                    bool hasInvalidChars = Regex.IsMatch(line, @"[^a-z, \s]");
                    if (hasInvalidChars) { throw new Exception("Inavlid characters were found in the input file. " +
                        "Please ensure only a-z (lowercase), spaces, commas, and carriage returns are used"); }
                }

                // perform Anagram Analysis for each line in input file
                foreach (string line in lines)
                {
                    string newOutputLine = performAnagramAnalysis(line);
                    File.AppendAllText(outputFilePath, newOutputLine);
                }
            }
            else { throw new Exception("Invalid input file path"); }
        } catch (Exception ex) { Console.Error.WriteLine(ex.Message); }
    }

    private static string performAnagramAnalysis(string line)
    {        
        line = line.Trim();
        Console.WriteLine("Perform Anagram Analysis on: " + line);

        // if the line is empty, return accordingly
        if (line.Length == 0)
        {
            return "[]\n";
        }

        // split line into phrases by comma
        string[] phrases = line.Split(',');

        // the working product will be represented by the following:
        // [Anagram obj, Anagram obj, ...]
        List<Anagram> anagramsList = [];
        
        // ---- Perform actual Anagram analysis ----

        // for each phrase in the list of phrases (N)
        foreach (string originalPhrase in phrases)
        {
            // trim originalPhrase
            string phrase = originalPhrase.Trim();

            // sort the characters into sortedText
            string noSpacesPhrase = phrase.Replace(" ", "");
            char[] charArray = noSpacesPhrase.ToCharArray();
            Array.Sort(charArray);
            string sortedPhrase = new String(charArray);

            // if list of Anagram objs is empty
            if (anagramsList.Count == 0)
            {
                // create Anagram obj and insert into list & continue
                Anagram newAnagram = new Anagram(phrase, sortedPhrase);
                anagramsList.Add(newAnagram);
                continue;
            }

            // create flag for if phrase matched another phrase as anagram
            bool matchedAnagram = false;

            // for each Anagram obj in anagramList
            foreach (Anagram anagramObj in anagramsList)
            {
                // if sortedPhrase length != Anagram obj's sortedText length, continue 
                if (sortedPhrase.Length != anagramObj.sortedText.Length) { continue; }

                // if sortedPhrase matches Anagram obj's sortedText
                if (sortedPhrase == anagramObj.sortedText)
                {
                    // add phrase to the Anagram obj (and increase count) and set flag to true
                    anagramObj.AddPhrase(phrase);
                    matchedAnagram = true;

                    // break (terminates inner for-loop, moves on to next phrase in line)
                    break;
                }
            }

            // if there were no matches, create new Anagram obj and add to list of Anagram objs
            if (!matchedAnagram)
            {
                Anagram newAnagram = new Anagram(phrase, sortedPhrase);
                anagramsList.Add(newAnagram);
            }
        }

        // ---- Anagram Analysis complete ----

        // sort Anagram objs by count
        anagramsList = anagramsList.OrderBy(anagram => anagram.phrasesCount).ToList();

        // create  string
        string outputLine = "";

        // for each Anagram obj
        for (int i = 0; i < anagramsList.Count; i++)
        {
            // use Anagram.PrintPhrases to add phrases of current Anagram obj to outputLine string
            outputLine += anagramsList[i].PrintPhrases();

            // if there's another Anagram obj, add a comma to outputLine string
            if (i <  anagramsList.Count - 1)
            {
                outputLine += ",";
            }
        }

        // return outputLine with new line
        return outputLine + "\n";
    }
}

public class Anagram
{
    public int phrasesCount = 0;

    public string sortedText = "";

    public List<string> phrases = [];

    public Anagram(string phrase, string sortedTextOfPhrase)
    {
        this.AddPhrase(phrase);
        sortedText = sortedTextOfPhrase;
    }

    public string PrintPhrases()
    {
        string toPrint = "[";
        for (int i = 0; i < phrases.Count; i++)
        {
            toPrint += phrases[i];
            if (i < phrases.Count - 1)
            {
                toPrint += ",";
            }
        }
        toPrint += "]";
        return toPrint;
    }
    public void AddPhrase(string newPhrase)
    {
        phrases.Add(newPhrase);
        IncrementPhrasesCount();
    }

    private void IncrementPhrasesCount()
    {
        phrasesCount++;
    }
}


