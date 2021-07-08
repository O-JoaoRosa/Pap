﻿using CRUD.ClassesEntidades.SQL;
using System.Collections.Generic;
using System.Windows;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserRaceList.xaml
    /// </summary>
    public partial class WindowUserRaceList : Window
    {

        #region load
        public WindowUserRaceList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //cria uma lista temporaria e uma lista que irá ser assiciada a listView
            List<UserRace> lista = SqlUserRace.GetAll();
            List<UserRace> listatemp = new List<UserRace>();
            UserRace userRace;

            //vai a cada elemneto da lista GetAll e controi as fks de maneira completa
            foreach (UserRace p in lista)
            {
                userRace = p;
                userRace.User = SqlUser.Get(p.User.Id);
                userRace.RaceTrack = SqlRaceTrack.Get(p.RaceTrack.Id);
                listatemp.Add(userRace);
            }
            ListView.ItemsSource = listatemp;
        }

        #endregion

        #region Buttons

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metodo que irá ser executado quando o botão edit for carregado
        /// server para editar o profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            //verifica se algum item foi selecionado 
            if (ListView.SelectedItems.Count > 0)
            {
                //vai buscar o item selecionado
                UserRace profileClickado = (UserRace)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (profileClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowUserRaceInsert(SQL_Connection.SQL_UPDATE, profileClickado);
                    w.ShowDialog();
                }
            }
            else
            {
                //avisa que nenhum item foi selecionado
                MessageBox.Show("Erro : nenhum item selecionado", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            RefreshListView();
        }

        /// <summary>
        /// Metodo que irá ser executado quando o botão de eliminar for selecionado
        /// server para eliminar um profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            //verifica se algum item foi selecionado 
            if (ListView.SelectedItems.Count > 0)
            {
                //vai buscar o item selecionado
                UserRace profileClickado = (UserRace)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (profileClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowUserRaceInsert(SQL_Connection.SQL_DELETE, profileClickado);
                    w.ShowDialog();
                }
            }
            else
            {
                //avisa que nenhum item foi selecionado
                MessageBox.Show("Erro : nenhum item selecionado", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            RefreshListView();
        }

        /// <summary>
        /// faz refresh a list View
        /// </summary>
        private void RefreshListView()
        {
            ListView.ItemsSource = null;                  // Elimina a associação da List à listView
            ListView.Items.Clear();                       // Limpa a ListView

            //cria uma lista temporaria e uma lista que irá ser assiciada a listView
            List<UserRace> lista = SqlUserRace.GetAll();
            List<UserRace> listatemp = new List<UserRace>();
            UserRace userRace;

            //vai a cada elemneto da lista GetAll e controi as fks de maneira completa
            foreach (UserRace p in lista)
            {
                userRace = p;
                userRace.User = SqlUser.Get(p.User.Id);
                userRace.RaceTrack = SqlRaceTrack.Get(p.RaceTrack.Id);
                listatemp.Add(userRace);
            }
            ListView.ItemsSource = listatemp;
        }
        #endregion
    }
}
