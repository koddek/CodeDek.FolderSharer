using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CodeDek.Lib.Mvvm;

namespace FolderSharer.ViewModels
{
    public sealed class AboutViewModel
    {
        //public byte[] AppIcon { get; set; } = File.ReadAllBytes(@"D:\.repo\_product\CodeDek.FolderSharer\art\ic_folder_mapper.ico");
        public string Home { get; set; } = "https://github.com/codedek/CodeDek.FolderSharer";
        public string Download { get; set; } = "https://github.com/codedek/CodeDek.FolderSharer/releases";
        public string Issues { get; set; } = "https://github.com/codedek/CodeDek.FolderSharer/issues";
        public string License { get; set; } = "https://github.com/codedek/CodeDek.FolderSharer/LICENSE";
        public string Changelog { get; set; } = "https://github.com/codedek/CodeDek.FolderSharer/CHANGELOG.md";
        public string UpdateUrl { get; set; }
        public string AppName => "Folder Sharer";
        public string AppVersion => $"v{FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion} development";
        public string Copyright => "© 2019 CodeDek. All Rights Reserved";
        public string Developer => "Written by CodeDek";

        public Cmd NavigateHomeUrlCmd => new Cmd(() => Process.Start(Home));
        public Cmd NavigateDownloadUrlCmd => new Cmd(() => Process.Start(Download));
        public Cmd NavigateIssuesUrlCmd => new Cmd(() => Process.Start(Issues));
        public Cmd NavigateLicenseUrlCmd => new Cmd(() => Process.Start(License));
        public Cmd NavigateChangelogUrlCmd => new Cmd(() => Process.Start(Changelog));
    }
}
