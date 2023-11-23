using System.Reflection;

namespace REST.Core.Utils
{
    public class GenericCollectionOperations<T>
    {
        public static int GetMaxId(List<object> objects)
        {
            var sameTypeObjects = objects.OfType<T>().ToList();

            List<int> existingIds = new List<int>();

            foreach (var obj in sameTypeObjects)
            {
                Type type = obj.GetType();
                PropertyInfo[] props = type.GetProperties();

                foreach (var prop in props)
                {
                    if (prop.GetIndexParameters().Length == 0)
                    {
                        if (prop.Name.Equals("Id"))
                        {
                            existingIds.Add((int)(prop.GetValue(obj) ?? 0));
                        }
                    }
                }
            }

            return existingIds.Any() ? existingIds.Max() : 0;
        }
    }
}
