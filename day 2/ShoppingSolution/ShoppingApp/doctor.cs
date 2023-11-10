using System;

class Program
{
    static Doctor[] doctors = new Doctor[100]; // Assuming a maximum of 100 doctors

    static int doctorCount = 0;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Clinic Management System (Admin)");

            Console.WriteLine("1. Add Doctor");
            Console.WriteLine("2. Modify Doctor Phone Number");
            Console.WriteLine("3. Modify Doctor Experience");
            Console.WriteLine("4. Delete Doctor");
            Console.WriteLine("5. Exit");

            Console.Write("Select an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddDoctor();
                    break;
                case 2:
                    ModifyDoctorPhoneNumber();
                    break;
                case 3:
                    ModifyDoctorExperience();
                    break;
                case 4:
                    DeleteDoctor();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void AddDoctor()
    {
        Console.WriteLine("Enter Doctor Details:");
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Phone Number: ");
        string phoneNumber = Console.ReadLine();
        Console.Write("Experience (in years): ");
        int experience = int.Parse(Console.ReadLine());

        doctors[doctorCount] = new Doctor(name, phoneNumber, experience);
        doctorCount++;

        Console.WriteLine("Doctor added successfully!");
    }

    static void ModifyDoctorPhoneNumber()
    {
        Console.Write("Enter the name of the doctor to modify phone number: ");
        string name = Console.ReadLine();

        int index = FindDoctorIndex(name);

        if (index != -1)
        {
            Console.Write("New Phone Number: ");
            doctors[index].PhoneNumber = Console.ReadLine();
            Console.WriteLine("Phone number modified successfully!");
        }
        else
        {
            Console.WriteLine("Doctor not found.");
        }
    }

    static void ModifyDoctorExperience()
    {
        Console.Write("Enter the name of the doctor to modify experience: ");
        string name = Console.ReadLine();

        int index = FindDoctorIndex(name);

        if (index != -1)
        {
            Console.Write("New Experience (in years): ");
            doctors[index].Experience = int.Parse(Console.ReadLine());
            Console.WriteLine("Experience modified successfully!");
        }
        else
        {
            Console.WriteLine("Doctor not found.");
        }
    }

    static void DeleteDoctor()
    {
        Console.Write("Enter the name of the doctor to delete: ");
        string name = Console.ReadLine();

        int index = FindDoctorIndex(name);

        if (index != -1)
        {
            for (int i = index; i < doctorCount - 1; i++)
            {
                doctors[i] = doctors[i + 1];
            }

            doctors[doctorCount - 1] = null;
            doctorCount--;

            Console.WriteLine("Doctor deleted successfully!");
        }
        else
        {
            Console.WriteLine("Doctor not found.");
        }
    }

    static int FindDoctorIndex(string name)
    {
        for (int i = 0; i < doctorCount; i++)
        {
            if (doctors[i].Name == name)
            {
                return i;
            }
        }
        return -1; // Doctor not found
    }
}

class Doctor
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public int Experience { get; set; }

    public Doctor(string name, string phoneNumber, int experience)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Experience = experience;
    }
}
