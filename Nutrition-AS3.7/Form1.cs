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
        readonly static int formwidth = 240;
        readonly int formheight = 520;
        public TextBox[] Forms_TextBoxes = new TextBox[11];
        public Button[] Forms_Buttons = new Button[11];
        public Label[] Forms_Labels = new Label[11];
        public ListBox[] Forms_ListBoxes = new ListBox[11];
        string Recipe_Name_String;
        // readonly int[] ItemsLeft = new int[] { 20, 100, TbRecipe, BSearch,BConfirm,BClearRecipe,LQuanity, LRecipeName, LSearchResults, LTitle};  //TbSearch, tbQuanitiy, TbRecipe, BSearch,BConfirm,BClearRecipe,LQuanity, LRecipeName, LSearchResults, LTitle
        // readonly int[] ItemsHeight = new int[] { TbSearch, tbQuanitiy, TbRecipe, BSearch,BConfirm,BClearRecipe,LQuanity, LRecipeName, LSearchResults, LTitle};
        //above, sets the locations of the forms items.
        static List<Ingredient> Recipies = new List<Ingredient>();
        //array to store nutrient info from file 
        // <--Old Array for file -->
            //static string[,] nutrientArray = new string[2534, 9];
        //array to store heading from file (if needed) 

        #endregion

        
        private void Form1_Load(object sender, EventArgs e)
        {
            //starts the program 
            OpenFile();
            Setup();
            Recipe_Name();
            //AddItems();

        }
        void Recipe_Name()
        {
            Controls.Add(Forms_TextBoxes[2]);
            Forms_TextBoxes[2].KeyDown += new KeyEventHandler(RecipeName_KeyDown);
            Forms_TextBoxes[2].Text = "Enter Recipe Name";

            
        }
        void Setup()
        {
            //uses the softvalues to setup the form
            ActiveForm.Height = formheight;
            ActiveForm.Width = formwidth;

            //TbSearch, tbQuanitiy, TbRecipe, BSearch,BConfirm,BClearRecipe,LQuanity, LRecipeName, LSearchResults,LTitle
            Forms_TextBoxes[0] = new TextBox();  //Tb Search
            Forms_TextBoxes[1] = new TextBox();  //Tb Quantitiy
            Forms_TextBoxes[2] = new TextBox();  //Tb Recipe Name
            Forms_Buttons[0] = new Button();   //B Search
            Forms_Buttons[1] = new Button();   //B Confirm
            Forms_Buttons[2] = new Button();   //B Clear Recipe
            Forms_Buttons[3] = new Button();   //B Complete Recipe
            Forms_Labels[0] = new Label();    //LQuantity
            Forms_Labels[1] = new Label();    //LRecipe Name
            Forms_Labels[2] = new Label();    //LSearch Results
            Forms_Labels[3] = new Label();    //L Title
            Forms_ListBoxes[0] = new ListBox(); //Lb Search Results
            Forms_ListBoxes[1] = new ListBox(); //Lb Recipe



            /*
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
            */
        }

        public void AddItems()
        {
            //adds items to controls
            int a = 0;//As  Recipe Textbox has already been added, is skipped(Index of it is 2)
            foreach (TextBox b in Forms_TextBoxes)
            {
                if (a != 2)
                {
                    Controls.Add(b);
                    a++;
                }
            }
            foreach (Button b in Forms_Buttons)//loops through each array, adding them to controls.
            {
                Controls.Add(b);
            }
            foreach (Label b in Forms_Labels)
            {
                Controls.Add(b);
            }
            //yet to add event handlers.
        }

        public void RecipeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Recipe_Name_String = Forms_TextBoxes[2].Text;
                //check for special cases
            }

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


        }

    }
}
/*
Next, to add button events, and add them to controls

    Add checks for recipe name strign.
    Add values for other items and add their event handlers.
To search list for .contains(searchQuery)
and display the heading.

Add the softcoded Alignment values for the Items (Buttons, labels, etc.) - Locations

Add method references.

Add proper code for filepath (In openfile method), so "Nutrients.txt" gets opened.

Add proper comments, so what is happening is easy to understand

Tidy up the ordering.
Put the others items set up method  call in the RecipeName method.
Put in a universal Run method, to get  out of  the Load method
----What Was Done---
Added RecipeName method before the setup of other items
Added items  to arrya
*/
