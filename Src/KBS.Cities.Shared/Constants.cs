﻿namespace KBS.Cities.Shared
{
    public static class Constants
    {
        public static class Pagination
        {
            public const int DefaultPageSize = 10;
            public const int DefaultPageIndex = 1;
        }

        public static class Date
        {
            public const string DefaultFormat = "ddMMyyyy";
        }

        public static class City
        {
            public const string Hub = "https://localhost:5001/hub";
            public const string EndPoint = "api/city";
            
            public const string Update = "UpdateCities";
            public const string Updated = "CitiesUpdated";
            
            public const string New = "New City";
        }

        public static class Markup
        {
            public const string Disabled = "disabled";
            public const string Active = "active";
        }

        public static class Toast
        {
            public const string BadRequest = "400 Bad Request";
            public const string NotFound = "404 Not Found";
            public const string Ok = "OK";
        }

        public static class OpenIconic
        {
            public const string Check = "oi oi-check";
            public const string Plus = "oi oi-plus";
            public const string Pencil = "oi oi-pencil";
            public const string X = "oi oi-x";
        }
    }
}