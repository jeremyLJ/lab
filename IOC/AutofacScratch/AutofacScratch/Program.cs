using System;
using Autofac;

namespace AutofacScratch
{
    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();

            //builder.RegisterGeneric(typeof(NHibernateRepository<>)).As(typeof(IRepository<>))
            //    .InstancePerLifetimeScope();

            Container = builder.Build();

            WriteDate();

            // Enumeration resolving services


            Console.WriteLine("done...");
            Console.Read();
        }

        private static void WriteDate()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IDateWriter>();
                writer.WriteDate();
            }
        }
    }
}
