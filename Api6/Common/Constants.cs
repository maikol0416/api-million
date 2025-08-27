using System;
namespace Api6.Common
{
	public static class ConstantsAPI
	{
        public static string NameProject => "API - MILLION";
        public static string DescriptionProject => "API for consuming services";
        public const string VersionProject = "v1";
        public const string UriForDefaultWebApi = $"api/{VersionProject}/";

    }
    public static class ContactProject
    {
        public static string Email => "ing.martinezs@outlook.com";
        public static string Name => "Maikol Martinez";
        public static Uri Url  => new Uri("www.google.com");
    }
}

