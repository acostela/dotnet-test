using TechTest.Domain;

namespace TechTest.Data;
public static class DataSeeder
    {
        public static void SeedData(DataContext context)
        {
            //Since we don't have a real database, treat this seed data as the preexisting data in the database
            var robot1 = new Robot
            {
                Id = 1,
                ConditionExpertise = "Kidney Beans"
            };

            context.Robots.Add(robot1);

            var robot2 = new Robot
            {
                Id = 2,
                ConditionExpertise = "Bloaty Head"
            };

            context.Robots.Add(robot2);

            var robot3 = new Robot
            {
                Id = 3,
                ConditionExpertise = "Kidney Beans"
            };

            context.Robots.Add(robot3);

            var robot4 = new Robot
            {
                Id = 4,
                ConditionExpertise = "Infectious Laughter"
            };

            context.Robots.Add(robot4);

            var appointment1 = new Appointment
            {
                Id = 11,
                StartDate = new DateTime(2021, 4, 26, 9, 0, 0),
                EndDate = new DateTime(2021, 4, 26, 9, 30, 0),
                RobotId = 1,
                Robot = robot1
            };

            context.Appointments.Add(appointment1);

            var appointment2 = new Appointment
            {
                Id = 12,
                StartDate = new DateTime(2021, 4, 26, 9, 30, 0),
                EndDate = new DateTime(2021, 4, 26, 10, 0, 0),
                RobotId = 1,
                Robot = robot1
            };

            context.Appointments.Add(appointment2);

            var appointment3 = new Appointment
            {
                Id = 13,
                StartDate = new DateTime(2021, 4, 26, 9, 0, 0),
                EndDate = new DateTime(2021, 4, 26, 9, 20, 0),
                RobotId = 2,
                Robot = robot2
            };

            context.Appointments.Add(appointment3);

            var appointment4 = new Appointment
            {
                Id = 14,
                StartDate = new DateTime(2021, 4, 26, 9, 0, 0),
                EndDate = new DateTime(2021, 4, 26, 9, 40, 0),
                RobotId = 3,
                Robot = robot3
            };

            context.Appointments.Add(appointment4);

            var appointment5 = new Appointment
            {
                Id = 15,
                StartDate = new DateTime(2021, 4, 26, 10, 0, 0),
                EndDate = new DateTime(2021, 4, 26, 10, 20, 0),
                RobotId = 1,
                Robot = robot1
            };

            context.Appointments.Add(appointment5);

            var appointment6 = new Appointment
            {
                Id = 16,
                StartDate = new DateTime(2021, 4, 26, 11, 0, 0),
                EndDate = new DateTime(2021, 4, 26, 11, 40, 0),
                RobotId = 4,
                Robot = robot4
            };

            context.Appointments.Add(appointment6);

            var appointment7 = new Appointment
            {
                Id = 17,
                StartDate = new DateTime(2021, 4, 26, 10, 0, 0),
                EndDate = new DateTime(2021, 4, 26, 10, 30, 0),
                RobotId = 4,
                Robot = robot4
            };

            context.Appointments.Add(appointment7);

            var appointment8 = new Appointment
            {
                Id = 18,
                StartDate = new DateTime(2021, 4, 26, 12, 30, 0),
                EndDate = new DateTime(2021, 4, 26, 13, 10, 0),
                RobotId = 1,
                Robot = robot1
            };

            context.Appointments.Add(appointment8);

            context.SaveChanges();
        }
    }