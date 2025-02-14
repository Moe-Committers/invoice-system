using invoice_system.Utils.Enums;

namespace invoice_system.Database.Seeder.Constants;

public static class RoleStuffs
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Staff = "Staff";

        public static readonly string[] All = { Admin, Manager, Staff };
    }

    public static class Permissions
    {
        public static class Customers
        {
            public const string View = "view_customers";
            public const string Create = "create_customers";
            public const string Edit = "edit_customers";
            public const string Delete = "delete_customers";
        }

        public static class Invoices
        {
            public const string View = "view_invoices";
            public const string Create = "create_invoices";
            public const string Edit = "edit_invoices";
            public const string Delete = "delete_invoices";
        }

        public static class Quotations
        {
            public const string View = "view_quotations";
            public const string Create = "create_quotations";
            public const string Edit = "edit_quotations";
            public const string Delete = "delete_quotations";
            public const string Convert = "convert_quotations";
        }

        public static class Payments
        {
            public const string View = "view_payments";
            public const string Process = "process_payments";
        }

        public static class Reports
        {
            public const string View = "view_reports";
            public const string Export = "export_reports";
        }

        public static readonly string[] All = {
            Customers.View, Customers.Create, Customers.Edit, Customers.Delete,
            Invoices.View, Invoices.Create, Invoices.Edit, Invoices.Delete,
            Quotations.View, Quotations.Create, Quotations.Edit, Quotations.Delete, Quotations.Convert,
            Payments.View, Payments.Process,
            Reports.View, Reports.Export
        };

        public static readonly string[] ViewOnly = {
            Customers.View,
            Invoices.View,
            Quotations.View,
            Payments.View,
            Reports.View
        };

        public static readonly string[] CreateOnly = {
            Customers.Create,
            Invoices.Create,
            Quotations.Create
        };

        public static readonly string[] EditOnly = {
            Customers.Edit,
            Invoices.Edit,
            Quotations.Edit
        };

        public static readonly string[] DeleteOnly = {
            Customers.Delete,
            Invoices.Delete,
            Quotations.Delete
        };
    }

    public static class DefaultAdmin
    {
        public const string Name = "System Admin";
        public const string Email = "admin@example.com";
        public const string Password = "admin123";
        public const int Age = 25;
        public const int PhoneNumber = 123456789;
        public const string Avatar = "default-avatar.png";
        public const Status status = Status.isActive;
    }
}