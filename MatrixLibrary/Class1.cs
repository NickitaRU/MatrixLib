namespace MatrixLibrary
{
    public class Matrix
    {
        double[,] matrix { get; }
        public Matrix(double[,] matrix)
        {
            if (matrix.GetUpperBound(0) + 1 == matrix.GetUpperBound(1)) this.matrix = matrix; else throw new Exception("matrix format not valid");
        }
        public double?[] Solve()
        {
            double?[] roots = new double?[matrix.GetUpperBound(0) + 1];
            double[,] determinateMatrix = new double[matrix.GetUpperBound(0) + 1, matrix.GetUpperBound(1)];
            for (int j = 0; j < matrix.GetUpperBound(0) + 1; j++)
            {
                for (int k = 0; k < matrix.GetUpperBound(1); k++)
                {
                    determinateMatrix[j, k] = matrix[j, k];
                }
            }
            double determinate = new Determinant(determinateMatrix).Calculate();
            Console.WriteLine(determinate);
            if (determinate == 0) return roots;
            double[,]? rootDeterminateMatrix = null;
            for (int i = 0; i < roots.Length; i++)
            {
                rootDeterminateMatrix = new double[matrix.GetUpperBound(0) + 1, matrix.GetUpperBound(1)];
                for (int j = 0; j < matrix.GetUpperBound(0) + 1; j++)
                {
                    for (int k = 0; k < matrix.GetUpperBound(0) + 1; k++)
                    {
                        if (k != i)
                        {
                            rootDeterminateMatrix[j, k] = matrix[j, k];
                        }
                        else
                        {
                            rootDeterminateMatrix[j, k] = matrix[j, matrix.GetUpperBound(0) + 1];
                        }
                    }
                }

                double rootDeterminate = new Determinant(rootDeterminateMatrix).Calculate();
                if (rootDeterminate == 0) roots[i] = null; else roots[i] = rootDeterminate / determinate;
            }
            return roots;
        }
    }

    public class Determinant
    {
        public double[,] matrix { get; }
        public Determinant(double[,] matrix)
        {
            this.matrix = matrix;
        }
        public double Calculate()
        {
            if (matrix.GetUpperBound(0) + 1 == 2)
            {
                double determinant2 = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
                return determinant2;
            }
            double determinant = 0;
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                double[,] lessGradeMatrix = new double[matrix.GetUpperBound(0), matrix.GetUpperBound(1)];
                for (int j = 0; j < matrix.GetUpperBound(0); j++)
                {
                    for (int k = 0; k < matrix.GetUpperBound(0) + 1; k++)
                    {
                        if (k != i)
                        {
                            lessGradeMatrix[j, k > i ? k - 1 : k] = matrix[j + 1, k];
                        }
                    }
                }
                double curDet = (i % 2 == 1 ? -1 : 1) * matrix[0, i] * new Determinant(lessGradeMatrix).Calculate();
                determinant += curDet;
            }
            return determinant;
        }
    }
}
