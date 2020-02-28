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
        readonly static string filepath = @"c:\Users\tree_\Downloads\";
        readonly static int formwidth = 400;
        readonly int formheight = 520;
        public TextBox[] Forms_TextBoxes = new TextBox[11];
        public Button[] Forms_Buttons = new Button[11];
        public Label[] Forms_Labels = new Label[11];
        public ListBox[] Forms_ListBoxes = new ListBox[11];
        int Default_Spacer = 30;
        int QuanitityTBMaxLength = 5;
        int QuantityBoxWidth = 30;
        string Recipe_Name_String;
        string SpecialChars = "!@#$%^&*()~`=+[{]}\\|;:'\",<.>/?";
        //readonly int[] ItemsLeft = new int[] { 20, 100, 20, this.width - 50, this.width - 50, this.width / 2 };  //TbSearch, tbQuanitiy, TbRecipe, BSearch,BConfirm,BClearRecipe,LQuanity, LRecipeName, LSearchResults, LTitle
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
            int[] ItemsLeft = new int[] { 20, 100, 20, this.Width - 50, this.Width - 50, this.Width / 2 };  //TbSearch, tbQuanitiy, TbRecipe, BSearch,BConfirm,BClearRecipe,LQuanity, LRecipeName, LSearchResults, LTitle
            //starts the program 
            OpenFile();
            Setup();
            Recipe_Name();


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
            this.Height = formheight;
            this.Width = formwidth;

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
            foreach (ListBox b in Forms_ListBoxes)
            {
                Controls.Add(b);
            }

            Setlocations();//in seperate method so later locations may be updated.
            //yet to add event handlers.
        }

        void Setlocations()
        {
            //changing locations of each item.

            //locks the search button to the search bar
            Forms_TextBoxes[0].Left = Default_Spacer;
            Forms_TextBoxes[0].Top = Default_Spacer;
            Forms_TextBoxes[0].Width = Forms_ListBoxes[0].Width;

            //Searchbutton
            Forms_Buttons[0].Left = Forms_TextBoxes[0].Left + Forms_TextBoxes[0].Width + Default_Spacer;
            Forms_Buttons[0].Top = Forms_TextBoxes[0].Top;
            Forms_Buttons[0].Text = "Search";
            //----------------------------------------
            //Search results left same as search bar

            //Search Results
            Forms_ListBoxes[0].Left = Forms_TextBoxes[0].Left;//locks the top and left of Search results to Search text box
            Forms_ListBoxes[0].Top = Forms_TextBoxes[0].Top + Default_Spacer;

            //Clear Button
            Forms_Buttons[2].Left = this.Width - Forms_Buttons[2].Width - 1;
            Forms_Buttons[2].Text = "Clear";


            //complete button
            Forms_Buttons[3].Top = Forms_ListBoxes[1].Top + Forms_ListBoxes[1].Height + Default_Spacer;
            Forms_Buttons[3].Left = Forms_ListBoxes[1].Left;
            Forms_Buttons[3].Text = "Complete Recipe";
            //Confirm Button
            Forms_Buttons[1].Top = Forms_ListBoxes[0].Top; //locks confirm button to the Listbox results
            Forms_Buttons[1].Left = Forms_ListBoxes[0].Left + Forms_ListBoxes[0].Width + Default_Spacer;
            Forms_Buttons[1].Text = "Confirm";

            //Quanitity textbox
            Forms_TextBoxes[1].Left = Forms_Buttons[1].Left;//makes quanitity below the 
            Forms_TextBoxes[1].Top = Forms_Buttons[1].Top + Forms_Buttons[1].Height;
            Forms_TextBoxes[1].MaxLength = QuanitityTBMaxLength;
            Forms_TextBoxes[1].Width = QuantityBoxWidth;

            //Recipe contents
            Forms_ListBoxes[1].Top = Forms_Buttons[2].Top + Forms_Buttons[2].Height + Default_Spacer;
            Forms_ListBoxes[1].Left = this.Width - Forms_ListBoxes[1].Width;
        }




        public void RecipeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    //checks for special characters or errors
                    Recipe_Name_String = Forms_TextBoxes[2].Text;
                    foreach (char a in SpecialChars)
                    {
                        if (!Recipe_Name_String.Contains(a))
                        {
                            Forms_TextBoxes[2].Hide();
                            AddItems();
                        }
                        else
                        {
                            MessageBox.Show("Only AlphaNumerics and -_ are allowed.");
                        }


                    }
                }
                catch
                {
                    MessageBox.Show("Recipe Name error has occured.");
                }


            }

        }

        static void OpenFile()
        {
            //Adds to a list for the Ingredient class, an instance of the ingredient class which stores all the information for each entry, for easy accessability.

            //Open Nutrient file 
            string filename = "Nutrientfile.txt";
            StreamReader reader = File.OpenText(filepath + filename);
            int i = 0;
            Console.Write(".... reading file");

            //Load data into an array 
            while (reader.Peek() != -1)
            {
                string[] line = reader.ReadLine().Split('\t');
                if (!(i == 0))
                {


                    //read in line and split by tab 

                    foreach (string s in line)
                    {
                        Console.WriteLine(s);
                    }

                    Recipies.Add(new Ingredient(line[0], line[1], float.Parse(line[2]), float.Parse(line[3]), float.Parse(line[4]), float.Parse(line[5]), float.Parse(line[6]), float.Parse(line[7]), float.Parse(line[8])));

                    i++;
                }
            }
            reader.Close();
            Console.WriteLine(".... file read");
            Console.WriteLine();


        }

    }
}
/*
Next, to add button events, and add them to controls


    Add values for other items and add their event handlers.
To search list for .contains(searchQuery)
and display the heading.

    <-----Need2Do--->
Labels
EventHandlers
.contains, search result. (foreach ingridient.fname. contains(searchquery))


Add method references.

Add proper code for filepath (In openfile method), so "Nutrients.txt" gets opened.

Add proper comments, so what is happening is easy to understand

Tidy up the ordering.

Put in a universal Run method, to get  out of  the Load method

*/
