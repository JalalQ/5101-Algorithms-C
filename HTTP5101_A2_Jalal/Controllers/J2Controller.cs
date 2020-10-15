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
    public class J2Controller : ApiController
    {
        /// <summary>
        /// Determines the number of possibilities (unordered) the sum of two dices will be equal to 10.
        /// </summary>
        /// <param name="dice1">the number of sides of the first dice</param>
        /// <param name="dice2">the number of sides of the second dice</param>
        /// <returns>number of possibilities so that the sum of the two dices is equal to 10.</returns>
        [Route("api/J2/DiceGame/{dice1}/{dice2}")]
        [HttpGet]

        public string Menu(int dice1, int dice2)
        {
            int totalDice = dice1 + dice2; //the sum of the max values of the two dices.
            int totalWays = 0; //if totalDice<=9, then this value of 0 is returned.

            //arbitrary initialization, will be validated later
            int higherValue = dice1; 
            int lowerValue = dice2;

            //This if condition is used, so that computation are only performed when totalWays>0.
            if (totalDice>9)
            {
                //first determine which of the two dices has a higher value, this is needed for calculation later.
                if (lowerValue>higherValue)
                {
                    lowerValue = dice1;
                    higherValue = dice2;
                }

                // Need to determine the number of combination of x1 and x2 pairs so that the equality x1+x2 = 10 is satisfied (unordered), 
                // given that the lower bound is xi>=1, so the upper bound will be xi <= 9 under the constraint of the equation.
                // Hence we only need to consider cases where the values on both of the two dice is less than or equal to 9.
                if (lowerValue>9)
                {
                    lowerValue = 9;
                }
                if (higherValue>9)
                {
                    higherValue = 9;
                }

                //As the feasible pairs of x1, x2 follow a natural pattern, e.g. (4,6), (5,5), (6,4), (7,3), (8,2), (9,1)
                //The total number of ways can be given by finding the firstValue of the dice with smaller number of sides, as shown later.
                //For the above example in this comment, the firstValue is 4 from the pair (4,6).
                int firstValue = 10 - higherValue;

                //The total ways is given by the range [firstValue, lowerValue].
                totalWays = lowerValue - firstValue + 1;

            }

            return "There are " + totalWays + " to get the sum 10.";

        }
    }
}
