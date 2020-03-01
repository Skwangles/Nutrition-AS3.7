using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Nutrition_AS3._7
{
    class Ingredient
    {

        public Ingredient(string iD, string name, float energy, float protein, float fat, float satFat, float carb, float sug, float sod)
        {
            FID = iD;
            FName = name;
            FEnergy = energy;
            FProtein = protein;
            FFatTotal = fat;
            FSat = satFat;
            FCarb = carb;
            FSug = sug;
            FSodium = sod;
            Quantity = 0;
        }
        public override string ToString()
        {
            string temp = FName.Replace("\"", "");
            if (Quantity != 0)
            {
                temp = " Quantity: " + Quantity+ "g |    "+ temp ;
            }
            return temp;
        }
        public float Quantity { get; set; }
        public string FName { get; set; }
        public string FID { get; set; }
        public float FEnergy { get; set; }
        public float FProtein { get; set; }
        public float FFatTotal { get; set; }
        public float FSat { get; set; }
        public float FCarb { get; set; }
        public float FSug { get; set; }
        public float FSodium { get; set; }






    }
}
