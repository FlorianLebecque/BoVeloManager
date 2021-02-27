using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace BoVeloManager.Management.kit
{
    /// <summary>
    /// Logique d'interaction pour modKitWindow.xaml
    /// </summary>
    public partial class modKitWindow : Window
    {
        private int kitId;
        public modKitWindow(int kitId_)
        {
            InitializeComponent();

            // initialise the windows
            kitId = kitId_;
            //get the user data
            string q = tools.DatabaseQuery.getKit_by_id(kitId);
            DataTable res = tools.Database.getData(q);
            //display the user data
            tb_editName.Text = (string)res.Rows[0]["name"];
            tb_editProperties.Text = (string)res.Rows[0]["properties"];
            
        }



        private void BT_update_Click(object sender, RoutedEventArgs e)
        {
            

            string newKitName = tb_editName.Text;
            if (newKitName != "")
            {
                updateKitName(kitId, newKitName);
            }

            string newProperties = tb_editProperties.Text;
            if (newProperties != "")
            {
                updateKitProperties(kitId, newProperties);
            }

        }

        private void updateKitName(int id, string name)
        {
            string q = tools.DatabaseQuery.setKitName(id, name);
            int res = tools.Database.setData(q);

            if (res == -1)
            {
                MessageBox.Show("An error has occured");
            }
            else if (res == 1)
            {
                MessageBox.Show("Name changed");
            }
            else
            {
                MessageBox.Show("The database is corrupted [name]");
            }
            this.Close();
        }

        private void updateKitProperties(int id, string newProp)
        {
            string q = tools.DatabaseQuery.setKitProperties(id, newProp);
            int res = tools.Database.setData(q);


            if (res == -1)
            {
                MessageBox.Show("An error has occured");
            }
            else if (res == 1)
            {
                MessageBox.Show("Properties changed");
            }
            else
            {
                MessageBox.Show("The database is corrupted [properties]");
            }
            this.Close();

        }


        private void BTCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
