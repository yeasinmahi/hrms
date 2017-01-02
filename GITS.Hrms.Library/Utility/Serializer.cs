using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Xml;

namespace GITS.Hrms.Library.Utility
{
    public class Serializer
    {
        public Serializer()
        {
           
        }

        public static string Serialize(Object entity)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(Serialize(doc, entity, true));
            return doc.OuterXml;
        }

        public static XmlElement Serialize(XmlDocument xmlDoc, Object obj, Boolean excludeNonTable)
        {
            Type type = obj.GetType();

            PropertyInfo[] properties = type.GetProperties();

            XmlElement node = xmlDoc.CreateElement(type.FullName);

            foreach (PropertyInfo pi in properties)
            {
                Boolean update = true;

                if (excludeNonTable)
                {
                    Attribute[] attributes = Attribute.GetCustomAttributes(pi);

                    foreach (Attribute att in attributes)
                    {
                        if (att.GetType() == typeof(PropertyAttribute))
                        {
                            PropertyAttribute attribute = (PropertyAttribute)att;

                            switch (attribute.Attribute)
                            {
                                case PropertyAttribute.Attributes.NonTable:
                                    update = false;
                                    break;
                            }
                        }
                    }
                }

                if (update == false || pi.PropertyType == typeof(Byte[]))
                {
                    continue;
                }

                if (pi.PropertyType == typeof(IList) || pi.PropertyType.FullName.StartsWith("System.Collections.Generic.IList"))
                {
                    XmlElement list = xmlDoc.CreateElement(pi.Name);
                    node.AppendChild(list);

                    Object children = pi.GetValue(obj, new Object[] { });

                    if (children != null)
                    {
                        foreach (Object child in ((IEnumerable)children))
                        {
                            list.AppendChild(Serialize(xmlDoc, child, excludeNonTable));
                        }
                    }
                }
                else if (pi.PropertyType.IsValueType == false && pi.PropertyType != typeof(String))
                {
                    Object value = pi.GetValue(obj, new Object[] { });

                    if (value != null)
                    {
                        XmlElement item = xmlDoc.CreateElement(pi.PropertyType.FullName);
                        XmlAttribute attr = xmlDoc.CreateAttribute("Name");
                        item.Attributes.Append(attr);
                        attr.Value = pi.Name;
                        node.AppendChild(item);
                        item.AppendChild(Serialize(xmlDoc, value, excludeNonTable));
                    }
                }
                else
                {
                    XmlAttribute attr = xmlDoc.CreateAttribute(pi.Name);
                    node.Attributes.Append(attr);
                    Object value = pi.GetValue(obj, new Object[] { });

                    if (value != null)
                    {
                        if (pi.PropertyType == typeof(DateTime) || pi.PropertyType == typeof(TimeSpan))
                        {
                            attr.Value = Convert.ToDateTime(value).ToString(CultureInfo.InvariantCulture.DateTimeFormat);
                        }
                        else if (pi.PropertyType.BaseType == typeof(Enum))
                        {
                            attr.Value = Convert.ToInt32(value).ToString();
                        }
                        else
                        {
                            attr.Value = value.ToString();
                        }
                    }
                }
            }

            return node;
        }

        public static Object DeSerialize(XmlDocument xmlDoc, XmlNode node)
        {
            Type type = Type.GetType(node.Name);

            Object obj = Activator.CreateInstance(type);

            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo pi in properties)
            {
                if (pi.GetSetMethod() == null)
                {
                    continue;
                }

                if (pi.PropertyType == typeof(IList) || pi.PropertyType.FullName.StartsWith("System.Collections.Generic.IList"))
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == pi.Name)
                        {
                            if (child.ChildNodes.Count > 0)
                            {
                                IEnumerable list = null;
                                Type listType = null;

                                if (pi.PropertyType == typeof(IList))
                                {
                                    listType = typeof(ArrayList);
                                }
                                else
                                {
                                    listType = Type.GetType(pi.PropertyType.FullName.Replace("System.Collections.Generic.IList", "System.Collections.Generic.List"));
                                }

                                list = (IEnumerable)Activator.CreateInstance(listType);
                                pi.SetValue(obj, list, new Object[] { });

                                foreach (XmlNode item in child)
                                {
                                    listType.InvokeMember("Add", BindingFlags.InvokeMethod, null, list, new Object[] { DeSerialize(xmlDoc, item) });
                                }
                            }

                            break;
                        }
                    }
                }
                else if (pi.PropertyType.IsValueType == false && pi.PropertyType != typeof(String))
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Attributes["Name"].Value == pi.Name)
                        {
                            if (child.ChildNodes.Count > 0)
                            {
                                pi.SetValue(obj, DeSerialize(xmlDoc, child.ChildNodes[0]), new Object[] { });
                            }

                            break;
                        }
                    }
                }
                else
                {
                    if (node.Attributes[pi.Name] != null)
                    {
                        Object value = node.Attributes[pi.Name].Value;

                        if (pi.PropertyType == typeof(Boolean))
                        {
                            value = Convert.ToBoolean(value);
                        }
                        else if (pi.PropertyType == typeof(DateTime) || pi.PropertyType == typeof(TimeSpan))
                        {
                            value = Convert.ToDateTime(value, CultureInfo.InvariantCulture.DateTimeFormat);
                        }
                        else if (pi.PropertyType == typeof(Int32))
                        {
                            value = Convert.ToInt32(value);
                        }
                        else if (pi.PropertyType == typeof(Int64))
                        {
                            value = Convert.ToInt64(value);
                        }
                        else if (pi.PropertyType == typeof(Double))
                        {
                            value = Convert.ToDouble(value);
                        }
                        else if (pi.PropertyType == typeof(String))
                        {
                            value = Convert.ToString(value);
                        }
                        else if (pi.PropertyType.BaseType == typeof(Enum))
                        {
                            value = Convert.ToInt32(value);
                        }
                        else if (value.ToString() != "")
                        {
                            value = Convert.ChangeType(value, Type.GetType(pi.PropertyType.FullName.Split(new Char[] { ',' })[0].Replace("System.Nullable`1[[", "")));
                        }
                        else
                        {
                            value = null;
                        }

                        pi.SetValue(obj, value, new Object[] { });
                    }
                }
            }

            return obj;
        }
    }
}
