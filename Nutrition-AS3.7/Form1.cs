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
        readonly static string filepath = @"c:\Users\tree_\Downloads\"; //file path
        readonly static int formwidth = 700;
        readonly int formheight = 900;
        public TextBox[] Forms_TextBoxes = new TextBox[11]; //stores the instances of textboxes
        public Button[] Forms_Buttons = new Button[11];//stores the instances of Buttons
        public Label[] Forms_Labels = new Label[11]; //stores the instances of Labels
        public ListBox[] Forms_ListBoxes = new ListBox[11]; //stores the instances of Listboxes
        bool hasCalled = false; //checks if the eventhandler creator and controls add has occured
        int Default_Spacer = 30; //For spacing
        int SearchResultsWidth = 280; //Sets size of search results box
        int SearchBarLeft = 0; //the left of item whic
        int RecipeContentsheight = 300;
        int QuanitityTBMaxLength = 5;
        int QuantityBoxWidth = 30;
        int ServingSizeWidth = 20;
        string Recipe_Name_String;
        string SpecialChars = "!@#$%^&*~`=+[{]}\\|;:'\"<>/?";
        //above, sets the locations of the forms items.
        static List<Ingredient> Recipies = new List<Ingredient>();
        //array to store nutrient info from file 
        //starts the program 
        public PictureBox Table = new PictureBox();



        #endregion

        #region Collapse for work on Nutrients table


        private void Form1_Load(object sender, EventArgs e)
        {  //TbSearch, tbQuanitiy, TbRecipe, BSearch,BConfirm,BClearRecipe,LQuanity, LRecipeName, LSearchResults, LTitle

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
            Forms_TextBoxes[3] = new TextBox(); //Tb Serving Size
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
            if (!hasCalled)//checks if the items have already been added.
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
                    else
                    {
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
                Forms_TextBoxes[0].KeyDown += new KeyEventHandler(SearchTextBox_KeyDown);//checks for the "enter" key in hte textbox
                hasCalled = true;
                Forms_TextBoxes[0].Focus();//Focuses the cursour on the search bar

                Setlocations();//in seperate method so later locations may be updated.

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

            //Serving size
            Forms_TextBoxes[3].Top = Forms_Buttons[3].Top + Forms_Buttons[3].Height;
            Forms_TextBoxes[3].Left = Forms_ListBoxes[1].Left;
            Forms_TextBoxes[3].MaxLength = 3;
            Forms_TextBoxes[3].Width = ServingSizeWidth;
        }

        void Search_Click(object sender, EventArgs e)
        {

            Console.WriteLine("SearchClicked"); //notifies correct call
            Forms_ListBoxes[0].Items.Clear(); //makes sure no prior searches remain
                                              //makes sure the entries are put in alphabetical order
            if (!(Forms_TextBoxes[0].Text == "" || Forms_TextBoxes[0].Text == null)) //checks if box is empty
            {
                foreach (char f in SpecialChars)//checks if there are any disallowed chars in the string
                {
                    if (!Forms_TextBoxes[0].Text.Contains(f))
                    {
                        foreach (Ingredient a in Recipies) //searches each item
                        {
                            if (a.FName.ToLower().Contains(Forms_TextBoxes[0].Text.ToLower()))//checks if search query exists, and if it is present
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
                    ((Ingredient)Forms_ListBoxes[0].SelectedItem).Quantity = float.Parse(Forms_TextBoxes[1].Text);//adds the quanitity added
                    Forms_ListBoxes[1].Items.Add((Ingredient)Forms_ListBoxes[0].SelectedItem);//adds to the recipe list.
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
            Forms_ListBoxes[1].Items.Clear();//clears listbox
        }
        void Complete_Click(object sender, EventArgs e)
        {
            if (Forms_ListBoxes[1].SelectedItem != null && Forms_ListBoxes[1].Items.Count > 0 && Forms_TextBoxes[3].Text != "" || Forms_TextBoxes[3].Text != null)
            {

                try
                {




                    float Quantitys = 0;
                    float servingSize = float.Parse(Forms_TextBoxes[3].Text);
                    float servings = Quantitys / servingSize;
                    var Per100Grams = new float[9];
                    var AverageSize = new float[9];
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
                    foreach (Ingredient s in Forms_ListBoxes[1].Items)
                    {
                        Quantitys += s.Quantity;
                    }

                    Console.WriteLine("CompleteClicked");
                    int b;
                    foreach (Ingredient s in Forms_ListBoxes[1].Items)
                    {
                        b = 0;
                        //Energy, Protein, FatTotal, FatSat, Carbs, Sodium, Sugar
                        Per100Grams[b] += s.FEnergy * (s.Quantity / Quantitys);
                        b++;
                        Per100Grams[b] += s.FProtein * (s.Quantity / Quantitys);
                        b++;
                        Per100Grams[b] += s.FFatTotal * (s.Quantity / Quantitys);
                        b++;
                        Per100Grams[b] += s.FSat * (s.Quantity / Quantitys);
                        b++;
                        Per100Grams[b] += s.FCarb * (s.Quantity / Quantitys);
                        b++;
                        Per100Grams[b] += s.FSodium * (s.Quantity / Quantitys);
                        b++;
                        Per100Grams[b] += s.FSug * (s.Quantity / Quantitys);
                    }

                    foreach (Ingredient s in Forms_ListBoxes[1].Items)
                    {
                        //Energy, Protein, FatTotal, FatSat, Carbs, Sodium, Sugar
                        for (b = 0; b < Per100Grams.Count(); b++)
                        {
                            AverageSize[b] += Per100Grams[b] * (servingSize / 100);//loops through each entry and applys the average calculation.                            
                        }
                    }



                    DrawNutrientsTable(Per100Grams, AverageSize);




                }
                catch
                {
                    MessageBox.Show("Serving size must be a number.");

                }
            }
            else
            {
                MessageBox.Show("No item selected, or no serving size.");
            }

        }

        #endregion
        public void DrawNutrientsTable(float[] Per100g, float[] avg)
        {
            try
            {
                Controls.Add(Table);
                Table.BringToFront();
                Table.Location = new Point(0, 0);
                Table.Height = formheight;
                Table.Width = formwidth;
                Graphics g = Table.CreateGraphics();

                string[] NutritionSubjects = new string[] { "Energy", "Protein", "Fat, Total", " -Saturated Fat", "Carbohydrates", "Sugars", "Sodium" };
                g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(Table.Left, Table.Top, formwidth, formheight));
                g.DrawString("Entry       Per100g       Average Per Serving", new Font("Arial", 16), new SolidBrush(Color.White), 0, 0);
                for (int i = 1; i < 7; i++)
                {
                    g.DrawString(NutritionSubjects[i-1] + Per100g[i-1], new Font("Arial", 16), new SolidBrush(Color.White), 0, i * 20);
                }
                g.Dispose();
            }
            catch
            {
                throw new Exception();
            }
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

Tidy up the ordering.

*/
