namespace hm5_delegateevent
{
    public class Scanner
    {
        public event EventHandler<FileArgs> FileFound;

        private bool isCancel;
        public int Counter { get; set; }
        public void Scan(string path)
        {
            isCancel = false;

            Counter = 0;

            if (Directory.Exists(path))
            {
                ProcessDirectory(path);
            }           
            else
            {
                Console.WriteLine("Path " + path + " doesn't exist.");
            }
        }

        private void ProcessDirectory(string targetDirectory)
        {
            if (isCancel)
            {
                return;
            }


            var fileEntries = Directory.GetFiles(targetDirectory);

            foreach (string fileName in fileEntries)
            {
                if (isCancel)
                {
                    return;
                }

                ProcessFile(fileName);
            }

            var subdirectoryEntries = Directory.GetDirectories(targetDirectory);

            foreach (string subdirectory in subdirectoryEntries)
            {
                if (isCancel)
                {
                    return;
                }

                ProcessDirectory(subdirectory);
            }
        }

        private void ProcessFile(string path)
        {
            Counter++;

            // срабытывание события (обработка будет в FileFoundProc)
            FileFound?.Invoke(this, new FileArgs(path));
        }

        public void StopScanning()
        {
            Console.WriteLine("\nПрерывание обработки\n");

            isCancel = true;
        }
    }
}
