using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


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
        //file path
        readonly static int formwidth = 700;
        readonly int formheight = 500;
        public TextBox[] Forms_TextBoxes = new TextBox[5]; //stores the instances of textboxes
        public Button[] Forms_Buttons = new Button[5];//stores the instances of Buttons
        public Label[] Forms_Labels = new Label[4]; //stores the instances of Labels
        public ListBox[] Forms_ListBoxes = new ListBox[2]; //stores the instances of Listboxes
        bool hasCalled = false; //checks if the eventhandler creator and controls add has occured
        bool hadItems = false;
        bool hasSetup = false;
        int recipe_Contents_Height = 300;
        int default_Spacer = 30; //For spacing
        int searchResultsWidth = 280; //Sets size of search results box
        int searchBarLeft = 20; //the left of item whic        
        int quanitityTBMaxLength = 5;
        int quantityBoxWidth = 30;
        int moveDown = 40;//move down controls to put in exit button        
        Color buttonsColor = SystemColors.ButtonFace;
        string recipe_Name_String;
        string searchSpecChars = "!@#$^&*~`=+[{]}\\|;:'\"<>/?";
        string allSpecialChars = "\",.()-_\"!@#$%^&*~`=+[{]}\\|;:'\"<>/?";
        //above, sets the locations of the forms items.
        static List<Ingredient> Recipies = new List<Ingredient>();
        //array to store nutrient info from file 
        //starts the program 
        //public PictureBox Table = new PictureBox();



        #endregion


        private void Form1_Load(object sender, EventArgs e)
        {  //TbSearch, tbQuanitiy, TbRecipe, BSearch,BConfirm,BClearRecipe,LQuanity, LRecipeName, LSearchResults, LTitle
            this.Hide();
            OpenFile();
            Setup();
            this.Show();
            Recipe_Name();

        }//Program Starter
        void Recipe_Name()
        {
            this.Hide();//hides any setup glitches so form looks slick.

            Controls.Add(Forms_TextBoxes[2]);

            Forms_TextBoxes[2].KeyDown += new KeyEventHandler(RecipeName_KeyDown);



            //Recipe Name TextBox
            Forms_TextBoxes[2].Left = 0;
            Forms_TextBoxes[2].Top = 0 + moveDown;
            //---textproperties
            Forms_TextBoxes[2].AutoSize = true;
            Forms_TextBoxes[2].Text = "Recipe Name";
            Forms_TextBoxes[2].Font = new Font("Arial", 16);
            Forms_TextBoxes[2].ClientSize = new Size(this.Width, 100);
            Forms_TextBoxes[2].TextAlign = HorizontalAlignment.Center;
            Forms_TextBoxes[2].MaxLength = 15; //Doesn't need to be longer
            this.Show();
            //---
        }//Runs the method to get the recipe name. And creates EXit and Recipetextbox controls


        #region Setup Processes
        void Setup()
        {
            if (!hasSetup)
            {


                //uses the softvalues to setup the form
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.Height = formheight;
                this.Width = formwidth;

                this.MaximizeBox = false;
                this.MinimizeBox = false;


                this.Icon = Properties.Resources.icons8_hamburger_50;
                this.Text = "Nutrient Calculator";



                //TbSearch, tbQuanitiy, TbRecipe, BSearch,BConfirm,BClearRecipe,LQuanity, LRecipeName, LSearchResults,LTitle
                Forms_TextBoxes[0] = new TextBox();  //Tb Search
                Forms_TextBoxes[1] = new TextBox();  //Tb Quantitiy
                Forms_TextBoxes[2] = new TextBox();  //Tb Recipe Name
                Forms_TextBoxes[3] = new TextBox(); //Tb Serving Size -- Would have done a combo box, but had to accept float as per exam requirments

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
                hasSetup = true;
            }
        }//sets instances, and formprioetues
        public void AddItems()
        {
            if (!hasCalled)//checks if the items have already been added.
            {
                //adds items to controls
                int counts = 0;//As  Recipe Textbox has already been added, is skipped(Index of it is 2)
                foreach (TextBox TextboxInstanceForeach in Forms_TextBoxes)
                {
                    if (counts != 2 && counts != 4)
                    {
                        Controls.Add(TextboxInstanceForeach);
                        counts++;
                    }
                    else
                    {
                        counts++;
                    }
                }
                foreach (Button counters in Forms_Buttons)//loops through each array, adding them to controls.
                {
                    if (counters != Forms_Buttons[0])
                    {
                        Controls.Add(counters);
                    }

                }
                foreach (Label b in Forms_Labels)
                {
                    Controls.Add(b);
                    b.BackColor = Color.Transparent;
                }
                foreach (ListBox b in Forms_ListBoxes)
                {
                    Controls.Add(b);
                }

                //Event Handlers
                #region create eventhandlers
                //Forms_Buttons[0].Click += new EventHandler(Search_Click);
                Forms_Buttons[1].Click += new EventHandler(Confirm_Click);
                Forms_Buttons[2].Click += new EventHandler(ClearRecipe_Click);
                Forms_Buttons[3].Click += new EventHandler(Complete_Click);
                Forms_TextBoxes[0].TextChanged += new EventHandler(Search_TextChanged);
                //checks for the "enter" key in hte textbox


                #endregion
                hasCalled = true;
                Forms_TextBoxes[0].Focus();//Focuses the cursour on the search bar

                this.BackColor = Color.Beige;
                Setlocations();//in seperate method so later locations may be updated.


            }
        }//adds items to controls


        void Setlocations()          // --------------------------------Properties off Controls are HERE--------------------------------
        {
            this.Hide();
            //changing locations of each item.

            //
            //search box
            //
            Forms_TextBoxes[0].Width = Forms_ListBoxes[0].Width;
            Forms_TextBoxes[0].Left = searchBarLeft;//Other controls base their left off this control
            Forms_TextBoxes[0].Top = default_Spacer;
            Forms_TextBoxes[0].BackColor = Color.LightGray;
            //
            //Search Results--------
            //
            Forms_ListBoxes[0].Left = Forms_TextBoxes[0].Left;//locks the top and left of Search results to Search text box
            Forms_ListBoxes[0].Top = Forms_TextBoxes[0].Top + default_Spacer;

            int recipeContentsheight = this.Height - Forms_ListBoxes[0].Top - default_Spacer * 2;//sets the height of the listbox

            Forms_ListBoxes[0].Height = recipeContentsheight;
            Forms_ListBoxes[0].Width = searchResultsWidth;
            Forms_ListBoxes[0].HorizontalScrollbar = true;
            Forms_ListBoxes[0].Font = new Font(FontFamily.GenericSerif, 10);
            //
            //sets search box witdth.
            //
            Forms_TextBoxes[0].Width = Forms_ListBoxes[0].Width;
            //
            //Quanitity textbox----------
            //
            Forms_TextBoxes[1].Width = quantityBoxWidth;
            Forms_TextBoxes[1].MaxLength = quanitityTBMaxLength;
            Forms_TextBoxes[1].Top = Forms_ListBoxes[0].Top; //locks confirm button to the Listbox results
            Forms_TextBoxes[1].Left = Forms_ListBoxes[0].Left + Forms_ListBoxes[0].Width;
            Forms_TextBoxes[1].BackColor = Color.LightGray;
            //
            //Confirm Button--------------
            //
            Forms_Buttons[1].Left = Forms_TextBoxes[1].Left;//makes quanitity below the 
            Forms_Buttons[1].Top = Forms_TextBoxes[1].Top + Forms_TextBoxes[1].Height;
            Forms_Buttons[1].Text = "Add";
            Forms_Buttons[1].BackColor = buttonsColor;
            //
            //Recipe listbox----------------
            //
            Forms_ListBoxes[1].Height = recipe_Contents_Height;
            Forms_ListBoxes[1].Width = searchResultsWidth;
            Forms_ListBoxes[1].Top = Forms_ListBoxes[0].Top;
            Forms_ListBoxes[1].Left = this.Width - Forms_ListBoxes[1].Width - default_Spacer;//-2 makes it not stick perfectly to the right
            Forms_ListBoxes[1].HorizontalScrollbar = true;
            Forms_ListBoxes[1].Font = new Font(FontFamily.GenericSerif, 10);
            //
            //Clear Button-------------
            //
            Forms_Buttons[2].Left = Forms_ListBoxes[1].Left + Forms_ListBoxes[1].Width - Forms_Buttons[2].Width;
            Forms_Buttons[2].Top = Forms_ListBoxes[1].Top + Forms_ListBoxes[1].Height;
            Forms_Buttons[2].Text = "Clear";
            Forms_Buttons[2].BackColor = buttonsColor;
            //
            //Serving size Textbox------------
            //
            Forms_TextBoxes[3].Top = Forms_ListBoxes[1].Top + Forms_ListBoxes[1].Height + default_Spacer;
            Forms_TextBoxes[3].Left = Forms_ListBoxes[1].Left;
            Forms_TextBoxes[3].MaxLength = 4;
            Forms_TextBoxes[3].Width = Forms_Buttons[3].Width;
            Forms_TextBoxes[3].Font = new Font("Ariel", 16);
            Forms_TextBoxes[3].BackColor = Color.LightGray;
            //
            //complete button----------------
            //
            Forms_Buttons[3].Top = Forms_TextBoxes[3].Top + Forms_TextBoxes[3].Height;
            Forms_Buttons[3].Left = Forms_TextBoxes[3].Left;
            Forms_Buttons[3].Text = "Complete Recipe";
            Forms_Buttons[3].BackColor = buttonsColor;
            //
            //Quantity Label---------------
            //
            Forms_Labels[0].AutoSize = true;
            Forms_Labels[0].Left = Forms_TextBoxes[1].Left + Forms_TextBoxes[1].Width;
            Forms_Labels[0].Top = Forms_TextBoxes[1].Top;
            Forms_Labels[0].Text = "(g)Quantity";
            Forms_Labels[0].Width = 75;
            //
            //RecipeName Label--------------
            //
            Forms_Labels[1].Left = Forms_ListBoxes[1].Left;
            Forms_Labels[1].Top = Forms_ListBoxes[1].Top - Forms_Labels[1].Height;
            Forms_Labels[1].Width = Forms_ListBoxes[1].Width;
            Forms_Labels[1].Text = "Ingredients: ";
            Forms_Labels[1].Font = new Font(FontFamily.GenericSansSerif, 16, FontStyle.Bold);
            //
            //Search Here Label--------------
            //
            Forms_Labels[2].Width = 100;
            Forms_Labels[2].Left = searchBarLeft;
            Forms_Labels[2].Top = Forms_TextBoxes[0].Top - Forms_Labels[2].Height;
            Forms_Labels[2].Text = "Search Here";
            Forms_Labels[2].Font = new Font(FontFamily.GenericSerif, 12);
            //
            //Serving Size Label----------
            //
            Forms_Labels[3].Left = Forms_TextBoxes[3].Left + Forms_TextBoxes[3].Width;
            Forms_Labels[3].Top = Forms_TextBoxes[3].Top;
            Forms_Labels[3].Text = "Serving Size(g)";
            Forms_Labels[3].AutoSize = true;

            foreach (TextBox TextboxInstance in Forms_TextBoxes)
            {
                try
                {
                    TextboxInstance.TextAlign = HorizontalAlignment.Center;
                }
                catch
                { }

            }
            for (int counter = 0; counter < 4; counter++)
            {
                Forms_Buttons[counter].UseVisualStyleBackColor = true;
                Forms_Buttons[counter].FlatStyle = 0;
            }
            this.Show();
        }//sets item propetires
        #endregion

        bool HasSpecChars(int tbindex, int specialList)//reused to check against special chars
        {

            foreach (char charInside in (specialList == 1 ? searchSpecChars : allSpecialChars))//reuses the check char's function. One list needs to be used to allow things that the other lists completely block.
            {

                if (Forms_TextBoxes[tbindex].Text.Contains(charInside) && (specialList == 1 ? true : charInside != char.Parse(".")))
                {
                    return true;
                }
            }
            return false;
        }



        #region Event Handlers

        void Search_TextChanged(object sender, EventArgs e)
        {
            hadItems = false;
            //notifies correct call
            Forms_ListBoxes[0].Items.Clear();
            if (Forms_TextBoxes[0].Text == "")
            {
                Forms_ListBoxes[0].Items.Add("No Results");
            }
            else
            {


                //makes sure no prior searches remain
                //makes sure the entries are put in alphabetical order
                if (!(Forms_TextBoxes[0].Text == "" || Forms_TextBoxes[0].Text == null)) //checks if box is empty
                {


                    Forms_ListBoxes[0].Items.Clear();
                    foreach (Ingredient RecipeIngredientInstance in Recipies) //searches each item
                    {
                        if (RecipeIngredientInstance.FName.ToLower().Contains(Forms_TextBoxes[0].Text.ToLower()))//checks if search query exists, and if it is present
                        {
                            Forms_ListBoxes[0].Items.Add(RecipeIngredientInstance);
                            hadItems = true;
                        }
                        //removed debug -- not contained print
                    }
                    if (!hadItems)
                    {
                        Forms_ListBoxes[0].Items.Add("No Results");
                    }
                }
            }
        }
        //eventhandler for search button
        void Confirm_Click(object sender, EventArgs e)
        {
            Console.WriteLine("ConfirmClicked");
            if ((Forms_TextBoxes[1].Text != "" || Forms_TextBoxes[1].Text != null) && Forms_ListBoxes[0].SelectedItem != null && Forms_TextBoxes[1].Text != "0" && hadItems == true)//checks if empty, or if the number is 0 as serving size cannot be 0g
            {

                if (!HasSpecChars(1, 0))
                {
                    try
                    {
                        ((Ingredient)Forms_ListBoxes[0].SelectedItem).Quantity += float.Parse(Forms_TextBoxes[1].Text);
                        if (Forms_ListBoxes[1].Items.Contains((Ingredient)Forms_ListBoxes[0].SelectedItem))
                        {
                            Forms_ListBoxes[1].Items.Remove((Ingredient)Forms_ListBoxes[0].SelectedItem);
                        }//adds the quanitity added
                        Forms_ListBoxes[1].Items.Add((Ingredient)Forms_ListBoxes[0].SelectedItem);//adds to the recipe list.
                        Forms_TextBoxes[0].Text = "";
                        Forms_TextBoxes[1].Text = "";//so textboxes are ready for next ingredient.
                    }
                    catch
                    {

                        Forms_TextBoxes[1].Text = "";//so textboxes are ready for next ingredient.
                        MessageBox.Show("Invalid item selected OR Quanitity must be a number >0");
                    }
                }
                else
                {

                    Forms_TextBoxes[1].Text = "";//so textboxes are ready for next ingredient.
                    MessageBox.Show("No special characters.");
                }
            }
            else
            {

                Forms_TextBoxes[1].Text = "";//so textboxes are ready for next ingredient.
                MessageBox.Show("Invalid Quanitity OR Item Selected.");
            }
        }//confirm event handler.
        void ClearRecipe_Click(object sender, EventArgs e)
        {
            Console.WriteLine("ClearClicked");
            foreach (Ingredient IngredientInstances in Forms_ListBoxes[1].Items)
            {
                IngredientInstances.Quantity = 0;
            }
            Forms_ListBoxes[1].Items.Clear();//clears listbox
        }//Clears the recipe listbox       
        void Complete_Click(object sender, EventArgs e)
        {
            if (Forms_ListBoxes[1].Items.Count > 0 && !HasSpecChars(3, 0) && float.TryParse(Forms_TextBoxes[3].Text, out var serving_Size))
            {
                this.Hide();
                float Quantitys = 0;
                var Per100Grams = new float[7];
                var AverageSize = new float[7];
                foreach (Ingredient IngredientInstance in Forms_ListBoxes[1].Items)
                {
                    Quantitys += IngredientInstance.Quantity;
                }
                Console.WriteLine("CompleteClicked");
                int indexCount;
                foreach (Ingredient s in Forms_ListBoxes[1].Items)
                {
                    //Energy, Protein, FatTotal, FatSat, Carbs, Sodium, Sugar
                    indexCount = 0;
                    Per100Grams[indexCount] += s.FEnergy * (s.Quantity / Quantitys);//adds calculations to the listbox.
                    indexCount++;
                    Per100Grams[indexCount] += s.FProtein * (s.Quantity / Quantitys);
                    indexCount++;
                    Per100Grams[indexCount] += s.FFatTotal * (s.Quantity / Quantitys);
                    indexCount++;
                    Per100Grams[indexCount] += s.FSat * (s.Quantity / Quantitys);
                    indexCount++;
                    Per100Grams[indexCount] += s.FCarb * (s.Quantity / Quantitys);
                    indexCount++;
                    Per100Grams[indexCount] += s.FSug * (s.Quantity / Quantitys);
                    indexCount++;
                    Per100Grams[indexCount] += s.FSodium * (s.Quantity / Quantitys);
                }
                foreach (Ingredient s in Forms_ListBoxes[1].Items)
                {
                    //Energy, Protein, FatTotal, FatSat, Carbs, Sodium, Sugar
                    for (indexCount = 0; indexCount < Per100Grams.Count(); indexCount++)
                    {
                        AverageSize[indexCount] = Per100Grams[indexCount] * ((serving_Size) / 100);//loops through each entry and applys the average calculation.                            
                    }
                }
                this.BackColor = Color.Black;
                this.BackgroundImage = null;
                string[] NutritionSubjects = new string[] { "", "Per 100g", "Per Serving" };
                var TableDisplay = new NutrientsForm(Per100Grams, AverageSize, NutritionSubjects, recipe_Name_String, Quantitys/float.Parse(Forms_TextBoxes[3].Text));
                TableDisplay.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid item selected and/or invalid serving size.");
            }
        }
        #endregion

        #region KeyDown

        public void RecipeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && (Forms_TextBoxes[2].Text != "" || Forms_TextBoxes[2].Text != null))
            {
                //checks for special characters or errors
                recipe_Name_String = Forms_TextBoxes[2].Text;

                if (!HasSpecChars(2, 1))
                {
                    Forms_TextBoxes[2].Hide();
                    AddItems();
                }
                else
                {
                    Forms_TextBoxes[2].Text = "Recipe Name";
                    MessageBox.Show("Only AlphaNumerics and \",.()-_\" are allowed.");

                }
            }
        }
        #endregion

        static void OpenFile()
        {
            //Adds to a list for the Ingredient class, an instance of the ingredient class which stores all the information for each entry, for easy accessability.
            //Open Nutrient file 
            string filename = "Nutrientfile.txt";
            StreamReader reader = File.OpenText(filename);

            int counting = 0;//Skip the very first line, which says the layout
            Console.Write(".... reading file");

            //Load data into an array 
            while (reader.Peek() != -1)
            {
                string[] line = reader.ReadLine().Split('\t');

                if (!(counting == 0))
                {
                    //read in line and split by tab 

                    Recipies.Add(new Ingredient(line[0], line[1], float.Parse(line[2]), float.Parse(line[3]), float.Parse(line[4]), float.Parse(line[5]), float.Parse(line[6]), float.Parse(line[7]), float.Parse(line[8])));
                    counting++;
                }
                else
                {
                    counting++;
                }
            }
            reader.Close();
            Console.WriteLine(".... file read");
            Console.WriteLine();
        }//Reads the file and inputs each ingredient.
    }
}
/*
    <-----Need2Do--->
Make labels  font change and look nicer.
Tidy up the ordering.
*/
