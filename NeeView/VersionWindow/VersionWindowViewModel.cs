﻿using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using NeeLaboratory.ComponentModel;
using System.Globalization;
using NeeView.Properties;

namespace NeeView
{
    /// <summary>
    /// VersionWindow の ViewModel
    /// </summary>
    public class VersionWindowViewModel : BindableBase
    {
        public VersionWindowViewModel()
        {
            var readmeFile = (TextResources.Culture.Name == "ja") ? "README.ja-jp.html" : "README.html";
            LicenseUri = "file://" + Environment.AssemblyFolder.Replace('\\', '/').TrimEnd('/') + $"/{readmeFile}";

            this.Icon = ResourceBitmapUtility.GetIconBitmapFrame("/Resources/App.ico", 256);

            // チェック開始
            Checker.CheckStart();
        }


        public string ApplicationName => Environment.ApplicationName;
        public string DisplayVersion => Environment.DisplayVersion + $" ({(Environment.IsX64 ? "64bit" : "32bit")})";
        public string LicenseUri { get; private set; }
        public string ProjectUri => "https://neelabo.github.io/NeeView";
        public bool IsCheckerEnabled => Checker.IsEnabled;

        public BitmapFrame Icon { get; set; }

        // バージョンチェッカーは何度もチェックしないように static で確保する
        public static VersionChecker Checker { get; set; } = new VersionChecker();


        public void CopyVersionToClipboard()
        {
            var s = new StringBuilder();
            s.AppendLine(CultureInfo.InvariantCulture, $"Version: {ApplicationName} {DisplayVersion}");
            s.AppendLine(CultureInfo.InvariantCulture, $"Package: {Environment.PackageType} {Environment.DateVersion}");
            s.AppendLine(CultureInfo.InvariantCulture, $"OS: {System.Environment.OSVersion}");

            Debug.WriteLine(s);

            Clipboard.SetText(s.ToString());
        }

    }
}
