using System;
using Autofac;
using Autofac.Core;

namespace AutofacScratch
{
    /// <summary>
    /// Working with Lifetime Scopes
    /// </summary>
    class EmailSender : IEmailSender
    {
        private static void EmailSenderInstance()
        {
            var builder = new ContainerBuilder();
            // Register your transaction-level shared component
            // as InstancePerMatchingLifetimeScope and give it
            // a "known tag" that you'll use when starting new transactions
            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerMatchingLifetimeScope("transaction");

            // Both the order processor and the receipt manager
            // need to send email notification
            builder.RegisterType<OrderProcessor>().As<IOrderProcessor>();
            builder.RegisterType<ReceiptManager>().As<IReceiptManager>();

            var container = builder.Build();

            // Create transaction scopes with a tag
            using (var transactionScope = container.BeginLifetimeScope("transaction"))
            {
                using (var orderScope = transactionScope.BeginLifetimeScope())
                {
                    // This would resolve an IEmailSender to use, but the 
                    // IEmailSender would "live" in the paranet transaction
                    // scope and be shared across any children of the 
                    // transaction scope because of that tag.
                    var op = orderScope.Resolve<IOrderProcessor>();
                    op.ProcessOrder();
                }

                using (var receiptScope = transactionScope.BeginLifetimeScope())
                {
                    // This would also resolve an IEmailSender to use, but
                    // it would find the existing IEmailSender in the parent
                    // scope and use that. It'd be the same instance used
                    // by the order processor.
                    var rm = receiptScope.Resolve<IReceiptManager>();
                    rm.SendReceipt();
                }
            }
        }

        private void AddingRegistrationsToALiftimeScope()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();
            // **** Adding Registrations to a Lifttime Scope
            using (var scope = container.BeginLifetimeScope(
                build =>
                {
                    build.RegisterType<Override>().As<IService>();
                    build.RegisterModule<MyModule>();
                }))
            {
                // The additional registration will be available only
                // in this lifetime scope
            }
        }
    }

    internal interface IService
    {
    }

    internal class Override : IService
    {
    }

    internal class MyModule : IModule
    {
        public void Configure(IComponentRegistry componentRegistry)
        {
            throw new NotImplementedException();
        }
    }

    internal interface IEmailSender
    {
    }

    internal class ReceiptManager : IReceiptManager
    {
        public void SendReceipt()
        {
            throw new NotImplementedException();
        }
    }

    internal interface IReceiptManager
    {
        void SendReceipt();
    }

    internal interface IOrderProcessor
    {
        void ProcessOrder();
    }

    internal class OrderProcessor : IOrderProcessor
    {
        public void ProcessOrder()
        {
            throw new NotImplementedException();
        }
    }
}
