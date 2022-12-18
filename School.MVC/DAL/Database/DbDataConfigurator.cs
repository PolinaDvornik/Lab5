using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database
{
    public static class DbDataConfigurator
    {
        public static List<Class> Classes { get; private set; }
        public static List<ClassType> ClassTypes { get; private set; }
        public static List<Schedule> Schedules { get; private set; }
        public static List<Student> Students { get; private set; }
        public static List<Subject> Subjects { get; private set; }
        public static List<Teacher> Teachers { get; private set; }

        private readonly static string[] _surnames =
        {
            "Smirnov", "Ivanov", "Kuznecov", "Sokolov", "Popov", "Lebedev", "Kozlov", "Novikov", "Morozov",
            "Petrov", "Volkov", "Soloviev", "Vasiliev", "Zaicev", "Pavlov", "Semenov", "Golubev", "Vinogradov",
            "Bogdanov", "Vorobiev", "Fedorov", "Michailov", "Belyaev", "Tarasov", "Belov", "Komarov",
            "Orlov", "Kiselev", "Makarov", "Andreev", "Kovalev", "Iliin", "Gusev"
        };

        private readonly static string[] _womanNames =
        {
            "Anastasiya", "Anna", "Mariya", "Elena", "Daria", "Alina", "Irina", "Ekaterina", "Arina", "Vladislava",
            "Polina", "Olga", "Julia", "Tatiana", "Natalia", "Viktoria", "Elizaveta", "Ksenia", "Milana", "Veronika",
            "Alisa", "Valeria", "Aleksandra", "Uliana", "Christina", "Sophia", "Lilia"
        };

        private readonly static string[] _manNames =
        {
            "Aleksandr", "Dmitriy", "Maksim", "Sergey", "Andrew", "Aleksey", "Artem", "Iliya", "Kirill", "Michail",
            "Nikita", "Matvei", "Roman", "Egor", "Arseniy", "Ivan", "Denis", "Evgeniy", "Daniil", "Timofey",
            "Vladislav", "Igor", "Vladimir", "Pavel", "Ruslan", "Mark", "Konstantin"
        };

        private readonly static string[] _middleNames =
        {
            "Aleksandrov", "Dmitriev", "Maksimov", "Sergeev", "Andreev", "Alekseev", "Kirillov", "Michailov",
            "Matveev", "Romanov", "Egorov", "Arseniev", "Ivanov", "Denisov", "Evgeniev", "Daniilov",
            "Timofeev", "Vladislavov", "Igorev", "Vladimirov", "Pavlov", "Raslanov", "Konstantinov"
        };

        private readonly static Dictionary<string, string> _subjects = new Dictionary<string, string>
        {
            { "Maths",
                "Mathematics is the science of quantitative relations and spatial forms of the real world. " +
                "It includes such disciplines as arithmetic, algebra, geometry, trigonometry, higher " +
                "mathematics (analytical geometry, linear algebra, mathematical analysis, differential and integral calculus, etc.)" },
            { "Geography",
                "A complex of natural and social sciences that studies the structure, functioning and " +
                "evolution of the geographical envelope, the interaction and distribution in space of " +
                "natural and natural-social geosystems and their components." },
            { "History",
                "History is a science that studies the past, real facts and patterns of change in historical " +
                "events, the evolution of society and relations within it, conditioned by human activity over many generations." },
            { "Russian",
                "This subject studies the grammar and vocabulary of the Russian language." },
            { "English",
                "This subject studies the grammar and vocabulary of the English language." },
            { "Biology",
                "The science of living beings and their interaction with the environment. It studies all aspects " +
                "of life, in particular: the structure, functioning, growth, origin, evolution and distribution " +
                "of living organisms on Earth." },
            { "Informatics",
                "Informatics is a complex, technical science that studies and systematizes the laws and methods " +
                "of creating, storing, reproducing, receiving, processing and transmitting data by means of " +
                "computer technology, as well as the principles of functioning of these tools and methods of managing them." },
            { "Physics",
                "Physics is a field of natural science: the science of the most general laws of nature, of matter, " +
                "its structure, movement and rules of transformation." },
            { "Physical Culture",
                "Physical culture is a human activity aimed at improving health and developing physical abilities. " +
                "It develops the body harmoniously and maintains an impeccable physical condition for many years." },
            { "Literature",
                "Literature - in the broadest sense, any written text." },
        };

        private static readonly Dictionary<string, string> _classTypes = new Dictionary<string, string>
        {
            { "Elementary", "Class with students from 1 till 4 grade." },
            { "Middle", "Class with students from 5 till 9 grade." },
            { "Senior", "Class with students from 10 till 11 grade." }
        };

        public readonly static string[] _towns =
        {
            "Minsk", "Gomel", "Brest", "Mogilev", "Zhlobin", "Slonim", "Rechica", "Baranovichi",
            "Bobruisk", "Vitebsk", "Shklov", "Grodno"
        };

        public readonly static string[] _streets =
        {
            "Volkova", "Pervomaiskaya", "Shosseynaya", "Uritskogo", "Solnechnaya", "Sovietskaya",
            "Bakunina", "Lenina", "Barykina", "Polevaya", "Rabochiaya", "Kozlova", "Karibskogo"
        };

        public readonly static TimeOnly[] _startLessonTimes =
        {
            new TimeOnly(8, 0), new TimeOnly(8, 55), new TimeOnly(9, 45), new TimeOnly(10, 40),
            new TimeOnly(11, 30), new TimeOnly(12, 30), new TimeOnly(13, 20), new TimeOnly(14, 10),
            new TimeOnly(15, 0), new TimeOnly(15, 55), new TimeOnly(16, 45), new TimeOnly(17, 35)
        };


        public static void FillModelArrays()
        {
            Teachers = new List<Teacher>();
            Subjects = new List<Subject>();
            ClassTypes = new List<ClassType>();
            Classes = new List<Class>();
            Students = new List<Student>();
            Schedules = new List<Schedule>();


            for (int i = 0; i < 50; i++)
            {
                int sex = new Random((int)DateTime.Now.Ticks + i).Next(2);
                int surnameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_surnames.Length);
                int middleNameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_middleNames.Length);

                var positions = _subjects.Keys.AsEnumerable();
                int positionIndex = new Random((int)DateTime.Now.Ticks + i).Next(positions.Count());

                if (sex == 0)
                {
                    int nameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_womanNames.Length);

                    Teachers.Add(
                        new Teacher
                        {
                            Id = i + 1,
                            Surname = _surnames[surnameIndex] + "a",
                            Name = _womanNames[nameIndex],
                            MiddleName = _middleNames[middleNameIndex] + "na",
                            Position = positions.ElementAt(positionIndex) + " teacher"
                        });
                }
                else
                {
                    int nameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_manNames.Length);

                    Teachers.Add(
                        new Teacher
                        {
                            Id = i + 1,
                            Surname = _surnames[surnameIndex],
                            Name = _manNames[nameIndex],
                            MiddleName = _middleNames[middleNameIndex] + "ich",
                            Position = positions.ElementAt(positionIndex) + " teacher"
                        });
                }
            }


            int index = 1;
            foreach (var key in _subjects.Keys)
            {
                Subjects.Add(new Subject
                {
                    Id = index,
                    Name = key,
                    Description = _subjects[key]
                });

                index++;
            }


            index = 1;
            foreach (var key in _classTypes.Keys)
            {
                ClassTypes.Add(new ClassType
                {
                    Id = index,
                    Name = key,
                    Description = _classTypes[key]
                });

                index++;
            }


            string[] classLetters = { "A", "B", "C", "D", "E", "F", "G" };
            index = 1;
            for (int i = 1; i < 12; i++)
            {
                for (int j = 0; j < classLetters.Length; j++)
                {
                    var studentsCount = new Random((int)DateTime.Now.Ticks + i).Next(23, 32);
                    var teacherIndex = new Random((int)DateTime.Now.Ticks + i).Next(Teachers.Count);
                    int classType;

                    if (i <= 4)
                        classType = 1;
                    else if (i > 4 && i <= 9)
                        classType = 2;
                    else
                        classType = 3;

                    Classes.Add(new Class
                    {
                        Id = index,
                        Number = i + classLetters[j],
                        StudentsCount = studentsCount,
                        CreationYear = 2023 - i,
                        TeacherId = Teachers[teacherIndex].Id,
                        ClassTypeId = classType
                    });

                    index++;
                }
            }


            index = 1;
            foreach (var c in Classes)
            {
                for (int i = 0; i < c.StudentsCount; i++)
                {
                    int sex = new Random((int)DateTime.Now.Ticks + i).Next(2);
                    int surnameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_surnames.Length);
                    int middleNameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_middleNames.Length);

                    int motherNameIndex = new Random((int)DateTime.Now.Ticks + i + 1).Next(_womanNames.Length);
                    int motherMiddleNameIndex = new Random((int)DateTime.Now.Ticks + i + 1).Next(_middleNames.Length);

                    int futherMiddleNameIndex = new Random((int)DateTime.Now.Ticks + i + 2).Next(_middleNames.Length);

                    int month = new Random((int)DateTime.Now.Ticks + i).Next(1, 13);
                    int day = new Random((int)DateTime.Now.Ticks + i).Next(1, 29);

                    int townIndex = new Random((int)DateTime.Now.Ticks + i).Next(_towns.Length);
                    int streetIndex = new Random((int)DateTime.Now.Ticks + i).Next(_streets.Length);
                    int houseNumber = new Random((int)DateTime.Now.Ticks + i).Next(1, 50);

                    if (sex == 0)
                    {
                        int nameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_womanNames.Length);

                        Students.Add(
                            new Student
                            {
                                Id = index,
                                Surname = _surnames[surnameIndex] + "a",
                                Name = _womanNames[nameIndex],
                                MiddleName = _middleNames[middleNameIndex] + "na",
                                BirthDate = new DateTime(c.CreationYear - 6, month, day),
                                Sex = false,
                                Address = _towns[townIndex] + ", " + _streets[streetIndex] + ", " + houseNumber,
                                MotherFullName = _surnames[surnameIndex] + "a " + 
                                    _womanNames[motherNameIndex] + " " + _middleNames[motherMiddleNameIndex] + "na",
                                FutherFullName = _surnames[surnameIndex] + " " +
                                    _middleNames[middleNameIndex].Substring(0, _middleNames[middleNameIndex].Length - 2) + 
                                    " " + _middleNames[futherMiddleNameIndex] + "ich",
                                AdditionalInfo = "-",
                                ClassId = c.Id
                            });
                    }
                    else
                    {
                        int nameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_manNames.Length);

                        Students.Add(
                            new Student
                            {
                                Id = index,
                                Surname = _surnames[surnameIndex],
                                Name = _manNames[nameIndex],
                                MiddleName = _middleNames[middleNameIndex] + "ich",
                                BirthDate = new DateTime(c.CreationYear - 6, month, day),
                                Sex = true,
                                Address = _towns[townIndex] + ", " + _streets[streetIndex] + ", " + houseNumber,
                                MotherFullName = _surnames[surnameIndex] + "a " +
                                    _womanNames[motherNameIndex] + " " + _middleNames[motherMiddleNameIndex] + "na",
                                FutherFullName = _surnames[surnameIndex] + " " +
                                    _middleNames[middleNameIndex].Substring(0, _middleNames[middleNameIndex].Length - 2) +
                                    " " + _middleNames[futherMiddleNameIndex] + "ich",
                                AdditionalInfo = "-",
                                ClassId = c.Id
                            });
                    }

                    index++;
                }
            }

            for (int i = 0; i < 1000; i++)
            {
                int month = new Random((int)DateTime.Now.Ticks + i).Next(1, 13);
                int day = new Random((int)DateTime.Now.Ticks + i).Next(1, 29);
                DateTime date = new DateTime(2022, month, day);

                int timeIndex = new Random((int)DateTime.Now.Ticks + i).Next(_startLessonTimes.Length);
                int classIndex = new Random((int)DateTime.Now.Ticks + i).Next(Classes.Count);
                int subjectIndex = new Random((int)DateTime.Now.Ticks + i).Next(Subjects.Count);

                var suitTeachers = Teachers.Where(t => t.Position.Contains(Subjects[subjectIndex].Name));
                int teacherIndex = new Random((int)DateTime.Now.Ticks + i).Next(suitTeachers.Count());

                var startTime = _startLessonTimes[timeIndex];
                var endTime = _startLessonTimes[timeIndex].AddMinutes(45);

                Schedules.Add(new Schedule
                {
                    Id = i + 1,
                    Date = date,
                    DayOfWeek = date.DayOfWeek.ToString(),
                    StartLessonTime = startTime.Hour + ":" + (startTime.Minute == 0 ? "00" : startTime.Minute),
                    EndLessonTime = endTime.Hour + ":" + (endTime.Minute == 0 ? "00" : endTime.Minute),
                    ClassId = Classes[classIndex].Id,
                    SubjectId = Subjects[subjectIndex].Id,
                    TeacherId = suitTeachers.ToList()[teacherIndex].Id
                });
            }
        }
    }
}
