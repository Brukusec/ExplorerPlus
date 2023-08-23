using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ExplorerPlus
{
    public partial class Form1 : Form
    {
        private string currentPath;

        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("This application only works with standalone executables.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LoadDirectory(Application.StartupPath);
            EnsureLogFileExists();
        }

        // Navigate to the parent directory
        private void OnBackButtonClicked(object sender, EventArgs e)
        {
            if (currentPath != null && Directory.GetParent(currentPath) != null)
            {
                string parentPath = Directory.GetParent(currentPath).FullName;
                LoadDirectory(parentPath);
                pathTextBox.Text = parentPath;
            }
        }

        // Handle file or directory double-click
        private void OnFileDoubleClicked(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                var selectedItem = listView.SelectedItems[0];
                var path = selectedItem.Tag.ToString();

                if (Directory.Exists(path))
                {
                    LoadDirectory(path);
                    pathTextBox.Text = path;
                }
                else
                {
                    ExecuteFile(path);
                }
            }
        }

        // Load the specified directory into the ListView
        private void LoadDirectory(string path)
        {
            listView.Items.Clear();
            currentPath = path;
            statusBar.Text = "Current Path: " + currentPath;
            pathTextBox.Text = currentPath;

            foreach (var dir in Directory.GetDirectories(path))
            {
                var item = new ListViewItem(new[] { Path.GetFileName(dir), "Folder" });
                item.Tag = dir;
                listView.Items.Add(item);
            }

            foreach (var file in Directory.GetFiles(path))
            {
                var item = new ListViewItem(new[] { Path.GetFileName(file), "File" });
                item.Tag = file;
                listView.Items.Add(item);
            }

            listView.Columns[0].Width = -2;
        }

        // Handle directory path input
        private void pathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newPath = pathTextBox.Text;
                if (Directory.Exists(newPath))
                {
                    LoadDirectory(newPath);
                }
                else
                {
                    MessageBox.Show("The specified path does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Ensure the log file exists
        private void EnsureLogFileExists()
        {
            string logFileName = "timestamp_log.log";
            string logFilePath = Path.Combine(Application.StartupPath, logFileName);

            if (!File.Exists(logFilePath))
            {
                using (StreamWriter sw = File.CreateText(logFilePath))
                {
                    sw.WriteLine("Data\tHora\tAction\tFilename");
                }
            }
        }

        // Log actions to the log file
        private void LogAction(string action, string filename)
        {
            string logFileName = "timestamp_log.log";
            string logFilePath = Path.Combine(Application.StartupPath, logFileName);

            using (StreamWriter sw = File.AppendText(logFilePath))
            {
                sw.WriteLine($"{DateTime.Now:yyyy-MM-dd}\t{DateTime.Now:HH:mm:ss}\t{action}\t{filename}");
            }
        }

        // Execute the specified file
        private void ExecuteFile(string path)
        {
            var tempFolder = Path.Combine("C:\\", Path.GetRandomFileName());
            Directory.CreateDirectory(tempFolder);

            var tempFilePath = Path.Combine(tempFolder, Path.GetFileName(path));
            File.Copy(path, tempFilePath);

            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo(tempFilePath),
                EnableRaisingEvents = true
            };

            process.Exited += (s, args) =>
            {
                LogAction("Closed", Path.GetFileName(tempFilePath));
                try
                {
                    File.Delete(tempFilePath);
                    Directory.Delete(tempFolder);
                }
                catch
                {
                    // Handle potential errors
                }
            };

            LogAction("Open", Path.GetFileName(path));
            process.Start();
        }

        // Handle the CMD context menu item click
        private void CmdMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                var selectedItem = listView.SelectedItems[0];
                var path = selectedItem.Tag.ToString();

                if (!Directory.Exists(path))
                {
                    var tempFolder = Path.Combine("C:\\", Path.GetRandomFileName());
                    Directory.CreateDirectory(tempFolder);

                    var tempFilePath = Path.Combine(tempFolder, Path.GetFileName(path));
                    File.Copy(path, tempFilePath);

                    var cmdProcess = new System.Diagnostics.Process
                    {
                        StartInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", $"/k cd {tempFolder} & {Path.GetFileName(tempFilePath)}"),
                        EnableRaisingEvents = true
                    };

                    cmdProcess.Exited += (s, args) =>
                    {
                        LogAction("Cmd Closed", Path.GetFileName(path));
                        try
                        {
                            File.Delete(tempFilePath);
                            Directory.Delete(tempFolder);
                        }
                        catch
                        {
                            // Handle potential errors
                        }
                    };

                    cmdProcess.Start();
                    LogAction("Cmd Opened", Path.GetFileName(path));
                }
            }
        }
         /// <summary>
         /// 
         /// </summary>
         /// <param </param>
         /// 
         /// <param name=></param>
        // Handle the CMD with arguments context menu item click
        private void RunWithArgsMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                var selectedItem = listView.SelectedItems[0];
                var path = selectedItem.Tag.ToString();

                if (!Directory.Exists(path))
                {
                    string inputArgs = Microsoft.VisualBasic.Interaction.InputBox("Enter arguments for the executable:", "Arguments", "");

                    var tempFolder = Path.Combine("C:\\", Path.GetRandomFileName());
                    Directory.CreateDirectory(tempFolder);

                    var tempFilePath = Path.Combine(tempFolder, Path.GetFileName(path));
                    File.Copy(path, tempFilePath);

                    var cmdProcess = new System.Diagnostics.Process
                    {
                        StartInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", $"/k cd {tempFolder} & {Path.GetFileName(tempFilePath)} {inputArgs}"),
                        EnableRaisingEvents = true
                    };

                    cmdProcess.Exited += (s, args) =>
                    {
                        // Log the action
                        LogAction("Cmd Closed with Args", Path.GetFileName(path));
                        try
                        {
                            File.Delete(tempFilePath);
                            Directory.Delete(tempFolder);
                        }
                        catch
                        {
                            // Handle errors if necessary
                        }
                    };

                    cmdProcess.Start();
                    // Log the action
                    LogAction($"Cmd Opened with Args: {inputArgs}", Path.GetFileName(path));
                }
            }
        }


    }
}
