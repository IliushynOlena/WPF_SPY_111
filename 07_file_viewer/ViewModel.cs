﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace _07_file_viewer
{
    public class ViewModel :INotifyPropertyChanged
    {
        private string directoryPath;
        public string DirectoryPath
        {
            get => directoryPath; 
            set { directoryPath = value;
                 OnPropertyChanged();
                 OnPropertyChanged(nameof(DirectoryName));
            }
        }
        public string DirectoryName => Path.GetFileName(directoryPath)!;
        private FileInfo fileInfo;

        public FileInfo SelectedFile
        {
            get { return fileInfo; }
            set { fileInfo = value; OnPropertyChanged(); }
        }      

        private ObservableCollection<FileInfo> files = new ObservableCollection<FileInfo>();

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerable<FileInfo> Files => files;
        public ViewModel()
        {
            LoadFiles(@"C:\Users\helen\Desktop\Заняття");
        }
        public void LoadFiles(string dirPath)
        {
            this.DirectoryPath = dirPath;
            DirectoryInfo directory = new DirectoryInfo(dirPath);
            var data =  directory.GetFiles();

            files.Clear();
            foreach (var item in data)
            {
                files.Add(item);

            }         
          
        }
    }
}
