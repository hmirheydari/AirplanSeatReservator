using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirplanSeatReservator
{
    class Program
    {
        static void Main(string[] args)
        {
            //classutill contains all of needed functions so we are creating an instance of the class
            ClassUtil instanceOfClassUtil = new ClassUtil();
            //Gets a string representing an array of arrays, which each subarray contains number of rows and columns of seats in that part
            string seatArray = instanceOfClassUtil.GetSeatArray();
            //Fets number of total passenger to be fitted into available seats
            int totalNumberofPassengers=instanceOfClassUtil.GetTotalNumberOfPassengers();
            uint[] inputDimensionArray;
            //Convert input string to a C# array
            instanceOfClassUtil.ConvertStringToArray(seatArray,out inputDimensionArray);
            //Find maximum value rows to set for mainArray row
            uint maxRow= instanceOfClassUtil.FindMaxRowAndSumColumn(inputDimensionArray)[0];
            //Find maximum value of columns to set for mainArray column
            uint maxColumn= instanceOfClassUtil.FindMaxRowAndSumColumn(inputDimensionArray)[1];
            int[,] mainArrayOfSeats;
            mainArrayOfSeats=instanceOfClassUtil.InitMainSeatsArray(maxRow, maxColumn, inputDimensionArray);
            Console.WriteLine("Initial state of seat status:\n");
            instanceOfClassUtil.PrintAllSeatsStatusDecorated(inputDimensionArray, mainArrayOfSeats);
            if (!instanceOfClassUtil.ReserveSeatForTotalNumberOfPassengers(totalNumberofPassengers, mainArrayOfSeats))
            {
                Console.WriteLine("There is not enough seats available for " + totalNumberofPassengers + " passengers!");
            }
            else
            {
                Console.WriteLine("Reserved state of seats is as below:");
                instanceOfClassUtil.PrintAllSeatsStatusDecorated(inputDimensionArray, mainArrayOfSeats);
            }
            Console.Write("Press a key to continue...");
            Console.ReadKey();

        }
    }
}
