using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    // Declaration of variables
    private List<int> _numbers;
    private Dictionary<int, List<int>> _dividableDict;

    void Start()
    {
        // Calling function
        FindTheDividableNumbers(10, 30);
    }

    // Declaration of function
    void FindTheDividableNumbers(int start, int end)
    {
        _dividableDict = new Dictionary<int, List<int>>();  // creating empty dictionary
        _numbers = Enumerable.Range(start, end).ToList();   // generating numbers between start and end
        
        // Finding divider numbers per number in generated list
        foreach (int number in _numbers)
        {
            FindDividers(number);
        }
        
        // Calling PrintLists function
        PrintLists();
    }

    // Find divider numbers
    private void FindDividers(int number)
    {
        for (int i = 2; i < number; i++)
        {
            if (number % i == 0)
            {
                if (!_dividableDict.ContainsKey(i))
                    _dividableDict.Add(i, new List<int> { number });
                else
                    _dividableDict[i].Add(number);
            }
        }
    }

    // Printing to the console
    private void PrintLists()
    {
        StringBuilder sb = new StringBuilder();

        // Writing all numbers to console
        sb.Append("All Numbers ==> ");
        foreach (var number in _numbers)
        {
            sb.Append($"{number}, ");
        }

        print(sb.ToString());
        sb.Clear();

        // Writing dividable numbers to console
        foreach (var key in _dividableDict.Keys)
        {
            sb.Append($"Dividable by {key} ==> ");

            foreach (var number in _dividableDict[key])
            {
                sb.Append($"{number}, ");
            }

            print(sb.ToString());
            sb.Clear();
        }
    }
}
