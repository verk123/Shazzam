﻿namespace Shazzam
{
    using System.Windows;
    using Shazzam.Views;
    using FileLoaderPlugin = Shazzam.Plugins.FileLoaderPlugin;

    internal static class ShazzamSwitchboard
    {
        public static Window MainWindow { get; set; }

        public static CodeView CodeView { get; set; }

        public static FileLoaderPlugin FileLoaderPlugin { get; set; }
    }
}
