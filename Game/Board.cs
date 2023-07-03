using CoordinateNS;
using Microsoft.VisualBasic;
using System;

namespace BoardNS
{

    public class Board
    {
        private const int MAX_HEIGHT = 5;
        private const int MAX_WIDTH = 5;

        private List<Coordinate> items = new List<Coordinate>();

        public Board() {}

        public List<Coordinate> Items { get => items; set => items = value; }
    }
}
