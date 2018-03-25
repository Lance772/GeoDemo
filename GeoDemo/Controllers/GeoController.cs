using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GeoDemo.Controllers
{
    public class GeoController : ApiController
    {
        static readonly int iGridPixels = 10;
        static readonly char[] Columns = new[] { 'A', 'B', 'C', 'D', 'E', 'F' };

        public IEnumerable<string> Get(char cRow, int iColumn)
        {
            //Check paramters
            if (!Columns.Contains(cRow) || iColumn < 1 || iColumn > 12){
                return new string[] { "Invalid input parameter." };
            }

            //Variables
            int iRow = char.ToUpper(cRow) - 65;
            int x1, y1, x2, y2, x3, y3;

            //Get the coordinates and return
            if (iColumn % 2 == 0)
            {                
                x3 = iGridPixels;
                y3 = 0;
            }
            else
            {
                iColumn += 1;
                x3 = 0;
                y3 = iGridPixels;
            }

            iColumn = (iColumn / 2) - 1;
            x1 = iColumn * iGridPixels;
            y1 = iRow * iGridPixels;
            x2 = x1 + iGridPixels;
            y2 = y1 + iGridPixels;
            x3 = x1 + x3;
            y3 = y1 + y3;

            return new string[] { x1 + ", " + y1, x2 + ", " + y2, x3 + ", " + y3 };
        }

        public string Get(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            //Check paramters
            if (x1 < 0 || y1 > 60 || x2 < 0 || y2 > 60 || x3 < 0 || y3 > 60) {
                return "Invalid input parameter."; }

            //Variables
            int iRow, iColumn;
            char cRow;

            //Get the row/column and return
            int iMinY = Math.Min(y1, Math.Max(y2, y3));
            int iMinX = Math.Min(x1, Math.Max(x2, x3)) + iGridPixels;

            if (iMinY != 0) { iRow = iMinY / 10; } else { iRow = 0; }
            cRow = Columns[iRow];

            if ((iMinY == y1 && iMinY == y2) || (iMinY == y1 && iMinY == y3))
            {
                iColumn = iMinX / 10;
                iColumn *= 2;                
            }
            else {
                iColumn = iMinX / 10;
                iColumn *= 2;
                iColumn -= 1;
            }

            return cRow + iColumn.ToString();
        }
    }
}
