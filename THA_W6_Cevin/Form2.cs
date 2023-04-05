using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THA_W6_Cevin
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        public Button button;
        
        public string[] alphabeticalqwerty = { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p" };
        public string[] alphabeticalasdfghjkl = { "a", "s", "d", "f", "g", "h", "j", "k", "l" };
        public string[] alphabeticalzxcvbnm = { "z", "x", "c", "v", "b", "n", "m" };
        public string[] wordlist;

        public char[] wordarray;

        public List<Button> alphabeticalqwertylist = new List<Button>();
        public List<Button> alphabeticalasdfghjkllist = new List<Button>();
        public List<Button> alphabeticalzxcvbnmlist = new List<Button>();
        public List<Button> guessbutton = new List<Button>();

        public List<string> guessedwordlist = new List<string>();

        public List<int> wordchecker = new List<int>();

        public int Guesses { get; set; }
        public int EnterCounter = 1;
        public int WordCounter = 1;

        public void randtxt()
        {
            string wordtxt = File.ReadAllText(@"C:\Users\Cevin-Predator\Documents\Coding stuff\WordleWordList.txt.txt", Encoding.UTF8);
            wordlist = wordtxt.Split(',');
            Random rnd = new Random();
            int wordrnd = rnd.Next(0, wordlist.Length);
            string word = wordlist[wordrnd];
            wordarray = word.ToCharArray();
        }

        public void keyboardgen()
        {
            for (int i = 1; i <= 10; i++)
            {
                button = new Button();
                button.Size = new Size(50, 50);
                button.Location = new Point(500 + 55 * i, 100);
                button.Name = "btn" + alphabeticalqwerty[i - 1];
                button.Text = alphabeticalqwerty[i - 1];
                button.Enabled = true;
                button.Visible = true;
                button.Click += Button_Click;
                this.Controls.Add(button);
                alphabeticalqwertylist.Add(button);
            }
            for (int i = 1; i <= 9; i++)
            {
                button = new Button();
                button.Size = new Size(50, 50);
                button.Location = new Point(525 + 55 * i, 160);
                button.Name = "btn" + alphabeticalasdfghjkl[i - 1];
                button.Text = alphabeticalasdfghjkl[i - 1];
                button.Enabled = true;
                button.Visible = true;
                button.Click += Button_Click;
                this.Controls.Add(button);
                alphabeticalasdfghjkllist.Add(button);
            }
            for (int i = 1; i <= 7; i++)
            {
                button = new Button();
                button.Size = new Size(50, 50);
                button.Location = new Point(580 + 55 * i, 220);
                button.Name = "btn" + alphabeticalzxcvbnm[i - 1];
                button.Text = alphabeticalzxcvbnm[i - 1];
                button.Enabled = true;
                button.Visible = true;
                button.Click += Button_Click;
                this.Controls.Add(button);
                alphabeticalzxcvbnmlist.Add(button);
            }
            {
                button = new Button();
                button.Size = new Size(90, 50);
                button.Location = new Point(540, 220);
                button.Name = "btnenter";
                button.Text = "Enter";
                button.Enabled = true;
                button.Visible = true;
                button.Click += Button_Click;
                this.Controls.Add(button);
            }
            {
                button = new Button();
                button.Size = new Size(90, 50);
                button.Location = new Point(1020, 220);
                button.Name = "btndelete";
                button.Text = "Delete";
                button.Enabled = true;
                button.Visible = true;
                button.Click += Button_Click;
                this.Controls.Add(button);
            }
        }

        public void boardgen()
        {
            for (int i = 1; i <= Guesses; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    button = new Button();
                    button = new Button();
                    button.Size = new Size(65, 65);
                    button.Location = new Point(70 * j, 70 * i);
                    button.Name = "btn" + i + j;
                    button.Text = "";
                    button.Enabled = true;
                    button.Visible = true;
                    this.Controls.Add(button);
                    guessbutton.Add(button);
                }
            }
        }

        public void checkfilledbtn(object sender)
        {
            button = (Button)sender;
            string place = "btn" + EnterCounter + WordCounter;
            foreach (Button btn in guessbutton)
            {
                if (btn.Name == place && btn.Text == "" && WordCounter <= 5)
                {
                    btn.Text = button.Text;
                    guessedwordlist.Add(btn.Text);
                    WordCounter++;
                    break;
                }
            }
        }

        public void checkenter(object sender)
        {
            button = (Button)sender;
            int same = 0;
            string guess = "";
            foreach (string c in guessedwordlist)
            {
                if (guess.Contains(c))
                {
                    same++;
                }
                guess += c;
            }
            if (same >= 2)
            {
                MessageBox.Show("Input must not be multiple same letter");
            }

            if (WordCounter >= 6 && wordlist.Contains(guess) && same < 2)
            {
                checkletter();
                guessedwordlist.Clear();
                EnterCounter++;
                WordCounter = 1;
            }
            else if (WordCounter < 6 && !wordlist.Contains(guess) && same < 2)
            {
                MessageBox.Show($"'{guess}' not in the word list");
            }
        }

        public void checkdelete(object sender)
        {
            button = (Button)sender;
            string order = "btn" + EnterCounter + (WordCounter - 1);

            foreach (Button btn in guessbutton)
            {
                if (btn.Name == order && WordCounter > 1)
                {
                    guessedwordlist.Remove(btn.Text);
                    btn.Text = "";
                    WordCounter--;
                    break;
                }
            }
        }

        public void checkletter()
        {
            string order = "btn" + EnterCounter;
            string guess = "";
            wordchecker.Clear();

            foreach (char c in wordarray)
            {
                guess += c;
            }

            for (int i = 0; i < wordarray.Length; i++)
            {
                if (guess.Contains(guessedwordlist[i]))
                {
                    if (wordarray[i].ToString() == guessedwordlist[i])
                    {
                        wordchecker.Add(2);
                    }
                    else
                    {
                        wordchecker.Add(1);
                    }
                }
                else
                {
                    wordchecker.Add(0);
                }
            }
            foreach (Button btn in guessbutton)
            {
                for (int i = 0; i < wordarray.Length; i++)
                {
                    if (btn.Name == order+i.ToString())
                    {
                        if (wordchecker[i] == 1)
                        {
                            btn.BackColor = Color.LightYellow;
                        }
                        else if (wordchecker[i] == 2)
                        {
                            btn.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            foreach (int c in wordchecker)
            {
                int count = 0;
                if (c == 2)
                {
                    count++;
                }
                if (count == 5)
                {
                    MessageBox.Show($"{guess} Is the word. You Win!!");
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            keyboardgen();
            boardgen();
            randtxt();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            button = (Button)sender;
            if (button.Text == "Enter")
            {
                checkenter(button);
            }
            else if (button.Text == "Delete")
            {
                checkdelete(button);
            }
            else if (WordCounter <= 5)
            {
                checkfilledbtn(button);
            }
        }
    }
}
