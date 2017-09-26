namespace Minesweeper
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public CellState State { get; set; }
        public bool IsMine { get; set; }
    }
}