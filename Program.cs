using System;
using System.Collections.Generic;
using System.IO;

namespace Phase1Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream(@"C:\Users\Yamini\teachers.txt", FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            {
                string data = sr.ReadToEnd();
                string[] text = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                List<Teacher> listTeachers = new List<Teacher>();
                foreach (string line in text)
                {
                    string[] col = line.Split(',');
                    Teacher teacher = new Teacher();
                    teacher.Id = Convert.ToInt32(col[0]);
                    teacher.FirstName = col[1];
                    teacher.LastName = col[2];
                    teacher.AClass = col[3];
                    teacher.Section = col[4];
                    listTeachers.Add(teacher);
                }
                Console.WriteLine(data);
            }
            Console.WriteLine("Operations: 1.Display\n2.Create\n3.Update\n4.Delete\n5.Search");
            while (true)
            {
                int operation;
                Console.WriteLine("enter the operation to perform:  ");
                operation = Convert.ToInt32(Console.ReadLine());
                switch (operation)
                {
                    case 1:
                        display();
                        break;
                    case 2:
                        create();
                        break;
                    case 3:
                        update();
                        break;
                    case 4:
                        delete();
                        break;
                    case 5:
                        search();
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
            
            static List<Teacher> getTeachers()
            {
                List<Teacher> teachersList = new List<Teacher>();
                string teacherFile = @"C:\Users\Yamini\teachers.txt";
                string[] arrayteacher = File.ReadAllLines(teacherFile);

                foreach (string line in arrayteacher)
                {
                    string[] l = line.Split(',');
                    Teacher teacher = new Teacher();
                    teacher.Id = Convert.ToInt32(l[0]);
                    teacher.FirstName = l[1];
                    teacher.LastName = l[2];
                    teacher.AClass = l[3];
                    teacher.Section = l[4];
                    teachersList.Add(teacher);
                }
                return teachersList;
            }
            
            static void display()
            {
                Console.WriteLine("Data is sorted based on:");  
                string s = Console.ReadLine();
                sort(s);
            }

            static void sort(string s)
            {
                List<Teacher> teachersList = getTeachers();
                Console.WriteLine("After sorting by " + s + " : ");
                switch (s)
                {
                    case "id":
                        teachersList.Sort((a, b) => a.Id.CompareTo(b.Id));
                        break;
                    case "firstname":
                        teachersList.Sort((a, b) => a.FirstName.CompareTo(b.FirstName));
                        break;
                    case "lastname":
                        teachersList.Sort((a, b) => a.LastName.CompareTo(b.LastName));
                        break;
                    case "aclass":
                        teachersList.Sort((a, b) => a.AClass.CompareTo(b.AClass));                        
                        break;
                    case "section":
                        teachersList.Sort((a,b) => a.Section.CompareTo(b.Section));
                        break;
                    default:
                        Console.WriteLine("Invalid entry..!");
                        break;
                }
                foreach (Teacher t in teachersList)
                {
                    Console.WriteLine($"{t.Id},{t.FirstName},{t.LastName},{t.AClass},{t.Section}");
                }
                overWriteTextFile(teachersList);
            }

            static void overWriteTextFile(List<Teacher> teachersList)
            {
                int count = 0;
                string[] arr = new string[teachersList.Count];
                foreach (Teacher t1 in teachersList)
                {
                    string s = ($"{t1.Id},{t1.FirstName},{t1.LastName},{t1.AClass},{t1.Section}");
                    arr[count] = s;
                    count++;
                }
                File.WriteAllLines(@"C:\Users\Yamini\teachers.txt", arr);
            }

            static void create()
            {
                List<Teacher> teachersList = getTeachers();
                using (FileStream fs = new FileStream(@"C:\Users\Yamini\teachers.txt", FileMode.Append))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    Console.WriteLine("Creating new teachers data..!!");
                    Console.WriteLine("Enter ID: ");
                    int NewId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter firstName: ");
                    string NewFirstName = Console.ReadLine();
                    Console.WriteLine("Enter lastName: ");
                    string NewLastName = Console.ReadLine();
                    Console.WriteLine("Enter aClass: ");
                    string NewAClass = Console.ReadLine();
                    Console.WriteLine("Enter Section: ");
                    string NewSection = Console.ReadLine();

                    Teacher t = new Teacher();
                    t.Id = NewId;
                    t.FirstName = NewFirstName;
                    t.LastName = NewLastName;
                    t.AClass = NewAClass;
                    t.Section = NewSection;

                    string newData = (NewId + "," + NewFirstName + "," + NewLastName + "," + NewAClass + "," + NewSection);
                    sw.WriteLine(newData);
                    int count = 0;
                    string[] arr = new string[teachersList.Count];
                }
            }

            static void update()
            {
                List<Teacher> teachersList = getTeachers();
                Console.WriteLine("Enter existing Id to update the record: ");
                int id = Convert.ToInt32(Console.ReadLine());
                bool flag = true;
                foreach (Teacher t in teachersList)
                {
                    if (t.Id == id)
                    {
                        Console.WriteLine("Enter firstName: ");
                        string fName = Console.ReadLine();
                        Console.WriteLine("Enter LastName: ");
                        string lName = Console.ReadLine();
                        Console.WriteLine("Enter class: ");
                        string aClass = Console.ReadLine();
                        Console.WriteLine("Enter section: ");
                        string sec = Console.ReadLine();
                        t.FirstName = fName;
                        t.LastName = lName;
                        t.AClass = aClass;
                        t.Section = sec;
                        Console.WriteLine("Updated Id is: ");
                        Console.WriteLine($"{t.Id},{t.FirstName},{t.LastName},{t.AClass},{t.Section}");
                        flag = false;
                        break;
                    }
                }
                if (flag)
                    Console.WriteLine("Id Not found!");
                overWriteTextFile(teachersList);
            }
 
            static void delete()
            {
                List<Teacher> teachersList = getTeachers();
                Console.WriteLine("Enter Id to delete:");
                int id = Convert.ToInt32(Console.ReadLine());
                bool flag = false;
                foreach (Teacher t in teachersList)
                {
                    if (t.Id == id)
                    {
                        teachersList.Remove(t);
                        flag = true;
                        break;
                    }                    
                }
                if (flag)
                    Console.WriteLine("Delete operation is successful.");
                else
                    Console.WriteLine("Id not found.");

                int count = 0;
                string[] arr = new string[teachersList.Count];
                foreach (Teacher t1 in teachersList)
                {
                    string s = ($"{t1.Id},{t1.FirstName},{t1.LastName},{t1.AClass},{t1.Section}");
                    arr[count] = s;
                    count++;
                }
                File.WriteAllLines(@"C:\Users\Yamini\teachers.txt", arr);
            }

            static void search()
            {
                List<Teacher> teachersList = getTeachers();
                Console.WriteLine("Enter Id to Search:");
                int id = Convert.ToInt32(Console.ReadLine());
                bool flag = true;
                foreach (Teacher t in teachersList)
                {
                    if (t.Id == id)
                    {
                        Console.WriteLine("Entered Id-{0} is present in the textFile.",id);
                        Console.WriteLine($"{t.Id},{t.FirstName},{t.LastName},{t.AClass},{t.Section}");
                        flag = false;
                        break;
                    }            
                }
                if (flag)
                    Console.WriteLine("Id not found.");
            }    
        }
    }
}
