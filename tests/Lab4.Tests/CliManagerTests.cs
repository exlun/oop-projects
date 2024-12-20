using Itmo.ObjectOrientedProgramming.Lab4.Cli;
using Itmo.ObjectOrientedProgramming.Lab4.Filesystems;
using System.Text;
using Xunit;

namespace Lab4.Tests;

public static class CliManagerTests
{
    [Fact]
    public static void HandleConnectCommand_ValidLocalMode_ConnectsToFilesystem()
    {
        string[] parts = ["connect", "file:///path/to/filesystem", "-m", "local"];

        CliManager.HandleConnectCommand(parts);

        Assert.NotNull(CliManager.Filesystem);
        Assert.Equal(new Uri("file:///path/to/filesystem/"), CliManager.Cwd);
        Assert.True(CliManager.IsConnected());
    }

    [Fact]
    public static void HandleDisconnectCommand_DisconnectsFromFilesystem()
    {
        CliManager.Filesystem = new LocalFilesystem();
        CliManager.Cwd = new Uri("file:///path/to/filesystem/");

        CliManager.HandleDisconnectCommand();

        Assert.Null(CliManager.Filesystem);
        Assert.Null(CliManager.Cwd);
        Assert.False(CliManager.IsConnected());
    }

    [Fact]
    public static void HandleFileCommand_ShowSubCommand_DisplaysFileContent()
    {
        string tempFilePath = Path.Combine(Path.GetTempPath(), "testfile.txt");
        string expectedContent = "File content";
        File.WriteAllText(tempFilePath, expectedContent);

        var fileUri = new Uri($"file:///{tempFilePath.Replace(Path.DirectorySeparatorChar, '/')}");

        string[] parts = ["file", "show", fileUri.ToString(), "-m", "console"];
        CliManager.Filesystem = new LocalFilesystem();
        CliManager.Cwd = new Uri("file:///");

        TextWriter originalOut = Console.Out;

        try
        {
            var testOutStream = new MemoryStream();
            CliManager.OutStream = testOutStream;
            CliManager.HandleFileCommand(parts);
            string output = Encoding.UTF8.GetString(testOutStream.ToArray());

            Assert.Contains(expectedContent, output, StringComparison.InvariantCulture);
        }
        finally
        {
            Console.SetOut(originalOut);

            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
        }
    }

    [Theory]
    [InlineData("goto", "file:///path/to/directory")]
    [InlineData("unknown", "file:///path/to/unknown")]
    public static void HandleTreeCommand_ValidSubCommand_CallsCorrectMethod(string subCommand, string path)
    {
        string[] parts = { "tree", subCommand, path };
        TextWriter originalOut = Console.Out;

        try
        {
            if (subCommand == "goto")
            {
                CliManager.Filesystem = new LocalFilesystem();
                CliManager.Cwd = new Uri("file:///");
                CliManager.HandleTreeCommand(parts);
                Assert.Equal(new Uri(path), CliManager.Cwd);
            }
            else
            {
                using var stringWriter = new StringWriter();
                Console.SetOut(stringWriter);
                CliManager.HandleTreeCommand(parts);
                string output = stringWriter.ToString();

                Assert.Contains("Unknown tree command.", output, StringComparison.InvariantCultureIgnoreCase);
            }
        }
        finally
        {
            Console.SetOut(originalOut);
        }
    }

    [Fact]
    public static void HandleFileMoveCommand_MovesFileToNewLocation()
    {
        string sourcePath = Path.Combine(Path.GetTempPath(), "sourcefile.txt");
        string destinationPath = Path.Combine(Path.GetTempPath(), "destinationfile.txt");
        File.WriteAllText(sourcePath, "Test content");

        string[] parts = ["file", "move", sourcePath, destinationPath];
        CliManager.Filesystem = new LocalFilesystem();
        CliManager.Cwd = new Uri("file:///");

        CliManager.HandleFileCommand(parts);

        Assert.False(File.Exists(sourcePath));
        Assert.True(File.Exists(destinationPath));

        if (File.Exists(destinationPath))
        {
            File.Delete(destinationPath);
        }
    }

    [Fact]
    public static void HandleFileCopyCommand_CopiesFileToNewLocation()
    {
        string sourcePath = Path.Combine(Path.GetTempPath(), "sourcefile.txt");
        string destinationPath = Path.Combine(Path.GetTempPath(), "destinationfile.txt");
        File.WriteAllText(sourcePath, "Test content");

        string[] parts = ["file", "copy", sourcePath, destinationPath];
        CliManager.Filesystem = new LocalFilesystem();
        CliManager.Cwd = new Uri("file:///");

        CliManager.HandleFileCommand(parts);

        Assert.True(File.Exists(sourcePath));
        Assert.True(File.Exists(destinationPath));

        if (File.Exists(sourcePath))
        {
            File.Delete(sourcePath);
        }

        if (File.Exists(destinationPath))
        {
            File.Delete(destinationPath);
        }
    }

    [Fact]
    public static void HandleFileDeleteCommand_DeletesFile()
    {
        string filePath = Path.Combine(Path.GetTempPath(), "testfile.txt");
        File.WriteAllText(filePath, "Test content");

        string[] parts = ["file", "delete", filePath];
        CliManager.Filesystem = new LocalFilesystem();
        CliManager.Cwd = new Uri("file:///");

        CliManager.HandleFileCommand(parts);

        Assert.False(File.Exists(filePath));
    }

    [Fact]
    public static void HandleFileRenameCommand_RenamesFile()
    {
        string filePath = Path.Combine(Path.GetTempPath(), "testfile.txt");
        string newFilePath = Path.Combine(Path.GetTempPath(), "renamedfile.txt");
        File.WriteAllText(filePath, "Test content");

        string[] parts = ["file", "rename", filePath, "renamedfile.txt"];
        CliManager.Filesystem = new LocalFilesystem();
        CliManager.Cwd = new Uri("file:///");

        CliManager.HandleFileCommand(parts);

        Assert.False(File.Exists(filePath));
        Assert.True(File.Exists(newFilePath));

        if (File.Exists(newFilePath))
        {
            File.Delete(newFilePath);
        }
    }

    [Fact]
    public static void HandleTreeListCommand_ListsDirectoryContentsWithDepthLimit()
    {
        string tempDirPath = Path.Combine(Path.GetTempPath(), "testdir");
        string subDirPath = Path.Combine(tempDirPath, "subdir");
        string filePath = Path.Combine(tempDirPath, "testfile.txt");
        string subDirFilePath = Path.Combine(subDirPath, "subdirfile.txt");

        Directory.CreateDirectory(tempDirPath);
        Directory.CreateDirectory(subDirPath);
        File.WriteAllText(filePath, "Test content");
        File.WriteAllText(subDirFilePath, "Subdir test content");

        var tempDirUri = new Uri($"file:///{tempDirPath.Replace(Path.DirectorySeparatorChar, '/')}");
        var subDirUri = new Uri($"file:///{subDirPath.Replace(Path.DirectorySeparatorChar, '/')}");

        string[] parts = ["tree", "list", "-d", "0"];
        CliManager.Filesystem = new LocalFilesystem();
        CliManager.Cwd = tempDirUri;

        TextWriter originalOut = Console.Out;

        try
        {
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            CliManager.HandleTreeCommand(parts);
            string output = stringWriter.ToString();

            Assert.Contains("testfile.txt", output, StringComparison.InvariantCulture);
            Assert.Contains("subdir", output, StringComparison.InvariantCulture);
            Assert.DoesNotContain("subdirfile.txt", output, StringComparison.InvariantCulture);
        }
        finally
        {
            Console.SetOut(originalOut);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            if (File.Exists(subDirFilePath))
            {
                File.Delete(subDirFilePath);
            }

            if (Directory.Exists(subDirPath))
            {
                Directory.Delete(subDirPath);
            }

            if (Directory.Exists(tempDirPath))
            {
                Directory.Delete(tempDirPath);
            }
        }
    }
}