using CRUD.ClassesEntidades;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Desktop___interfaces.ClassesEntidades.SQL;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;
using System.Windows.Media;
using CRUD.ClassesEntidades.SQL;
using System.IO;
using Firebase.Auth;
using System.Threading;
using Firebase.Storage;
using System.Threading.Tasks;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowPowerUpInsert.xaml
    /// </summary>
    public partial class WindowPowerUpInsert : Window
    {
        int dbAction;
        PowerUp powerUpTemp;
        int openWarning = 0;
        string imageGlobal = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSrKIaJKJQyRPd518Qz0-VI3CFMwBcytEAv-A&usqp=CAU";

        #region Loads

        public WindowPowerUpInsert( int action, PowerUp power)
        {
            InitializeComponent();
            dbAction = action;
            powerUpTemp = power;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //altera a interface para encaixar com a action 
            switch (dbAction)
            {
                case SQL_DELETE:
                    LabelTitle.Content = "Eliminar o PowerUp";
                    TextBoxDescri.Text = powerUpTemp.Descri;
                    SliderTime.Value = powerUpTemp.TimeCharge;
                    ButtonAction.Content = "Eliminar";
                    ButtonImagePick.IsEnabled = false;
                    TextBoxDescri.IsEnabled = false;
                    SliderTime.IsEnabled = false;
                    ButtonImagePick.IsEnabled = false;
                    ImagePerfil.Source = new BitmapImage(new Uri(powerUpTemp.ImageUrl));
                    break;

                case SQL_UPDATE:
                    LabelTitle.Content = "Editar o PowerUp"; 
                    TextBoxDescri.Text = powerUpTemp.Descri;
                    SliderTime.Value = powerUpTemp.TimeCharge;
                    ImagePerfil.Source = new BitmapImage(new Uri(powerUpTemp.ImageUrl));
                    ButtonAction.Content = "Editar";
                    break;
            }
        }
        #endregion

        #region buttons



        /// <summary>
        /// metodo executado sempre que o slider muda de valor
        /// server para associar o valor do slider a uma laber de forma aque se possa ver o valor do slider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LabelTimeValue.Content = SliderTime.Value;
        }

        /// <summary>
        /// metedo executado quando o botão para a escolha da imagem é clicado
        /// irá abrir uma janela de colha de ficheiros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (op.ShowDialog() == true)
            {
                ImagePerfil.Source = new BitmapImage(new Uri(op.FileName));
            }
        }


        /// <summary>
        /// Metodo que serve para verificar se os dados estão correctos
        /// </summary>
        /// <returns></returns>
        private bool ValidaDados()
        {
            //verifica se existem caracteres especiais
            if (Validações.ValidaTexto(TextBoxDescri.Text))
            {
                TextBoxDescri.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                TextBoxDescri.Background = Brushes.White;
                if (openWarning == 0)
                {
                    switch (dbAction)
                    {
                        case SQL_INSERT:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("PowerUp inserido com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_DELETE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("PowerUp eliminado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_UPDATE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("PowerUp atualizado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;
                    }
                }
            }
            if (openWarning > 0)
            {
                //faz uma message box para avisar o utilizador
                MessageBox.Show("Erro : caracteres inválidos", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                openWarning = 0;
                return false;
            }
            return false;
        }

        private async void ButtonAction_Click(object sender, RoutedEventArgs e)
        {
            //muda a operação dependendo da action escolhida
            switch (dbAction)
            {
                case SQL_INSERT:

                    //verifica se os dados estão validos ou não
                    if (ValidaDados())
                    {
                        //Colocar o Caminho da Imagem
                        FileStream stream = File.Open(imageGlobal, FileMode.Open);
                        //Colocar a API KEY da Firebase
                        FirebaseAuthProvider auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDFcJX9irqR0GK5qPVIcZnfsaWPkfqpjGc"));
                        //Colocar o Email e a Password criados na Firebase
                        FirebaseAuthLink a = await auth.SignInWithEmailAndPasswordAsync("jonnypink007@gmail.com", "jonnyjonny");
                        //Objeto para Cancelar caso exista algum problema
                        CancellationTokenSource cancellation = new CancellationTokenSource();
                        //Criar uma task para inserir na Firebase
                        FirebaseStorageTask task = new FirebaseStorage(
                        //Adicionar o Bucket da Storage
                        "roadrush---pap.appspot.com",
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                            //Caso exista algum problema dá exception aqui
                            ThrowOnCancel = true
                        })
                        //Criar uma pasta User dentro da Storage
                        .Child("Users")
                        //Criar uma pasta com o UserName dentro da pasta User
                        .Child(TextBoxDescri.Text)
                        //Colocar o nome do ficheiro e a extensão
                        .Child(TextBoxDescri.Text + ".png")
                        //Colocar Async
                        .PutAsync(stream, cancellation.Token);

                        //Colocar a Url da Imagem dentro de uma variavel e colocar a tast em await
                        string urlImagem = await task;

                        //insere um novo user na bd
                        PowerUp user = new PowerUp(-1, TextBoxDescri.Text,
                        urlImagem,
                        Convert.ToInt32(SliderTime.Value)
                        );
                        SqlPowerUp.Add(user);
                    }
                    break;

                case SQL_DELETE:
                    ValidaDados();

                    //elimina o utilizador escolhido
                    SqlPowerUp.Del(powerUpTemp);
                    this.Close();
                    break;

                case SQL_UPDATE:

                    //verifica se os dados estão validos ou não
                    if (ValidaDados())
                    {
                        //Colocar o Caminho da Imagem
                        FileStream stream = File.Open(imageGlobal, FileMode.Open);
                        //Colocar a API KEY da Firebase
                        FirebaseAuthProvider auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDFcJX9irqR0GK5qPVIcZnfsaWPkfqpjGc"));
                        //Colocar o Email e a Password criados na Firebase
                        FirebaseAuthLink a = await auth.SignInWithEmailAndPasswordAsync("jonnypink007@gmail.com", "jonnyjonny");
                        //Objeto para Cancelar caso exista algum problema
                        CancellationTokenSource cancellation = new CancellationTokenSource();
                        //Criar uma task para inserir na Firebase
                        FirebaseStorageTask task = new FirebaseStorage(
                        //Adicionar o Bucket da Storage
                        "roadrush---pap.appspot.com",
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                            //Caso exista algum problema dá exception aqui
                            ThrowOnCancel = true
                        })
                        //Criar uma pasta User dentro da Storage
                        .Child("Users")
                        //Criar uma pasta com o UserName dentro da pasta User
                        .Child(TextBoxDescri.Text)
                        //Colocar o nome do ficheiro e a extensão
                        .Child(TextBoxDescri.Text + ".png")
                        //Colocar Async
                        .PutAsync(stream, cancellation.Token);

                        //Colocar a Url da Imagem dentro de uma variavel e colocar a tast em await
                        string urlImagem = await task;

                        //altera os atributos da entidade
                        powerUpTemp.Descri = TextBoxDescri.Text;
                        powerUpTemp.ImageUrl = "ola";
                        powerUpTemp.TimeCharge = Convert.ToInt32(SliderTime.Value);

                        SqlPowerUp.Set(powerUpTemp);
                    }
                    break;
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
