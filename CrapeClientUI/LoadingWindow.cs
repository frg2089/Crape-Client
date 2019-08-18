﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
//using System.Drawing;
using Crape_Client.Initialization;

namespace Crape_Client.CrapeClientUI
{
    class LoadingWindow
    {
        public Window Window = new Window();
        readonly Canvas Canvas = new Canvas();

        static readonly string LogoStr = "Crape Client";
        static readonly double Width = 600;
        static readonly double Height = 400;
        static readonly double Left = 200;
        static readonly double FontSize = 28;
        [STAThread]
        public void LoadWindowInit()
        {
            // 主窗口创建
            Window.Title = "Crape Client 初始化";
            Window.Width = Width;
            Window.Height = Height;
            Window.Foreground = Tools.String2Brush("#FFF");
            Window.ResizeMode = ResizeMode.NoResize;
            Window.Style = (Style)Window.FindResource("WindowStyle");
            Window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Window.Name = "LoadWindow";
            // 窗口组件创建
            // 创建Path
            Path path = new Path();
            #region path
            LinearGradientBrush pathFillBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0)
            };
            pathFillBrush.GradientStops.Add(new GradientStop(
                Color.FromArgb(0xCC, 0x15, 0x07, 0x38), 0));
            pathFillBrush.GradientStops.Add(new GradientStop(
                Color.FromArgb(0xCC, 0x15, 0x07, 0x38), 0.3));
            pathFillBrush.GradientStops.Add(new GradientStop(
                Color.FromArgb(0x99, 0x29, 0x22, 0x3A), 0.5));
            pathFillBrush.GradientStops.Add(new GradientStop(
                Color.FromArgb(0x44, 0x2B, 0x28, 0x33), 1));
            path.Fill = pathFillBrush;
            path.Data = new RectangleGeometry(new Rect(0,0, Width, Height));
            #endregion
            // Image
            Image image = new Image();
            #region image
            image.Width = Width - Left;
            image.Height = Height;
            Canvas.SetLeft(image, Left);
            image.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri(Global.ImagesDir + "Welcome.png",
                UriKind.RelativeOrAbsolute));
            #endregion
            Grid Logo = new Grid();
            Canvas.SetTop(Logo, 0);
            Logo.Width = Left;
            Logo.Height = 60;
            TextBlock text2 = new TextBlock
            {
                Text = LogoStr,
                FontWeight = FontWeights.Bold,
                FontSize = FontSize,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(17, 14, 10, 10),
                Foreground = Tools.String2Brush("#50EAEAEA")
            };
            TextBlock text1 = new TextBlock
            {
                Text = LogoStr,
                FontWeight = FontWeights.Bold,
                FontSize = FontSize,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Tools.String2Brush("#FFEAEAEA")
            };
            Logo.Children.Add(text2);
            Logo.Children.Add(text1);

            TextBlock Copyright = new TextBlock();
            Canvas.SetLeft(Copyright, 20);
            Canvas.SetBottom(Copyright, 10);
            Copyright.Width = Width - 40;
            Copyright.Inlines.Add(new System.Windows.Documents.Run
            {
                Text = "Crape Client\n",
                FontSize = 12
            });
            Copyright.Inlines.Add(new System.Windows.Documents.Run
            {
                Text = "Created By 清廉正洁的射命丸文\n",
                FontSize = 10
            });
            Copyright.Inlines.Add(new System.Windows.Documents.Run
            {
                Text = "Copyright © 2019 Crape STAFF,\n",
                FontSize = 9
            });
            Copyright.Inlines.Add(new System.Windows.Documents.Run
            {
                Text = "\t\tAll Rights Reserved.\n",
                FontSize = 9
            });
            // 状态信息文字块
            TextBlock Status = new TextBlock
            {
                Name = "tbStatus"
            };
            Canvas.SetLeft(Status, 30);
            Canvas.SetTop(Status, 70);
            Status.Width = Width - 60;
            Status.Inlines.Add(new System.Windows.Documents.Run
            {
                Text = "正在初始化...\nIs Initializing",
            });
            // ----
            Canvas.Children.Add(path);
            Canvas.Children.Add(image);
            Canvas.Children.Add(Logo);
            Canvas.Children.Add(Copyright);
            Canvas.Children.Add(Status);
            Window.Content = Canvas;// 用户区设置
            Window.Show();// 显示窗口
            // --------
            Status.Text = "正在准备初始化";
            Status.Text = "初始化日志系统";
            Nlog.NLogInit();
            Status.Text = "初始化首选项";
            Global.MissionConfig.LoadFromFile(Global.LocalPath + Global.ConfigsDir + "Missions.ini");
            Global.MainConfig.LoadFromFile(Global.LocalPath + Global.ConfigsDir + "Config.conf");
            Global.Ra2mdConf.LoadFromFile(Global.LocalPath + "ra2md.ini");
            Status.Text = "初始化存档列表";
            SavedList.SavesListInit();
            Status.Text = "初始化任务列表";
            Initialization.Mission.MissionConfigAnalyze();// 初始化列表
            Status.Text = "初始化GUI";
            Initialization.MainWindow.MainWindowInit();
            Status.Text = "初始化完成";
            System.Threading.Thread.Sleep(1000);

            //MainWindow MainWindow = new MainWindow();
            //MainWindow.Show();
            Window.Close();
        }

    }

}