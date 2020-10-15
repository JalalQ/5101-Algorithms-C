using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// author: Jalaluddin Qureshi, HTTP5101 (Spring 2020, Humber College).
// Assignment 2

namespace HTTP5101_A2_Jalal.Controllers
{
    public class J1Controller : ApiController
    {
        /// <summary>
        /// It is assumed that the user will provide a valid input in the range 
        /// of 1-4. No user input validation is performed. 
        /// In this API, the user provides the food id of each of the food items.
        /// Based on the user provided input, the calorie values are fetched from the
        /// table, and the user is provided with the total calories of the order.
        /// </summary>
        /// <param name="burgerId">The id of the burger order, range [1,4].</param>
        /// <param name="drinkId">The id of the drink order, range [1,4]</param>
        /// <param name="sideId">The id of the side item order, range [1,4]</param>
        /// <param name="dessertId">The id of the dessert order, range [1,4]</param>
        /// <returns>The total calories of the order</returns>
        /// 
        [Route("api/J1/Menu/{burgerId}/{drinkId}/{sideId}/{dessertId}")]
        [HttpGet]
        public string Menu(int burgerId, int drinkId, int sideId, int dessertId)
        {
            int totalCalories = 0; //variable intialization
            const int foodItems = 4; //the number of food types on the menu.

            //Table storing the calories for each of the 4*4=16 cases.
            int[,] calories =
            {   {461, 431, 420, 0}, //burgers
                {130, 160, 118, 0}, //drink
                {100,  57,  70, 0}, //side order
                {167, 266,  75, 0}  //dessert
            };

            //the id of food selected for each of the four items
            int[] foodSelectionId = {burgerId, drinkId, sideId, dessertId };

            //By appropriately using the index of the calories array, the totalCalories is added up.
            //One is subtracted from foodSelection variable, as the index of an array starts with 0.
            for (int row = 0; row< foodItems; row++)
            {
                totalCalories += calories[row, foodSelectionId[row]-1];
            }
              
            return "Your total calorie count is " + totalCalories;
        }
    }
}
