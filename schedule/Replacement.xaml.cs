using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;
using System.Configuration;

namespace schedule
{
    /// <summary>
    /// Логика взаимодействия для Replacement.xaml
    /// </summary>
    public partial class Replacement : Window, INotifyPropertyChanged
    {



        public CustomCommand SimpleCommand { get; set; }
        public CustomCommand TipleCommand { get; set; }
        public CustomCommand PipleCommand { get; set; }
        public CustomCommand GipleCommand { get; set; }
        public CustomCommand FlipCommand { get; set; }
        public Schedule SelectedSavod { get; set; }
        public Schedule ChangeSavod { get; set; }
        public Schedule Selected { get => selected; set { selected = value; Fill(); } }


        private Visibility visibility = Visibility.Hidden;
        public Visibility kreating
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;

                Fill("kreating");
            }
        }
        public Schedule schedule { get => selected; set { selected = value; Fill(); } }
        public Schedule Sel { get => sel; set { sel = value; Fill(); } }
        Random random = new Random();
        private Schedule selected = new Schedule();
        private Schedule sel = new Schedule();

        public ObservableCollection<Schedule> schedules { get; set; } = new ObservableCollection<Schedule>();

        public Replacement()


        {
            InitializeComponent();
            DataContext = this;


            //дописать сохранение этой команде
            TipleCommand = new CustomCommand(
                () => MessageBox.Show("f"));


            //Добавить
            GipleCommand = new CustomCommand(
               () => { kreating = Visibility.Visible; });

            PipleCommand = new CustomCommand(
               () => {
                   schedules.Add(new Schedule { Group = sel.Group, Pair = sel.Pair, Predmet = sel.Predmet, Cabinet = sel.Cabinet, Name = sel.Name });

                   kreating = Visibility.Hidden;
                   Fill(nameof(schedules));
               });

            //Добавление в табл в замене если надо, но нужно добавлять самому
            schedules.Add(new Schedule { Group = 1125, Pair = 1, Predmet = "ОАИП", Cabinet = 4, Name = "Пушкин А.А." });
            listschedule.ItemsSource = schedules;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listschedule.ItemsSource);
            view.Filter = UserFilter;


        }

        //Поиск
        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;

            else
                return ((item as Schedule).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);





        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listschedule.ItemsSource).Refresh();
        }


        // protected
        private void Fill([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        //
        public event PropertyChangedEventHandler? PropertyChanged;

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void Button_Click2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            schedules.Remove(Selected);
        }

        private void Dobav_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
