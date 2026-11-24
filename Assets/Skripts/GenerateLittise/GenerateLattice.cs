using System;
using System.Collections.Generic;
using System.Linq;

public static class GenerateLattise 
{
    static long LinqFactorial(int n) =>
        n == 0 ? 1 : Enumerable.Range(1, n).Aggregate(1L, (acc, x) => acc * x);
    private static short[][] CreateLatticeMatrix(int n) {
        if (n < 1)
            throw new ArgumentException("Число элементов должно быть не меньше 1.", nameof(n));

        // Частный случай: решётка из 1 элемента
        if (n == 1)
            return new short[1][] { new short[1] { 1 } };

        short[][] resultMatrix = new short[n][];
        for (int i = 0; i < n; i++) 
            resultMatrix[i] = new short[n];
        
        for (int i = 0; i < n; i++) {
            resultMatrix[i][i] = 1;
            for (int j = i+1; j < n; j++) {
                short ranValue = (short)(LinqFactorial(j-i)*2);
                resultMatrix[i][j] = ranValue;
                resultMatrix[j][i] = (short)-ranValue;
            }
        }

        return resultMatrix;
    }
    private static List<string> CreateNameLattice(ushort order) {
        List<string> names = new ();
        for (int i = 0; i < order; i++) 
            names.Add($"{i}");
        return names;
        
    }
    public static (short[][], List<string>) GetRandomLattice(ushort order) {
        return (
            CreateLatticeMatrix(order),
            //CreateRandomLatticeMatrix(order),
            CreateNameLattice(order)
            );
    }

}
