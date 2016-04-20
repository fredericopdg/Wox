using System;
using System.IO;

namespace Wox.Infrastructure.Storage
{
    [Serializable]
    public abstract class BaseStorage<T> : IStorage where T : new()
    {
        protected static T serializedObject;

        protected string DirectoryPath { get; } = Path.Combine(WoxDirectroy.Executable, "Config");

        protected string FilePath => Path.Combine(DirectoryPath, FileName + FileSuffix);

        protected abstract string FileSuffix { get; }

        protected abstract string FileName { get; }

        protected BaseStorage()
        {
            Load();
        }

        protected abstract void LoadInternal();
        protected abstract void SaveInternal();

        public void Load()
        {
            if (!File.Exists(FilePath))
            {
                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }
                File.Create(FilePath).Close();
            }
            LoadInternal();
        }

        public void Save()
        {
            SaveInternal();
        }
    }
}