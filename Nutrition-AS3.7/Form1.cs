using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Nutrition_AS3._7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region Default Values and Arrays
        //sets the integers and arrays
        readonly static string filepath = @"c:\";
        readonly static int formwidth = 100;
        readonly int formheight = 100;
        // readonly int[] ItemsLeft = new int[] { Title, Ingredientlbl, Searchbox, search button, listbox, quanitiy, confirmlbl, tick, x, clear recipe, Listbox - Ingredientsselected, Rect - Nutritionbox };
        static List<Ingredient> Recipies = new List<Ingredient>();
        //array to store nutrient info from file 
        static string[,] nutrientArray = new string[2534, 9];
        //array to store heading from file (if needed) 
        static string[] headings = new string[9];
        #endregion

        #region Items Declarations
        //creates the items
        ListBox ListBox_Search = new ListBox();
        TextBox TextBox_Search = new TextBox();
        TextBox TextBox_Quantity = new TextBox();
        TextBox TextBox_Recipe_Name = new TextBox();
        Button Button_Search = new Button();
        Button Button_Confirm = new Button();
        Button Button_Clear_Recipe = new Button();
        Button Button_Finished_Recipe = new Button();
        Label label_Quantity = new Label();
        Label label_Recipe = new Label();
        Label label_Search_Results = new Label();
        Label label_Title = new Label();
        //Int[] ItemsLeft = new int[] { Title, SearchResults, Searchbox, search button, quanitiytb, quantitylbl, confirmbtn, tick, x, clear recipe, Listbox - Ingredientsselected, Rect - Nutritionbox };








        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            //starts the program 
            OpenFile();
            Setup();
        }

        void Setup()
        {
            //uses the softvalues to setup the form
            ActiveForm.Height = formheight;
            ActiveForm.Width = formwidth;

        }

        public void AddItems()
        {
            //adds items to controls
        }


        static void OpenFile()
        {
            //Adds to a list for the Ingredient class, an instance of the ingredient class which stores all the information for each entry, for easy accessability.

            //Open Nutrient file 
            string filename = "Nutrientfile.txt";
            StreamReader reader = File.OpenText(filepath + filename);
            int i = 0; Console.Write(".... reading file");

            //Load data into an array 
            while (reader.Peek() != -1)
            {

                //read in line and split by tab 
                string[] line = reader.ReadLine().Split('\t');

                Recipies.Add(new Ingredient(line[0], line[1], float.Parse(line[2]), float.Parse(line[3]), float.Parse(line[4]), float.Parse(line[5]), float.Parse(line[6]), float.Parse(line[7]), float.Parse(line[8])));

                i++;
            }
            reader.Close();
            Console.WriteLine(".... file read");
            Console.WriteLine();

            //store the headings into the "headings" Array 
            int loop = 0;
            foreach (Ingredient ing in Recipies)
            {
                headings[loop] = ing.FName;
                loop++;

            }
        }

    }
}
