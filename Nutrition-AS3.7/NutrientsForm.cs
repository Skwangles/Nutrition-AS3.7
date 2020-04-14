using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nutrition_AS3._7
{
    public partial class NutrientsForm : Form
    {
        public NutrientsForm(float[] G100, float[] Per, string[] namesTitle, string recipeName, float servings)
        {
            InitializeComponent();
            Graphics backgroundgraphics = this.CreateGraphics();
            this.g100 = G100;
            this.PerS = Per;
            this.titleNames = namesTitle;
            this.servings = servings;
            this.recipeName = recipeName;

        }
        string[] Labels = new string[] { "Energy", "Protein", "Fat, Total", " -Saturated Fat", "Carbohydrates", "Sugars", "Sodium" };
        bool hasRun = false;
        int default_spacer = 30;
        int gapBeforeTable = 100;
        string[] titleNames;
        string recipeName;
        float servings;
        float[] g100;
        float[] PerS;
        TableLayoutPanel TabLayout = new TableLayoutPanel();
        string[] ChangeToString(float[] h)
        {
            var temporary = new string[h.Length];
            int num = 0;
            foreach (float f in h)
            {
                temporary[num] = Math.Round(f, 2).ToString();
                num++;
            }
            return temporary;
        }
        void Graph()
        {
            if (!hasRun)
            {

                //
                //Serving Size
                //

                var RecipeNamelbl = new Label();
                var Servinglbl = new Label();
                var RecipeNametxt = new Label();
                var Servingtxt = new Label();
                this.ClientSize = new Size(350, 300);
                this.Controls.Add(Servingtxt);
                this.Controls.Add(RecipeNametxt);
                this.Controls.Add(RecipeNamelbl);
                this.Controls.Add(Servinglbl);
                this.Controls.Add(TabLayout);

                RecipeNamelbl.AutoSize = true;
                Servinglbl.AutoSize = true;
                RecipeNametxt.AutoSize = true;
                Servingtxt.AutoSize = true;



                RecipeNamelbl.Location = new Point(default_spacer, default_spacer);
                RecipeNamelbl.Text = "Recipe Name: ";
                RecipeNamelbl.Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);
                //
                //Recipe name value location - locked to previous lable
                //
                RecipeNametxt.Location = new Point(RecipeNamelbl.Left + RecipeNamelbl.Width, RecipeNamelbl.Top);
                RecipeNametxt.Text = recipeName;
                RecipeNametxt.Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Regular);
                //
                //Serving size
                //
                Servinglbl.Location = new Point(default_spacer, default_spacer * 2);
                Servinglbl.Text = "Servings: ";
                Servinglbl.Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);
                //
                //Recipe name value location - locked to previous lable
                //
                Servingtxt.Location = new Point(RecipeNamelbl.Left + RecipeNamelbl.Width, Servinglbl.Top);
                Servingtxt.Text = servings.ToString();
                Servingtxt.Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Regular);
                //
                //TableLayoutPanel setup
                //

                TabLayout.ClientSize = new Size(this.Width, this.Height - gapBeforeTable);
                TabLayout.Location = new Point(0, gapBeforeTable);

                TabLayout.ColumnCount = 3;
                TabLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                TabLayout.RowCount = 7;
                TabLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;



                var Per100 = ChangeToString(g100);
                var PerServing = ChangeToString(PerS);

                //Recipe Name, "per 100g" and per serving notes
                for (int i = 0; i < 3; i++)
                {
                    var temp = new Label();
                    temp.Text = titleNames[i];
                    temp.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
                    TabLayout.Controls.Add(temp, i, 0);
                }
                //labels first colum
                int countup = 1;
                foreach (string st in Labels)
                {
                    var temp = new Label();
                    temp.Text = st;
                    TabLayout.Controls.Add(temp, 0, countup);

                    countup++;
                }
                //
                //per 100g information entry
                //
                countup = 1;
                foreach (string st in Per100)
                {

                    var temp = new Label();
                    temp.Text = st;
                    TabLayout.Controls.Add(temp, 1, countup);

                    countup++;
                }

                //
                //Per serving enter
                //
                countup = 1;
                foreach (string st in PerServing)
                {

                    var temp = new Label();
                    temp.Text = st;
                    TabLayout.Controls.Add(temp, 2, countup);

                    countup++;
                }
                this.Show();
                hasRun = true;
            }
        }
        private void NutrientsForm_Load(object sender, EventArgs e)
        {
            this.Hide();
            //
            //Form setup
            //
            this.Icon = Properties.Resources.icons8_hamburger_50;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Nutrient Values";

            //
            //Draws Table
            //
            Graph();
        }

    }
}
