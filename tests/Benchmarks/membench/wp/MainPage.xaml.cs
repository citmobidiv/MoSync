﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.ComponentModel;

namespace MemBench
{
    public partial class MainPage : PhoneApplicationPage
    {

        private string benchRes;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            //run the benchmark
            Progress.IsIndeterminate = true;
            StartButton.Content = "Running...";

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);

            bw.RunWorkerAsync();

        }

        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MemBench mb = new MemBench();
            benchRes = mb.bench();
        }

        void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                // Close the Progress Dialog
                Progress.Opacity = 0;
                StartButton.Content = "Done!";
                MainText.Text = benchRes;
            }
            );
        }
    }
}