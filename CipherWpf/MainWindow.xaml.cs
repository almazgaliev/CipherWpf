using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using Cipher;

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
        private delegate string getKeyDel();
        getKeyDel getKey;


        public MainWindow()
        {
            InitializeComponent();
            alphabets = new IAlphabet[]
            {
                new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"),
                new Alphabet("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ0123456789"),
            };

            cipher = new Vizhener(alphabets[0]) { Key = "QPRST" };


            keyTextbox.Text = getKey();
        }
        private void CreateCipherButton_Click(object sender, RoutedEventArgs e)
        {
            IAlphabet alphabet = alphabets[selectedLanguage];
            if (selectedCipher == 0)
            {
                if (IsCorrectInput(keyTextbox.Text))
                {
                    cipher = new Vizhener(alphabet) { Key = keyTextbox.Text };
                    MessageBox.Show("Good");
                }
                else
                    MessageBox.Show("Ключ введен на неправильном языке");
            }
            else //if (selected == 1)
            {
                if (BigInteger.TryParse(keyTextbox.Text, out BigInteger key))
                {
                    cipher = new Caesar(alphabet) { Key = key };
                    MessageBox.Show("Good");
                }
                else
                    MessageBox.Show("Key aint a number");
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

        //private void InputTextbox_TextChanged(object sender, TextChangedEventArgs e) не успевает обрабатывать изменения
        //{
        //    foreach(TextChange x in e.Changes)
        //    {
        //        string s = (sender as TextBox).Text.Substring(x.Offset, x.AddedLength);
        //        s = cipher.Encrypt(s);
        //        encryptedOutputTextbox.Text = encryptedOutputTextbox.Text.Insert(x.Offset, s);
        //    }
        //}

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
            if (IsCorrectInput(inputTextbox.Text))
                encryptedOutputTextbox.Text = cipher.Encrypt(inputTextbox.Text);
            else
                MessageBox.Show("Неправильный ввод, проверьте язык");
        }

        private void LanguageRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            selectedLanguage = int.Parse((sender as RadioButton).Tag.ToString());
        }

        private bool IsCorrectInput(string input)
        {
            for (int i = 0; i < input.Length; i++)
                if (!alphabets[selectedLanguage].Contains(input[i]) && !char.IsWhiteSpace(input[i]) && !char.IsPunctuation(input[i])) 
                        return false;
            return true;
        }

        //private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e){}
    }
}
