using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using SerializeAndDeserializeLogic;

namespace SerializeFolder
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

        private void SelectFolderSerialize(object sender, RoutedEventArgs e)
        {
            GetPath(FolderForSerialize);
        }
        private void SelectFolderForSaveSerialize(object sender, RoutedEventArgs e)
        {
            GetPath(FolderForSaveFile);
        }
        private void SelectFileDesialize(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FileDesirialize.Text=openFileDialog.FileName;
            }
        }
        private void SelectFolderForSaveDeserialize(object sender, RoutedEventArgs e)
        {
            GetPath(FolderForSaveDesirialize);
        }

        private void GetPath(TextBox name)
        {
            System.Windows.Forms.FolderBrowserDialog FBD = new System.Windows.Forms.FolderBrowserDialog();
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                name.Text = FBD.SelectedPath;
            }
        }

        private void Serialize(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FolderForSerialize.Text))
            { 
                Text_Serialize.Text = "Please select folder for serialise!\n";
                return;
            }

            if (string.IsNullOrEmpty(FolderForSaveFile.Text))
            {
                Text_Serialize.Text = "Please select the folder where the serializer file will be stored!\n";
                return;
            }
            FolderSerializer serializer = new FolderSerializer();
            try
            {
                string file_name = null;
                serializer.Serialize(FolderForSerialize.Text, FolderForSaveFile.Text, out file_name);
                FileDesirialize.Text = file_name;
                Text_Serialize.Text += "Serialize is finished.\n File serializate is in folder "+ FolderForSaveFile.Text+"\n";
                Text_Serialize.Text += "It is file with name - "+ System.IO.Path.GetFileName(file_name)+ ".\nThank you.";
            }
            catch
            {
                Text_Serialize.Text = "I'm so sorry. Serialize is not finish.\n";
            }
        }
        private void Deserialize(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FileDesirialize.Text))
            {
                Text_Deserialize.Text = "Please select file for deserialise!\n";
                return;
            }
            if (System.IO.Path.GetExtension(FileDesirialize.Text)!=".dat")
            {
                Text_Deserialize.Text = "Please select file with \".dat\" extension!\n";
                return;
            }

            if (string.IsNullOrEmpty(FolderForSaveDesirialize.Text))
            {
                Text_Deserialize.Text = "Please select the folder where the deserializer file will be stored!\n";
                return;
            }
            FolderSerializer serializer = new FolderSerializer();
            try
            {
                serializer.Deserialize(FileDesirialize.Text, FolderForSaveDesirialize.Text);
                Text_Deserialize.Text += "Deserialize is finished.\n Folder with subfolder and file is in folder " + FolderForSaveDesirialize.Text + "\n";
                Text_Deserialize.Text += "It is folder with name - " + GetFolderName() + "\nThank you.";
            }
            catch
            {
                Text_Serialize.Text = "I'm so sorry. Deserialize is not finish.\n";
            }
        }
        private string GetFolderName()
        {
            return System.IO.Path.GetFileName(FileDesirialize.Text).Replace("Serialize_", "").Replace(".dat", "");
        }
    }
}
