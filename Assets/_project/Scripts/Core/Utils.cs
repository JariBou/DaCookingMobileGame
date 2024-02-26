using System.Collections.Generic;
using System.Linq;

namespace _project.Scripts.Core
{
    public static class Utils
    {

        public static bool ListHasAllElements<T>(List<T> listToTest, List<T> listToTestFor)
        {
            foreach (T el in listToTest.Where(listToTestFor.Contains))
            {
                listToTestFor.Remove(el);
            }

            return !listToTestFor.Any();
        }
        
    }
}