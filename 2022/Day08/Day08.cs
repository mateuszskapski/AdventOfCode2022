public class Day08 : Problem<List<int>>
{
    public Day08(int year, int day, string fileName) : base(year, day, fileName)
    {
    }

    protected override object PartOne(List<List<int>> input)
    {
        var visibleTrees = (input.Count * 2) + ((input.First().Count - 2) * 2);
        for (var row = 1; row < input.Count - 1; row++)
        {
            for (var col = 1; col < input[row].Count - 1; col++)
            {   
                if (IsVisible(input, row, col))
                    visibleTrees++;
            }
        }

        return visibleTrees;
    }

    protected override object PartTwo(List<List<int>> input)
    {   
        var distances = new List<int>();

        for (var row = 1; row < input.Count - 1; row++)
        {   
            for (var col = 1; col < input[row].Count - 1; col++)
            {   
                var leftVisibilityDistance = 0;
                var rightVisibilityDistance = 0;
                var topVisibilityDistance = 0;
                var bottomVisibilityDistance = 0;
                
                var nextLeft = col - 1;
                var nextRight = col + 1;
                var nextTop = row - 1;
                var nextBottom = row + 1;
                IsLeftVisible(input, row, col, row, nextLeft, ref leftVisibilityDistance);
                IsRightVisible(input, row, col, row, nextRight, ref rightVisibilityDistance);
                IsTopVisible(input, row, col, nextTop, col, ref topVisibilityDistance);
                IsBottomVisible(input, row, col, nextBottom, col, ref bottomVisibilityDistance);

                var score = new int[] {leftVisibilityDistance, rightVisibilityDistance, topVisibilityDistance, bottomVisibilityDistance}.Aggregate(1, (result, next) => result * next);

                distances.Add(score);
            }            
        }

        return distances.Max();
    }

    bool IsVisible(List<List<int>> input, int treeRow, int treeCol)
    {
            var visibilityDistance = 0;
            var nextLeft = treeCol - 1;
            var nextRight = treeCol + 1;
            var nextTop = treeRow - 1;
            var nextBottom = treeRow + 1;
            
            if (IsLeftVisible(input, treeRow, treeCol, treeRow, nextLeft, ref visibilityDistance))
                return true;

            if (IsRightVisible(input, treeRow, treeCol, treeRow, nextRight, ref visibilityDistance))
                return true;

            if (IsTopVisible(input, treeRow, treeCol, nextTop, treeCol, ref visibilityDistance))
                return true;

            if (IsBottomVisible(input, treeRow, treeCol, nextBottom, treeCol, ref visibilityDistance))
                return true;

            return false;
    }
    
    bool IsLeftVisible(List<List<int>> input, int treeRow, int treeCol, int nextTreeRow, int nextTreeCol, ref int visibilityDistance)
    {
        if (nextTreeCol < 0)
            return true;
        
        visibilityDistance++;
        return input[treeRow][treeCol] > input[nextTreeRow][nextTreeCol] ? 
            IsLeftVisible(input, treeRow, treeCol, nextTreeRow, --nextTreeCol, ref visibilityDistance) :
            false;
    }

    bool IsRightVisible(List<List<int>> input, int treeRow, int treeCol, int nextTreeRow, int nextTreeCol, ref int visibilityDistance)
    {
        if (nextTreeCol == input[treeRow].Count)
            return true;

        visibilityDistance++;
        return input[treeRow][treeCol] > input[nextTreeRow][nextTreeCol] ? 
            IsRightVisible(input, treeRow, treeCol, nextTreeRow, ++nextTreeCol, ref visibilityDistance) :
            false;
    }

    bool IsTopVisible(List<List<int>> input, int treeRow, int treeCol, int nextTreeRow, int nextTreeCol, ref int visibilityDistance)
    {
        if (nextTreeRow < 0)
            return true;

        visibilityDistance++;
        return input[treeRow][treeCol] > input[nextTreeRow][nextTreeCol] ? 
            IsTopVisible(input, treeRow, treeCol, --nextTreeRow, nextTreeCol, ref visibilityDistance) :
            false;
    }

    bool IsBottomVisible(List<List<int>> input, int treeRow, int treeCol, int nextTreeRow, int nextTreeCol, ref int visibilityDistance)
    {
        if (nextTreeRow == input.Count)
            return true;

        visibilityDistance++;
        return input[treeRow][treeCol] > input[nextTreeRow][nextTreeCol] ?
            IsBottomVisible(input, treeRow, treeCol, ++nextTreeRow, nextTreeCol, ref visibilityDistance) : 
            false;
    }

    protected override List<int>? ProcessLine(string line) => line.Select(x => x - '0').ToList();
}
