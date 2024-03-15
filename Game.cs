using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hangman
{
    internal class Game
    {
        static int lives = 10;
        static int livesKeeper = 10;
        private string _extracted;
        private string _extractedKeeper;
        string HiddenExtract;
        char[] ExtractedCharAr;
        char[] HiddenExtractedCharAr;
        char PlayerChoicee;
        public int showlives { get { return lives; } }
        public char PlayerChoice { get; set; }
        public string HiddenExtraction { get  { return HiddenExtract; } }
        public string Extracted { get { return _extracted; } }
        public string ExtractionKeeper { get { return _extractedKeeper; } }
        public Game(int txtcount) 
        {
            Random rnd = new Random();
            int Extract = rnd.Next(1, txtcount);
            using (StreamReader TxtRead = new StreamReader("parole.txt")) {
                for (int i = 0; i < Extract; i++) {
                    TxtRead.ReadLine();
                }
                _extracted = TxtRead.ReadLine();
                HiddenExtract = _extracted;
                _extractedKeeper = _extracted;
                HiddenExtractedCharAr = HiddenExtract.ToCharArray();

                for (int i = 0; i< HiddenExtract.Length; i++)
                {
                    if (HiddenExtractedCharAr[i] == 'a' || HiddenExtractedCharAr[i] == 'e' || HiddenExtractedCharAr[i] == 'i' || HiddenExtractedCharAr[i] == 'o' || HiddenExtractedCharAr[i] == 'u')
                    {
                        HiddenExtractedCharAr[i] = '+';
                    }
                    else
                    {
                        HiddenExtractedCharAr[i] = '-';
                    }
                    HiddenExtractedCharAr[0] = HiddenExtract[0];
                    HiddenExtractedCharAr[HiddenExtractedCharAr.Length - 1] = HiddenExtract[HiddenExtractedCharAr.Length-1];
                }
                HiddenExtract = new string(HiddenExtractedCharAr);
                ExtractedCharAr = _extracted.ToCharArray();
            }
        }
        public void ExtractionGuess()
        {
            if (Extracted.Contains(PlayerChoice))
            {
                for (int i = 0; i < ExtractedCharAr.Length; i++)
                {
                    if (ExtractedCharAr[i] == PlayerChoice)
                    {
                        HiddenExtractedCharAr[i] = PlayerChoice;
                        HiddenExtract = new string(HiddenExtractedCharAr);
                    }
                }
            }
            else
            {
                lives--;
            }
        }
        public void RestartGame()
        {
            lives = livesKeeper;
        }
    }
}

