namespace NotesApp.Api.Constants;

public static class RouteNames
{
    public const string ApiBase = "api";
    public static class Users
    {
        public const string Resource = "users";
        public const string Base = $"{ApiBase}/{Resource}";


        public const string GetById = $"{Base}/{{id:guid}}";
        public const string GetAll = $"{Base}";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }

    public static class Auth
    {
        public const string Resource = "auth";
        public const string Base = $"{ApiBase}/{Resource}";

        public const string Register = $"{Base}/register";
        public const string Login = $"{Base}/login";
    }
}
