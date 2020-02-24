using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        public CreatePrizeForm()
        {
            InitializeComponent();
        }
        

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PrizeModel model = new PrizeModel(
                    placeNameValue.Text,
                    placeNumberValue.Text,
                    prizeAmountValue.Text,
                    prizePercentageValue.Text);

                foreach (IDataConnection db in GlobalConfig.Connections)
                {
                    db.CreatePrize(model);
                }
                placeNameValue.Text = "";
                placeNumberValue.Text = "";
                prizeAmountValue.Text = "0";
                prizePercentageValue.Text = "0";

            } else
            {
                MessageBox.Show("This form has invalid information.");
            }
        }

        private bool ValidateForm()
        {
            bool output = true;

            if (!int.TryParse(placeNumberValue.Text, out int placeNumber))
            {
                output = false;
            }

            if (placeNumber < 1)
            {
                output = false;
            }

            if(placeNameValue.Text.Length == 0)
            {
                output = false;
            }

            double prizePercentage = 0;
            if (!decimal.TryParse(prizeAmountValue.Text, out decimal prizeAmount) || !double.TryParse(prizePercentageValue.Text, out prizePercentage))
            {
                output = false;
            }

            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                output = false;
            }
            
            if(prizePercentage <0 || prizePercentage > 100)
            {
                output = false;
            }

            return output;
        }
    }
}
