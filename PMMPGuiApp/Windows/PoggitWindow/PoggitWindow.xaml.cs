﻿using PMMPGuiApp.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMMPGuiApp.Windows.PoggitWindow {
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class PoggitWindow : Window {

        private PoggitData pd;

        private int page = 1;

        private bool download = false;

        private List<PoggitListData> source;

        private MainWindow window;

        public PoggitWindow(MainWindow window) {
            this.window = window;
            InitializeComponent();
        }

        private  void PluginList_Loaded(object sender, RoutedEventArgs e) {
            reload();
        }

        private void get_Click(object sender, RoutedEventArgs e) {
            if (download) {
                MessageBoxResult result = MessageBox.Show(Properties.Resources.AlreadyDownloadNow, "PMMPGUI", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            label.Text = Properties.Resources.GetPluginListNow;
            pageText.Text = "0/0";
            reload(true);
        }

        private void combo_DropDownClosed(object sender, EventArgs e) {
            if (download) {
                label.Text = Properties.Resources.DataDownloadNow;
                combo.SelectedIndex = 0;
                return;
            }
            if (combo.SelectedIndex == 0) {
                pd.sortByName();
                label.Text = Properties.Resources.SortName;
            } else {
                pd.sortByDownloadCount();
                label.Text = Properties.Resources.SortDownload;
            }
            
            source=pd.getPoggitDataInPage(0);
            
            PluginList.ItemsSource = source;
            source = null;
            changePageText();
            PluginList.SelectedIndex = 0;
            PluginList.ScrollIntoView(PluginList.SelectedItem);
            page = 1;
        }

        public void exit_click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void SearchTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            if (download) {
                label.Text = Properties.Resources.PluginListDownloadingNow;
                return;
            }
            search();
        }

        private void search() {
            combo.SelectedIndex = 0;
            pd.getSearchPoggitData(SearchTextBox.Text);
            pd.sortByName();
            source = pd.getPoggitDataInPage(0);
            PluginList.ItemsSource = source;
            page = 1;
            changePageText();
        }

        private async void reload(bool update=false) {
            progress.Visibility = Visibility.Visible;
            PluginList.ItemsSource = null;
            ObservableCollection<PoggitData> data = new ObservableCollection<PoggitData>();
            combo.SelectedIndex = 0;
            pd = new(window,this);
            download = true;
            if (update) {
                await Task.Run(() => { pd.DownloadString(); });
                label.Text = Properties.Resources.PluginListDownloadComplete;
            }
            await Task.Run(() => { pd.setList(); });
            pd.sortByName();
            progress.Visibility = Visibility.Hidden;
            download = false;
            changePageText();
            await Task.Run(() => {
                source = pd.getPoggitDataInPage(0);
            });
            PluginList.ItemsSource = source;
            page = 1;
            source = null;
            
        }


        private void PoggitWindow_Closing(object sender, CancelEventArgs e) {
            if (download) {
                MessageBoxResult result = MessageBox.Show(Properties.Resources.LoadingPluginList, "PMMPGUI", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Cancel = true;
                return;
            }
            pd.Disponse();
            pd = null;
        }

        public void AddPlugin(string plugin) {
            label.Text = Properties.Resources.Introduction+ " >> " + plugin;
        }

        private void changePageText() {
            pageText.Text = page + "/" + pd.getMax();
        }

        private async void PrevButton_Click(object sender, RoutedEventArgs e) {
            if (page == 1) return;
            page--;
            await Task.Run(() => {
                source = pd.getPoggitDataInPage(page - 1);
            });
            PluginList.ItemsSource = source;
            source = null;
            changePageText();
            PluginList.SelectedIndex = 0;
            PluginList.ScrollIntoView(PluginList.SelectedItem);
        }

        private async void NextButton_Click(object sender, RoutedEventArgs e) {
            if (page == pd.getMax()) return;
            page++;
            await Task.Run(() => {
                source = pd.getPoggitDataInPage(page - 1);
            });
            PluginList.ItemsSource = source;
            source = null;
            changePageText();
            PluginList.SelectedIndex = 0;
            PluginList.ScrollIntoView(PluginList.SelectedItem);
        }

        private async void FirstButton_Click(object sender, RoutedEventArgs e) {
            if (page == 1) return;
            page = 1;
            await Task.Run(() => {
                source = pd.getPoggitDataInPage(0);
            });
            PluginList.ItemsSource = source;
            source = null;
            changePageText();
            PluginList.SelectedIndex = 0;
            PluginList.ScrollIntoView(PluginList.SelectedItem);
        }

        private async void LastButton_Click(object sender, RoutedEventArgs e) {
            if (page == pd.getMax()) return;
            page = pd.getMax();
            await Task.Run(() => {
                source = pd.getPoggitDataInPage(pd.getMax() - 1);
            });
            PluginList.ItemsSource = source;
            source = null;
            changePageText();
            PluginList.SelectedIndex = 0;
            PluginList.ScrollIntoView(PluginList.SelectedItem);
        }
    }
}
