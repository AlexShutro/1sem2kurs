    Set[] sets = new Set[5];

    sets[0] = new Set();
    sets[0].Add(1);
    sets[0].Add(2);
    sets[0].Add(3);
    sets[0].Add(4);

    sets[1] = new Set();
    sets[1].Add(2);
    sets[1].Add(4);
    sets[1].Add(6);
    sets[1].Add(8);

    sets[2] = new Set();
    sets[2].Add(-1);
    sets[2].Add(-2);
    sets[2].Add(3);
    sets[2].Add(4);

    sets[3] = new Set();
    sets[3].Add(1);
    sets[3].Add(3);
    sets[3].Add(5);
    sets[3].Add(7);

    sets[4] = new Set();
    sets[4].Add(-1);
    sets[4].Add(-2);
    sets[4].Add(-3);
    sets[4].Add(-4);
    
    

// Множества только с четными элементами
    Console.WriteLine("Множества только с четными элементами:");
    foreach (Set set in sets)
    {
        if (set.IsEven())
        {
            Console.WriteLine(set);
        }
    }

// Множества только с нечетными элементами
    Console.WriteLine("Множества только с нечетными элементами:");
    foreach (Set set in sets)
    {
        if (set.IsOdd())
        {
            Console.WriteLine(set);
        }
    }

// Множества, содержащие отрицательные элементы
    Console.WriteLine("Множества, содержащие отрицательные элементы:");
    foreach (Set set in sets)
    {
        if (set.HasNegative())
        {
            Console.WriteLine(set);
        }
    }

    public class Set
    {
        private List<int> elements;

        public Set()
        {
            elements = new List<int>();
        }

        public void Add(int element)
        {
            if (!elements.Contains(element))
            {
                elements.Add(element);
            }
        }

        public void Remove(int element)
        {
            elements.Remove(element);
        }

        public Set Intersection(Set otherSet)
        {
            Set result = new Set();

            foreach (int element in elements)
            {
                if (otherSet.Contains(element))
                {
                    result.Add(element);
                }
            }

            return result;
        }

        public Set Difference(Set otherSet)
        {
            Set result = new Set();

            foreach (int element in elements)
            {
                if (!otherSet.Contains(element))
                {
                    result.Add(element);
                }
            }

            return result;
        }

        public bool Contains(int element)
        {
            return elements.Contains(element);
        }

        public bool IsEmpty()
        {
            return elements.Count == 0;
        }

        public bool IsEven()
        {
            foreach (int element in elements)
            {
                if (element % 2 != 0)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsOdd()
        {
            foreach (int element in elements)
            {
                if (element % 2 == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public bool HasNegative()
        {
            foreach (int element in elements)
            {
                if (element < 0)
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return "{" + string.Join(", ", elements) + "}";
        }
    }