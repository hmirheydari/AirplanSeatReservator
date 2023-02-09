using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AirplanSeatReservator
{

    class ClassUtil
    {
        /// <summary>
        /// Gets a string from the console whose elements are one-dimensional arrays including 
        /// the number of rows and columns of seats in each section. 
        /// </summary>
        /// <returns>
        /// String of array of array of number of columns and rows in each part
        /// </returns>
        public string GetSeatArray()
        {
            string inputSeatArray;
            do
            {
                //Console.WriteLine("Please enter seat arrays(For example [[2,3],[3,4],[4,2]]):");
                //inputSeatArray = Console.ReadLine();
                inputSeatArray = "[[2,3],[3,4],[5,6]]";
            } while (!VerifyCorrectnessOfSeatArray(inputSeatArray));
            return inputSeatArray;

        }

        /// <summary>
        /// Parse input string into an array of type int[] to contain each parts seats rows and columns 
        /// </summary>
        /// <param name="inputArrayString">
        /// input string of array
        /// </param>
        /// <param name="outputDimensionArray">
        /// output array of type int[]
        /// </param>
        /// <returns>
        /// True if conversion succeeds and False if it failes
        /// </returns>
        public Boolean ConvertStringToArray(string inputArrayString, out uint[] outputDimensionArray)
        {
            inputArrayString = Regex.Replace(inputArrayString, @"[\[\]]", "");
            try
            {
                outputDimensionArray = inputArrayString.Split(',').Select(uint.Parse).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\nThe input array is not enetered correctly, please re-enter.");
                outputDimensionArray = new uint[0];
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks the correctness of input string to be sure that it can be converted to an array of int[] correctly
        /// </summary>
        /// <param name="inputArray">
        /// string containing input array
        /// </param>
        /// <returns></returns>
        private Boolean VerifyCorrectnessOfSeatArray(string inputArray)
        {
            try
            {
                Match matchResult = Regex.Match(inputArray, @"^\[(\[\d+[,]\d+\][,])*(\[\d+,\d+\]){1}\]$");
                if (!matchResult.Success)
                {
                    Console.WriteLine("The entered string doesn't match the correct input pattern,\n" +
                        "some correct samples as below:\n[[1,1]]\n[[2,3],[6,67]]\n[[34,34],[24,3],[99,1]]\n" +
                        "please try again.\n"
                        );
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The entered string doesn't match the correct input pattern,\n" +
                    "some correct samples as below:\\n[[1,1]]\n[[2,3],[6,67]]\n[[34,34],[24,3],[99,1]]\n" +
                    "please try again."
                    );
                return false;
            }


            return true;

        }

        /// <summary>
        /// Checks correctness of input number to be an unsigned integer greater than zero
        /// </summary>
        /// <param name="inputNumber">
        /// String containing unsigned integer
        /// </param>
        /// <returns></returns>
        private Boolean VerifyCorrectnessOfTotalNumberOfPassengers(string inputNumber)
        {
            uint tempResult;
            if (!uint.TryParse(inputNumber, out tempResult))
            {
                Console.WriteLine("The entered string is not an unsigned integer number, please enter correctly.");
                return false;
            }
            if (!(tempResult < 100))
            {
                Console.WriteLine("The maximum allowed number of passengers is 99, please enter new one.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Find maximum value of rows and maximum value of columns, it is needed to initiate main seat array
        /// </summary>
        /// <param name="inputArray">
        /// Array of type unsigned integer containg all part's number of rows and columns of seats
        /// </param>
        /// <returns>
        /// Array of type unsigned integer containg maximum value of rows and maximum value of columns
        /// </returns>
        public uint[] FindMaxRowAndSumColumn(uint[] inputArray)
        {
            uint[] output = new uint[2];
            for (int i = 0; i < inputArray.Length / 2; i++)
            {
                if (inputArray[i * 2] > output[0]) output[0] = inputArray[i * 2];
                output[1] += inputArray[(i * 2) + 1];
            }
            return output;
        }

        public void PrintAllSeatsStatus(uint[] dimensionArray, int[,] mainSeatArray)
        {
            for (int i = 0; i < mainSeatArray.GetLength(0); i++)
            {
                for (int j = 0; j < mainSeatArray.GetLength(1); j++)
                {
                    //conditional string format
                    Console.Write(mainSeatArray[i, j].ToString("00;-0;00") + "  ");

                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints main array on console, every empty seat is show with a ... character
        /// and every reserved seat is shown with its eqivalent passenger number
        /// </summary>
        /// <param name="dimensionArray">
        /// Array of type unsigned integer containg all part's number of rows and columns of seats
        /// </param>
        /// <param name="mainSeatArray">
        /// The main array containing status of all part's seats
        /// </param>
        public void PrintAllSeatsStatusDecorated(uint[] dimensionArray, int[,] mainSeatArray)
        {

            for (int i = 0; i < mainSeatArray.GetLength(0); i++)
            {
                for (int j = 0; j < mainSeatArray.GetLength(1); j++)
                {
                    if (mainSeatArray[i, j] > -4 && mainSeatArray[i, j] < 0)
                    {
                        Console.OutputEncoding = Encoding.UTF8;
                        Console.Write("\u25a2   ");
                    }
                    else
                    {
                        if (mainSeatArray[i, j] == 0)
                        {
                            Console.Write("    ");
                        }
                        else
                        {
                            //conditional string format
                            Console.Write(mainSeatArray[i, j].ToString("00;-0;00") + "  ");
                        }
                    }
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// MainSeatsArray is an array containing all airplane seats status
        /// Each element of array contains seat status value equal to one of beolow values:
        /// 
        /// 0  means the seat is not available(no seat at this position)
        /// -1 means the seat is available and is a window seat
        /// -2 means the seat is available and is a middle seat
        /// -3 means the seat is available and is an aisle seat
        /// other numbers means seat is available but reserved for passenger number equal to element value
        /// 
        /// </summary>
        /// <param name="maxRow">
        /// is used for mainArray row
        /// </param>
        /// <param name="maxColumn">
        /// is used for mainArray column
        /// </param>
        /// <param name="dimensionArray">
        /// used to know each 2D subarray dimension
        /// </param>
        /// <returns>
        /// A 2D array which each element represnt status of that seat(this array is a representation of all of airplane seats)
        /// </returns>
        public int[,] InitMainSeatsArray(uint maxRow, uint maxColumn, uint[] dimensionArray)
        {
            int[,] tempMainSeatsArray = new int[maxRow, maxColumn];
            uint columnStartIndex = 0;
            for (int i = 0; i < 3; i++) //(dimensionArray.Length / 2); i++)
            {
                for (int x = 0; x < dimensionArray[i * 2]; x++)
                {
                    for (int y = 0; y < dimensionArray[i * 2 + 1]; y++)
                    {
                        //1- if first array then first column of arrray is window seats=-1 and last column of array is aisl seats=-3
                        //   and remaining are middle seats=-2
                        if (i == 0)
                        {
                            switch (y)
                            {
                                //window seats
                                case 0:
                                    {
                                        tempMainSeatsArray[x, y + columnStartIndex] = -1;
                                        break;
                                    }
                                //aisle seats
                                case int _temp_y when (_temp_y == dimensionArray[i * 2 + 1] - 1):
                                    {
                                        tempMainSeatsArray[x, y + columnStartIndex] = -3;
                                        break;
                                    }
                                //middle seats
                                default:
                                    {
                                        tempMainSeatsArray[x, y + columnStartIndex] = -2;
                                        break;
                                    }
                            }
                        }


                        //2- if last array then last column of arrray is window seats=-1 and first column of array is aisl seats=-3
                        //   and remaining are middle seats=-2
                        if (i == (dimensionArray.Length / 2) - 1)
                        {
                            switch (y)
                            {
                                //if first column of array then set as window seat
                                case 0:
                                    {
                                        tempMainSeatsArray[x, y + columnStartIndex] = -3;
                                        break;
                                    }
                                //if last column of array then set as aisle seat
                                case int _temp_y when (_temp_y == dimensionArray[i * 2 + 1] - 1):
                                    {
                                        tempMainSeatsArray[x, y + columnStartIndex] = -1;
                                        break;
                                    }
                                //middle seats
                                default:
                                    {
                                        tempMainSeatsArray[x, y + columnStartIndex] = -2;
                                        break;
                                    }
                            }
                        }

                        //3- if middle array then first and last column of arrray is aisle seats=-3 
                        //   and remaining are middle seats=-2
                        if (i > 0 && i < (dimensionArray.Length / 2) - 1)
                        {
                            switch (y)
                            {
                                //if first column of array then set as window seat
                                case 0:
                                    {
                                        tempMainSeatsArray[x, y + columnStartIndex] = -3;
                                        break;
                                    }
                                //if last column of array the set as aisle seat
                                case int _temp_y when (_temp_y == dimensionArray[i * 2 + 1] - 1):
                                    {
                                        tempMainSeatsArray[x, y + columnStartIndex] = -3;
                                        break;
                                    }
                                //middle seats
                                default:
                                    {
                                        tempMainSeatsArray[x, y + columnStartIndex] = -2;
                                        break;
                                    }
                            }
                        }

                    }
                }
                columnStartIndex += dimensionArray[(i * 2) + 1];
            }
            return tempMainSeatsArray;
        }
        /// <summary>
        /// It receives as input the total number of passengers to be accommodated in the airplane seats, then assigns each passenger
        /// to a seat according to the established rules.
        /// </summary>
        /// <param name="totalNumberOfPassengers">
        /// The number of total passengers to be accomodated in airplan seats
        /// </param>
        /// <param name="mainSeatArray">
        /// Array holding the status of whole airplane seats
        /// </param>
        /// <returns></returns>
        public Boolean ReserveSeatForTotalNumberOfPassengers(int totalNumberOfPassengers, int[,] mainSeatArray)
        {
            for (int i = 1; i <= totalNumberOfPassengers; i++)
            {
                if (!ReserveSeatForOnePassenger(i, mainSeatArray)) return false;
            }
            return true;
        }
        Boolean ReserveSeatForOnePassenger(int passengerNumber, int[,] mainSeatArray)
        {
            for (int x = 0; x < mainSeatArray.GetLength(0); x++)
            {
                for (int y = 0; y < mainSeatArray.GetLength(1); y++)
                {
                    //check that seat is available
                    if (mainSeatArray[x, y] != 0)
                    {
                        //if reserved already so its value is greater than zero
                        if (mainSeatArray[x, y] > 0)
                            continue;

                        //aisle seat
                        if (mainSeatArray[x, y] == -3)
                        {
                            mainSeatArray[x, y] = passengerNumber;
                            return true;
                        }

                    }
                }
            }

            for (int x = 0; x < mainSeatArray.GetLength(0); x++)
            {
                for (int y = 0; y < mainSeatArray.GetLength(1); y++)
                {
                    //if reserved already so its value is greater than zero
                    if (mainSeatArray[x, y] > 0)
                        continue;                    //window seat
                    if (mainSeatArray[x, y] == -1)
                    {
                        mainSeatArray[x, y] = passengerNumber;
                        return true;
                    }
                }
            }

            for (int x = 0; x < mainSeatArray.GetLength(0); x++)
            {
                for (int y = 0; y < mainSeatArray.GetLength(1); y++)
                {
                    //if reserved already so its value is greater than zero
                    if (mainSeatArray[x, y] > 0)
                        continue;                   //middle seat
                    if (mainSeatArray[x, y] == -2)
                    {
                        mainSeatArray[x, y] = passengerNumber;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// It receives the total number of passengers from the console and checks that the input number is a positive integer greater than zero.
        /// </summary>
        /// <returns>
        /// An integer representing total number of passengers 
        /// </returns>
        public int GetTotalNumberOfPassengers()
        {
            string totalNumberOfPassengers;
            do
            {
                Console.Write("Please enter total number of passengers:");
                totalNumberOfPassengers = Console.ReadLine();
                //inputSeatArray = "[[2,3],[3,4],[5,6]]";
            } while (!VerifyCorrectnessOfTotalNumberOfPassengers(totalNumberOfPassengers));
            return int.Parse(totalNumberOfPassengers);

        }
    }


}
