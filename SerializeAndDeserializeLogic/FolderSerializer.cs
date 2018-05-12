using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SerializeAndDeserializeLogic
{
    public class FolderSerializer
    {
        private IEnumerable<Folder> GetFolders(string root)
        {
            foreach (var folder in Directory.GetDirectories(root))
            {
                var folder_info = new DirectoryInfo(folder);
                var directory = new Folder
                {
                    Name = folder_info.Name,
                    files = GetFiles(root).ToArray(),
                    SubFolders = GetFolders(folder).ToArray()
                };
                yield return directory;
            }
        }
        private IEnumerable<Folder> GetRootFolder(string root)
        {           
            var folder_info = new DirectoryInfo(root);
            var directory = new Folder
            {
                 Name = folder_info.Name,
                 files = GetFiles(root).ToArray(),
                 SubFolders = GetFolders(root).ToArray()
            };
            yield return directory;           
        }
        private IEnumerable<File> GetFiles(string directory)
        {
            foreach(var file in Directory.GetFiles(directory))
            {
                var file_info = new FileInfo(file);
                yield return new File
                {
                    Name = file_info.Name,
                    Data = System.IO.File.ReadAllBytes(file)
                };
            }
        }
        public  void Serialize(string folderSerialize, string FolderforSaveSerialize, out string path)
        {
            path = GetFullPathFile(folderSerialize, FolderforSaveSerialize);
            FileStream fs = new FileStream(path, FileMode.Create);

            var information = GetRootFolder(folderSerialize).ToArray();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, information);
            fs.Close();
        }

        private string GetFullPathFile(string folderSerialize, string FolderforSaveSerialize)
        {
            return Path.Combine(Path.GetFullPath(FolderforSaveSerialize), "Serialize" + "_" + Path.GetFileName(folderSerialize) + ".dat");
        }

        public void Deserialize(string file, string directory)
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            Folder[] folder = (Folder[])formatter.Deserialize(fs);
            fs.Close();
            CreateFolder(folder, directory);
            
        }
        private void CreateFolder(Folder[] folders, string path)
        {
            foreach(var directory in folders)
            {
                string new_path =Path.Combine(path,directory.Name);
                Directory.CreateDirectory(new_path);
                CreateFiles(directory,new_path);
                CreateFolder(directory.SubFolders, new_path);
            }
        }
        private void CreateFiles(Folder folder,string path)
        {
            foreach(var file in folder.files)
            {
                string file_name = Path.Combine(path, file.Name);
                FileStream fs = new FileStream(file_name, FileMode.Create);
                fs.Close();
                System.IO.File.WriteAllBytes(file_name, file.Data);
                
            }
        }
    }
}
