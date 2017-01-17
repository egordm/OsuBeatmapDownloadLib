﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OsuMapDownload;
using OsuMapDownload.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private const string SONGS_PATH = @"G:\Games\OsuTest\Songs";
        private const string TEMP_PATH = @"G:\Games\OsuTest\TempDL";

        [TestMethod]
        public void TestAsyncDownload()
        {
            var download = new MapSetDownload("http://bloodcat.com/osu/s/138554", TEMP_PATH);
            var task = download.CreateTask(SONGS_PATH);
            task.Start();
            while (!task.IsCompleted)
            {
                Debug.WriteLine(download.Progress + " with speed " + download.Speed);
                Thread.Sleep(100);
            }
            Debug.WriteLine(download.Completed);
            Debug.WriteLine(download.Failed);
        }

        [TestMethod]
        public void TestAsyncDownloadMultiple()
        {
            var download = new MapSetDownload("http://bloodcat.com/osu/s/138554", TEMP_PATH);
            var task = download.CreateTask(SONGS_PATH);
            var download2 = new MapSetDownload("http://bloodcat.com/osu/s/553711", TEMP_PATH);
            var task2 = download2.CreateTask(SONGS_PATH);
            task.Start();
            task2.Start();
            while (!task.IsCompleted || !task.IsCompleted)
            {
                Debug.WriteLine($"Download 1 - Progress: {download.Progress} Speed: {download.Speed} kb/s");
                Debug.WriteLine($"Download 2 - Progress: {download2.Progress} Speed: {download2.Speed} kb/s");
                Thread.Sleep(100);
            }
            Debug.WriteLine($"Download 1 Completed: {download.Completed} or Failed: {download.Failed}");
            Debug.WriteLine($"Download 2 Completed: {download2.Completed} or Failed: {download2.Failed}");
        }

    }
}