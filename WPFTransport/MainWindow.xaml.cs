using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SwissTransport;

namespace WPFTransport
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        SolidColorBrush On = new SolidColorBrush(Color.FromRgb(240, 222, 45));
        Transport Helper = new Transport();
        int AbAn = 0;
        private bool Toggled = false;
        public MainWindow()
        {
            InitializeComponent();
            //MouseDown+=Grid_MouseDown;
        }

        // Button für die Umschaltung von Ab und An
        private void Bu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Bu.Toggled1 == true)
            {
                AbAn = 1;
            }
            else
            {
                AbAn = 0;
            }
        }

        //Button für das umdrehen von Standort und Destination
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string a = VonBox.Text;
            string b = NachBox.Text;
            VonBox.Text = b;
            NachBox.Text = a;
        }

        //Button zum schliessen des Programms
        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        //Button um Verbindung zu Suchen
        private void VerbindungSuchen_Click(object sender, RoutedEventArgs e)
        
        {
            try
            {
                DateTime? x = Datum.SelectedDate;
                DateTime? y = Zeit.Value;
                var Verbindungen = Helper.GetConnections(VonBox.Text, NachBox.Text, x.Value.ToString("yyyy-MM-dd"), y.Value.ToString("HH:mm"), AbAn);

                gayBox.Items.Clear();

                string Head = "  Von" + "         " + "Abfahrt"
                    + "           " + "Dauer"
                    + "           " + "Destination"
                    + "           " + "Ankunft";
                string Line = "‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾";
                gayBox.Items.Add(Head);
                gayBox.Items.Add(Line);

                foreach (var Verbindung in Verbindungen.ConnectionList)
                {
                    string Item = Verbindung.From.Station.Name + "       "
                        + Verbindung.From.Departure.Remove(0, 11).Remove(5, 8) + "         "
                        + Verbindung.Duration.Remove(0, 3).Remove(5, 3) + "        "
                        + Verbindung.To.Station.Name + "       "
                        + Verbindung.To.Arrival.Remove(0, 11).Remove(5, 8);
                    gayBox.Items.Add(Item);

                }
            }
            catch { };
        }

        //Standorts Checkbox, Keyup = Vorschläge
        private void VonBox_KeyUp(object sender, KeyEventArgs e)
        {
            var Stationen = Helper.GetStations(VonBox.Text);

            for (var irgendwas = 2; irgendwas < VonBox.Items.Count; irgendwas++) //1 ish bhd
            {
                VonBox.Items.RemoveAt(irgendwas);
            }

            foreach (var irgendeppis in Stationen.StationList)
            {
                VonBox.Items.Add(irgendeppis.Name);
            }

            VonBox.IsDropDownOpen = true;
        }

        //Destinations Checkbox, Keyup = Vorschläge
        private void NachBox_KeyUp(object sender, KeyEventArgs e)
        {
            var Stationen = Helper.GetStations(NachBox.Text);

            for (var irgendwas = 2; irgendwas < NachBox.Items.Count; irgendwas++) //1 ish bhd
            {
                NachBox.Items.RemoveAt(irgendwas);
            }

            foreach (var irgendeppis in Stationen.StationList)
            {
                NachBox.Items.Add(irgendeppis.Name);
            }

            NachBox.IsDropDownOpen = true;
        }

        //Button für die Ausgabe des Station Fahrplans
        private void StationButton_Click(object sender, RoutedEventArgs e)
        {
            var sb = Helper.GetStationBoard(VonBox.Text);

            gayBox.Items.Clear();

            string Head = "Zug" + "               " + "Destination";
            string Line = "‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾";

            gayBox.Items.Add(Head);
            gayBox.Items.Add(Line);

            foreach (var OEV in sb.Entries)
            {
                string Item = OEV.Category+OEV.Number + "                 " + OEV.To;
                gayBox.Items.Add(Item);
            }
        }

        //Chu
        public bool Toggled2 { get => Toggled; set => Toggled = value; }
        private void Chuchu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Train.Margin = new Thickness(290, -37, -331, 0);
                Toggled2 = true;
            }
            else
            {
                Train.Margin = new Thickness(216, -37, -331, 0);
                Toggled = false;
            }

        }

        //Ist für das Bewegen des Programms zuständig
        private void BGGRID_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
