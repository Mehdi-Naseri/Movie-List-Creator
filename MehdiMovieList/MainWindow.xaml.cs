using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MehdiMovieList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region UI
        private void buttonBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
                if (FolderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textboxAddress.Text = FolderBrowserDialog1.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                listboxResult.Items.Clear();
                System.IO.DirectoryInfo DirectoryInfoMain = new System.IO.DirectoryInfo(textboxAddress.Text);
                List<string> result = null;

                //Find List
                if (radioButton1.IsChecked == true)
                    result = OneLevel(DirectoryInfoMain, "");
                else if (radioButton2.IsChecked == true)
                    result = TwoLevel(DirectoryInfoMain, "");
                else if (radioButton3.IsChecked == true)
                    result = nLevel(DirectoryInfoMain, "");

                //Show Results in ListBox
                foreach(string string1 in result)
                {
                    listboxResult.Items.Add(string1);
                }

                if (listboxResult.Items.Count > 0)
                {
                    buttonSave.IsEnabled = true;
                }
                else
                {
                    buttonShow.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.SaveFileDialog SaveFileDialog1 = new Microsoft.Win32.SaveFileDialog();
                SaveFileDialog1.Filter = "Text Documents (*.txt)|*.txt";
                SaveFileDialog1.FileName = (new System.IO.DirectoryInfo(textboxAddress.Text)).Name;
                if ((bool)SaveFileDialog1.ShowDialog())
                {
                    StringBuilder StringBuilder1 = new StringBuilder();
                    foreach (string string1 in listboxResult.Items)
                    {
                        StringBuilder1.AppendLine(string1);
                    }
                    System.IO.File.WriteAllText(SaveFileDialog1.FileName, StringBuilder1.ToString(), Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void windowMain_Loaded(object sender, RoutedEventArgs e)
        {
            buttonSave.IsEnabled = false;
            buttonShow.IsEnabled = false;
            radioButton1.IsChecked = true;
        }

        private void textboxAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxAddress.Text.Length > 0)
            {
                buttonShow.IsEnabled = true;
            }
            else
            {
                buttonShow.IsEnabled = false;
            }
        }
        #endregion UI


        #region FindList
        private List<string> OneLevel(System.IO.DirectoryInfo DirectoryInfo1, string stringDirectoryPath)
        {
            List<string> result = new List<string>();
            if (stringDirectoryPath.Length > 0)
                stringDirectoryPath = stringDirectoryPath + " => " + DirectoryInfo1.Name;
            else
                stringDirectoryPath = DirectoryInfo1.Name;
            result.Add("-------------------------" + stringDirectoryPath + "-------------------------");

            foreach (System.IO.DirectoryInfo DirectoryInfoSub in DirectoryInfo1.GetDirectories())
            {
                result.Add(DirectoryInfoSub.Name);
            }
            foreach (System.IO.FileInfo FileInfo1 in DirectoryInfo1.GetFiles())
            {
                result.Add(FileInfo1.Name);
            }

            foreach (System.IO.DirectoryInfo DirectoryInfoSub in DirectoryInfo1.GetDirectories("_*"))
            {
                List<string> subDirectoryList = OneLevel(DirectoryInfoSub, stringDirectoryPath);
                foreach (string string1 in subDirectoryList)
                {
                    result.Add(string1);
                }
            }
            return result;
        }

        private List<string> TwoLevel(System.IO.DirectoryInfo DirectoryInfo1, string stringDirectoryPath)
        {
            bool boolMainDirectory;
            if (stringDirectoryPath == "")
                boolMainDirectory = true;
            else
                boolMainDirectory = false;
            List<string> result = new List<string>();
            if (stringDirectoryPath.Length > 0)
                stringDirectoryPath = stringDirectoryPath + " => " + DirectoryInfo1.Name;
            else
                stringDirectoryPath = DirectoryInfo1.Name;
            result.Add("-------------------------" + stringDirectoryPath + "-------------------------");

            foreach (System.IO.DirectoryInfo DirectoryInfoSub in DirectoryInfo1.GetDirectories())
            {
                result.Add(DirectoryInfoSub.Name);
            }
            foreach (System.IO.FileInfo FileInfo1 in DirectoryInfo1.GetFiles())
            {
                result.Add(FileInfo1.Name);
            }

            foreach (System.IO.DirectoryInfo DirectoryInfoSub in DirectoryInfo1.GetDirectories("_*"))
            {
                List<string> subDirectoryList = TwoLevel(DirectoryInfoSub, stringDirectoryPath);
                foreach (string string1 in subDirectoryList)
                {
                    result.Add(string1);
                }
            }
            //Read all subfolders of main folder
            if (boolMainDirectory)
            {
                foreach (System.IO.DirectoryInfo DirectoryInfoSub in DirectoryInfo1.GetDirectories())
                {
                    List<string> subDirectoryList = TwoLevel(DirectoryInfoSub, stringDirectoryPath);
                    foreach (string string1 in subDirectoryList)
                    {
                        result.Add(string1);
                    }
                }
            }
            return result;
        }

        private List<string> nLevel(System.IO.DirectoryInfo DirectoryInfo1, string stringDirectoryPath)
        {
            List<string> result = new List<string>();
            if (stringDirectoryPath.Length > 0)
                stringDirectoryPath = stringDirectoryPath + " => " + DirectoryInfo1.Name;
            else
                stringDirectoryPath = DirectoryInfo1.Name;
            result.Add("-------------------------" + stringDirectoryPath + "-------------------------");

            foreach (System.IO.DirectoryInfo DirectoryInfoSub in DirectoryInfo1.GetDirectories())
            {
                result.Add(DirectoryInfoSub.Name);
            }
            foreach (System.IO.FileInfo FileInfo1 in DirectoryInfo1.GetFiles())
            {
                result.Add(FileInfo1.Name);
            }

            foreach (System.IO.DirectoryInfo DirectoryInfoSub in DirectoryInfo1.GetDirectories())
            {
                List<string> subDirectoryList = nLevel(DirectoryInfoSub, stringDirectoryPath);
                foreach (string string1 in subDirectoryList)
                {
                    result.Add(string1);
                }
            }
            return result;
        }
        #endregion FindList
    }
}
