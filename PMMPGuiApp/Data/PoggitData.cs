﻿using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json.Linq;
using PMMPGuiApp.Windows.PoggitWindow;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace PMMPGuiApp.Data {
    internal class PoggitData {

        private string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\yurisi\PMMPGuiApp";

        private List<PoggitListData> poggitList = new();

        private List<PoggitListData> viewPoggitList = new();

        private int maxValue;

        private MainWindow window;

        private PoggitWindow sub;

        public PoggitData(MainWindow window,PoggitWindow sub) {
            this.window = window;
            this.sub = sub;
        }

        public void setList() {
            JArray jsondata;
            if (!File.Exists(path + @"\PoggitData.data")) {
               DownloadString();
            }

            using (StreamReader reader = new(path + @"\PoggitData.data")) {
                jsondata = JArray.Parse(reader.ReadLine());
            }

            List<int> list = new();

            for (int i = 0; i < jsondata.Count; i++) {
                if (!list.Contains(int.Parse(jsondata[i]["repo_id"].ToString()))) {
                    PoggitListData pd = new PoggitListData(window,sub);
                    pd.Id = int.Parse(jsondata[i]["id"].ToString());
                    pd.Name = jsondata[i]["name"].ToString();
                    pd.DownloadUrl = jsondata[i]["artifact_url"].ToString() + "/" + pd.Name + ".phar";
                    pd.AboutUrl = jsondata[i]["html_url"].ToString();
                    pd.Image = @"../../Image/no_image_square.jpg";
                    if (jsondata[i]["icon_url"].ToString() != "") {
                        pd.Image = jsondata[i]["icon_url"].ToString();
                    }
                    pd.Tagline = jsondata[i]["tagline"].ToString();
                    pd.Download = jsondata[i]["downloads"].ToString();
                    pd.RepositoryId = int.Parse(jsondata[i]["repo_id"].ToString());
                    pd.Version = jsondata[i]["version"].ToString();
                    pd.FromApi = "-";
                    pd.ToApi = "-";
                    if (jsondata[i]["api"].ToString() != "[]") {
                        pd.FromApi = jsondata[i]["api"][0]["from"].ToString();
                        pd.ToApi = jsondata[i]["api"][0]["to"].ToString();
                    }
                    poggitList.Add(pd);
                    list.Add(pd.RepositoryId);
                }
            }
            maxValue = list.Count / 20;
            if (list.Count % 20 != 0) {
                maxValue++;
            }
            viewPoggitList = poggitList;
            jsondata = null;
        }

        public void DownloadString() {
            using (WebClient webClient = new WebClient()) {
                Encoding enc = new System.Text.UTF8Encoding(false);
                webClient.DownloadFile(@"https://poggit.pmmp.io/plugins.min.json", path + @"\PoggitData.data");
            }
        }

        public void sortByName() {
            poggitList.Sort((a, b) => a.Name.CompareTo(b.Name));
        }

        public void sortByDownloadCount() {
            poggitList.Sort((a, b) => int.Parse(a.Download).CompareTo(int.Parse(b.Download)));
            poggitList.Reverse();
        }

        public void getSearchPoggitData(string str) {
            List<PoggitListData> pd = new();
            foreach(PoggitListData poggitlist in poggitList) {
                if (poggitlist.Name.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0) {
                    pd.Add(poggitlist);
                    continue;
                }
                if (poggitlist.Tagline.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0) {
                    pd.Add(poggitlist);
                    continue;
                }
            }
            maxValue = pd.Count / 20;
            if (pd.Count % 20 != 0) {
                maxValue++;
            }
            viewPoggitList = pd;
        }

        public List<PoggitListData> getPoggitDataInPage(int page) {
            List<PoggitListData> pd = new();
            int max = page * 20 + 19;
            for (int i = page * 20; i < max; i++) {
                if (viewPoggitList.Count <= i) break;
                pd.Add(viewPoggitList[i]);
            }
            return pd;
        }

        public int getMax() {
            return maxValue;
        }

        public void Disponse() {
            poggitList = null;
            GC.Collect();
        }
    }

    internal class PoggitListData {

        private bool download;

        private MainWindow window;

        private PoggitWindow sub;

        public PoggitListData(MainWindow window,PoggitWindow sub) {
            this.window = window;
            this.sub = sub;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string AboutUrl { get; set; }

        public string DownloadUrl { get; set; }

        public string Tagline { get; set; }

        public string Download { get; set; }

        public string Image { get; set; }

        public int RepositoryId { get; set; }

        public string Version { get; set; }

        public string FromApi { get; set; }

        public string ToApi { get; set; }

        public RelayCommand ClickCommand {
            get {
                return new RelayCommand(() => {
                    if (!download) {
                        using (System.Net.WebClient wc = new()) {
                            Directory.CreateDirectory(window.getPath() + @"\plugins");
                            wc.DownloadFile(DownloadUrl, window.getPath() + @"\plugins\" + Name + ".phar");
                            download = true;
                            window.textboxApeendToAddTimestamp("[PluginManager] Install>>" + Name + "\n");
                            sub.AddPlugin(Name);
                        }
                    }
                });
            }
        }

        public RelayCommand AboutClickCommand {
            get { return new RelayCommand(() => { Process.Start(new ProcessStartInfo("cmd", $"/c start " + AboutUrl) { CreateNoWindow = true }); }); }
        }
    }
}
