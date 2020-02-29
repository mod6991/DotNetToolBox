#region license

//DotNetToolbox .NET helper library 
//Copyright (C) 2012  Josué Clément
//mod6991@gmail.com

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion


using DotNetToolBox.Utils;
using System;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace DotNetToolBox.Icons.OpenIconLibrary
{
    public static class PngIcons
    {
        private static Assembly _assembly = Assembly.GetAssembly(typeof(PngIcons));
        private static string _pathFormat = "DotNetToolBox.Icons.OpenIconLibrary.{0}.{1}.png";

        /// <summary>
        /// Get an icon with name and size
        /// </summary>
        /// <param name="iconName">Icon name</param>
        /// <param name="iconSize">Icon size</param>
        public static BitmapImage GetIcon(IconName iconName, IconSize iconSize)
        {
            return GetIcon(GetIconSize(iconSize), GetIconName(iconName));
        }

        /// <summary>
        /// Get an icon with string name and size
        /// </summary>
        /// <param name="iconSize">Size</param>
        /// <param name="iconName">Name</param>
        private static BitmapImage GetIcon(string iconSize, string iconName)
        {
            return AssemblyHelper.GetEmbeddedImage(_assembly, String.Format(_pathFormat, iconSize, iconName));
        }

        /// <summary>
        /// Get icon size string
        /// </summary>
        /// <param name="icon">Size</param>
        private static string GetIconSize(IconSize icon)
        {
            switch (icon)
            {
                case IconSize.Size16:
                    return "_16";
                case IconSize.Size32:
                    return "_32";
                case IconSize.Size64:
                    return "_64";
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Get icon filename
        /// </summary>
        /// <param name="icon">Icon</param>
        private static string GetIconName(IconName icon)
        {
            switch (icon)
            {
                case IconName.Exit:
                    return "application-exit-2";
                case IconName.Internet:
                    return "applications-internet-2";
                case IconName.System:
                    return "applications-system-3";
                case IconName.AppointmentNew:
                    return "appointment-new";
                case IconName.AppointmentSoon:
                    return "appointment-soon";
                case IconName.ArchiveFiles:
                    return "archive-insert-2";
                case IconName.ArrowDown:
                    return "arrow-down-3";
                case IconName.ArrowLeft:
                    return "arrow-left-3";
                case IconName.ArrowRight:
                    return "arrow-right-3";
                case IconName.ArrowUp:
                    return "arrow-up-3";
                case IconName.Star:
                    return "bookmark-2";
                case IconName.Bookmark:
                    return "bookmark-5";
                case IconName.BookmarkNew:
                    return "bookmark-new-7";
                case IconName.Clock:
                    return "clock-2";
                case IconName.Colors:
                    return "colorize";
                case IconName.Configure:
                    return "configure-5";
                case IconName.Db:
                    return "db";
                case IconName.DbAdd:
                    return "db_add-2";
                case IconName.DbCommit:
                    return "db_comit-2";
                case IconName.DbRemove:
                    return "db_remove-2";
                case IconName.DbStatus:
                    return "db_status-2";
                case IconName.DbUpdate:
                    return "db_update-2";
                case IconName.Apply:
                    return "dialog-apply";
                case IconName.Close:
                    return "dialog-close";
                case IconName.Error:
                    return "dialog-error-4";
                case IconName.Key:
                    return "dialog-password-2";
                case IconName.Warning:
                    return "dialog-warning-2";
                case IconName.Decrypt:
                    return "document-decrypt";
                case IconName.Encrypt:
                    return "document-encrypt";
                case IconName.Document:
                    return "document-new";
                case IconName.DocumentNew:
                    return "document-new-8";
                case IconName.DocumentOpen:
                    return "document-open-8";
                case IconName.Print:
                    return "document-print";
                case IconName.DocumentProperties:
                    return "document-properties-2";
                case IconName.Save:
                    return "document-save-5";
                case IconName.SaveAll:
                    return "document-save-all";
                case IconName.SaveAs:
                    return "document-save-as-3";
                case IconName.Send:
                    return "document-send";
                case IconName.Download:
                    return "download";
                case IconName.DocumentEdit:
                    return "edit-6";
                case IconName.Clear:
                    return "edit-clear-2";
                case IconName.Copy:
                    return "edit-copy-3";
                case IconName.Cut:
                    return "edit-cut";
                case IconName.Delete:
                    return "edit-delete-3";
                case IconName.Find:
                    return "edit-find-5";
                case IconName.Paste:
                    return "edit-paste-3";
                case IconName.Rename:
                    return "edit-rename";
                case IconName.Package:
                    return "emblem-package-2";
                case IconName.Urgent:
                    return "emblem-urgent";
                case IconName.Home:
                    return "go-home-6";
                case IconName.Help:
                    return "help";
                case IconName.Info:
                    return "help-contents";
                case IconName.Hint:
                    return "help-hint";
                case IconName.Add:
                    return "list-add-4";
                case IconName.Remove:
                    return "list-remove-4";
                case IconName.Play:
                    return "media-playback-start";
                case IconName.Record:
                    return "media-record-5";
                case IconName.ChartLine:
                    return "office-chart-line";
                case IconName.ChartPie:
                    return "office-chart-pie";
                case IconName.Terminal:
                    return "openterm";
                case IconName.Plugin:
                    return "preferences-plugin";
                case IconName.Settings:
                    return "system-settings";
                case IconName.Trash:
                    return "trash-empty-3";
                case IconName.Display:
                    return "video-display";
                case IconName.Calendar:
                    return "view-calendar";
                case IconName.Filter:
                    return "view-filter";
                case IconName.Refresh:
                    return "view-refresh";
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
