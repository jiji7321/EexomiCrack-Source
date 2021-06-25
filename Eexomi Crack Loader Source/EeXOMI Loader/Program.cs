using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace EeXOMI_Loader
{
	internal class Program
	{
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		public static void CloseIt()
		{
			Thread.Sleep(3000);
			SendKeys.SendWait(" ");
		}

		private static void Main(string[] args)
		{
			IntPtr consoleWindow = Program.GetConsoleWindow();
			Console.WriteLine("==================================================================");
			Console.WriteLine("| __   ___                       _    _____                _     |");
			Console.WriteLine("| \\ \\ / (_)                     (_)  / ____|              | |    |");
			Console.WriteLine("|  \\ V / _  __ _  ___  _ __ ___  _  | |     _ __ __ _  ___| | __ |");
			Console.WriteLine("|   > < | |/ _` |/ _ \\| '_ ` _ \\| | | |    | '__/ _` |/ __| |/ / |");
			Console.WriteLine("|  / . \\| | (_| | (_) | | | | | | | | |____| | | (_| | (__|   <  |");
			Console.WriteLine("| /_/ \\_\\_|\\__,_|\\___/|_| |_| |_|_|  \\_____|_|  \\__,_|\\___|_|\\_\\ |");
			Console.WriteLine("|                                                                |");
			Console.WriteLine("==================================================================");
			Console.WriteLine("EeXOMI aka Xiaomi aka IkFuckOff h00k aka iqvw64e.sys aka simv0l anticrack by process name  CrACK LoADER");
			Thread.Sleep(1000);
			Console.WriteLine("Cracked: 18.06.2021");
			Console.WriteLine("Credits: GoGi, Zodiak, Infirms (B1g Thanks bro), Catahustle, Gamania");
			Thread.Sleep(5000);
			Directory.CreateDirectory(Program.eexomiFolder);
			Program.FilesXiaomi = Directory.GetFiles(Program.eexomiFolder, "*.*", SearchOption.AllDirectories);
			Console.WriteLine("Checking files...");
			Program.FileHasher();
			Program.HostsManipulation(true);
			Console.WriteLine("Opening loader...");
			new Thread(delegate()
			{
				Thread.CurrentThread.IsBackground = true;
				Program.ExecuteAsAdmin(Program.eexomiFolder + "loader.exe");
			}).Start();
			Console.WriteLine("Authenticate with ANY login and password");
			Console.WriteLine("Введите ЛЮБОЙ логин и пароль в лоадере EeXOMI и нажмите кнопку LOGIN");
			Thread.Sleep(5000);
			Program.ShowWindow(consoleWindow, 0);
			Console.WriteLine("Waiting csgo.exe...");
			Program.proc.WaitForExit();
			Console.WriteLine("Waiting 2 minutes for loading CSGO with injected cheat...");
			for (int i = 120; i >= 0; i--)
			{
				Console.Write("\r{0} Seconds   ", i.ToString());
				Thread.Sleep(TimeSpan.FromSeconds(1.0));
			}
			Console.WriteLine();
			Program.HostsManipulation(false);
			Console.WriteLine("Closing crackloader...");
			Thread.Sleep(3000);
			Environment.Exit(0);
		}
 
		public static void ExecuteAsAdmin(string fileName)
		{
			Program.proc.StartInfo.FileName = fileName;
			Program.proc.StartInfo.UseShellExecute = true;
			Program.proc.StartInfo.Verb = "runas";
			Program.proc.Start();
		}

		public static void HostsManipulation(bool add)
		{
			string path = Program.systemDrive + "Windows\\System32\\drivers\\etc\\hosts";
			string value = "panel.eexomi.host";
			string[] array = File.ReadAllLines(path);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Contains(value))
				{
					array[i] = null;
				}
			}
			if (add)
			{
				array = new List<string>(array)
				{
					"148.251.174.200 panel.eexomi.host"
				}.ToArray();
			}
			File.WriteAllLines(path, array);
		}

		public static string DeleteLines(string s, int linesToRemove)
		{
			return s.Split(Environment.NewLine.ToCharArray(), linesToRemove + 1).Skip(linesToRemove).FirstOrDefault<string>();
		}

		public static void Downloader(string file)
		{
			try
			{
				WebClient webClient = new WebClient();
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				string address = "https://github.com/GiaNTizmO/EeXOMICrack/raw/master/" + file;
				webClient.DownloadFile(address, Program.eexomiFolder + file);
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Concat(new string[]
				{
					"Unable download file: ",
					file,
					"\nException: ",
					ex.ToString(),
					"\nStack-Trace: ",
					ex.StackTrace
				}));
				new Thread(new ThreadStart(Program.CloseIt)).Start();
				MessageBox.Show(string.Concat(new string[]
				{
					"Unable download file: ",
					file,
					"\nException: ",
					ex.ToString(),
					"\nStack-Trace: ",
					ex.StackTrace
				}));
			}
		}

		public static string SHA256CheckSum(string filePath)
		{
			SHA256 sha = SHA256.Create();
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(filePath, FileMode.Open);
			}
			catch (IOException)
			{
				Process.Start("taskkill.exe", "/f /im loader.exe");
				Thread.Sleep(500);
				try
				{
					fileStream = new FileStream(filePath, FileMode.Open);
				}
				catch (IOException)
				{
					Console.BackgroundColor = ConsoleColor.Red;
					Console.WriteLine("Unable to openRead file: {0}. Please, close file and rerun loader!!!", filePath);
					new Thread(new ThreadStart(Program.CloseIt)).Start();
					MessageBox.Show("Unable to openRead file: {0}. Please, close file and rerun loader!!!", filePath);
					Thread.Sleep(5000);
					Environment.Exit(-1);
				}
			}
			fileStream.Position = 0L;
			byte[] value = sha.ComputeHash(fileStream);
			fileStream.Close();
			return BitConverter.ToString(value).Replace("-", string.Empty);
		}

		public static void FileHasher()
		{
			WebClient webClient = new WebClient();
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			string[] array = webClient.DownloadString("http://raw.githubusercontent.com/GiaNTizmO/EeXOMICrack/master/checksums").Split(new string[]
			{
				"\r\n",
				"\r",
				"\n"
			}, StringSplitOptions.None);
			string[] array2 = new string[0];
			string[] array3 = new string[0];
			for (int i = 0; i < array.Length - 1; i++)
			{
				string[] array4 = array[i].Split(new char[]
				{
					':'
				});
				array2 = new List<string>(array2)
				{
					array4[0]
				}.ToArray();
				array3 = new List<string>(array3)
				{
					array4[1]
				}.ToArray();
			}
			if (Program.FilesXiaomi.Length != 0)
			{
				int j = 0;
				while (j < Program.FilesXiaomi.Length)
				{
					string text = "";
					try
					{
						string fileName = Path.GetFileName(Program.FilesXiaomi[j]);
						text = array2[Array.IndexOf<string>(array2, fileName)];
					}
					catch (IndexOutOfRangeException)
					{
						goto IL_17D;
					}
					goto IL_DA;
					IL_17D:
					j++;
					continue;
					IL_DA:
					if (!(Path.GetFileName(Program.FilesXiaomi[j]) == text))
					{
						Program.filestoupdate = new List<string>(Program.filestoupdate)
						{
							Program.FilesXiaomi[j]
						}.ToArray();
						Program.Downloader(Path.GetFileName(Program.FilesXiaomi[j]));
						goto IL_17D;
					}
					if (!(Program.SHA256CheckSum(Program.FilesXiaomi[j]) == array3[Array.IndexOf<string>(array2, text)]))
					{
						Program.filestoupdate = new List<string>(Program.filestoupdate)
						{
							Path.GetFileName(Program.FilesXiaomi[j])
						}.ToArray();
						Program.Downloader(Path.GetFileName(Program.FilesXiaomi[j]));
						goto IL_17D;
					}
					goto IL_17D;
				}
				for (int k = 0; k < array2.Length; k++)
				{
					if (!File.Exists(array2[k]))
					{
						Program.Downloader(Path.GetFileName(array2[k]));
					}
				}
				return;
			}
			for (int l = 0; l < array2.Length; l++)
			{
				Program.Downloader(Path.GetFileName(array2[Array.IndexOf<string>(array2, array2[l])]));
			}
		}

		public static string[] FilesXiaomi;

		public static string[] checksums;

		public static string[] filestoupdate = new string[0];

		public static string systemDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));

		public static string eexomiFolder = Program.systemDrive + "EeXOMI_Loader/";

		public static Process proc = new Process();

		private const int SW_HIDE = 0;

		private const int SW_SHOW = 5;
	}
}
