using System;
using System.Collections.Generic;

namespace GITS.Hrms.Library.Utility
{
    public class SortingUtility<T>
    {
        public SortingUtility()
        {
        }

        public static void Sort(IList<T> list, String property)
        {
            Sort(list, property, Comparator<T>.SortOrder.Ascending);
        }

        public static void Sort(IList<T> list, String property, Comparator<T>.SortOrder sortOrder)
        {
            System.Reflection.PropertyInfo propertyInfo = typeof(T).GetProperty(property);
            Comparator<T> cmp = new Comparator<T>(sortOrder);
            cmp.PropertyInfo = propertyInfo;
            ((List<T>)list).Sort(cmp);
        }
    }

    public class Comparator<T> : IComparer<T>
    {
        public SortOrder Order;
        public System.Reflection.PropertyInfo PropertyInfo;

        public enum SortOrder
        {
            Ascending = 1,
            Desending = 2
        }

        public Comparator()
        {
            Order = SortOrder.Ascending;
        }

        public Comparator(SortOrder so)
        {
            Order = so;
        }

        private int Compare(double x, double y)
        {
            if (Order == SortOrder.Ascending)
            {
                if (x.CompareTo(y) > 0)
                {
                    return 1;
                }
                else if (x == y)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (x.CompareTo(y) > 0)
                {
                    return -1;
                }
                else if (x == y)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public int Compare(T x, T y)
        {
            object obj1 = PropertyInfo.GetValue(x, null);
            object obj2 = PropertyInfo.GetValue(y, null);

            if (obj1 == null || obj2 == null)
            {
                return 1;
            }

            if (PropertyInfo.PropertyType == typeof(int) || PropertyInfo.PropertyType == typeof(long) || PropertyInfo.PropertyType == typeof(double))
            {
                return Compare(Convert.ToDouble(obj1), Convert.ToDouble(obj2));
            }
            else if (PropertyInfo.PropertyType == typeof(DateTime))
            {
                if (Order == SortOrder.Ascending)
                {
                    if ((Convert.ToDateTime(obj1).CompareTo(Convert.ToDateTime(obj2))) > 0)
                    {
                        return 1;
                    }
                    else if (Convert.ToDateTime(obj1).Date == Convert.ToDateTime(obj2).Date)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if ((Convert.ToDateTime(obj1).CompareTo(Convert.ToDateTime(obj2))) > 0)
                    {
                        return -1;
                    }
                    else if (Convert.ToDateTime(obj1).Date == Convert.ToDateTime(obj2).Date)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            else
            {
                if (Order == SortOrder.Ascending)
                {
                    if (obj1.ToString().CompareTo(obj2.ToString()) > 0)
                    {
                        return 1;
                    }
                    else if (obj1 == obj2)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (obj1.ToString().CompareTo(obj2.ToString()) > 0)
                    {
                        return -1;
                    }
                    else if (obj1 == obj2)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
        }
    }
}
