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
        readonly static int formwidth = 700;
        readonly int formheight = 900;
        public TextBox[] Forms_TextBoxes = new TextBox[11];
        public Button[] Forms_Buttons = new Button[11];
        public Label[] Forms_Labels = new Label[11];
        public ListBox[] Forms_ListBoxes = new ListBox[11];
        bool hasCalled = false;
        int Default_Spacer = 30;
        int SearchResultsWidth = 280;
        int SearchBarLeft = 0;
        int RecipeContentsheight = 300;
        int QuanitityTBMaxLength = 5;
        int QuantityBoxWidth = 30;
        string Recipe_Name_String;
        string SpecialChars = "!@#$%^&*~`=+[{]}\\|;:'\"<>/?";
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

        }

        public void AddItems()
        {
            if (!hasCalled)
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
                //Event Handlers

                Forms_Buttons[0].Click += new EventHandler(Search_Click);
                Forms_Buttons[1].Click += new EventHandler(Confirm_Click);
                Forms_Buttons[2].Click += new EventHandler(ClearRecipe_Click);
                Forms_Buttons[3].Click += new EventHandler(Complete_Click);
                Forms_TextBoxes[0].KeyDown += new KeyEventHandler(SearchTextBox_KeyDown);
                hasCalled = true;
                Forms_TextBoxes[0].Focus();

                Setlocations();//in seperate method so later locations may be updated.
                               //yet to add event handlers.
            }
        }

        void Setlocations()
        {
            //changing locations of each item.

            //locks the search button to the search bar
            Forms_TextBoxes[0].Left = SearchBarLeft;
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
            Forms_ListBoxes[0].Height = RecipeContentsheight;
            Forms_ListBoxes[0].Width = SearchResultsWidth;
            Forms_ListBoxes[0].HorizontalScrollbar = true;


            //Clear Button
            Forms_Buttons[2].Left = this.Width - Forms_Buttons[2].Width - 1;
            Forms_Buttons[2].Text = "Clear";


            //Confirm Button
            Forms_Buttons[1].Top = Forms_ListBoxes[0].Top; //locks confirm button to the Listbox results
            Forms_Buttons[1].Left = Forms_ListBoxes[0].Left + Forms_ListBoxes[0].Width + Default_Spacer;
            Forms_Buttons[1].Text = "Confirm";

            //Quanitity textbox
            Forms_TextBoxes[1].Width = QuantityBoxWidth;
            Forms_TextBoxes[1].Left = Forms_Buttons[1].Left;//makes quanitity below the 
            Forms_TextBoxes[1].Top = Forms_Buttons[1].Top + Forms_Buttons[1].Height;
            Forms_TextBoxes[1].MaxLength = QuanitityTBMaxLength;

            //Recipe contents
            Forms_ListBoxes[1].Height = RecipeContentsheight;
            Forms_ListBoxes[1].Width = SearchResultsWidth;
            Forms_ListBoxes[1].Top = Forms_Buttons[2].Top + Forms_Buttons[2].Height + Default_Spacer;
            Forms_ListBoxes[1].Left = this.Width - Forms_ListBoxes[1].Width;
            Forms_ListBoxes[1].HorizontalScrollbar = true;


            //complete button
            Forms_Buttons[3].Top = Forms_ListBoxes[1].Top + Forms_ListBoxes[1].Height + Default_Spacer;
            Forms_Buttons[3].Left = Forms_ListBoxes[1].Left;
            Forms_Buttons[3].Text = "Complete Recipe";



        }

        void Search_Click(object sender, EventArgs e)
        {

            Console.WriteLine("SearchClicked"); //notifies correct call
            Forms_ListBoxes[0].Items.Clear(); //makes sure no prior searches remain
                                              //makes sure the entries are put in alphabetical order
            if (!(Forms_TextBoxes[0].Text == "" || Forms_TextBoxes[0].Text == null)) //checks if box is empty
            {
                foreach (char f in SpecialChars)
                {
                    if (!Forms_TextBoxes[0].Text.Contains(f))
                    {
                        foreach (Ingredient a in Recipies) //searches each item
                        {
                            if (a.FName.ToLower().Contains(Forms_TextBoxes[0].Text.ToLower()))//checks if search query exists, and if 
                            {
                                Forms_ListBoxes[0].Items.Add(a);

                            }
                            else
                            {
                                Console.WriteLine("Not contained");
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Only AlphaNumerics and \",.()-_\" are allowed.");//As some names contain these punctuation.
                    }


                }
               
            }
            else
            {
                MessageBox.Show("Error with search Query");
            }
        }
        void Confirm_Click(object sender, EventArgs e)
        {
            Console.WriteLine("ConfirmClicked");
            if (Forms_TextBoxes[1].Text != "" || Forms_TextBoxes[1].Text != null && Forms_ListBoxes[0].SelectedItem != null)
            {
                try
                {

                    ((Ingredient)Forms_ListBoxes[0].SelectedItem).Quantity = float.Parse(Forms_TextBoxes[1].Text);
                    Forms_ListBoxes[1].Items.Add((Ingredient)Forms_ListBoxes[0].SelectedItem);
                }
                catch
                {
                    MessageBox.Show("Quanitity must be a number");
                }
            }
            else
            {
                MessageBox.Show("No Quanitity OR No Item Selected.");
            }

        }
        void ClearRecipe_Click(object sender, EventArgs e)
        {
            Console.WriteLine("ClearClicked");
            Forms_ListBoxes[1].Items.Clear();
        }
        void Complete_Click(object sender, EventArgs e)
        {
            float Energy = 0;
            float Protein = 0;
            float FatTot = 0;
            float FatSats = 0;
            float Carbs = 0;
            float Sugars = 0;
            float Sodium = 0;
            float Quantitys = 0;
            /*
           0 Food ID
1 Food name
2 Energy (kJ)
3 Protein (g)
4 Fat, total (g)
5 Fat, saturated (g)
6 Available carbohydrate (g)
7 Total sugars (g)
8 Sodium (mg)
             */

            Console.WriteLine("CompleteClicked");
            foreach (Ingredient s in Forms_ListBoxes[1].Items)
            {
                Quantitys += s.Quantity;
                Energy += s.FEnergy;
                Protein += s.FProtein;
                FatTot += s.FFatTotal;
                FatSats += s.FSat;
                Carbs += s.FCarb;
                Sodium += s.FSodium;
                Sugars += s.FSug;
            }
            //put out the lables here
            /*
            int a = 0;
            foreach (TextBox b in Forms_TextBoxes)
            {
                if (a != 2)
                {
                    b.Hide();
                    a++;
                }
            }
            foreach (Button b in Forms_Buttons)//loops through each array, adding them to controls.
            {
                b.Hide();
            }
            foreach (Label b in Forms_Labels)
            {
                b.Hide();
            }
            foreach (ListBox b in Forms_ListBoxes)
            {
                b.Hide();
            }
            */
            Console.WriteLine(Energy);
            Console.WriteLine(Protein);
            Console.WriteLine(FatTot);
            Console.WriteLine(FatSats);
            Console.WriteLine(Carbs);
            Console.WriteLine(Sugars);
            Console.WriteLine(Sodium);
        }
        public void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                Forms_Buttons[0].PerformClick();

            }
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
                            MessageBox.Show("Only AlphaNumerics and \",.()-_\" are allowed.");
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


            int i = 0;//Skip the very first line, which says the layout
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
                else
                {
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




    <-----Need2Do--->
Labels

Add method references.

Add proper code for filepath (In openfile method), so "Nutrients.txt" gets opened.

Add proper comments, so what is happening is easy to understand

Tidy up the ordering.

Put in a universal Run method, to get  out of  the Load method

    Comment.
Add serving size textbox.(plan on reusing recipename tb, just with a bool running a different  series  of checks.)
hide all other controls, so only recipe name is  showing, so user can enter that in. (may just add in a seperate box for quanitity)

*/
