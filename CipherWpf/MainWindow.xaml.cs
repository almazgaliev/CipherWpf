using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using Cipher;
using Cipher.Caesar;
using Cipher.Vizhener;
using System.Linq;
using System.Text;

namespace CipherWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAlphabet[] alphabets;
        private ICipher cipher;
        private int selectedCipher = 0;
        private int selectedLanguage = 1;
        //private delegate string getKeyDel();
        private Func<string> getKey;


        public MainWindow()
        {
            InitializeComponent();
            alphabets = new IAlphabet[]
            {
                new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"),
                new Alphabet("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ0123456789"),
            };

            cipher = new Caesar(alphabets[0])
            {
                Key = 123
            };
            //cipher = new Vizhener(alphabets[0]) { Key = "QPRST" };

            keyTextbox.Text = getKey();
        }
        private void CreateCipherButton_Click(object sender, RoutedEventArgs e)
        {
            IAlphabet alphabet = alphabets[selectedLanguage];
            if (selectedCipher == 0)
            {
                var invalidCharacters = InvalidChars(keyTextbox.Text);
                int invalidAmount = invalidCharacters.Count();
                if (invalidAmount > 0)
                {
                    string errorFormat = "on position {0} '{1}';\n";
                    StringBuilder errorInfo = new StringBuilder(errorFormat.Length * invalidAmount + 1);
                    errorInfo.Append("Invalid key\n");
                    foreach (var invalid in invalidCharacters)
                        errorInfo.Append(string.Format(errorFormat, invalid.Item1, invalid.Item2));
                    MessageBox.Show(errorInfo.ToString());
                }
                else
                { 
                    cipher = new Vizhener(alphabet) { Key = keyTextbox.Text };
                    MessageBox.Show("Good");
                }
            }
            else //if (selected == 1)
            {
                if (BigInteger.TryParse(keyTextbox.Text, out BigInteger key))
                {
                    cipher = new Caesar(alphabet) { Key = key };
                    MessageBox.Show("Good");
                }
                else
                    MessageBox.Show("Key is not a number");
            }
        }

        private void CipherRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (int.TryParse((sender as RadioButton).Tag.ToString(), out selectedCipher))
            {
                if (selectedCipher == 0)
                    getKey = () => (cipher as Vizhener).Key;

                else
                    getKey = () => (cipher as Caesar).Key.ToString();
            }
        }

        private void СlearButton_Click(object sender, RoutedEventArgs e)
        {
            encryptedOutputTextbox.Clear();
            inputTextbox.Clear();
            decryptedOutputTextbox.Clear();
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            decryptedOutputTextbox.Text = cipher.Decrypt(encryptedOutputTextbox.Text);
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            var invalidCharacters = InvalidChars(inputTextbox.Text);
            int invalidAmount = invalidCharacters.Count();
            if (invalidAmount > 0)
            {
                string errorFormat = "on position {0} '{1}';\n";
                StringBuilder errorInfo = new StringBuilder(errorFormat.Length * invalidAmount + 1);
                errorInfo.Append("Invalid inputs\n");
                foreach (var invalid in invalidCharacters)
                    errorInfo.Append(string.Format(errorFormat, invalid.Item1, invalid.Item2));
                MessageBox.Show(errorInfo.ToString());
            }
            else
                encryptedOutputTextbox.Text = cipher.Encrypt(inputTextbox.Text);
        }

        private void LanguageRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            selectedLanguage = int.Parse((sender as RadioButton).Tag.ToString());
        }

        private IEnumerable<Tuple<int, char>> InvalidChars(string input)
        {
            for (int i = 0; i < input.Length; ++i)
                if (!(alphabets[selectedLanguage].Contains(input[i]) || !char.IsPunctuation(input[i])))
                    yield return Tuple.Create(i, input[i]);
        }
    }
}
