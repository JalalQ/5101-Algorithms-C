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
    public class J3Controller : ApiController
    {

        /// <summary>
        /// Problem J3: Cell-Phone Messaging from, "2006 Canadian Computing Competition: Junior Division".
        /// The objective of this program is to determine the time duration to type a message (string) on
        /// a mobile with keypad. Different letters requires different number of key presses in the range of
        /// [1,4], and two consecutive letters on the same key also requires a pause.
        /// </summary>
        /// <param name="message">A string message for which we want to determine the min time consumed.</param>
        /// <returns>The minimum time duration to type that message on the mobile keypad.</returns>
        [Route("api/J3/Messaging/{message}")]
        [HttpGet]

        public string Messaging(string message)
        {
            const int maxKeyPress = 4; // the maximum number of key to be pressed.
            const int keysWithLetters = 8; //the number of mobile keys with alphabet character.

            //elements (letters) within the same columns are on the same key. '0' is a junk character.
            //elements (letters) on the same row requires same number of key press(es).
            char[,] keyLetters =
            {   {'a', 'd', 'g', 'j', 'm', 'p', 't', 'w'}, //letters which can be typed with a single key press.
                {'b', 'e', 'h', 'k', 'n', 'q', 'u', 'x'}, //letters which can be typed with two key press.
                {'c', 'f', 'i', 'l', 'o', 'r', 'v', 'y'}, //letters which can be typed with three key press.
                {'0', '0', '0', '0', '0', 's', '0', 'z'}  //letters which can be typed with four key press. '0' is a junk character.
            };

            char[] charArr = message.ToCharArray(); //the string is first converted into a charcters and stored in an array.

            //the total time is given by key presses + number of pauses. 
            //First we calculate the number of key presses.

            int totalKeyPress = 0;
            bool found = false;
            int row = 0;

            //for each of the character in the string, determine the number of times the key has to be pressed.
            for (int i=0; i<charArr.Length; i++)
            {
                //linear search process. initialization.
                found = false;
                row = 0;
                do
                {
                    for (int column = 0; column < keysWithLetters; column++)
                    {
                        if (keyLetters[row,column] == charArr[i])
                        {
                            totalKeyPress += (row + 1); //the variable "row+1" indicates the number of key presses.
                            found = true;
                            break;
                        }
                    }

                    row++;

                } while (found == false);

            }

            //now determine the number of "pauses". A pause is used if two consecutive letters are on the same key.
            for (int i = 1; i < charArr.Length; i++)
            {
                //first determine the key (i.e. the column of the 2d array) on which the first of the two consecutive letters is.
                //linear search process. initialization.
                found = false;
                row = 0;

                do
                {
                    for (int column = 0; column < keysWithLetters; column++)
                    {
                        //the key of the first letter is found.
                        if (keyLetters[row, column] == charArr[i-1])
                        {
                            found = true;
                            for (int rowSecondLetter=0; rowSecondLetter<maxKeyPress; rowSecondLetter++)
                            {
                                //evaluate whether the seond consecutive letter is also on the same key (i.e. on the same column of the 2d array).
                                if (keyLetters[rowSecondLetter, column] == charArr[i])
                                {
                                    totalKeyPress += 2; //each pause is 2 seconds long.
                                    break;
                                }
                            }

                            break;
                        }
                    }

                    row++;

                } while (found == false);

            }


            return "Total time (key presses+pauses) are: " + totalKeyPress + ".";

        }
    }
}
